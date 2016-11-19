using System.Collections.Generic;
using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services
{
    public class UnitService
    {
        private readonly RequestManager _requestManager;
        //https://sv36.tribalwars.se/game.php?village=2173&screen=train&ajaxaction=train&mode=train&h=ce53156c&
        //post
        //Data = units%5Bspear%5D=29&units%5Bsword%5D=33

        public UnitService(RequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        public bool ReqruitTroops(Dictionary<Units, int> units, string token, int village)
        {
            var url =
                $"https://sv36.tribalwars.se/game.php?village={village}&screen=train&ajaxaction=train&mode=train&h={token}";
            var postData = "";
            foreach (var unitKeyPair in units)
            {
                var unitName = UnitHelper.GetNameForType(unitKeyPair.Key);
                postData += $"units%5B{unitName}%5D={unitKeyPair.Value}&";
            }
            var res =_requestManager.SendPOSTRequest(url, postData, null, null, true);
            var jsonRes = RequestManager.GetResponseStringFromResponse(res);

            return jsonRes.Contains("success:true");
        }
    }
}