using System;

namespace Happy_Zoo
{
    /// <summary>
    /// The main class.
    /// </summary>
    public class Animal
    {
        private Enum animalType;

        public Animal(Enum AnimalType) {
            animalType = AnimalType;
        }

        //returns the enum of the animaltype
        public Enum GetAnimalType(){
            return animalType;
        }
    }
}
