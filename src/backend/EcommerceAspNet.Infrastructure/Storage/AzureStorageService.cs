using Azure.Storage.Blobs;
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
            var container = _blobClient.GetBlobContainerClient(product.Name.ToString());
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(fileName);
            blob.Upload(file, overwrite: true);
        }

        public async Task DeleteContainer(UserEntitie user)
        {
            var container = _blobClient.GetBlobContainerClient(user.UserIdentifier.ToString());
            await container.DeleteIfExistsAsync();
        }

        public Task<string> GetUrlImage(string fileName)
        {
            
        }
    }
}
