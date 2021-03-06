﻿using System.Linq;
using CsQuery;
using TribalWarsBot.Domain;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services
{
    public class AttackService
    {
        public Attack SendAttack(RequestManager requestManager, RootObject rootObject, PlanedAttack planedAttack)
        {
            var postPayload = SendAttackInit(requestManager, planedAttack, rootObject.village.id);
            SendAttackConfirm(requestManager, postPayload, rootObject.csrf, rootObject.village.id);

            return new Attack(planedAttack);
        }


        private string SendAttackInit(RequestManager requestManager, PlanedAttack planedAttack, int villageNr)
        {
            var url = $"https://sv36.tribalwars.se/game.php?village={villageNr}&screen=place&try=confirm";
            var postData =
                "22c2d931d74cd8a06d92b4=e6c57ef722c2d9" +
                "&template_id=" +
                $"&source_village={villageNr}" +
                $"&{UnitHelper.GetUnitQueryStringFromUnitDict(planedAttack.Units)}" +
                $"&x={planedAttack.EnemyVillageXCord}" +
                $"&y={planedAttack.EnemyVillageYCord}" +
                "&target_type=coord" +
                "&input=&attack=Attack";
            var req = requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = requestManager.GetResponse(req);

            var str = RequestManager.GetResponseStringFromResponse(res);
            CQ html = str;
            var formElement = html.Select("#command-data-form");
            var inputs = formElement.Children("input").Where(ele => ele.Type == "hidden").ToList();

            var queryString = inputs.Aggregate("", (current, input) => current + $"{input.Name}={input.Value}&");
            return queryString;
        }


        private void SendAttackConfirm(RequestManager requestManager, string postData, string token, int village)
        {
            var url = $"https://sv36.tribalwars.se/game.php?village={village}&screen=place&action=command&h={token}";
            var res = requestManager.SendPOSTRequest(url, postData, null, null, true);
            var htmlResponse = RequestManager.GetResponseStringFromResponse(res);
        }
    }
}