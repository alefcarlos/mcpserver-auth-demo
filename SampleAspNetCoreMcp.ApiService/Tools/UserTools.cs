using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
[Authorize]
public sealed class UserTools
{
    private readonly ClaimsPrincipal _principal;

    public UserTools(ClaimsPrincipal principal)
    {
        _principal = principal;
    }

    [McpServerTool, Description("Prints user")]
    public Task<Dictionary<string, string>> UserInfo()
    {
        var claims = _principal.Claims.ToDictionary(x => x.Type, x => x.Value);

        return Task.FromResult(claims);
    }
}
