﻿using IkemenToolbox.Models;
using System;

namespace IkemenToolbox.Extensions
{
    public static class StringExtensions
    {
        public static bool IsSection(this string value) => value.StartsWith("[");

        public static bool TryGetSection(this string value, out Section section)
        {
            section = null;

            if (!value.IsSection())
            {
                return false;
            }

            value = value[1..^1];   //Remove brackets

            var name = string.Empty;
            var nameSplit = value.Split(',', 2);
            if (nameSplit.Length == 2)
            {
                name = nameSplit[1].Trim();
            }

            int? id = null;
            var idSplit = nameSplit[0].Split(' ', 2);
            if (!Enum.TryParse(typeof(SectionType), idSplit[0], true, out var type))
            {
                return false;
            }
            if (idSplit.Length == 2 && int.TryParse(idSplit[1], out int result))
            {
                id = result;
            }

            section = new Section
            {
                Id = id,
                Name = name,
                Type = (SectionType)type,
            };

            return true;
        }

        public static bool TryGetKeyValue(this string line, out string key, out string value)
        {
            key = null;
            value = null;

            var split = line.Split('=', 2);
            if (split.Length != 2)
            {
                return false;
            }

            key = split[0].Trim();
            value = split[1].Trim();

            return true;
        }
    }
}