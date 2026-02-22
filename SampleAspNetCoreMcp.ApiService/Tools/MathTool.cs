using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class MathTools
{
    private readonly ClaimsPrincipal _principal;

    public MathTools(ClaimsPrincipal principal)
    {
        _principal = principal;
    }

    [McpServerTool, Description("Add two numbers together.")]
    [Authorize]
    public Task<string> Add(
        [Description("First operand")] double a,
        [Description("Second operand")] double b)
    {
        var result = a + b;
        return Task.FromResult($"Add from user {_principal.Identity!.Name}: a + b = {result}");
    }

    [McpServerTool, Description("Multiply two numbers together.")]
    public Task<double> Multiply(
        [Description("First operand")] double a,
        [Description("Second operand")] double b)
    {
        return Task.FromResult(a * b);
    }
}