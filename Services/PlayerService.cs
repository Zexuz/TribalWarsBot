using System;
using System.Net;

using CsQuery.ExtensionMethods.Internal;

using Newtonsoft.Json;

using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services {

    public class PlayerService {


        public RootObject GetPlayerAndCurrentVillageInfo(RequestCache requestCache) {
            string url = $"{Constants.BaseUrl}screen=overview";
            var res = requestCache.Manager.SendGETRequest(url, null, null, true);
            var htmlString = RequestManager.GetResponseStringFromResponse(res);

            var indexStart = htmlString.IndexOf("TribalWars.updateGameData({", StringComparison.Ordinal);
            var indexEnd = htmlString.IndexOf("});", indexStart, StringComparison.Ordinal);

            var jsonText = htmlString.SubstringBetween(indexStart + "TribalWars.updateGameData(".Length, indexEnd + 1);
            var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonText);

            return rootObject;
        }

    }

}