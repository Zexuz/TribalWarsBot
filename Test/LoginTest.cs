using System;
using System.Configuration;
using NUnit.Framework;
using TribalWarsBot.Services;

namespace Test
{
    [TestFixture]
    public class Tests
    {


        [Test]
        public void LoginGood()
        {
            var userName = ConfigurationManager.AppSettings["username"];
            var userPassword = ConfigurationManager.AppSettings["password"];
            var goodUser = new User(userName, userPassword);
            var requestCacheGood = new RequestCache(goodUser);
            Assert.DoesNotThrow(() => requestCacheGood.DoLogin());
        }

        [Test]
        public void LoginBad()
        {
            var invalidUser = new User("asd", "ASdasd");
            var requestCacheBad = new RequestCache(invalidUser);
            TestDelegate testdelegate = () => requestCacheBad.DoLogin();
            Assert.That(testdelegate,Throws.TypeOf<Exception>());
        }


    }
}