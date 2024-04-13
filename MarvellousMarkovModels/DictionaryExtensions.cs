using System;
using System.Collections.Generic;

namespace MarvellousMarkovModels;

internal static class DictionaryExtensions
{
    public static void AddOrUpdate<K, V>(this Dictionary<K, V> dict, K key, V value, Func<V, V> update)
    {
        if (!dict.TryGetValue(key, out var existing))
            dict.Add(key, value);
        else
            dict[key] = update(existing);
    }
}