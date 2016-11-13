using System;

using TribalWarsBot.Buildings;

namespace TribalWarsBot.Screens.Buildings {

    public class Storage : Building {

        public override int CurrentLevel { get; }
        public override int MaxLevel { get; }
        public override int WoodNeededToUpgrade { get; }
        public override int StoneNeededToUpgrade { get; }
        public override int IronNeededToUpgrade { get; }
        public override TimeSpan TimeNeededToUpgrade { get; }
        public override int FarmersNeededToUpgrade { get; }
        public override string UpgradeToNextLevelLink { get; }


        public override Screens GetType() {
            return Screens.Storage;
        }

    }

}