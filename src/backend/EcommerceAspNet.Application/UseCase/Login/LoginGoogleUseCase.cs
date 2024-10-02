using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Login
{
    public class LoginGoogleUseCase : ILoginGoogleUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserWriteOnlyRepository _repositoryWrite;
        private readonly IUserReadOnlyRepository _repositoryRead;
        private readonly IGenerateToken _generateToken;

        public LoginGoogleUseCase(IUnitOfWork unitOfWork, IUserWriteOnlyRepository repositoryWrite, IUserReadOnlyRepository repositoryRead, IGenerateToken generateToken)
        {
            _unitOfWork = unitOfWork;
            _repositoryWrite = repositoryWrite;
            _repositoryRead = repositoryRead;
            _generateToken = generateToken;
        }

        public async Task<string> Execute(string name, string email)
        {
            var user = await _repositoryRead.UserByEmail(email);
            
            if (user is null)
            {
                user = new UserEntitie()
                {
                    Email = email,
                    Username = name,
                    Password = "-"
                };

                await _repositoryWrite.Add(user);
                await _unitOfWork.Commit();
            }

            return _generateToken.Generate(user.UserIdentifier);
        }
    }
}
