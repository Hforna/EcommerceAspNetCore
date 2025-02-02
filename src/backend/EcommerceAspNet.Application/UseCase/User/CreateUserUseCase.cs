﻿using AutoMapper;
using EcommerceAspNet.Application.Service.Email;
using EcommerceAspNet.Application.UseCase.Repositorie.User;
using EcommerceAspNet.Application.Validator;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security.Cryptography;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserWriteOnlyRepository _addUser;
        private readonly IMapper _mapper;
        private readonly IPasswordCryptography _cryptography;
        private readonly UserManager<Domain.Entitie.User.User> _userManager;
        private readonly EmailService _emailService;

        public CreateUserUseCase(IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork, 
            IUserWriteOnlyRepository userWriteOnlyRepository, IMapper mapper, 
            IPasswordCryptography passwordCryptography, UserManager<Domain.Entitie.User.User> userManager, EmailService emailService)
        {
            _readOnly = userReadOnlyRepository;
            _commit = unitOfWork;
            _addUser = userWriteOnlyRepository;
            _mapper = mapper;
            _cryptography = passwordCryptography;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ResponseCreateUser> Execute(RequestCreateUser request)
        {
            await Validate(request);

            var user = _mapper.Map<EcommerceAspNet.Domain.Entitie.User.User>(request);

            user.Password = _cryptography.Encrypt(request.Password);
            user.SecurityStamp = Guid.NewGuid().ToString();

            await _addUser.Add(user);

            await _commit.Commit();

            await _userManager.AddToRoleAsync(user, "customer");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var message = $"Click here for confirm your email: http://localhost:5008/api/user/confirm-email?email={user.Email}&token={token}";

            await _emailService.SendEmail(message, user.Email!, user.UserName!);

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
                throw new UserException("E-mail already exists");
            }

            if (await _readOnly.UsernameExists(request.Username!) == true)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = "Username already exists" });

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new UserException(errorMessages);
            }
        }
    }
}
