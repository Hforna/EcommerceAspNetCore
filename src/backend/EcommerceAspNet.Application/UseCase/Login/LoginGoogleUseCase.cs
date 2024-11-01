using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly UserManager<Domain.Entitie.User.User> _userManager;

        public LoginGoogleUseCase(IUnitOfWork unitOfWork, IUserWriteOnlyRepository repositoryWrite, 
            IUserReadOnlyRepository repositoryRead, IGenerateToken generateToken,
            UserManager<Domain.Entitie.User.User> userManager)
        {
            _unitOfWork = unitOfWork;
            _repositoryWrite = repositoryWrite;
            _repositoryRead = repositoryRead;
            _generateToken = generateToken;
            _userManager = userManager;
        }

        public async Task<string> Execute(string name, string email)
        {
            var user = await _repositoryRead.UserByEmail(email);
            
            if (user is null)
            {
                user = new Domain.Entitie.User.User()
                {
                    Email = email,
                    UserName = name,
                    Password = "-"
                };

                await _userManager.AddToRoleAsync(user, "customer");

                await _repositoryWrite.Add(user);
                await _unitOfWork.Commit();
            }
  

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "customer")              
            };

            return _generateToken.Generate(user.UserIdentifier, claims);
        }
    }
}
