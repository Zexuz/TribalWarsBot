using System.Collections.Generic;

using TribalWarsBot.Cache;

namespace TribalWarsBot.Services {

    public class WorldMapPlayerService {

        private readonly WorldMapPlayerCache _mapPlayerCache;

        public WorldMapPlayerService(WorldMapPlayerCache mapPlayerCache) {
            _mapPlayerCache = mapPlayerCache;
        }


        public List<WordlMapPlayer> GetPlayers() {
            if (_mapPlayerCache.TimeToRenewCacheData()) {
                _mapPlayerCache.FetchAndSetWorldMapAsync();
            }

            return _mapPlayerCache.Data;
        }

    }

}