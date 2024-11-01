using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Application.Validator;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.Security.Cryptography;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _repositoryRead;
        private readonly IGetUserByToken _userByToken;
        private readonly IPasswordCryptography _cryptography;
        private readonly IMapper _mapper;
        
        public UpdateUserUseCase(IUserReadOnlyRepository userReadOnlyRepository, IGetUserByToken userByToken, IUnitOfWork unitOfWork, IUserWriteOnlyRepository userWriteOnlyRepository, IMapper mapper, IPasswordCryptography passwordCryptography)
        {
            _repositoryRead = userReadOnlyRepository;
            _repositoryWrite = userWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cryptography = passwordCryptography;
            _userByToken = userByToken;
        }

        public async Task<ResponseUpdateUser> Execute(RequestUpdateUser request)
        {
            var user = await _userByToken.GetUser();

            var oldPasswordEncrypt = _cryptography.Encrypt(request.OldPassword!);

            if(await _repositoryRead.PasswordEqual(user!, oldPasswordEncrypt) == true)
                throw new UserException("Password is wrong");

            user = _mapper.Map<EcommerceAspNet.Domain.Entitie.User.User>(request);
            user.Password = _cryptography.Encrypt(request.NewPassword!);

            _repositoryWrite.Update(user);
            await _unitOfWork.Commit();

            return new ResponseUpdateUser()
            {
                Email = request.Email!,
                Username = request.Name!
            };
        }

        public async Task Validate(RequestUpdateUser request)
        {
            var validator = new UpdateUserValidator();
            var result = validator.Validate(request);

            if (await _repositoryRead.EmailExists(request.Email!) == true)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = "Email already exists" });

            if (await _repositoryRead.UsernameExists(request.Name!) == true)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = "Username already exists" });

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new UserException(errorMessages);
            }
        }
    }
}
