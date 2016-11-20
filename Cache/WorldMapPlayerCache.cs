using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;

using TribalWarsBot.Services;

namespace TribalWarsBot.Cache {

    public class WorldMapPlayerCache {

        public DateTime LastUpdated { get; private set; }

        private readonly TimeSpan _cacheTime;
        private const string Url = "https://sv36.tribalwars.se/map/player.txt";

        public List<WordlMapPlayer> Data { get; private set; }

        public WorldMapPlayerCache() {
            _cacheTime = new TimeSpan(1, 0, 0);
            FetchAndSetWorldMapAsync();
        }

        public bool TimeToRenewCacheData() {
            return DateTime.Now - LastUpdated > _cacheTime;
        }


        public async void FetchAndSetWorldMapAsync() {
            LastUpdated = DateTime.Now;
            var res = await FetchNewWorldMapTask();
            var lines = RequestManager.GetResponseStringFromResponse(res).Split('\n');

            var list = new List<WordlMapPlayer>();

            foreach (var line in lines) {
                var props = line.Split(',');
                var worldMapPlayer = new WordlMapPlayer {
                    Id = int.Parse(props[0]),
                    Name = props[1],
                    AllyId = int.Parse(props[2]),
                    NrOfVillages = int.Parse(props[3]),
                    Points = int.Parse(props[4]),
                    CurrentRank = int.Parse(props[5])
                };
                list.Add(worldMapPlayer);
            }

            Data = list;
        }

        private static Task<HttpWebResponse> FetchNewWorldMapTask() {
            var requestManger = new RequestManager();
            return Task.Run(() => requestManger.SendGETRequest(Url, null, null, false));
        }

    }

    public class WordlMapPlayer {

        public int Id { get; set; }
        public string Name { get; set; }
        public int AllyId { get; set; }
        public int NrOfVillages { get; set; }
        public int Points { get; set; }
        public int CurrentRank { get; set; }

    }


}