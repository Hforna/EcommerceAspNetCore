using AutoMapper;
using EcommerceAspNet.Application.Service.Cryptography;
using EcommerceAspNet.Application.UseCase.Repositorie.User;
using EcommerceAspNet.Application.Validator;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserReadOnlyRepository _readOnly;
        private readonly IUnitOfWork _commit;
        private readonly IAddUser _addUser;
        private readonly IMapper _mapper;
        private readonly IPasswordCryptography _cryptography;
        public async Task<ResponseCreateUser> Execute(RequestCreateUser request)
        {
            await Validate(request);

            var user = _mapper.Map<UserEntitie>(request);
            user.Password = _cryptography.Encrypt(request.Password);

            await _addUser.Add(user);

            await _commit.Commit();

            return new ResponseCreateUser()
            {
                Email = user.Email,
            };
        }

        public async Task Validate(RequestCreateUser request)
        {
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);

            if(await _readOnly.EmailExists(request.Email))
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "E-mail already exists"));
            }

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new UserException(errorMessages);
            }
        }
    }
}
