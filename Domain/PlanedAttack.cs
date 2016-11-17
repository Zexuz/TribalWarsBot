using System.Collections.Generic;

using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Domain {

    public class PlanedAttack {

        public Village Attacker { get; set; }
        public int EnemyVillageXCord { get; set; }
        public int EnemyVillageYCord { get; set; }
        public Dictionary<Units,int> Units { get; set; }

    }

}