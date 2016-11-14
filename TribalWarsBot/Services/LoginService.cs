﻿using System;
using System.Net;

namespace TribalWarsBot.Services
{
    public class LoginService
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly RequestManager _requestManager;

        public LoginService(string userName, string password, RequestManager requestManager)
        {
            _userName = userName;
            _password = password;
            _requestManager = requestManager;
        }

        public void DoLogin()
        {
            if (!LoginToTw() || !LoginToV36())
            {
                throw new Exception("Can't login to TribalWars");
            }
        }

        private bool LoginToTw()
        {
            string postData = $"user={_userName}&password={_password}&cookie=true&clear=true";
            const string url = "https://www.tribalwars.se/index.php?action=login&show_server_selection=1";

            var loginReq = _requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var loginRes = _requestManager.GetResponse(loginReq);

            return loginRes.StatusCode == HttpStatusCode.OK;
        }

        public bool LoginToV36()
        {
            const string url = "https://www.tribalwars.se/index.php?action=login&server_sv36";
            var passwordCookie = _requestManager.GetCookieValue(new Uri("http://www.tribalwars.se/"), "password");
            var postData = $"user={_userName}&password={passwordCookie}&sso=0";
            var loginReq = _requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var loginRes = _requestManager.GetResponse(loginReq);

            return loginRes.StatusCode == HttpStatusCode.OK;
        }
    }
}