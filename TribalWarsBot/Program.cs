using System;
using System.IO;
using System.Net;
using TribalWarsBot.Services;

namespace TribalWarsBot
{
    internal class Program
    {


        public static void Main(string[] args)
        {
            new Program().Start();
        }

        private void Start()
        {
            var reqManager = new RequestManager();
            var loginService = new LoginService("newUser", "0000", reqManager);
            loginService.DoLogin();

        }


    }
}