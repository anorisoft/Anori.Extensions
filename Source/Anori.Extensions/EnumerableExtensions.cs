// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Extensions
{
    using JetBrains.Annotations;

    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Enumerable Extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Values at or null.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <returns>
        ///     The value or null.
        /// </returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static TSource? ValueAtOrNull<TSource>([NotNull] this IEnumerable<TSource> source, int index)
            where TSource : struct
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ElementAtOrNull(index);
        }

        /// <summary>
        ///     References at or null.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value or null.</returns>
        /// <exception cref="ArgumentNullException">source is null.</exception>
        public static TSource ReferenceAtOrNull<TSource>([NotNull] this IEnumerable<TSource> source, int index)
            where TSource : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ElementAtOrNull(index);
        }
    }
}