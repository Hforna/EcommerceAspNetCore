using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Domain.Repository;
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

        public DeleteUserUseCase(IUserWriteOnlyRepository writeRepository, IUnitOfWork unitOfWork)
        {
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid uid)
        {
            await _writeRepository.Delete(uid);
            await _unitOfWork.Commit();
        }
    }
}
