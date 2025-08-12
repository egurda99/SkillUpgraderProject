﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace MyCodeBase.Utils
{
    public static class ParseUtils
    {
        public static Dictionary<string, float> StringToStringFloatDictionary(
            string str,
            char separator = ',',
            char valueSeparator = ':')
        {
            var cards = StringToStringList(str, separator);
            var requiredCards = new Dictionary<string, float>();
            for (var index = 0; index < cards.Count; index++)
            {
                var card = cards[index];
                var s = card.Split(valueSeparator);
                if (s.Length == 2)
                {
                    if (float.TryParse(s[1], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat,
                            out var value))
                    {
                        if (!requiredCards.ContainsKey(s[0]))
                        {
                            requiredCards.Add(s[0], value);
                        }
                        else
                        {
                            Debug.LogError($"duplicate key {s[0]}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"wrong key {s[0]} or value {s[1]} in {card}");
                    }
                }
                else
                {
                    Debug.LogError($"{card} don't contains :");
                }
            }

            return requiredCards;
        }

        public static string ConvertDictionaryStringFloatToString(
            Dictionary<string, float> records,
            char separator = ',',
            char valueSeparator = ':') // master:-30,ui:-60
        {
            var stringBuilder = new StringBuilder();
            foreach (var (key, value) in records)
            {
                stringBuilder.Append($"{key}{valueSeparator}{value}");
                stringBuilder.Append(separator);
            }

            var result = stringBuilder.ToString();
            result = result.Remove(result.Length - 1);
            return result;
        }

        public static List<string> StringToStringList(
            string str,
            char separator = ',',
            StringSplitOptions splitOptions = StringSplitOptions.None)
        {
            str = str.Replace("\n", string.Empty).Replace("\r", string.Empty);
            str = str.Replace(separator + " ", separator.ToString());
            str = str.Replace(" " + separator, separator.ToString());
            if (str == "") return new List<string>();
            var elements = new List<string>(str.Split(separator, splitOptions));
            return elements;
        }
    }
}
