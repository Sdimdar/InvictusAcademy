using Amazon.S3;
using Amazon.S3.Model;

namespace CloudStorage.Application.Features.Commands.UploadFile;

public class S3Bucket
{
    public static async Task<bool> UploadFileAsync(
        IAmazonS3 client,
        string bucketName,
        string objectName,
        string filePath)
    {
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = objectName,
            FilePath = filePath,
        };

        var response = await client.PutObjectAsync(request);
        
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine($"Successfully uploaded {objectName} to {bucketName}.");
            return true;
        }
        else
        {
            Console.WriteLine($"Could not upload {objectName} to {bucketName}.");
            return false;
        }
    }
    
    public static async Task<bool> DeleteFileAsync(
        IAmazonS3 client,
        string bucketName,
        string objectName)
    {
        var request = new DeleteObjectRequest()
        {
            BucketName = bucketName,
            Key = objectName
        };

        var response = await client.DeleteObjectAsync(request);
        
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine($"Successfully deleted {objectName} to {bucketName}.");
            return true;
        }
        else
        {
            Console.WriteLine($"Could not deleted {objectName} to {bucketName}.");
            return false;
        }
    }
    
}