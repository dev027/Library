// <copyright file="ToInvalidResultTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ErrorTests;

using System;
using Ardalis.Result;

/// <summary>
/// Tests for <see cref="Error.ToInvalidResult"/> method.
/// </summary>
[TestClass]
public class ToInvalidResultTests
{
    /// <summary>
    /// Test that when called then it returns expected result.
    /// </summary>
    [TestMethod]
    public void Error_ToInvalidResultTests_WhenCalled_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorDescription = "Test error description.";
        string errorCode = "TestIdentifier.TestErrorCode";
        string[] errorCodeParts = errorCode.Split('.');
        Error sourceError = new(errorCode, errorDescription);

        // ACT
        Result result = sourceError.ToInvalidResult();

        // ASSERT
        Assert.AreEqual(ResultStatus.Invalid, result.Status);
        Assert.AreEqual(1, result.ValidationErrors.Count());

        ValidationError validationError = result.ValidationErrors.First();

        Assert.AreEqual(errorCodeParts[0], validationError.Identifier);
        Assert.AreEqual(errorCodeParts[1], validationError.ErrorCode);
        Assert.AreEqual(errorDescription, validationError.ErrorMessage);
        Assert.AreEqual(ValidationSeverity.Error, validationError.Severity);
    }

    /// <summary>
    /// Test that when called with <see cref="Error.None"/> then it throws <see cref="InvalidOperationException"/>.
    /// </summary>
    [TestMethod]
    public void Error_ToInvalidResult_WhenErrorIsNone_ThrowsInvalidOperationException()
    {
        // ARRANGE
        Error sourceError = Error.None;

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => sourceError.ToInvalidResult());
    }
}
