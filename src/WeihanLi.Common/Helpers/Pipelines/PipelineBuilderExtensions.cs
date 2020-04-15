﻿using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace WeihanLi.Common.Helpers
{
    public static class PipelineBuilderExtensions
    {
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext, Action> action)

        {
            return builder.Use(next =>
                context =>
                {
                    action(context, () => next(context));
                });
        }

        public static IPipelineBuilder<TContext> Run<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext> handler)
        {
            return builder.Use(_ => handler);
        }

        public static IAsyncPipelineBuilder<TContext> Use<TContext>(this IAsyncPipelineBuilder<TContext> builder,
            Func<TContext, Func<Task>, Task> func)
        {
            return builder.Use(next =>
                context =>
                {
                    return func(context, () => next(context));
                });
        }

        public static IAsyncPipelineBuilder<TContext> Run<TContext>(this IAsyncPipelineBuilder<TContext> builder, Func<TContext, Task> handler)
        {
            return builder.Use(_ => handler);
        }
    }
}
