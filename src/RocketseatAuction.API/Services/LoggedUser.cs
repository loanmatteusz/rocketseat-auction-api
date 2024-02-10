﻿using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.Services;

public class LoggedUser: ILoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public LoggedUser(IHttpContextAccessor httpContext, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContext;
        _userRepository = userRepository;
    }

    public User User()
    {
        var token = TokenOnRequest();
        var email = FromBase64String(token);

        return _userRepository.GetUserByEmail(email);
    }

    private string TokenOnRequest()
    {
        var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authentication["Bearer ".Length..];
    }

    private string FromBase64String(string value)
    {
        var data = Convert.FromBase64String(value);

        return System.Text.Encoding.UTF8.GetString(data);
    }
}
