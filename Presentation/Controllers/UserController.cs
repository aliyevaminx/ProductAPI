﻿using Business.Features.User.Queries.Dtos;
using Business.Features.User.Queries.GetAllUsers;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IMediator _mediator;

	public UserController(IMediator mediator)
	{
		_mediator = mediator;
	}

	#region Documentation
	/// <summary>
	/// Users list
	/// </summary>
	[ProducesResponseType(typeof(Response<List<UserDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
	#endregion
	[HttpGet]
	public async Task<Response<List<UserDto>>> GetAllUsersAsync()
	=> await _mediator.Send(new GetAllUsersQuery());
}
