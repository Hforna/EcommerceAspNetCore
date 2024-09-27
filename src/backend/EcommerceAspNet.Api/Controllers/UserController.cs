﻿using EcommerceAspNet.Application.UseCase.Repositorie.User;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Communication.Request.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestCreateUser request, [FromServices] ICreateUserUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RequestUpdateUser request, [FromServices] IUpdateUserUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
    }
}