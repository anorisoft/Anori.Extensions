﻿// -----------------------------------------------------------------------
// <copyright file="TaskExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Extensions
{
    using System;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    /// <summary>
    ///     Task Extensions.
    /// </summary>
    public static class TaskExtensions
    {
#pragma warning disable S3168 // "async" methods should not return "void"

        /// <summary>
        ///     Fires the and forget safe asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="error">The error.</param>
        public static async void FireAndForgetSafeAsync(this Task task, Action<Exception>? error = null)
#pragma warning restore S3168 // "async" methods should not return "void"
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                error.Raise(ex);
            }
        }

#pragma warning disable S3168 // "async" methods should not return "void"

        /// <summary>
        ///     Fires the and forget safe asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="final">The final.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <exception cref="System.ArgumentNullException">task is null.</exception>
        public static async void FireAndForgetSafeAsync(
            [NotNull] this Task task,
            Action? completed = null,
            Action<Exception>? error = null,
            Action? final = null,
            Action? cancel = null,
            bool configureAwait = false)
#pragma warning restore S3168 // "async" methods should not return "void"
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                await task.ConfigureAwait(configureAwait);
                completed?.Invoke();
            }
            catch (TaskCanceledException)
            {
                cancel.Raise();
            }
            catch (Exception ex)
            {
                error.Raise(ex);
            }
            finally
            {
                final.Raise();
            }
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
#pragma warning disable S3168 // "async" methods should not return "void"

        /// <summary>
        /// Fires the and forget safe asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="task">The task.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="final">The final.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <exception cref="ArgumentNullException">task is null.</exception>
        public static async void FireAndForgetSafeAsync<T>(
            [NotNull] this Task<T> task,
            Action<T>? completed = null,
            Action<Exception>? error = null,
            Action? final = null,
            Action? cancel = null,
            bool configureAwait = false)
#pragma warning restore S3168 // "async" methods should not return "void"
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                var result = await task.ConfigureAwait(configureAwait);
                completed.Raise(result);
            }
            catch (TaskCanceledException)
            {
                cancel.Raise();
            }
            catch (Exception ex)
            {
                error.Raise(ex);
            }
            finally
            {
                final.Raise();
            }
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
#pragma warning disable S3168 // "async" methods should not return "void"

        /// <summary>
        ///     Fires the and forget safe asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="task">The task.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="final">The final.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <exception cref="System.ArgumentNullException">task is null.</exception>
        public static async void FireAndForgetSafeAsync<T>(
            [NotNull] this Task<T> task,
            Func<T, Task>? completed = null,
            Func<Exception, Task>? error = null,
            Func<Task>? final = null,
            Func<Task>? cancel = null,
            bool configureAwait = false)
#pragma warning restore S3168 // "async" methods should not return "void"
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                var result = await task.ConfigureAwait(configureAwait);
                await completed.RaiseAsync(result);
            }
            catch (TaskCanceledException)
            {
                await cancel.RaiseAsync();
            }
            catch (Exception ex)
            {
                await error.RaiseAsync(ex);
            }
            finally
            {
                await final.RaiseAsync();
            }
        }

        /// <summary>
        ///     Determines whether this instance is finished.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        ///     <c>true</c> if the specified task is finished; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">task is null.</exception>
        public static bool IsFinished([NotNull] this Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (task.IsCompleted)
            {
                return true;
            }

            if (task.IsCanceled)
            {
                return true;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (task.IsFaulted)
            {
                return true;
            }

            return false;
        }
    }
}