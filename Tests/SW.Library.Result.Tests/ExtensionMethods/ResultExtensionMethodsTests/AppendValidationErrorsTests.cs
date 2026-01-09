// <copyright file="AppendValidationErrorsTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Test for <see cref="ResultExtensionMethods.AppendValidationErrors{T}(Result, Result{T})"/>.
/// </summary>
[TestClass]
public class AppendValidationErrorsTests
{
    /// <summary>
    /// Test that when status is ok and invalid is appended then status is invalid.
    /// </summary>
    [TestMethod]
    public void AppendValidationErrors_WhenStatusisOkAndInvalidAppended_StatusIsInvalid()
    {
        // ARRANGE
        Result result = Result.Success();

        Result<int> invalidResult1 = new Error("IDENTIFER1.ERRORCODE1", "ERROR MESSAGE 1").ToInvalidResult();

        // ACT
        result = result.AppendValidationErrors(invalidResult1);

        // ASSERT
        Assert.AreEqual(ResultStatus.Invalid, result.Status);
        Assert.AreEqual(1, result.ValidationErrors.Count());
    }

    /// <summary>
    /// Test that when status is ok and success is appended then status is ok.
    /// </summary>
    [TestMethod]
    public void AppendValidationErrors_WhenStatusisOkAndSuccessAppended_StatusIsOk()
    {
        // ARRANGE
        Result result = Result.Success();

        Result<int> successResult1 = Result<int>.Success(42);

        // ACT
        result = result.AppendValidationErrors(successResult1);

        // ASSERT
        Assert.AreEqual(ResultStatus.Ok, result.Status);
    }

    /// <summary>
    /// Test that when status is invalid and success is appended then status is invalid.
    /// </summary>
    [TestMethod]
    public void AppendValidationErrors_WhenStatusisInvalidAndInvalidAppended_StatusIsInvalid()
    {
        // ARRANGE
        Result result = Result.Success();

        Result<int> invalidResult1 = new Error("IDENTIFER1.ERRORCODE1", "ERROR MESSAGE 1").ToInvalidResult();
        Result<int> invalidResult2 = new Error("IDENTIFER2.ERRORCODE2", "ERROR MESSAGE 2").ToInvalidResult();
        result = result.AppendValidationErrors(invalidResult1);

        // ACT
        result = result.AppendValidationErrors(invalidResult2);

        // ASSERT
        Assert.AreEqual(ResultStatus.Invalid, result.Status);
        Assert.AreEqual(2, result.ValidationErrors.Count());
    }

    /// <summary>
    /// Test that when status is invalid and success is appended then status is invalid.
    /// </summary>
    [TestMethod]
    public void AppendValidationErrors_WhenStatusisInvalidAndSuccessAppended_StatusIsInvalid()
    {
        // ARRANGE
        Result result = Result.Success();

        Result<int> invalidResult1 = new Error("IDENTIFER1.ERRORCODE1", "ERROR MESSAGE 1").ToInvalidResult();
        Result<int> successResult1 = Result<int>.Success(42);
        result = result.AppendValidationErrors(invalidResult1);

        // ACT
        result = result.AppendValidationErrors(successResult1);

        // ASSERT
        Assert.AreEqual(ResultStatus.Invalid, result.Status);
        Assert.AreEqual(1, result.ValidationErrors.Count());
    }

    /// <summary>
    /// Test that when status is ok and error is appended then exception is thrown.
    /// </summary>
    [TestMethod]
    public void AppendValidationErrors_WhenStatusisOkAndErrorAppended_ExceptionIsThrown()
    {
        // ARRANGE
        Result result = Result.Success();

        Result<int> errorResult1 = new Error("IDENTIFER1.ERRORCODE1", "ERROR MESSAGE 1").ToErrorResult();

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => result.AppendValidationErrors(errorResult1));
    }

    /// <summary>
    /// Test that when status is invalid and error is appended then exception is thrown.
    /// </summary>
    [TestMethod]
    public void AppendValidationErrors_WhenStatusisInvalidAndErrorAppended_ExceptionIsThrown()
    {
        // ARRANGE
        Result result = Result.Success();
        Result<int> invalidResult1 = new Error("IDENTIFER1.ERRORCODE1", "ERROR MESSAGE 1").ToInvalidResult();
        result = result.AppendValidationErrors(invalidResult1);

        Result<int> errorResult1 = new Error("IDENTIFER1.ERRORCODE1", "ERROR MESSAGE 1").ToErrorResult();

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => result.AppendValidationErrors(errorResult1));
    }

    /// <summary>
    /// Test that when status is error and invalid is appended then exception is thrown.
    /// </summary>
    [TestMethod]
    public void AppendValidationErrors_WhenStatusisErrorAndInvalidAppended_ExceptionIsThrown()
    {
        // ARRANGE
        Result result = Result.Error();
        Result<int> invalidResult1 = new Error("IDENTIFER1.ERRORCODE1", "ERROR MESSAGE 1").ToInvalidResult();

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => result.AppendValidationErrors(invalidResult1));
    }
}