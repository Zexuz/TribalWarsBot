using System;
using System.Net;

using CsQuery.ExtensionMethods.Internal;

using Newtonsoft.Json;

using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services {

    public class PlayerService {

        private readonly RequestManager _requestManager;

        public PlayerService( RequestManager requestManager) {
            _requestManager = requestManager;
        }

        public void DoLogin(string userName, string password) {
            if (!LoginToTw(userName,password) || !LoginToV36(userName)) {
                throw new Exception("Can't login to TribalWars");
            }
        }

        private bool LoginToTw(string userName, string password) {
            string postData = $"user={userName}&password={password}&cookie=true&clear=true";
            const string url = "https://www.tribalwars.se/index.php?action=login&show_server_selection=1";

            var loginReq = _requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var loginRes = _requestManager.GetResponse(loginReq);

            return loginRes.StatusCode == HttpStatusCode.OK;
        }

        public bool LoginToV36(string userName) {
            const string url = "https://www.tribalwars.se/index.php?action=login&server_sv36";
            var passwordCookie = _requestManager.GetCookieValue(new Uri("http://www.tribalwars.se/"), "password");
            var postData = $"user={userName}&password={passwordCookie}&sso=0";
            var loginReq = _requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var loginRes = _requestManager.GetResponse(loginReq);

            return loginRes.StatusCode == HttpStatusCode.OK;
        }

        public RootObject GetPlayerAndCurrentVillageInfo() {
            string url = $"{Constants.BaseUrl}screen=overview";
            var req = _requestManager.GenerateGETRequest(url, null, null, true);
            var res = _requestManager.GetResponse(req);
            var htmlString = RequestManager.GetResponseStringFromResponse(res);

            var indexStart = htmlString.IndexOf("TribalWars.updateGameData({", StringComparison.Ordinal);
            var indexEnd = htmlString.IndexOf("});", indexStart, StringComparison.Ordinal);

            var jsonText = htmlString.SubstringBetween(indexStart + "TribalWars.updateGameData(".Length, indexEnd + 1);
            var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonText);

            return rootObject;
        }

    }

}