using Amazon.Runtime;
using Amazon.S3;
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
    public UploadFileHandler(ICloudStorageRepository repository, ILogger<UploadFileHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<Result<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        IAmazonS3 client = new AmazonS3Client(
            new BasicAWSCredentials("ZZAWUNF55JQZQRHE48JZ", "7slBKnj3FGuJfh4by6mvBgC5fUh2UIAsgvCqgjWO"),
            new AmazonS3Config
            {
                ServiceURL = "https://object.pscloud.io",
            });
        string bucketName = "invictus";
        string keyName = "6o9kWZOVrJ9xFDNP6tGKe1A9oJgKBgaTHMepbJ6Z.458351.33458351";
        using (var newMemoryStream = new MemoryStream())
        {
            request.File.CopyTo(newMemoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = request.File.FileName,
                BucketName = bucketName
            };

            var fileTransferUtility = new TransferUtility(client);
            await fileTransferUtility.UploadAsync(uploadRequest);
        }

        CloudStorageDbModel newFile = new CloudStorageDbModel();
        newFile.FileName = request.File.FileName;
        newFile.LastModifiedDate = DateTime.Now;
        var result = await _repository.AddAsync(newFile);
        if (result is null)
        {
            _logger.LogWarning($"{BussinesErrors.RequestIsNull.ToString()}: Request is Null");
            return Result.Error($"{BussinesErrors.RequestIsNull.ToString()}: Request is Null");
        }
        return Result.Success();
    }
}