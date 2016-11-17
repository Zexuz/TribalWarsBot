using System;

using TribalWarsBot.Enums;
using TribalWarsBot.Screens;

namespace TribalWarsBot.Interfacies {

    public interface IBuilding {
        Guid Id { get; }
        int CurrentLevel { get; }

        BuildingTypes GetType();

    }

}