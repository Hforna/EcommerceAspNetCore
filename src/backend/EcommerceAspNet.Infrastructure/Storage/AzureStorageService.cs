using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Storage
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobClient;

        public AzureStorageService(BlobServiceClient blobClient) => _blobClient = blobClient;

        public async Task Upload(Product product, Stream file, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(product.ProductIdentifier.ToString());
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(fileName);
            blob.Upload(file, overwrite: true);
        }

        public async Task DeleteContainer(Product product)
        {
            var container = _blobClient.GetBlobContainerClient(product.ProductIdentifier.ToString());
            await container.DeleteIfExistsAsync();
        }

        public async Task<string> GetUrlImageProduct(Product product, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(product.ProductIdentifier.ToString());

            var exists = await container.ExistsAsync();

            if(exists.Value == false || string.IsNullOrEmpty(fileName))
                return string.Empty;

            var blob = container.GetBlobClient(fileName);
            exists = await blob.ExistsAsync();
            if(exists.Value)
            {
                var sasBuilder = new BlobSasBuilder()
                {
                    BlobName = fileName,
                    BlobContainerName = product.ProductIdentifier.ToString(),
                    ExpiresOn = DateTime.UtcNow.AddMinutes(40),
                    Resource = "b"
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                return blob.GenerateSasUri(sasBuilder).ToString();
            }

            return string.Empty;
        }

        public async Task<string> GetUrlImageUser(UserEntitie user, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(user.UserIdentifier.ToString());

            var exists = await container.ExistsAsync();

            if (exists.Value == false || string.IsNullOrEmpty(fileName))
                return string.Empty;

            var blob = container.GetBlobClient(fileName);
            exists = await blob.ExistsAsync();
            if (exists.Value)
            {
                var sasBuilder = new BlobSasBuilder()
                {
                    BlobName = fileName,
                    BlobContainerName = user.UserIdentifier.ToString(),
                    ExpiresOn = DateTime.UtcNow.AddMinutes(40),
                    Resource = "b"
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                return blob.GenerateSasUri(sasBuilder).ToString();
            }

            return string.Empty;
        }

        public async Task UploadUser(UserEntitie user, Stream file, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(user.UserIdentifier.ToString());
            await container.CreateIfNotExistsAsync();

            var client = container.GetBlobClient(fileName);
            await client.UploadAsync(file, overwrite: true);
        }
    }
}
