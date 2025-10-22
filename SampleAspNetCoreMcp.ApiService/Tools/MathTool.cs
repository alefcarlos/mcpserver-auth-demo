using System;
using System.ComponentModel;
using ModelContextProtocol.Server;

namespace SampleAspNetCoreMcp.ApiService.Tools;

[McpServerToolType]
public sealed class MathTools
{
    private readonly IHttpContextAccessor _h;

    public MathTools(IHttpContextAccessor h)
    {
        _h = h;
    }

    [McpServerTool, Description("Add two numbers together.")]
    public Task<double> Add(
        [Description("First operand")] double a,
        [Description("Second operand")] double b)
    {
        Console.WriteLine(_h.HttpContext.User.Identity.Name);
        return Task.FromResult(a + b);
    }

    [McpServerTool, Description("Multiply two numbers together.")]
    public Task<double> Multiply(
        [Description("First operand")] double a,
        [Description("Second operand")] double b)
    {
        return Task.FromResult(a * b);
    }
}