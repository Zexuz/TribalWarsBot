using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Domain.ValueObjects.Units {

    public class Catapult : IUnit {

        public int Wood {
            get { return 320; }
            set { }
        }

        public int Clay {
            get { return 400; }
            set { }
        }

        public int Iron {
            get { return 100; }
            set { }
        }

        public int FarmSpace {
            get { return 8; }
            set{}
        }

        public int Speed {
            get { return 28; }
            set{}
        }

    }

}