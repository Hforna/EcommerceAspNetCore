using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnly;
        private readonly IUserReadOnlyRepository _userReadOnly;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorageService _azureStorage;

        public DeleteUserUseCase(IUserReadOnlyRepository userReadOnly, IUserWriteOnlyRepository userWriteOnly,
            IUnitOfWork unitOfWork, IAzureStorageService azureStorage)
        {
            _userWriteOnly = userWriteOnly;
            _userReadOnly = userReadOnly;
            _unitOfWork = unitOfWork;
            _azureStorage = azureStorage;
        }

        public async Task Execute(Guid uid)
        {
            var user = await _userReadOnly.UserByIdentifier(uid);

            if (user is null)
                return;

            await _azureStorage.DeleteUserContainer(user!);

            await _userWriteOnly.Delete(uid);

            await _unitOfWork.Commit();
        }
    }
}
