// <copyright file="ErrorCodeTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.ErrorCode(Result)"/>.
/// </summary>
[TestClass]
public class ErrorCodeTests
{
    /// <summary>
    /// Test that when using a non typed result and the status is error then
    /// it returns the expected result.
    /// </summary>
    [TestMethod]
    public void ErrorCode_NonTypedResult_WhenStatusIsError_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorCode = "IDENTIFIER.ERRORCODE";
        string errorMessage = "ERROR MESSAGE";
        Result result = new Error(errorCode, errorMessage).ToErrorResult();

        // ACT
        string actualErrorCode = result.ErrorCode();

        // ASSERT
        Assert.AreEqual(errorCode, actualErrorCode);
    }

    /// <summary>
    /// Test that when using a typed result and the status is error then
    /// it returns the expected result.
    /// </summary>
    [TestMethod]
    public void ErrorCode_TypedResult_WhenStatusIsError_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorCode = "IDENTIFIER.ERRORCODE";
        string errorMessage = "ERROR MESSAGE";

        Result<int> result = Result.Error($"{errorCode}{Error.Separator}{errorMessage}");

        // ACT
        string actualErrorCode = result.ErrorCode();

        // ASSERT
        Assert.AreEqual(errorCode, actualErrorCode);
    }
}