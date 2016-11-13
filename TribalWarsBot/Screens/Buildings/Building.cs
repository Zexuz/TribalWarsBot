using System;

using TribalWarsBot.Screens;

namespace TribalWarsBot.Buildings {

    public abstract class Building:Screen {

        public abstract int CurrentLevel { get; }
        public abstract int MaxLevel { get; }

        public abstract int WoodNeededToUpgrade { get; }
        public abstract int StoneNeededToUpgrade { get; }
        public abstract int IronNeededToUpgrade { get; }

        public abstract TimeSpan TimeNeededToUpgrade { get; }
        public abstract int FarmersNeededToUpgrade { get; }

        public abstract string UpgradeToNextLevelLink { get; }

        public Guid Id { get; }

        protected Building() {
            Id = Guid.NewGuid();
        }


    }

}