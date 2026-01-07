// <copyright file="ToResult_TypedToTypedTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using System;
using System.Reflection;
using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.ToResult{TSource, TTarget}(Result{TSource})"/>.
/// </summary>
[TestClass]
public class ToResult_TypedToTypedTests
{
    /// <summary>
    /// Test that when status is ok then throws invalid operation exception.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsOk_ThrowsInvalidOperationException()
    {
        // ARRANGE
        Result<int> typedResult = Result<int>.Success(42);

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => typedResult.ToResult<int, string>());
    }

    /// <summary>
    /// Test that when status is error then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsError_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.Error(error);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.Error, result.Status);
        Assert.AreEqual(error, result.Errors.First());
    }

    /// <summary>
    /// Test that when status is forbidden then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsForbidden_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.Forbidden(error);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.Forbidden, result.Status);
        Assert.AreEqual(error, result.Errors.First());
    }

    /// <summary>
    /// Test that when status is unauthorized then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsUnauthorized_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.Unauthorized(error);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.Unauthorized, result.Status);
        Assert.AreEqual(error, result.Errors.First());
    }

    /// <summary>
    /// Test that when status is error then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsInvalid_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        ValidationError validationError = new(error);
        Result<int> typedResult = Result<int>.Invalid(validationError);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.Invalid, result.Status);
        Assert.AreEqual(error, result.ValidationErrors.First().ErrorMessage);
    }

    /// <summary>
    /// Test that when status is not found then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsNotFound_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.NotFound(error);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.NotFound, result.Status);
        Assert.AreEqual(error, result.Errors.First());
    }

    /// <summary>
    /// Test that when status is error then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsConflict_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.Conflict(error);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.Conflict, result.Status);
        Assert.AreEqual(error, result.Errors.First());
    }

    /// <summary>
    /// Test that when status is critical error then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsCriticalError_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.CriticalError(error);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.CriticalError, result.Status);
        Assert.AreEqual(error, result.Errors.First());
    }

    /// <summary>
    /// Test that when status is unavailable then it return expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsUnavailable_ReturnsExpectedResult()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.Unavailable(error);

        // ACT
        Result<string> result = typedResult.ToResult<int, string>();

        // ASSERT
        Assert.AreEqual(ResultStatus.Unavailable, result.Status);
        Assert.AreEqual(error, result.Errors.First());
    }

    /// <summary>
    /// Test that when status is unexpected then it throws invalid operation exception.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsUnexpected_ThrowsInvalidOperationException()
    {
        // ARRANGE
        string error = "Error message";
        Result<int> typedResult = Result<int>.Error(error);

        // 1. Get the PropertyInfo object for "Status"
        PropertyInfo prop = typeof(Result<int>).GetProperty("Status", BindingFlags.Public | BindingFlags.Instance)!;

        // 2. Use SetValue to bypass the 'protected' restriction
        // We cast the integer to the Enum type before setting it
        prop.SetValue(typedResult, (ResultStatus)99);

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => typedResult.ToResult<int, string>());
    }
}