﻿using Delivery.BaseLib.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Delivery.BaseLib.Core.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception.InnerException ?? context.Exception;

        context.HttpContext.Response.StatusCode = exception is NotFoundException ? 404 : 400;
        context.Result = new JsonResult(new
        {
            exception.Message,
            Type = exception.GetType().Name
        });
    }
}