    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    namespace SsoOAuth.BaseClasses
    {
        public class SoftAssert
        {
            private static readonly List<AssertionResult> _assertionResults = new();

            private class AssertionResult
            {
                public string Message { get; set; }
                public object Details { get; set; }
                public string Expected { get; set; }
                public string Actual { get; set; }
            }
            
            private static void TryAssert(Action assertion, string message, object details = null)
            {
                try
                {
                    assertion();
                }
                catch (AssertionException ex)
                {
                    var lines = ex.Message.Split(new[]
                    {
                        Environment.NewLine
                    }, StringSplitOptions.RemoveEmptyEntries);

                    var expectedLine = lines.LastOrDefault(line => line.Contains("Expected:"));
                    var actualLine = lines.LastOrDefault(line => line.Contains("But was:"));

                    var expected = expectedLine?.Replace("Expected:", string.Empty)
                        .Replace("\"", string.Empty).Trim();
                    var actual = actualLine?.Replace("But was:", string.Empty)
                        .Replace("\"", string.Empty).Trim();

                    _assertionResults.Add(new AssertionResult
                    {
                        Message = message,
                        Details = details,
                        Expected = expected,
                        Actual = actual
                    });
                }
            }

            public static void True(bool condition, string message = null)
            {
                TryAssert(
                    () => Assert.That(condition, Is.True),
                    message ?? "Expected: True, but was: False."
                );
            }

            public static void False(bool condition, string message = null)
            {
                TryAssert(
                    () => Assert.That(condition, Is.False),
                    message ?? "Expected: False, but was: True."
                );
            }

            public static void AreEqual<T>(T expected, T actual, string message = null)
            {
                TryAssert(
                    () => Assert.That(actual, Is.EqualTo(expected)),
                    message ?? $"Expected: {expected}, but was: {actual}."
                );
            }

            public static void AreNotEqual<T>(T notExpected, T actual, string message = null)
            {
                TryAssert(
                    () => Assert.That(actual, Is.Not.EqualTo(notExpected)),
                    message ?? $"Did not expect: {notExpected}, but was: {actual}."
                );
            }

            public static void NotNull(object obj, string message = null)
            {
                TryAssert(
                    () => Assert.That(obj, Is.Not.Null),
                    message ?? "Expected: not null, but was: null."
                );
            }

            public static void IsNull(object obj, string message = null)
            {
                TryAssert(
                    () => Assert.That(obj, Is.Null),
                    message ?? "Expected: null, but was not null."
                );
            }

            public static void AreEntitiesEqual<T>(
                T expected,
                T actual,
                Func<T, T, bool> comparer,
                string message = null)
            {
                if (comparer == null)
                    throw new ArgumentNullException(nameof(comparer));

                TryAssert(
                    () => Assert.That(actual, Is.EqualTo(expected).Using<T>(comparer)), // Явно указываем <T>
                    message ?? $"Entities are not equal. Expected: {expected}, Actual: {actual}."
                );
            }

            public static void AssertAll()
            {
                if (!_assertionResults.Any())
                    return;

                var sb = new StringBuilder();
                sb.AppendLine($"SoftAssert found {_assertionResults.Count} failure(s):");
                sb.AppendLine();

                for (int i = 0; i < _assertionResults.Count; i++)
                {
                    var result = _assertionResults[i];
                    sb.AppendLine($"{i + 1}. {result.Message}");

                    if (!string.IsNullOrEmpty(result.Expected))
                        sb.AppendLine($"   Expected: {result.Expected}");

                    if (!string.IsNullOrEmpty(result.Actual))
                        sb.AppendLine($"   Actual: {result.Actual}");

                    if (result.Details != null)
                        sb.AppendLine($"   Details: {result.Details}");

                    sb.AppendLine();
                }

                _assertionResults.Clear();
                throw new AssertionException(sb.ToString());
            }
        }
    }  