using System;

namespace TribalWarsBot.Domain.ValueObjects {

    public class BuildingLevel {

        public int Value { get; }

        public BuildingLevel(int value) {
            if (!IsValid())
                throw new Exception("Invalid building level");

            Value = value;
        }

        private bool IsValid() {
            return Value > 0 && Value <= 30;
        }

    }

}