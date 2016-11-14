using System;

using TribalWarsBot.Screens;

namespace TribalWarsBot {

    public class GetBuldingNameFromEnumType {

        public static string Get(Buildings building) {
            switch (building) {
                case Buildings.Main:
                    return "main";
                case Buildings.Barracks:
                    return "barracks";
                case Buildings.Smith:
                    return "smith";
                case Buildings.Place:
                    return "place";
                case Buildings.Statue:
                    return "statue";
                case Buildings.Market:
                    return "market";
                case Buildings.Wood:
                    return "wood";
                case Buildings.Stone:
                    return "stone";
                case Buildings.Iron:
                    return "iron";
                case Buildings.Farm:
                    return "farm";
                case Buildings.Storage:
                    return "storage";
                case Buildings.Hide:
                    return "hide";
                case Buildings.Wall:
                    return "wall";
                case Buildings.Stable:
                    return "stable";
                case Buildings.Garage:
                    return "garage";
                case Buildings.Watchtower:
                    return "watchtower";
                case Buildings.Snob:
                    return "snob";
                default:
                    throw new ArgumentOutOfRangeException(nameof(building), building, null);
            }
        }

    }

}