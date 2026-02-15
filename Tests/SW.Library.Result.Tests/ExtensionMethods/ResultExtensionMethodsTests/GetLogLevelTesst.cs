// <copyright file="GetLogLevelTesst.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using System;
using System.Reflection;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.GetLogLevel(Ardalis.Result.Result)"/>.
/// </summary>
[TestClass]
public class GetLogLevelTesst
{
    /// <summary>
    /// Test that when status is ok then it returns log level of information.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsOk_ReturnsLogLevelInformation()
    {
        // ARRANGE
        Result result = Result.Success();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Information, logLevel);
    }

    /// <summary>
    /// Test that when status is invalid then it returns log level of warning.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsInvalid_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.Invalid();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Warning, logLevel);
    }

    /// <summary>
    /// Test that when status is unauthorized then it returns log level of warning.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsUnauthorized_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.Unauthorized();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Warning, logLevel);
    }

    /// <summary>
    /// Test that when status is forbidden then it returns log level of warning.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsForbidden_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.Forbidden();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Warning, logLevel);
    }

    /// <summary>
    /// Test that when status is not found then it returns log level of warning.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsNotFound_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.NotFound();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Warning, logLevel);
    }

    /// <summary>
    /// Test that when status is conflict then it returns log level of warning.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsConflict_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.Conflict();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Warning, logLevel);
    }

    /// <summary>
    /// Test that when status is error then it returns log level of warning.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsError_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.Error();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Warning, logLevel);
    }

    /// <summary>
    /// Test that when status is critical error then it returns log level of error.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsCriticalError_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.CriticalError();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Error, logLevel);
    }

    /// <summary>
    /// Test that when status is unavailable then it returns log level of error.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsUnavilable_ReturnsLogLevelWarning()
    {
        // ARRANGE
        Result result = Result.Unavailable();

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreEqual(LogLevel.Error, logLevel);
    }

    /// <summary>
    /// Test that when status is unexpected then it throws invalid operation exception.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenStatusIsUnexpected_ThrowsInvalidOperationException()
    {
        // ARRANGE
        Result result = Result.Success();

        // 1. Get the PropertyInfo object for "Status"
        PropertyInfo prop = typeof(Result).GetProperty("Status", BindingFlags.Public | BindingFlags.Instance)!;

        // 2. Use SetValue to bypass the 'protected' restriction
        // We cast the integer to the Enum type before setting it
        prop.SetValue(result, (ResultStatus)99);

        // ACT & ASSERT
        Assert.ThrowsExactly<InvalidOperationException>(() => result.GetLogLevel());
    }

    /// <summary>
    /// Test that when using a typed result then it result an expected value.
    /// </summary>
    [TestMethod]
    public void GetLogLevel_WhenUsingTypedResult_ReturnsExpectedValue()
    {
        // ARRANGE
        Result<int> result = Result<int>.Success(42);

        // ACT
        LogLevel logLevel = result.GetLogLevel();

        // ASSERT
        Assert.AreNotEqual(LogLevel.None, logLevel);
    }
}