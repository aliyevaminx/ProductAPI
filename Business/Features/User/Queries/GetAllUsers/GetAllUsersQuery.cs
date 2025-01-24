﻿using Business.Features.User.Queries.Dtos;
using Business.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.User.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<Response<List<UserDto>>>
{
}
