using System;
using TribalWarsBot.Enums;

namespace TribalWarsBot.Helpers {

    public static class BuildingHelper {

        public static string GetNameForType(BuildingTypes building) {
            switch (building) {
                case BuildingTypes.Main:
                    return "main";
                case BuildingTypes.Barracks:
                    return "barracks";
                case BuildingTypes.Smith:
                    return "smith";
                case BuildingTypes.Place:
                    return "place";
                case BuildingTypes.Statue:
                    return "statue";
                case BuildingTypes.Market:
                    return "market";
                case BuildingTypes.Wood:
                    return "wood";
                case BuildingTypes.Clay:
                    return "stone";
                case BuildingTypes.Iron:
                    return "iron";
                case BuildingTypes.Farm:
                    return "farm";
                case BuildingTypes.Storage:
                    return "storage";
                case BuildingTypes.Hide:
                    return "hide";
                case BuildingTypes.Wall:
                    return "wall";
                case BuildingTypes.Stable:
                    return "stable";
                case BuildingTypes.Garage:
                    return "garage";
                case BuildingTypes.Watchtower:
                    return "watchtower";
                case BuildingTypes.Snob:
                    return "snob";
                default:
                    throw new ArgumentOutOfRangeException(nameof(building), building, null);
            }
        }

        public static BuildingTypes GetBuildingTypeFromString(string str)
        {
            if (str.Contains(GetNameForType(BuildingTypes.Barracks)))
                return BuildingTypes.Barracks;

            if (str.Contains(GetNameForType(BuildingTypes.Clay)))
                return BuildingTypes.Clay;

            if (str.Contains(GetNameForType(BuildingTypes.Farm)))
                return BuildingTypes.Farm;

            if (str.Contains(GetNameForType(BuildingTypes.Garage)))
                return BuildingTypes.Garage;

            if (str.Contains(GetNameForType(BuildingTypes.Hide)))
                return BuildingTypes.Hide;

            if (str.Contains(GetNameForType(BuildingTypes.Iron)))
                return BuildingTypes.Iron;

            if (str.Contains(GetNameForType(BuildingTypes.Main)))
                return BuildingTypes.Main;

            if (str.Contains(GetNameForType(BuildingTypes.Market)))
                return BuildingTypes.Market;

            if (str.Contains(GetNameForType(BuildingTypes.Place)))
                return BuildingTypes.Place;

            if (str.Contains(GetNameForType(BuildingTypes.Smith)))
                return BuildingTypes.Smith;

            if (str.Contains(GetNameForType(BuildingTypes.Snob)))
                return BuildingTypes.Snob;

            if (str.Contains(GetNameForType(BuildingTypes.Stable)))
                return BuildingTypes.Stable;

            if (str.Contains(GetNameForType(BuildingTypes.Storage)))
                return BuildingTypes.Storage;

            if (str.Contains(GetNameForType(BuildingTypes.Wall)))
                return BuildingTypes.Wall;

            if (str.Contains(GetNameForType(BuildingTypes.Watchtower)))
                return BuildingTypes.Watchtower;

            if (str.Contains(GetNameForType(BuildingTypes.Wood)))
                return BuildingTypes.Wood;

            throw new Exception("Building not found!");
        }

    }

}