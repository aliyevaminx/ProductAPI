﻿using Business.Features.Role.Queries.Dtos;
using Business.Features.Role.Queries.GetAllRoles;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
	private readonly IMediator _mediator;

	public RoleController(IMediator mediator)
	{
		_mediator = mediator;
	}

	#region Documentation
	/// <summary>
	/// Roles list
	/// </summary>
	[ProducesResponseType(typeof(Response<List<RoleDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
	#endregion
	[HttpGet]
	public async Task<Response<List<RoleDto>>> GetAllRolesAsync()
	=> await _mediator.Send(new GetAllRolesQuery());
}
