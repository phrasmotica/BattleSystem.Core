using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Extensions
{
    /// <summary>
    /// Extensions methods for dictionary types.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Subtracts the corresponding integer values of the two dictionaries and returns the
        /// result as a single dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <param name="dict1">The dictionary to subtract from.</param>
        /// <param name="dict2">The dictionary to subtract with.</param>
        /// <param name="includeZeroes">
        /// Whether to include values equal to zero in the resulting dictionary.
        /// </param>
        /// <param name="includeUnpairedKeys">
        /// Whether to include values for keys that were only present in one of the input
        /// dictionaries in the resulting dictionary.
        /// </param>
        public static IDictionary<TKey, int> Subtract<TKey>(
            this IDictionary<TKey, int> dict1,
            IDictionary<TKey, int> dict2,
            bool includeZeroes = false,
            bool includeUnpairedKeys = false)
        {
            var resultDict = new Dictionary<TKey, int>();

            var allKeys = dict1.Keys.Union(dict2.Keys);

            foreach (var key in allKeys)
            {
                var inDict1 = dict1.TryGetValue(key, out var value1);
                var inDict2 = dict2.TryGetValue(key, out var value2);

                int? result = null;

                if (inDict1 && inDict2)
                {
                    result = value1 - value2;
                }
                else if (inDict1 && includeUnpairedKeys)
                {
                    result = value1;
                }
                else if (inDict2 && includeUnpairedKeys)
                {
                    result = value2;
                }

                if (result.HasValue && (result != 0 || includeZeroes))
                {
                    resultDict[key] = result.Value;
                }
            }

            return resultDict;
        }

        /// <summary>
        /// Subtracts the corresponding numeric values of the two dictionaries and returns the
        /// result as a single dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <param name="dict1">The dictionary to subtract from.</param>
        /// <param name="dict2">The dictionary to subtract with.</param>
        /// <param name="includeZeroes">
        /// Whether to include values equal to zero in the resulting dictionary.
        /// </param>
        /// <param name="includeUnpairedKeys">
        /// Whether to include values for keys that were only present in one of the input
        /// dictionaries in the resulting dictionary.
        /// </param>
        public static IDictionary<TKey, double> Subtract<TKey>(
            this IDictionary<TKey, double> dict1,
            IDictionary<TKey, double> dict2,
            bool includeZeroes = false,
            bool includeUnpairedKeys = false)
        {
            var resultDict = new Dictionary<TKey, double>();

            var allKeys = dict1.Keys.Union(dict2.Keys);

            foreach (var key in allKeys)
            {
                var inDict1 = dict1.TryGetValue(key, out var value1);
                var inDict2 = dict2.TryGetValue(key, out var value2);

                double? result = null;

                if (inDict1 && inDict2)
                {
                    result = value1 - value2;
                }
                else if (inDict1 && includeUnpairedKeys)
                {
                    result = value1;
                }
                else if (inDict2 && includeUnpairedKeys)
                {
                    result = value2;
                }

                if (result.HasValue && (result != 0 || includeZeroes))
                {
                    resultDict[key] = result.Value;
                }
            }

            return resultDict;
        }
    }
}
