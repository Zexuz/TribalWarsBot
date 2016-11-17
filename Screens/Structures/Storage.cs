using System;

using TribalWarsBot.Helpers;
using TribalWarsBot.Services;

namespace TribalWarsBot.Screens.Structures {

    public class Storage : Building {


        public Storage(int currentLevel) : base(currentLevel) {
        }

        public TimeSpan TimeLeftUntillWoodFull (RootObject rootObject) {
            var village = rootObject.village;
            return GetTimeLeft(village.wood,village.wood_prod,village.storage_max);
        }

        public TimeSpan TimeLeftUntillClayFull (RootObject rootObject) {
            var village = rootObject.village;
            return GetTimeLeft(village.stone,village.stone_prod,village.storage_max);
        }
        public TimeSpan TimeLeftUntillIronFull (RootObject rootObject) {
            var village = rootObject.village;
            return GetTimeLeft(village.iron,village.iron_prod,village.storage_max);
        }

        public override BuildingTypes GetType() {
            return BuildingTypes.Storage;
        }


        private TimeSpan GetTimeLeft(double inStorage, double prodPerHour, int storageCap) {
            var delta = storageCap - inStorage;
            var resPerHour = prodPerHour * Constants.ProdValue;

            var hoursUntillFull = delta / resPerHour;
            return TimeSpan.FromHours(hoursUntillFull);
        }

    }

}