using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Ardalis.Result;
using AutoMapper;
using CloudStorage.Application.Contracts;
using CloudStorage.Domain.Entities;
using CommonStructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServicesContracts.CloudStorage.Requests.Commands;

namespace CloudStorage.Application.Features.Commands.UploadFile;

public class UploadFileHandler : IRequestHandler<UploadFileCommand, Result<string>>
{
    private readonly ICloudStorageRepository _repository;
    private readonly ILogger<UploadFileHandler> _logger;
    private readonly IMapper _mapper;
    private readonly CloudStorageDbModel _cloudStorage;

    public UploadFileHandler(ICloudStorageRepository repository, ILogger<UploadFileHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(UploadFileCommand filePath, CancellationToken cancellationToken)
    {
        if (filePath is not null)
        {
            IAmazonS3 client = new AmazonS3Client(
                new BasicAWSCredentials("ZZAWUNF55JQZQRHE48JZ", "7slBKnj3FGuJfh4by6mvBgC5fUh2UIAsgvCqgjWO"),
                new AmazonS3Config
                {
                    ServiceURL = "https://object.pscloud.io",
                });
            string bucketName = "invictus";
            string fileName = Path.GetFileName(filePath.FilePath);
            string keyName = Path.GetFileName(filePath.FilePath);
            string fileExtenstion = Path.GetExtension(filePath.FilePath);
            switch (fileExtenstion)
            {
                case ".mp4": case ".avi": case ".mov":
                    keyName = $"video/{keyName}";
                    break;
                case ".jpg": case ".jpeg": case ".png":
                    keyName = $"image/{keyName}";
                    break;
                case ".pdf": case ".doc": case ".docx": case ".zip": case ".7z": case ".rar":
                    keyName = $"documents/{keyName}";
                    break;
                default:
                    keyName = $"other/{keyName}";
                    break;
            }

            var success = await S3Bucket.UploadFileAsync(client, bucketName, keyName, filePath.FilePath);

            if (!success)
            {
                return Result.Error($"{BussinesErrors.ErrorWithUploadingFile.ToString()}: Request is Null");
            }

            var newFile = new CloudStorageDbModel();
            newFile.FilePath = keyName;
            newFile.FileName = fileName;
            newFile.LastModifiedDate = DateTime.Now;
            //var isExist = await _repository.GetAsync(x => x.FileName == fileName);
            var result = await _repository.AddAsync(newFile);
            if (result is null)
            {
                _logger.LogWarning($"{BussinesErrors.RequestIsNull.ToString()}: Request is Null");
                var delete = await S3Bucket.DeleteFileAsync(client, bucketName, keyName);
                return Result.Error($"{BussinesErrors.RequestIsNull.ToString()}: Request is Null");
            }
            File.Delete(filePath.FilePath);
            return Result.Success();
        }

        return Result.Error($"{BussinesErrors.RequestIsNull.ToString()}: Request is Null");
    }
}