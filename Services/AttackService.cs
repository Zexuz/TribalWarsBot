using System.Linq;

using CsQuery;

using TribalWarsBot.Domain;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services {

    public class AttackService {

        private readonly RequestManager _requestManager;

        public AttackService(RequestManager requestManager) {
            _requestManager = requestManager;
        }

        public Attack SendAttack(RootObject rootObject, PlanedAttack planedAttack) {
            var postPayload = SendAttackInit(planedAttack, rootObject.village.id);
            SendAttackConfirm(postPayload, rootObject.csrf);

            return new Attack(planedAttack);
        }


        private string SendAttackInit(PlanedAttack planedAttack, int villageNr) {
            var url = $"https://sv36.tribalwars.se/game.php?village={villageNr}&screen=place&try=confirm";
            var postData =
                "22c2d931d74cd8a06d92b4=e6c57ef722c2d9" +
                "&template_id=" +
                $"&source_village={villageNr}" +
                $"&{planedAttack.Units}" +
                $"&x={planedAttack.EnemyVillageXCord}" +
                $"&y={planedAttack.EnemyVillageYCord}" +
                "&target_type=coord" +
                "&input=&attack=Attack";
            var req = _requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = _requestManager.GetResponse(req);

            //post data 22c2d931d74cd8a06d92b4=e6c57ef722c2d9&template_id=&source_village=2173&spear=&sword=63&axe=&spy=&light=&heavy=&ram=&catapult=&knight=&snob=&x=525&y=473&target_type=coord&input=&attack=Attack
            // Response html is in attacjResponseHtml
            var str = RequestManager.GetResponseStringFromResponse(res);
            CQ html = str;
            var formElement = html.Select("#command-data-form");
            var inputs = formElement.Children("input").Where(ele => ele.Type == "hidden").ToList();

            var queryString = inputs.Aggregate("", (current, input) => current + $"{input.Name}={input.Value}&");
            return queryString;
        }


        private void SendAttackConfirm(string postData, string token) {
            var url = $"https://sv36.tribalwars.se/game.php?village=2173&screen=place&action=command&h={token}";
            var req = _requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = _requestManager.GetResponse(req);
            var htmlResponse = RequestManager.GetResponseStringFromResponse(res);
        }

    }

}