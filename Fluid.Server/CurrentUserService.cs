﻿using System.Security.Claims;
using Fluid.Core.Interfaces;

namespace Fluid.Server;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        Claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
    }
    public string UserId { get; }
    public string UserName { get; set; }
    public List<KeyValuePair<string, string>> Claims { get; set; }
}