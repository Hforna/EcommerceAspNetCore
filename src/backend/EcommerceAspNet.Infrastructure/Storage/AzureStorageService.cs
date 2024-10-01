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

        public async Task Upload(ProductEntitie product, Stream file, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(product.ProductIdentifier.ToString());
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(fileName);
            blob.Upload(file, overwrite: true);
        }

        public async Task DeleteContainer(ProductEntitie product)
        {
            var container = _blobClient.GetBlobContainerClient(product.ProductIdentifier.ToString());
            await container.DeleteIfExistsAsync();
        }

        public async Task<string> GetUrlImage(ProductEntitie product, string fileName)
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
    }
}
