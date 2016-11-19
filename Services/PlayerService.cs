using System;
using System.Net;

using CsQuery.ExtensionMethods.Internal;

using Newtonsoft.Json;

using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services {

    public class PlayerService {

        private readonly RequestServiceCache _serviceCache;

        public PlayerService( RequestServiceCache serviceCache) {
            _serviceCache = serviceCache;
        }

        public RootObject GetPlayerAndCurrentVillageInfo() {
            string url = $"{Constants.BaseUrl}screen=overview";
            var req = _serviceCache.Manager.GenerateGETRequest(url, null, null, true);
            var res = _serviceCache.Manager.GetResponse(req);
            var htmlString = RequestManager.GetResponseStringFromResponse(res);

            var indexStart = htmlString.IndexOf("TribalWars.updateGameData({", StringComparison.Ordinal);
            var indexEnd = htmlString.IndexOf("});", indexStart, StringComparison.Ordinal);

            var jsonText = htmlString.SubstringBetween(indexStart + "TribalWars.updateGameData(".Length, indexEnd + 1);
            var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonText);

            return rootObject;
        }

    }

}