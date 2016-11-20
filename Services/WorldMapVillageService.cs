using System;
using System.Collections.Generic;
using System.Linq;

using TribalWarsBot.Cache;

namespace TribalWarsBot.Services {

    public class WorldMapVillageService {

        private readonly WorldMapVillageCache _mapVillageCache;

        public WorldMapVillageService(WorldMapVillageCache mapVillageCache) {
            _mapVillageCache = mapVillageCache;
        }


        public List<WordlMapVillage> GetVillagesInRange(int range, int x, int y) {
            var allVillages = GetVillages();

            var villagesInRange = allVillages
                .Where(village => village.X - x <= range && village.Y - y <= range)
                .ToList();

            return villagesInRange;
        }

        public List<WordlMapVillage> GetBarbarianVillagesInRange(int range, int x, int y) {
            var allVillages = GetVillages();

            var villagesInRange = allVillages
                .Where(village => Math.Sqrt((village.X - x) * (village.X - x)) <= range
                                  && Math.Sqrt((village.Y - y) * (village.Y - y)) <= range
                                  && village.OwnerId == 0)
                .ToList();

            return villagesInRange;
        }


        public List<WordlMapVillage> GetVillages() {
            if (_mapVillageCache.TimeToRenewCacheData()) {
                _mapVillageCache.FetchAndSetWorldMap();
            }

            return _mapVillageCache.Data;
        }

    }

}