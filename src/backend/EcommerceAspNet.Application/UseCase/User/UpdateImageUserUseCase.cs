using EcommerceAspNet.Application.Extension;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class UpdateImageUserUseCase : IUpdateImageUser
    {
        private readonly IGetUserByToken _userLogged;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserWriteOnlyRepository _repositoryWrite;
        private readonly IAzureStorageService _storageService;

        public UpdateImageUserUseCase(IGetUserByToken userLogged, IUnitOfWork unitOfWork, IUserWriteOnlyRepository repositoryWrite, IAzureStorageService storageService)
        {
            _userLogged = userLogged;
            _unitOfWork = unitOfWork;
            _repositoryWrite = repositoryWrite;
            _storageService = storageService;
        }

        public async Task Execute(IFormFile file)
        {
            var user = await _userLogged.GetUser();

            var fileStream = file.OpenReadStream();

            var validateImage = FileImageExtension.ValidateImage(fileStream);

            if (validateImage.isImage == false)
                throw new UserException("File must be an image");

            user!.ImageIdentifier = $"{Guid.NewGuid()}{validateImage.typeImage}";

            _repositoryWrite.Update(user);
            await _unitOfWork.Commit();

            await _storageService.UploadUser(user, fileStream, user.ImageIdentifier);
        }
    }
}
