using System;
using System.Net;

namespace TribalWarsBot.Services
{
    public class RequestServiceCache
    {
        private readonly User _user;
        public RequestManager Manager { get; }
        
        
        public RequestServiceCache(User user)
        {
            _user = user;
            Manager = new RequestManager();
        }

        public void DoLogin() {
            if (!LoginToTw() || !LoginToV36()) {
                throw new Exception("Can't login to TribalWars");
            }
        }

        private bool LoginToTw() {
            string postData = $"user={_user.Name}&password={_user.Password}&cookie=true&clear=true";
            const string url = "https://www.tribalwars.se/index.php?action=login&show_server_selection=1";

            var loginReq = Manager.GeneratePOSTRequest(url, postData, null, null, true);
            var loginRes = Manager.GetResponse(loginReq);

            return loginRes.StatusCode == HttpStatusCode.OK;
        }

        public bool LoginToV36() {
            const string url = "https://www.tribalwars.se/index.php?action=login&server_sv36";
            var passwordCookie = Manager.GetCookieValue(new Uri("http://www.tribalwars.se/"), "password");
            var postData = $"user={_user.Name}&password={passwordCookie}&sso=0";
            var loginReq = Manager.GeneratePOSTRequest(url, postData, null, null, true);
            var loginRes = Manager.GetResponse(loginReq);

            return loginRes.StatusCode == HttpStatusCode.OK;
        }
    }
}