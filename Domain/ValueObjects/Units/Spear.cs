using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Domain.ValueObjects.Units {

    public class Spear : IUnit {

        public int Wood {
            get { return 50; }
            set { }
        }

        public int Clay {
            get { return 30; }
            set { }
        }

        public int Iron {
            get { return 30; }
            set { }
        }
        
        public int FarmSpace {
            get { return 1; }
            set{}
        }

        public int Speed {
            get { return 17; }
            set{}
        }

    }

}