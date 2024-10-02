using EcommerceAspNet.Application.Service.Cryptography;
using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _readRepository;
        private readonly IPasswordCryptography _cryptography;
        private readonly IGenerateToken _generateToken;

        public LoginUseCase(IUserReadOnlyRepository readRepository, IPasswordCryptography cryptography, IGenerateToken generateToken)
        {
            _readRepository = readRepository;
            _cryptography = cryptography;
            _generateToken = generateToken;
        }

        public async Task<ResponseCreateUser> Execute(RequestLoginUser request)
        {
            var password = _cryptography.Encrypt(request.Password);

            var user = await _readRepository.LoginByEmailAndPassword(request.Email, password) ?? throw new UserException("User doesn't exists");

            var token = _generateToken.Generate(user.UserIdentifier);

            return new ResponseCreateUser()
            {
                Email = user.Email,
                TokenGenerated = token
            };
        }
    }
}
