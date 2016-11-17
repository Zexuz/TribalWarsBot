using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Domain.ValueObjects.Units {

    public class Light : IUnit {

        public int Wood {
            get { return 125; }
            set { }
        }

        public int Clay {
            get { return 100; }
            set { }
        }

        public int Iron {
            get { return 250; }
            set { }
        }

        public int FarmSpace {
            get { return 4; }
            set{}
        }

        public int Speed {
            get { return 9; }
            set{}
        }

    }

}