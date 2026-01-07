// <copyright file="ToServiceUnavailableResultTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ErrorTests;

using System;
using Ardalis.Result;

/// <summary>
/// Tests for <see cref="Error.ToServiceUnavailableResult"/> method.
/// </summary>
[TestClass]
public class ToServiceUnavailableResultTests
{
    /// <summary>
    /// Test that when called then it returns expected result.
    /// </summary>
    [TestMethod]
    public void Error_ToServiceUnavailableResult_WhenCalled_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorDescription = "Test error description.";
        string errorCode = "TestIdentifier.TestErrorCode";
        Error sourceError = new(errorCode, errorDescription);

        // ACT
        Result result = sourceError.ToServiceUnavailableResult();

        // ASSERT
        Assert.AreEqual(ResultStatus.Unavailable, result.Status);
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
    public void Error_ToServiceUnavailableResult_WhenErrorIsNone_ThrowsInvalidOperationException()
    {
        // ARRANGE
        Error sourceError = Error.None;

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => sourceError.ToServiceUnavailableResult());
    }
}
