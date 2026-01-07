// <copyright file="ToErrorResultTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ErrorTests;

using System;
using Ardalis.Result;

/// <summary>
/// Tests for <see cref="Error.ToErrorResult"/> method.
/// </summary>
[TestClass]
public class ToErrorResultTests
{
    /// <summary>
    /// Test that when called then it returns expected result.
    /// </summary>
    [TestMethod]
    public void Error_ToErrorResult_WhenCalled_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorDescription = "Test error description.";
        string errorCode = "TestIdentifier.TestErrorCode";
        Error sourceError = new(errorCode, errorDescription);

        // ACT
        Result result = sourceError.ToErrorResult();

        // ASSERT
        Assert.AreEqual(ResultStatus.Error, result.Status);
        Assert.AreEqual(1, result.Errors.Count());

        string[] errorParts = result.Errors.First().Split(Error.Separator);

        Assert.HasCount(2, errorParts);
        Assert.AreEqual(errorCode, errorParts[0]);
        Assert.AreEqual(errorDescription, errorParts[1]);
    }

    /// <summary>
    /// Test that when called with <see cref="Error.None"/> then it throws <see cref="InvalidOperationException"/>.
    /// </summary>
    [TestMethod]
    public void Error_ToErrorResult_WhenErrorIsNone_ThrowsInvalidOperationException()
    {
        // ARRANGE
        Error sourceError = Error.None;

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => sourceError.ToErrorResult());
    }
}
