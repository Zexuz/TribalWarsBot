using System;

using TribalWarsBot.Enums;
using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Screens.Structures {

    public abstract class Building:IBuilding {
        public Guid Id { get; }
        public int CurrentLevel { get; }


        protected Building(int currentLevel) {
            Id = Guid.NewGuid();
            CurrentLevel = currentLevel;
        }

        public new abstract BuildingTypes GetType();




    }

}