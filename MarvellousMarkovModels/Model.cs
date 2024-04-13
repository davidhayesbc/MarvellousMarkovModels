using System;
using System.Collections.Generic;

namespace MarvellousMarkovModels;

public class Model
{
    private readonly int _order;
    private readonly Dictionary<string, KeyValuePair<string, float>[]> _productions;
    private readonly KeyValuePair<string, float>[] _startingStrings;

    public Model(int order, KeyValuePair<string, float>[] startingStrings,
        Dictionary<string, KeyValuePair<string, float>[]> productions)
    {
        _order = order;
        _startingStrings = startingStrings;
        _productions = productions;
    }

    public string Generate(Random random)
    {
        var builder = "";

        var lastSelected = WeightedRandom(random, _startingStrings);

        do
        {
            //Extend string
            builder += lastSelected;
            if (builder.Length < _order)
                break;

            //Key to use to find next production
            var key = builder.Substring(builder.Length - _order);

            //Find production rules for this key
            if (!_productions.TryGetValue(key, out var prod))
                break;

            //Produce next expansion
            lastSelected = WeightedRandom(random, prod);
        } while (lastSelected != string.Empty);

        return builder;
    }

    public static string WeightedRandom(Random random, KeyValuePair<string, float>[] items)
    {
        var num = random.NextDouble();

        foreach (var t in items)
        {
            num -= t.Value;
            if (num <= 0)
                return t.Key;
        }

        throw new InvalidOperationException();
    }
}