﻿using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repository.Identity;
using EcommerceAspNet.Communication.Request.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    public class RoleController : BaseController
    {
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRole([FromServices] ICreateRoleUseCase useCase, [FromBody] RequestCreateRole request)
        {
            await useCase.Execute(request.RoleName);

            return Ok();
        }
    }
}
