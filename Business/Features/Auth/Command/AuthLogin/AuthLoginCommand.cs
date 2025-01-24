using Business.Features.Auth.Command.Dtos;
using Business.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Auth.Command.AuthLogin;

public class AuthLoginCommand : IRequest<Response<ResponseTokenDto>>
{
	public string Email { get; set; }
	public string Password { get; set; }
}
