﻿using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Studly.BLL.DTO;
using Studly.BLL.Infrastructure.Exceptions;

namespace Studly.BLL.Infrastructure;

public class GlobalExceptionHandlingMiddleware 
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ve)
            {
                await HandleExceptionAsync(context, ve.Message, HttpStatusCode.BadRequest,ve.MessageForUser);
            }
            catch (NullDataException nde)
            {
                await HandleExceptionAsync(context, nde.Message, HttpStatusCode.BadRequest,nde.MessageForUser);
            }
            catch (NotFoundException nfe)
            {
                await HandleExceptionAsync(context, nfe.Message, HttpStatusCode.NotFound, nfe.MessageForUser);
            }
            catch (LoginException le)
            {
                await HandleExceptionAsync(context, le.Message, HttpStatusCode.Unauthorized, le.MessageForUser);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e.Message, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string exMessage, HttpStatusCode statusCode,
            string message = "")
        {
            _logger.LogError(exMessage);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            ErrorDto error = new()
            {
                Message = message,
                ExceptionMessage = exMessage,
                StatusCode = (int)statusCode
            };

            var result = JsonSerializer.Serialize(error);

            await response.WriteAsync(result);
        }
    }
