using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorageService _storageService;
        private readonly IUserReadOnlyRepository _repositoryRead;

        public DeleteUserUseCase(IUserWriteOnlyRepository writeRepository, IUnitOfWork unitOfWork, IAzureStorageService storageService, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
            _repositoryRead = userReadOnlyRepository;
        }

        public async Task Execute(Guid uid)
        {
            var user = await _repositoryRead.UserByIdentifier(uid);

            await _writeRepository.Delete(uid);
            await _unitOfWork.Commit();

            await _storageService.DeleteContainer(user!);
        }
    }
}
