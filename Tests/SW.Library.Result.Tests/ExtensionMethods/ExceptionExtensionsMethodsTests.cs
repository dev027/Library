// <copyright file="ExceptionExtensionsMethodsTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ExtensionMethods;

using System;
using SW.Library.Result.ExtensionMethods;

/// <summary>
/// Tests for <see cref="ExceptionExtensionsMethods.ExpandedMessage(Exception)"/>.
/// </summary>
[TestClass]
public class ExceptionExtensionsMethodsTests
{
    /// <summary>
    /// Test that when three levels of exceptionthen it returns the expected value.
    /// </summary>
    [TestMethod]
    public void ExpandedMessage_WhenThreeLevelsOfException_ReturnsExpectedValue()
    {
        // ARRANGE
        InvalidOperationException grandchildException = new("GRANDCHILD");
        InvalidOperationException childException = new("CHILD", grandchildException);
        InvalidOperationException parentException = new("PARENT", childException);

        // ACT
        string expandedMessage = parentException.ExpandedMessage();

        // ASSERT
        Assert.AreEqual("PARENT || CHILD || GRANDCHILD", expandedMessage);
    }
}
