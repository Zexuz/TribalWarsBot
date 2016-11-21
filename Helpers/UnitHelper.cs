using System;
using System.Collections.Generic;
using TribalWarsBot.Enums;

namespace TribalWarsBot.Helpers
{
    public class UnitHelper
    {
        public static string GetUnitQueryStringFromUnitDict(Dictionary<Units, int> units)
        {
            var str = "";

            foreach (var keyValuePair in units)
            {
                str += $"{GetNameForType(keyValuePair.Key)}={keyValuePair.Value}&";
            }

            return str;
        }

        public static string GetNameForType(Units unit)
        {
            switch (unit)
            {
                case Units.Spear:
                    return "spear";
                case Units.Sword:
                    return "sword";
                case Units.Axe:
                    return "axe";
                case Units.Spy:
                    return "spy";
                case Units.Light:
                    return "light";
                case Units.Heavy:
                    return "heavy";
                case Units.Ram:
                    return "ram";
                case Units.Catapult:
                    return "catapult";
                case Units.Knight:
                    return "knigth";
                case Units.Snob:
                    return "snob";
                case Units.Militia:
                    return "militia";
                default:
                    throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
            }
        }

        public static Units GetTypeForString(string unit)
        {
            unit = unit.ToLower();
            if (unit.Equals("spear")) return Units.Spear;
            if (unit.Equals("sword")) return Units.Sword;
            if (unit.Equals("axe")) return Units.Axe;
            if (unit.Equals("spy")) return Units.Spy;
            if (unit.Equals("light")) return Units.Light;
            if (unit.Equals("heavy")) return Units.Heavy;
            if (unit.Equals("ram")) return Units.Ram;
            if (unit.Equals("catapult")) return Units.Catapult;
            if (unit.Equals("knight")) return Units.Knight;
            if (unit.Equals("snob")) return Units.Snob;
            if (unit.Equals("militia")) return Units.Militia;

            throw new Exception("Invalid unit");
        }
    }
}