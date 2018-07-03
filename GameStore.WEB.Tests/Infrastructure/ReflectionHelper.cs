﻿using System;
using System.Linq.Expressions;

namespace GameStore.WEB.Tests.Infrastructure
{
    public class ReflectionHelper
    {
        public static string GetMethodName<T, U>(Expression<Func<T, U>> expression)
        {
            var method = expression.Body as MethodCallExpression;

            if (method != null)
                return method.Method.Name;

            throw new ArgumentException("Expression is wrong");
        }
    }
}
