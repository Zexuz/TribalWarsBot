namespace TribalWarsBot.Helpers {

    public class Constants {

        public static string BaseUrl => "https://sv36.tribalwars.se/game.php?";
        public static string UpgradeBuildingUrl => "village=__village__&screen=main&ajaxaction=upgrade_building&type=__type__&h=__csrfToken__";
        public static string CancelOrderUrl => "village=__village__&screen=main&ajaxaction=cancel_order&type=__type__&h=__csrfToken__";


    }

}