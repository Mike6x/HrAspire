﻿namespace HrAspire.Business.Common;

using System;

public class ServiceResult
{
    internal const string ErrorNotFoundMessage = "Object not found.";

    public static readonly ServiceResult Success = new();
    public static readonly ServiceResult ErrorNotFound = new() { ErrorMessage = ErrorNotFoundMessage };

    protected ServiceResult()
    {
    }

    public string? ErrorMessage { get; protected set; }

    public bool IsError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public static ServiceResult Error(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new ArgumentException("Error service result cannot have null or white space error message.");
        }

        return new ServiceResult { ErrorMessage = errorMessage };
    }
}