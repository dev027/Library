// <copyright file="ResultExtensionMethods.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.ExtensionMethods;

using System;
using System.Linq;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using MyError = SW.Library.Result.Error;

/// <summary>
/// Extension methods for <see cref="Result{T}"/>.
/// </summary>
public static class ResultExtensionMethods
{
    /// <summary>
    /// Converts <see cref="Result{T}"/> to <see cref="Result"/>.
    /// </summary>
    /// <typeparam name="T">Result type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>A <see cref="Result"/>.</returns>
    public static Result ToResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Result.SuccessWithMessage(result.SuccessMessage),
            ResultStatus.Error => Result.Error(new ErrorList(result.Errors)),
            ResultStatus.Forbidden => Result.Forbidden([.. result.Errors]),
            ResultStatus.Unauthorized => Result.Unauthorized([.. result.Errors]),
            ResultStatus.Invalid => Result.Invalid(result.ValidationErrors),
            ResultStatus.NotFound => Result.NotFound([.. result.Errors]),
            ResultStatus.Conflict => Result.Conflict([.. result.Errors]),
            ResultStatus.CriticalError => Result.CriticalError([.. result.Errors]),
            ResultStatus.Unavailable => Result.Unavailable([.. result.Errors]),
            _ => throw new InvalidOperationException($"Unrecognized Result Status of {result.Status}."),
        };
    }

    /// <summary>
    /// Converts any non-Ok result from one typed result to another.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>Target Type.</returns>
    /// <exception cref="InvalidOperationException">Cannot convert successful typed results to a different typed result.</exception>
    public static Result<TTarget> ToResult<TSource, TTarget>(this Result<TSource> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => throw new InvalidOperationException("Cannot convert successful typed results to a different typed result."),
            ResultStatus.Error => Result<TTarget>.Error(new ErrorList(result.Errors)),
            ResultStatus.Forbidden => Result<TTarget>.Forbidden([.. result.Errors]),
            ResultStatus.Unauthorized => Result<TTarget>.Unauthorized([.. result.Errors]),
            ResultStatus.Invalid => Result<TTarget>.Invalid(result.ValidationErrors),
            ResultStatus.NotFound => Result<TTarget>.NotFound([.. result.Errors]),
            ResultStatus.Conflict => Result<TTarget>.Conflict([.. result.Errors]),
            ResultStatus.CriticalError => Result<TTarget>.CriticalError([.. result.Errors]),
            ResultStatus.Unavailable => Result<TTarget>.Unavailable([.. result.Errors]),
            _ => throw new InvalidOperationException($"Unrecognized Result Status of {result.Status}."),
        };
    }

    /// <summary>
    /// Appends any validation errors.
    /// </summary>
    /// <typeparam name="T">Tye of the result.</typeparam>
    /// <param name="aggregatedResult">The aggregated result.</param>
    /// <param name="newResult">The new result.</param>
    /// <returns>
    /// <list>
    /// <item><see cref="Result"/>: <see cref="ResultStatus.Ok"/> if no validation errors so far.</item>
    /// <item><see cref="Result"/>: <see cref="ResultStatus.Invalid"/> if validation errors encountered</item>
    /// </list>
    /// </returns>
    public static Result AppendValidationErrors<T>(
        this Result aggregatedResult,
        Result<T> newResult) => newResult.Status switch
        {
            ResultStatus.Ok => aggregatedResult,
            ResultStatus.Invalid => aggregatedResult.Status switch
            {
                ResultStatus.Ok => newResult.ToResult(),
                ResultStatus.Invalid => Result.Invalid(
                    aggregatedResult.ValidationErrors
                        .Concat(newResult.ValidationErrors)),
                _ => throw new InvalidOperationException($"Aggregated result has unexpected status of {aggregatedResult.Status}"),
            },
            _ => throw new InvalidOperationException($"New result has unexpected status of {newResult.Status}"),
        };

    /// <summary>
    /// Determines whether this instance is a failure.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>
    ///   <c>true</c> if the specified result is failure; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>A failure is anything that is not a success.</remarks>
    public static bool IsFailure(this Result result) => !result.IsSuccess;

    /// <summary>
    /// Determines whether this instance is a failure.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>
    ///   <c>true</c> if the specified result is failure; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>A failure is anything that is not a success.</remarks>
    public static bool IsFailure<T>(this Result<T> result) => !result.IsSuccess;

    /// <summary>
    /// Convert list of errors into a single error.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>Consolidated error message.</returns>
    public static string Error(this Result result) =>
        string.Join(
            "; ",
            result.Errors.Concat(
            result.ValidationErrors.Select(x => $"{x.Identifier}.{x.ErrorCode}¬{x.ErrorMessage}")));

    /// <summary>
    /// Convert list of errors into a single error.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The result type.</typeparam>
    /// <returns>Consolidated error message.</returns>
    public static string Error<T>(this Result<T> result) =>
        string.Join(
            "; ",
            result.Errors.Concat(
            result.ValidationErrors.Select(x => $"{x.Identifier}.{x.ErrorCode}¬{x.ErrorMessage}")));

    /// <summary>
    /// Create a snapshot of the result.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>Snapshot.</returns>
    public static string SnapShot(this Result result) => result.IsSuccess
        ? $"SUCCESS: {result.SuccessMessage}"
        : $"{result.Status}: {result.Error()}";

    /// <summary>
    /// Create a snapshot of the result.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The type of the payload.</typeparam>
    /// <returns>Snapshot.</returns>
    public static string SnapShot<T>(this Result<T> result) => result.IsSuccess
        ? $"SUCCESS: {result.SuccessMessage}"
        : $"{result.Status}: {result.Error()}";

    /// <summary>
    /// Get the first error code.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>Error code.</returns>
    public static string ErrorCode(this Result result) =>
        MyError.FromResult(result).Code;

    /// <summary>
    /// Get the first error code.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>Error code.</returns>
    public static string ErrorCode<T>(this Result<T> result) =>
        result.ToResult().ErrorCode();

    /// <summary>
    /// Gets the first error message.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>Error message.</returns>
    public static string ErrorMessage(this Result result) =>
        MyError.FromResult(result).Description;

    /// <summary>
    /// Get the first error message.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>Error code.</returns>
    public static string ErrorMessage<T>(this Result<T> result) =>
        result.ToResult().ErrorMessage();

    /// <summary>
    /// Gets the log level.
    /// </summary>
    /// <param name="result">The Result.</param>
    /// <returns>The <see cref="LogLevel"/>.</returns>
    public static LogLevel GetLogLevel(this Result result) => result.Status switch
    {
        ResultStatus.Ok => LogLevel.Information,
        ResultStatus.Created => LogLevel.Information,
        ResultStatus.NoContent => LogLevel.Information,

        ResultStatus.Invalid => LogLevel.Warning,
        ResultStatus.Unauthorized => LogLevel.Warning,
        ResultStatus.Forbidden => LogLevel.Warning,
        ResultStatus.NotFound => LogLevel.Warning,
        ResultStatus.Conflict => LogLevel.Warning,
        ResultStatus.Error => LogLevel.Warning,

        ResultStatus.CriticalError => LogLevel.Error,
        ResultStatus.Unavailable => LogLevel.Error,
        _ => throw new InvalidOperationException($"Unrecognized Result Status of {result.Status}."),
    };

    /// <summary>
    /// Gets the log level.
    /// </summary>
    /// <param name="result">The Result.</param>
    /// <typeparam name="T"> The type of the value.</typeparam>
    /// <returns>The <see cref="LogLevel"/>.</returns>
    public static LogLevel GetLogLevel<T>(this Result<T> result) =>
        result.ToResult().GetLogLevel();
}