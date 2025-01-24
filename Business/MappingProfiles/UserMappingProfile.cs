using AutoMapper;
using Business.Features.Auth.Command.AuthRegister;
using Business.Features.User.Queries.Dtos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles;

public class UserMappingProfile : Profile
{
	public UserMappingProfile()
	{
		CreateMap<User, UserDto>();
	}
}
