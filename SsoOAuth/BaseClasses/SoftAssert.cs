using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SsoOAuth.BaseClasses
{
    public class SoftAssert
    {
        private static List<string> _failures = new();

        public static void True(bool condition, string message = null)
        {
            if (!condition)
                _failures.Add(message ?? "Expected: True, but was: False.");
        }

        public static void False(bool condition, string message = null)
        {
            if (condition)
                _failures.Add(message ?? "Expected: False, but was: True.");
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!Equals(expected, actual))
                _failures.Add(message ?? $"Expected: {expected}, but was: {actual}.");
        }

        public static void AreNotEqual<T>(T notExpected, T actual, string message = null)
        {
            if (Equals(notExpected, actual))
                _failures.Add(message ?? $"Did not expect: {notExpected}, but was: {actual}.");
        }

        public static void NotNull(object obj, string message = null)
        {
            if (obj == null)
                _failures.Add(message ?? "Expected: not null, but was: null.");
        }

        public static void IsNull(object obj, string message = null)
        {
            if (obj != null)
                _failures.Add(message ?? "Expected: null, but was not null.");
        }
        
        public static void AreEntitiesEqual<T>(
            T expected,
            T actual,
            Func<T, T, bool> comparer,
            string message = null)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            if (!comparer(expected, actual))
                _failures.Add(message ??
                    $"Entities are not equal. Expected: {expected}, Actual: {actual}.");
        }

        public static void AssertAll()
        {
            if (!_failures.Any())
                return;

            var failureCount = _failures.Count;

            var aggregatedMessage = string.Join(
                Environment.NewLine + "-----------------" + Environment.NewLine,
                _failures);

            _failures.Clear();

            throw new AssertionException(
                $"SoftAssert found {failureCount} failure(s):{Environment.NewLine}{aggregatedMessage}");
        }

        public static void Reset()
        {
            _failures.Clear();
        }
    }
}