// <copyright file="ErrorTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.Error(Result)"/>.
/// </summary>
[TestClass]
public class ErrorTests
{
    /// <summary>
    /// Test that when using a non typed result and the status is error then it returns expected result.
    /// </summary>
    [TestMethod]
    public void Error_WhenNonTypedResultAndStatusIsError_ReturnsExpectedResult()
    {
        // ARRANGE
        ErrorList errorList = new(["ERROR1", "ERROR2"]);
        Result result = Result.Error(errorList);

        // ACT
        string errorMessage = result.Error();

        // ASSERT
        Assert.AreEqual("ERROR1; ERROR2", errorMessage);
    }

    /// <summary>
    /// Test that when using a non typed result and the status is error then it returns expected result.
    /// </summary>
    [TestMethod]
    public void Error_WhenTypedResultAndStatusIsError_ReturnsExpectedResult()
    {
        // ARRANGE
        ErrorList errorList = new(["ERROR1", "ERROR2"]);
        Result<int> result = Result.Error(errorList);

        // ACT
        string errorMessage = result.Error();

        // ASSERT
        Assert.AreEqual("ERROR1; ERROR2", errorMessage);
    }

    /// <summary>
    /// Test that when using a non typed result and the status is invalid then it returns expected result.
    /// </summary>
    [TestMethod]
    public void Error_WhenNonTypedResultAndStatusIsInvalid_ReturnsExpectedResult()
    {
        // ARRANGE
        ValidationError error1 = new(
            "IDENTIFIER1",
            "MESSAGE1",
            "ERRORCODE1",
            ValidationSeverity.Error);
        ValidationError error2 = new(
            "IDENTIFIER2",
            "MESSAGE2",
            "ERRORCODE2",
            ValidationSeverity.Error);
        Result result = Result.Invalid(error1, error2);

        // ACT
        string errorMessage = result.Error();

        // ASSERT
        Assert.AreEqual("IDENTIFIER1.ERRORCODE1¬MESSAGE1; IDENTIFIER2.ERRORCODE2¬MESSAGE2", errorMessage);
    }

    /// <summary>
    /// Test that when using a typed result and the status is invalid then it returns expected result.
    /// </summary>
    [TestMethod]
    public void Error_WhenTypedResultAndStatusIsInvalid_ReturnsExpectedResult()
    {
        // ARRANGE
        ValidationError error1 = new(
            "IDENTIFIER1",
            "MESSAGE1",
            "ERRORCODE1",
            ValidationSeverity.Error);
        ValidationError error2 = new(
            "IDENTIFIER2",
            "MESSAGE2",
            "ERRORCODE2",
            ValidationSeverity.Error);
        Result<int> result = Result.Invalid(error1, error2);

        // ACT
        string errorMessage = result.Error();

        // ASSERT
        Assert.AreEqual("IDENTIFIER1.ERRORCODE1¬MESSAGE1; IDENTIFIER2.ERRORCODE2¬MESSAGE2", errorMessage);
    }
}
