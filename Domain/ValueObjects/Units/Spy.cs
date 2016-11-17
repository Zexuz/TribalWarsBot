using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Domain.ValueObjects.Units {

    public class Spy : IUnit {

        public int Wood {
            get { return 50; }
            set { }
        }

        public int Clay {
            get { return 50; }
            set { }
        }

        public int Iron {
            get { return 20; }
            set { }
        }

        public int FarmSpace {
            get { return 2; }
            set{}
        }

        public int Speed {
            get { return 8; }
            set{}
        }

    }

}