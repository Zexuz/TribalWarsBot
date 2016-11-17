using TribalWarsBot.Interfacies;

namespace TribalWarsBot.Domain.ValueObjects.Units {

    public class Heavy : IUnit {

        public int Wood {
            get { return 200; }
            set { }
        }

        public int Clay {
            get { return 150; }
            set { }
        }

        public int Iron {
            get { return 600; }
            set { }
        }

        public int FarmSpace {
            get { return 6; }
            set{}
        }

        public int Speed {
            get { return 10; }
            set{}
        }

    }

}