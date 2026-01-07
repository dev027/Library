// <copyright file="ExceptionExtensionsMethods.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.ExtensionMethods;

using System;
using System.Text;

/// <summary>
/// Extension methods for exceptions.
/// </summary>
public static class ExceptionExtensionsMethods
{
    /// <summary>
    /// Returns an expanded message that include the details from the inner exception on a single line.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <returns>An expended error message.</returns>
    public static string ExpandedMessage(this Exception ex)
    {
        StringBuilder sb = new();
        bool isInnerException = false;
        Exception? currentEx = ex;
        while (currentEx != null)
        {
            if (isInnerException)
            {
                sb.Append(" || ");
            }
            else
            {
                isInnerException = true;
            }

            sb.Append(currentEx.Message);
            currentEx = currentEx.InnerException;
        }

        return sb.ToString();
    }
}