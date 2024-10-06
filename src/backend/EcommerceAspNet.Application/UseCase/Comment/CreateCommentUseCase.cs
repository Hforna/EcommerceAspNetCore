﻿using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Comment;
using EcommerceAspNet.Application.Validator;
using EcommerceAspNet.Communication.Request.Comment;
using EcommerceAspNet.Communication.Response.Comment;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Comment;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Comment
{
    public class CreateCommentUseCase : ICreateComment
    {
        private readonly IGetUserByToken _userByToken;
        private readonly SqidsEncoder<long> _sqids;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentWriteOnlyRepository _repositoryWrite;

        public CreateCommentUseCase(IGetUserByToken userByToken, SqidsEncoder<long> sqids, IMapper mapper, IUnitOfWork unitOfWork, ICommentWriteOnlyRepository repositoryWrite)
        {
            _userByToken = userByToken;
            _sqids = sqids;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repositoryWrite = repositoryWrite;
        }

        public async Task<ResponseComment> Execute(RequestCreateComment request)
        {
            Validate(request);

            var user = await _userByToken.GetUser();

            var product = _mapper.Map<CommentEntitie>(request);
            product.UserId = user.Id;

            _repositoryWrite.Add(product);
            await _unitOfWork.Commit();

            return new ResponseComment()
            {
                Text = request.Text!,
                Note = request.Note,
                Username = user.Username,
                UserImage = user.ImageIdentifier,
            };
        }

        public void Validate(RequestCreateComment request)
        {
            var validator = new CreateCommentValidator();
            var result = validator.Validate(request);

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new ProductException(errorMessages);
            }
        }
    }
}
