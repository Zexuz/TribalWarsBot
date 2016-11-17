using System;

namespace TribalWarsBot.Screens.Structures {

    public interface IBuilding {
        Guid Id { get; }
        int CurrentLevel { get; }

        BuildingTypes GetType();

    }

}