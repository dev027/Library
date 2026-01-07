// <copyright file="ConstructorTests.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Tests.ErrorTests;

/// <summary>
/// Tests for <see cref="Error.Error(string, string)"/> class.
/// </summary>
[TestClass]
public class ConstructorTests
{
    /// <summary>
    /// Test that when valid values are provided to the constructor, the properties are set correctly.
    /// </summary>
    [TestMethod]
    public void Error_Constructor_WhenValidValues_ValuesAreSet()
    {
        // ARRANGE
        string code = "SampleCode";
        string description = "SampleDescription";

        // ACT
        Error error = new(code, description);

        // ASSERT
        Assert.AreEqual(code, error.Code);
        Assert.AreEqual(description, error.Description);
    }
}