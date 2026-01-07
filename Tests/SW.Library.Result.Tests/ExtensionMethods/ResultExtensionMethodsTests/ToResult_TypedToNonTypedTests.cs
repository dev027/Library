// <copyright file="ToResult_TypedToNonTypedTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using System;
using System.Reflection;
using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.ToResult{T}(Result{T})"/>.
/// </summary>
[TestClass]
public class ToResult_TypedToNonTypedTests
{
    /// <summary>
    /// Test that when status is ok then it returns expected result.
    /// </summary>
    [TestMethod]
    public void ToResult_WhenStatusIsOk_ReturnsExpectedResult()
    {
        // ARRANGE
        var typedResult = Result<int>.Success(42);

        // ACT
        Result result = typedResult.ToResult();

        // ASSERT
        Assert.AreEqual(ResultStatus.Ok, result.Status);
        Assert.AreEqual(0, result.Errors.Count());
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
        Result result = typedResult.ToResult();

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
        Result result = typedResult.ToResult();

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
        Result result = typedResult.ToResult();

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
        Result result = typedResult.ToResult();

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
        Result result = typedResult.ToResult();

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
        Result result = typedResult.ToResult();

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
        Result result = typedResult.ToResult();

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
        Result result = typedResult.ToResult();

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
        Assert.ThrowsExactly<InvalidOperationException>(() => typedResult.ToResult());
    }
}