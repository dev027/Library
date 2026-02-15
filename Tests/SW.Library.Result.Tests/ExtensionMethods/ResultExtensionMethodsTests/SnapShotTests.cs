// <copyright file="SnapShotTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.SnapShot(Result)"/>.
/// </summary>
[TestClass]
public class SnapShotTests
{
    /// <summary>
    /// Test that when non typed result and status is ok then it returns expected value.
    /// </summary>
    [TestMethod]
    public void SnapShot_WhenNonTypedResultAndStatusIsOk_ReturnsExpectedValue()
    {
        // ARRANGE
        Result result = Result.Success();

        // ACT
        string snapShot = result.SnapShot();

        // ASSERT
        Assert.Contains("SUCCESS: ", snapShot);
    }

    /// <summary>
    /// Test that when typed result and status is ok then it returns expected value.
    /// </summary>
    [TestMethod]
    public void SnapShot_WhenTypedResultAndStatusIsOk_ReturnsExpectedValue()
    {
        // ARRANGE
        Result<int> result = Result.Success(42, "Success Message");

        // ACT
        string snapShot = result.SnapShot();

        // ASSERT
        Assert.Contains("SUCCESS: Success Message", snapShot);
    }

    /// <summary>
    /// Test that when non typed result and status is error then it returns expected value.
    /// </summary>
    [TestMethod]
    public void SnapShot_WhenNonTypedResultAndStatusIsError_ReturnsExpectedValue()
    {
        // ARRANGE
        Result result = Result.Error("IDENTIFIER.ERRORCODE¬ERROR MESSAGE");

        // ACT
        string snapShot = result.SnapShot();

        // ASSERT
        Assert.Contains("Error: IDENTIFIER.ERRORCODE¬ERROR MESSAGE", snapShot);
    }

    /// <summary>
    /// Test that when typed result and status is error then it returns expected value.
    /// </summary>
    [TestMethod]
    public void SnapShot_WhenypedResultAndStatusIsError_ReturnsExpectedValue()
    {
        // ARRANGE
        Result<int> result = Result.Error("IDENTIFIER.ERRORCODE¬ERROR MESSAGE");

        // ACT
        string snapShot = result.SnapShot();

        // ASSERT
        Assert.Contains("Error: IDENTIFIER.ERRORCODE¬ERROR MESSAGE", snapShot);
    }
}
