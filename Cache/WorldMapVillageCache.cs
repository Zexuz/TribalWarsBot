using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using TribalWarsBot.Services;

namespace TribalWarsBot.Cache {

    public class WorldMapVillageCache {

        public DateTime LastUpdated { get; private set; }

        private readonly TimeSpan _cacheTime;
        private const string Url = "https://sv36.tribalwars.se/map/village.txt";

        public List<WordlMapVillage> Data { get; private set; }

        public WorldMapVillageCache() {
            _cacheTime = new TimeSpan(1, 0, 0);
            FetchAndSetWorldMap();
        }

        public bool TimeToRenewCacheData() {
            return DateTime.Now - LastUpdated > _cacheTime;
        }


        public void FetchAndSetWorldMap() {
            LastUpdated = DateTime.Now;
            var res = FetchNewWorldMapTask();
            var lines = RequestManager.GetResponseStringFromResponse(res).Split('\n').ToList();
            lines.RemoveAt(lines.Count - 1);

            var list = new List<WordlMapVillage>();

            foreach (var line in lines) {
                var props = line.Split(',');
                var worldMapPlayer = new WordlMapVillage();
                worldMapPlayer.Id = int.Parse(props[0]);
                worldMapPlayer.Name = props[1];
                worldMapPlayer.X = int.Parse(props[2]);
                worldMapPlayer.Y = int.Parse(props[3]);
                worldMapPlayer.OwnerId = int.Parse(props[4]);
                worldMapPlayer.Points = int.Parse(props[5]);
                worldMapPlayer.CurrentRank = int.Parse(props[6]);
                list.Add(worldMapPlayer);
            }

            Data = list;
        }

        private static HttpWebResponse FetchNewWorldMapTask() {
            var requestManger = new RequestManager();
           return requestManger.SendGETRequest(Url, null, null, false);
        }


    }

    public class WordlMapVillage {

        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int OwnerId { get; set; }
        public int Points { get; set; }
        public int CurrentRank { get; set; }

    }

}