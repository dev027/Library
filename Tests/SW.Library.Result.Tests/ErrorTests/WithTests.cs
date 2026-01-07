// <copyright file="WithTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ErrorTests;

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Test for <see cref="Error"/> with operator.
/// </summary>
[TestClass]
public class WithTests
{
    /// <summary>
    /// Test that Error using the with operator returns the expected result.
    /// </summary>
    [TestMethod]
    public void Error_With_WhenCalled_ReturnsExpectedResult()
    {
        // ARRANGE
        string errorDescription = "Test error description.";
        string errorCode = "TestIdentifier.TestErrorCode";
        Error original = new(errorCode, errorDescription);

        // ACT
        Error clone = original with { };

        // ASSERT
        Assert.AreEqual(errorDescription, clone.Description);
        Assert.AreEqual(errorCode, clone.Code);
    }
}
