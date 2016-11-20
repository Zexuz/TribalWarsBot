using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Domain.ValueObjects
{
    public class BuildingQueueItem
    {
        public BuildingTypes Type { get; set; }
        public int Level { get; set; }
        public RegExHelper.Time TimeLeft { get; set; }
        public string Id { get; set; }


        public override string ToString()
        {
            return $"The builiding {Type} is upgrading to {Level}, Duration: {TimeLeft}, Id:{Id}";
        }
    }
}