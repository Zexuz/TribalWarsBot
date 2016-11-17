using System.Collections.Generic;

namespace TribalWarsBot.Helpers {

    public class Player
    {
        public string id { get; set; }
        public string name { get; set; }
        public string ally { get; set; }
        public string sitter { get; set; }
        public string sleep_start { get; set; }
        public string sitter_type { get; set; }
        public string sleep_end { get; set; }
        public string sleep_last { get; set; }
        public string interstitial { get; set; }
        public string email_valid { get; set; }
        public string villages { get; set; }
        public string incomings { get; set; }
        public int supports { get; set; }
        public string knight_location { get; set; }
        public string knight_unit { get; set; }
        public int rank { get; set; }
        public string points { get; set; }
        public string date_started { get; set; }
        public string is_guest { get; set; }
        public string birthdate { get; set; }
        public string quest_progress { get; set; }
        public bool premium { get; set; }
        public bool account_manager { get; set; }
        public bool farm_manager { get; set; }
        public string points_formatted { get; set; }
        public string rank_formatted { get; set; }
        public string pp { get; set; }
        public string new_ally_application { get; set; }
        public string new_ally_invite { get; set; }
        public string new_buddy_request { get; set; }
        public string new_daily_bonus { get; set; }
        public string new_forum_post { get; set; }
        public string new_igm { get; set; }
        public string new_items { get; set; }
        public string new_report { get; set; }
        public string fire_pixel { get; set; }
        public string new_quest { get; set; }
    }

    public class Bonus
    {
        public double wood { get; set; }
        public double stone { get; set; }
        public double iron { get; set; }
    }

    public class Buildings
    {
        public string village { get; set; }
        public string main { get; set; }
        public string farm { get; set; }
        public string storage { get; set; }
        public string place { get; set; }
        public string barracks { get; set; }
        public string church { get; set; }
        public string church_f { get; set; }
        public string smith { get; set; }
        public string wood { get; set; }
        public string stone { get; set; }
        public string iron { get; set; }
        public string market { get; set; }
        public string stable { get; set; }
        public string wall { get; set; }
        public string garage { get; set; }
        public string hide { get; set; }
        public string snob { get; set; }
        public string statue { get; set; }
        public string watchtower { get; set; }
    }

    public class Village
    {
        public int id { get; set; }
        public string name { get; set; }
        public double wood_prod { get; set; }
        public double stone_prod { get; set; }
        public double iron_prod { get; set; }
        public int storage_max { get; set; }
        public int pop_max { get; set; }
        public double wood_float { get; set; }
        public double stone_float { get; set; }
        public double iron_float { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int trader_away { get; set; }
        public object bonus_id { get; set; }
        public Bonus bonus { get; set; }
        public Buildings buildings { get; set; }
        public string player_id { get; set; }
        public int modifications { get; set; }
        public int points { get; set; }
        public long last_res_tick { get; set; }
        public List<double> res { get; set; }
        public string coord { get; set; }
        public bool is_farm_upgradable { get; set; }
    }

    public class Nav
    {
        public int parent { get; set; }
    }

    public class RootObject
    {
        public Player player { get; set; }
        public Village village { get; set; }
        public Nav nav { get; set; }
        public string link_base { get; set; }
        public string link_base_pure { get; set; }
        public string csrf { get; set; }
        public string world { get; set; }
        public string market { get; set; }
        public bool RTL { get; set; }
        public string version { get; set; }
        public string majorVersion { get; set; }
        public string screen { get; set; }
        public object mode { get; set; }
        public string device { get; set; }
        public bool pregame { get; set; }
        public long time_generated { get; set; }
    }

}