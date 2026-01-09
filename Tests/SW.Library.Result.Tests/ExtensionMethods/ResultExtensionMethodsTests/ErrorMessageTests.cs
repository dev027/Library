// <copyright file="ErrorMessageTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.ErrorMessage(Result)"/>.
/// </summary>
[TestClass]
public class ErrorMessageTests
{
    /// <summary>
    /// Test that when using a non typed result and the status is error then
    /// it returns the expected result.
    /// </summary>
    [TestMethod]
    public void ErrorMessage_NonTypedResult_WhenStatusIsError_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorCode = "IDENTIFIER.ERRORCODE";
        string errorMessage = "ERROR MESSAGE";
        Result result = new Error(errorCode, errorMessage).ToErrorResult();

        // ACT
        string actualErrorMessage = result.ErrorMessage();

        // ASSERT
        Assert.AreEqual(errorMessage, actualErrorMessage);
    }

    /// <summary>
    /// Test that when using a typed result and the status is error then
    /// it returns the expected result.
    /// </summary>
    [TestMethod]
    public void ErrorMessage_TypedResult_WhenStatusIsError_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorCode = "IDENTIFIER.ERRORCODE";
        string errorMessage = "ERROR MESSAGE";

        Result<int> result = Result.Error($"{errorCode}{Error.Separator}{errorMessage}");

        // ACT
        string actualErrorMessage = result.ErrorMessage();

        // ASSERT
        Assert.AreEqual(errorMessage, actualErrorMessage);
    }
}