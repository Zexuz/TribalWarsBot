using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Domain.ValueObjects.Units {

    public class Ram : IUnit {

        public int Wood {
            get { return 300; }
            set { }
        }

        public int Clay {
            get { return 200; }
            set { }
        }

        public int Iron {
            get { return 200; }
            set { }
        }

        public int FarmSpace {
            get { return 5; }
            set{}
        }

        public int Speed {
            get { return 28; }
            set{}
        }

    }

}