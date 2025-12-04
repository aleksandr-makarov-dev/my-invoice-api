using Microsoft.AspNetCore.Mvc;
using MyInvoice.Application.Common;

namespace MyInvoice.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult Match<TInput>(
        this Result<TInput> result,
        Func<TInput, IActionResult> onSuccess,
        Func<Result<TInput>, IActionResult> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }

    public static IActionResult Match(
        this Result result,
        Func<IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }
}
