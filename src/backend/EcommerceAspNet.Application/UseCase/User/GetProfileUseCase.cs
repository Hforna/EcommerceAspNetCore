using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class GetProfileUseCase : IGetProfileUseCase
    {
        private readonly IGetUserByToken _userByToken;

        public GetProfileUseCase(IGetUserByToken userByToken)
        {
            _userByToken = userByToken;
        }

        public async Task<ResponseGetProfile> Execute()
        {
            var user = await _userByToken.GetUser() ?? throw new UserException("User doesn't exists");

            return new ResponseGetProfile()
            {
                Email = user.Email,
            };
        }
    }
}
