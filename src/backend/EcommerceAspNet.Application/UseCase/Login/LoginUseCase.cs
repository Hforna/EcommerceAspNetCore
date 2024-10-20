using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.Security.Cryptography;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _readRepository;
        private readonly IPasswordCryptography _cryptography;
        private readonly IGenerateToken _generateToken;
        private readonly UserManager<UserEntitie> _userManager;

        public LoginUseCase(IUserReadOnlyRepository readRepository, IPasswordCryptography cryptography, IGenerateToken generateToken, UserManager<UserEntitie> userManager)
        {
            _readRepository = readRepository;
            _cryptography = cryptography;
            _generateToken = generateToken;
            _userManager = userManager;
        }

        public async Task<ResponseCreateUser> Execute(RequestLoginUser request)
        {
            var user = await _readRepository.LoginByEmail(request.Email);

            if (user is null || _cryptography.IsValid(request.Password, user.Password) == false)
                throw new UserException("E-mail or password invalid");


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var isAdmin = false;

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                if(role == "admin")
                    isAdmin = true;

                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (isAdmin == false)
                if (user.EmailConfirmed == false)
                    throw new UserException("Please confirm your e-mail for continue");

            var token = _generateToken.Generate(user.UserIdentifier, claims);

            return new ResponseCreateUser()
            {
                Email = user.Email,
                TokenGenerated = token
            };
        }
    }
}
