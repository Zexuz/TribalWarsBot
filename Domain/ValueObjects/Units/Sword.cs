using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Domain.ValueObjects.Units {

    public class Sword : IUnit {

        public int Wood {
            get { return 30; }
            set { }
        }

        public int Clay {
            get { return 30; }
            set { }
        }

        public int Iron {
            get { return 70; }
            set { }
        }

        public int FarmSpace {
            get { return 1; }
            set{}
        }

        public int Speed {
            get { return 20; }
            set{}
        }

    }

}