// <copyright file="StringExtensions.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.ExtensionMethods;

using System.Text;
using Ardalis.GuardClauses;

/// <summary>
/// Provides extension methods for the <see cref="string"/> type.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Wraps the string into multiple lines so that no line exceeds the specified
    /// maximum length. Line breaks are inserted only at word boundaries and
    /// never in the middle of a word.
    /// </summary>
    /// <param name="text">The input text to wrap.</param>
    /// <param name="maxLineLength">The maximum number of characters per line.</param>
    /// <returns>
    /// A new string with line breaks inserted where necessary.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="maxLineLength"/> is less than or equal to zero.
    /// </exception>
    public static string WrapLines(this string text, int maxLineLength)
    {
        Guard.Against.NullOrWhiteSpace(text);
        Guard.Against.NegativeOrZero(maxLineLength);

        string[] words = text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        StringBuilder result = new();
        int currentLineLength = 0;

        foreach (string word in words)
        {
            int additionalLength = word.Length + (currentLineLength > 0 ? 1 : 0);

            if (currentLineLength + additionalLength > maxLineLength)
            {
                result.Append(Environment.NewLine);
                result.Append(word);
                currentLineLength = word.Length;
            }
            else
            {
                if (currentLineLength > 0)
                {
                    result.Append(' ');
                    currentLineLength++;
                }

                result.Append(word);
                currentLineLength += word.Length;
            }
        }

        return result.ToString();
    }
}