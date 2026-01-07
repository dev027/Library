// <copyright file="IsFailureTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods.ResultExtensionMethodsTests;

using Ardalis.Result;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ResultExtensionMethods.IsFailure(Result)"/> and
/// <see cref="ResultExtensionMethods.ToResult{TSource, TTarget}(Ardalis.Result.Result{TSource})"/>.
/// </summary>
[TestClass]
public class IsFailureTests
{
    /// <summary>
    /// Test that when status is error then it returns true.
    /// </summary>
    [TestMethod]
    public void IsFailure_WhenNonTypedResultAndStatusIsError_ThenReturnsTrue()
    {
        // ARRANGE
        Result result = Result.Error();

        // ACT
        bool isFailure = result.IsFailure();

        // ASSERT
        Assert.IsTrue(isFailure);
    }

    /// <summary>
    /// Test that when status is ok then it returns false.
    /// </summary>
    [TestMethod]
    public void IsFailure_WhenNonTypedResultAndStatusIsOk_ThenReturnsFalse()
    {
        // ARRANGE
        Result result = Result.Success();

        // ACT
        bool isFailure = result.IsFailure();

        // ASSERT
        Assert.IsFalse(isFailure);
    }

    /// <summary>
    /// Test that when status is error then it returns true.
    /// </summary>
    [TestMethod]
    public void IsFailure_WhenTypedResultAndStatusIsError_ThenReturnsTrue()
    {
        // ARRANGE
        Result<int> result = Result.Error();

        // ACT
        bool isFailure = result.IsFailure();

        // ASSERT
        Assert.IsTrue(isFailure);
    }

    /// <summary>
    /// Test that when status is ok then it returns false.
    /// </summary>
    [TestMethod]
    public void IsFailure_WhenTypedResultAndStatusIsOk_ThenReturnsFalse()
    {
        // ARRANGE
        Result<int> result = Result.Success(42);

        // ACT
        bool isFailure = result.IsFailure();

        // ASSERT
        Assert.IsFalse(isFailure);
    }
}