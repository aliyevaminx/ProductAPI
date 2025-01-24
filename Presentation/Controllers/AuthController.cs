﻿using Business.Features.Auth.Command.AuthLogin;
using Business.Features.Auth.Command.AuthRegister;
using Business.Features.Auth.Command.Dtos;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;

	public AuthController(IMediator mediator)
	{
		_mediator = mediator;
	}

	#region Documentation
	/// <summary>
	/// User registration
	/// </summary>
	/// <param name="request"></param>
	[ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
	#endregion
	[HttpPost("register")]
	public async Task<Response> RegisterAsync(AuthRegisterCommand request)
	=> await _mediator.Send(request);

	#region Documentation
	/// <summary>
	/// User login
	/// </summary>
	/// <param name="request"></param>
	[ProducesResponseType(typeof(Response<ResponseTokenDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
	#endregion
	[HttpPost("login")]
	public async Task<Response<ResponseTokenDto>> LoginAsync(AuthLoginCommand request)
	=> await _mediator.Send(request);
}
