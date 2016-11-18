using TribalWarsBot.Enums;

namespace TribalWarsBot.Domain.ValueObjects
{
    public class BuildingQueueItem
    {
        public BuildingTypes Type { get; set; }
        public int Level { get; set; }
    }
}