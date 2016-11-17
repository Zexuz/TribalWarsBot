namespace TribalWarsBot.Services {

    public class AttackService {

        public bool SendAttack(RequestManager requestManager, PlanedAttack planedAttack) {}

    }

    public class PlanedAttack {

        public int X { get; set; }
        public int Y { get; set; }

    }

    public class UnitsGroup {

        public int Spear { get; set; }
        public int Sword { get; set; }
        public int Axe { get; set; }
        public int Spy { get; set; }
        public int Light { get; set; }
        public int Heavy { get; set; }
        public int Ram { get; set; }
        public int Catapult { get; set; }
        public int Knight { get; set; }
        public int Snob { get; set; }

    }

}