using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.ServiceBus;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class RequestDeleteAccountUseCase : IRequestDeleteAccount
    {
        private readonly IGetUserByToken _getUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserWriteOnlyRepository _writeRepository;
        private readonly ISendDeleteUser _send;

        public RequestDeleteAccountUseCase(IGetUserByToken getUser, IUnitOfWork unitOfWork, IUserWriteOnlyRepository writeRepository, ISendDeleteUser send)
        {
            _getUser = getUser;
            _unitOfWork = unitOfWork;
            _writeRepository = writeRepository;
            _send = send;
        }

        public async Task Execute()
        {
            var user = await _getUser.GetUser() ?? throw new UserException("User doesn't exists");

            user.Active = false;
            _writeRepository.Update(user);
            await _unitOfWork.Commit();

            await _send.SendMessage(user);
        }
    }
}
