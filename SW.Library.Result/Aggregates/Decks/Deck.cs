// <copyright file="Deck.cs" company="Steve Wright">
// Copyright (c) Steve Wright. All rights reserved.
// </copyright>

namespace SW.Library.Result.Aggregates.Decks;

using Ardalis.GuardClauses;
using Ardalis.Result;

/// <summary>
/// Represents a deck of items.
/// </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public class Deck<T>
    where T : class
{
    private readonly List<T> items;
    private int ptr;

    /// <summary>
    /// Initializes a new instance of the <see cref="Deck{T}"/> class.
    /// </summary>
    /// <param name="items">Items to prime the deck with.</param>
    public Deck(IEnumerable<T> items)
    {
        Guard.Against.Null(items);
        this.items = [.. items];

        this.ptr = this.items.Count - 1;
    }

    /// <summary>
    /// Deals an item.
    /// </summary>
    /// <returns>
    /// <list>
    /// <item><see cref="Result.Success()"/> if item available to deal with payload of the dealt item./>.</item>
    /// <item><see cref="Result.NotFound()"/> if no items left to deal.</item>
    /// </list>
    /// </returns>
    public Result<T> Deal()
    {
        if (this.ptr == -1)
        {
            return Result<T>.NotFound();
        }

        T item = this.items[this.ptr];
        this.ptr--;

        return item;
    }

    /// <summary>
    /// Shuffles the deck and resets the pointer.
    /// </summary>
    public void Shuffle()
    {
        Random random = new();
        int n = this.items.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (this.items[i], this.items[j]) = (this.items[j], this.items[i]);
        }

        this.ptr = this.items.Count - 1;
    }
}
