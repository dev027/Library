// <copyright file="Error.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result;

using Ardalis.Result;

/// <summary>
/// Represents and error code and description.
/// </summary>
public sealed record Error(string Code, string Description)
{
    /// <summary>
    /// None.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>
    /// The separator between the error code and description.
    /// </summary>
    public static readonly string Separator = "¬";

    /// <summary>
    /// Converts a result back to an error.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>The result as an error.</returns>
    /// <remarks>
    /// In case of multiple errors, only the first error is returned.
    /// </remarks>
    public static Error FromResult(Result result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => throw new InvalidOperationException("Cannot convert successful result to error."),
            ResultStatus.Invalid => ConvertValidationErrorToError(result),
            _ => ConvertOtherErrorToError(result),
        };

        /* Local Functions */

        static Error ConvertValidationErrorToError(Result result)
        {
            ValidationError validationError = result.ValidationErrors.First();
            return new Error(
                    $"{validationError.Identifier}.{validationError.ErrorCode}",
                    validationError.ErrorMessage);
        }

        static Error ConvertOtherErrorToError(Result result)
        {
            string errorString = result.Errors.First();
            string[] parts = errorString.Split(Separator);
            if (parts.Length != 2)
            {
                throw new InvalidOperationException("Cannot convert result to error. Result error string not in expected format.");
            }

            return new Error(parts[0], parts[1]);
        }
    }

    /// <summary>
    /// Convert instance into an error result.
    /// </summary>
    /// <returns>The result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when Error is <see cref="None"/>.</exception>
    public Result ToErrorResult() => this == None
        ? throw new InvalidOperationException("Cannot convert Error.None into a result")
        : Result.Error(this.FormatErrorString());

    /// <summary>
    /// Convert instance into a not found result.
    /// </summary>
    /// <returns>The result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when Error is <see cref="None"/>.</exception>
    public Result ToNotFoundResult() => this == None
        ? throw new InvalidOperationException("Cannot convert Error.None into a result")
        : Result.NotFound(this.FormatErrorString());

    /// <summary>
    /// Convert instance into a service unavailable result.
    /// </summary>
    /// <returns>The result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when Error is <see cref="None"/>.</exception>
    public Result ToServiceUnavailableResult() => this == None
        ? throw new InvalidOperationException("Cannot convert Error.None into a result")
        : Result.Unavailable(this.FormatErrorString());

    /// <summary>
    /// Convert instance into a critical error result.
    /// </summary>
    /// <returns>The result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when Error is <see cref="None"/>.</exception>
    public Result ToCriticalErrorResult() => this == None
        ? throw new InvalidOperationException("Cannot convert Error.None into a result")
        : Result.CriticalError(this.FormatErrorString());

    /// <summary>
    /// Convert instance into a service unavailable result.
    /// </summary>
    /// <returns>The result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when Error is <see cref="None"/>.</exception>
    public Result ToForbiddenResult() => this == None
        ? throw new InvalidOperationException("Cannot convert Error.None into a result")
        : Result.Forbidden(this.FormatErrorString());

    /// <summary>
    /// Converts to instance to a <see cref="Result.Invalid(ValidationError)"/>.
    /// </summary>
    /// <returns><see cref="Result.Invalid(ValidationError)"/>.</returns>
    /// <exception cref="System.InvalidOperationException">Cannot convert Error.None into an invalid result.</exception>
    public Result ToInvalidResult()
    {
        if (this == None)
        {
            throw new InvalidOperationException("Cannot convert Error.None into an invalid result");
        }

        var parts = this.Code.Split('.');

        ValidationError validationError = new(
            identifier: parts[0],
            errorMessage: this.Description,
            errorCode: parts[1],
            severity: ValidationSeverity.Error);

        return Result.Invalid(validationError);
    }

    private string FormatErrorString() => $"{this.Code}{Separator}{this.Description}";
}