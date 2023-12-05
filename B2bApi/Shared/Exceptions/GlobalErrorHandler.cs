using Microsoft.AspNetCore.Mvc;

namespace B2bApi.Shared.Exceptions;

public class GlobalErrorHandler : IMiddleware
{
	private readonly ILogger<GlobalErrorHandler> _logger;
	
	public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
	{
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch(Exception exception)
		{
			_logger.LogError("Exception occurred: {Exception}", exception);
			await HandleAsync(exception, context);
		}
	}

	private async Task HandleAsync(Exception exception, HttpContext context)
	{
		var error = exception switch
		{
			CustomException => CreateProblemDetails(exception, StatusCodes.Status400BadRequest), 
			_ => CreateProblemDetails(exception, StatusCodes.Status500InternalServerError) 
		};
		
		context.Response.StatusCode = error.Status!.Value;
		await context.Response.WriteAsJsonAsync(error);
	}
	
	private static ProblemDetails CreateProblemDetails(Exception exception, int statusCode)
	{
		return new ProblemDetails
		{
			Status = statusCode,
			Title = exception.Message,
			Detail = exception.StackTrace,
			Type = exception.GetType().ToString()
		};
	}
}