// <copyright file="FromResultTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ErrorTests;

using System;
using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Test for <see cref="Error.FromResult(Result)"/> method.
/// </summary>
[TestClass]
public class FromResultTests
{
    /// <summary>
    /// Test that Resuls is status ok throws InvalidOperationException.
    /// </summary>
    [TestMethod]
    public void Error_FromResult_WhenResultIsStatusOk_ThrowsInvalidOperationException()
    {
        // ARRANGE
        Result result = Result.Success();

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => Error.FromResult(result));
    }

    /// <summary>
    /// Test that when result is status invalid, returns expected Error.
    /// </summary>
    [TestMethod]
    public void Error_FromResult_WhenResultIsStatusInvalid_ReturnsExpectedError()
    {
        // ARRANGE
        string errorDescription = "Test error description.";
        string errorCode = "TestIdentifier.TestErrorCode";
        Error sourceError = new(errorCode, errorDescription);
        Result result = sourceError.ToInvalidResult();

        // ACT
        Error error = Error.FromResult(result);

        // ASSERT
        Assert.AreEqual(errorCode, error.Code);
        Assert.AreEqual(errorDescription, errorDescription);
    }

    /// <summary>
    /// Test that when result is status error, returns expected Error.
    /// </summary>
    [TestMethod]
    public void Error_FromResult_WhenResultIsStatusError_ReturnsExpectedError()
    {
        // ARRANGE
        string errorDescription = "Test error description.";
        string errorCode = "TestIdentifier.TestErrorCode";
        Error sourceError = new(errorCode, errorDescription);
        Result result = sourceError.ToErrorResult();

        // ACT
        Error error = Error.FromResult(result);

        // ASSERT
        Assert.AreEqual(errorCode, error.Code);
        Assert.AreEqual(errorDescription, errorDescription);
    }

    /// <summary>
    /// Test that when result is status invalid with multiple validation errors, returns first Error.
    /// </summary>
    [TestMethod]
    public void Error_FromResult_WhenResultIsStatusInvalid_AndMultipleValidationErrors_ReturnsFirstError()
    {
        // ARRANGE
        Error error1 = new("FirstIdentifier.FirstErrorCode", "First error description.");
        Error error2 = new("SecondIndentifier.SecondErrorCode", "Second error description");
        Result result = Result.Success();
        Result result1 = error1.ToInvalidResult();
        Result result2 = error2.ToInvalidResult();
        result = result.AppendValidationErrors(result1);
        result = result.AppendValidationErrors(result2);

        // ACT
        Error error = Error.FromResult(result);

        // ASSERT
        Assert.AreEqual(error1.Code, error.Code);
        Assert.AreEqual(error1.Description, error.Description);
    }

    /// <summary>
    /// Test that when result is status error with multiple errors, returns first Error.
    /// </summary>
    [TestMethod]
    public void Error_FromResult_WhenResultIsStatusError_AndMultipleErrors_ReturnsFirstError()
    {
        // ARRANGE
        ErrorList errorList = new(
            [
                "First error",
                "Second error",

            ]);
        Result result = Result.Error(errorList);

        // ACT & ASSERT
        InvalidOperationException ex = Assert.ThrowsExactly<InvalidOperationException>(() => Error.FromResult(result));
        Assert.AreEqual(
            "Cannot convert result to error. Result error string not in expected format.",
            ex.Message);
    }
}
