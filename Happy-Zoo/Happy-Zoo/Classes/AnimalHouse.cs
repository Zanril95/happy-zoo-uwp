using System;
using System.Collections.Generic;
using System.Linq;

namespace Happy_Zoo
{
    /// <summary>
    /// The main class.
    /// </summary>
    public class Animalhouse
    {
        #region Fields
        private int maxAnimals;
        private int currentAnimals;
        private Enum animalType;
        private int level;
        private Queue<Animal> animals;
        int maxlvl1;
        int maxlvl2;
        float upgrlvl1 { get; set; }
        float upgrlvl2 { get; set; }
        int animalRep;
        float animalPrice;
        private float price;
        private int reputation;
        private String Name;
        #endregion

        public Animalhouse(Enum animalType, String name)
        {
            animals = new Queue<Animal>();
            this.animalType = animalType;
            this.Name = name;
            level = 1;
            if (animalType.Equals(AnimalType.Kangaroo) || animalType.Equals(AnimalType.Flamingo))
            {
                maxAnimals = 3;
            }
            else if (animalType.Equals(AnimalType.Panda) || animalType.Equals(AnimalType.Elephant))
            {
                maxAnimals = 1;
            }
            else
            {
                maxAnimals = 2;
            }
            setValues();
        }

        //returns the name
        public String getName()
        {
            return Name;
        }

        //returns the price
        public float getPrice()
        {
            return price;
        }

        //returns the price of the animal
        public float getAnimalPrice()
        {
            return animalPrice;
        }

        //set the price
        private void setPrice(float fee)
        {
            price = fee;
        }

        //returns the reputation
        public int getReputation()
        {
            int mutiplier = 100;
            if (level == 2)
            {
                mutiplier = 125;
            }
            if (level == 3)
            {
                mutiplier = 150;
            }
            int totalRep = (reputation * mutiplier) / 100;

            return totalRep;
        }

        //returns the enum of the Animaltype
        public Enum getAnimalType()
        {
            return animalType;
        }

        //returns the maximum amount of animals can be in this animalhouse
        public int getMaxAnimals()
        {
            return maxAnimals;
        }

        //sets the maximum amount of animals can be in this animalhouse
        public void setMaxAnimals(int maxAnimals)
        {
            this.maxAnimals = maxAnimals;
        }

        //returns the current amount of visitors of the animalhouse
        public int getCurrentAnimals()
        {
            return currentAnimals;
        }

        //returns the level
        public int getLevel()
        {
            return level;
        }
        
        //increases the level of the animalhouse
        public void increaseLevel()
        {
            if (level == 1)
            {
                maxAnimals += maxlvl1;
                level++;
            }
            else if (level == 2)
            {
                maxAnimals += maxlvl2;
                level++;
            }
            else
            {
                throw new Exception("Can't upgrade this building anymore");
            }
        }

        //adds animal in animalhouse of the same type as the animalhouse
        public void addAnimal()
        {
            animals.Enqueue(new Animal(animalType));
            currentAnimals++;
            reputation += animalRep;
        }

        //removes an animal
        public void removeAnimal()
        {
            animals.Dequeue();
            currentAnimals--;
            reputation -= animalRep;
        }

        //sets the upgradeprice to price
        public void setPriceUpgrade()
        {
            if (level == 1)
            {
                setPrice(upgrlvl1);

            }
            else if (level == 2)
            {
                setPrice(upgrlvl2);
            }
            else
            {

            }
        }

        //sets multiple values of the animalhouse depending on the type the animalhouse is
        private void setValues()
        {

            if (animalType.Equals(AnimalType.Kangaroo))
            {
                price = 1200;
                maxlvl1 = 1;
                maxlvl2 = 2;
                animalRep = 6;
                animalPrice = 150;
                upgrlvl1 = 1400;
                upgrlvl2 = 1700;

            }
            else if (animalType.Equals(AnimalType.Flamingo))
            {
                price = 1000;
                maxlvl1 = 1;
                maxlvl2 = 2;
                animalRep = 3;
                animalPrice = 75;
                upgrlvl1 = 750;
                upgrlvl2 = 750;
            }
            else if (animalType.Equals(AnimalType.Panda))
            {
                price = 3000;
                maxlvl1 = 1;
                maxlvl2 = 1;
                animalRep = 40;
                animalPrice = 1000;
                upgrlvl1 = 4500;
                upgrlvl2 = 6000;
            }
            else if (animalType.Equals(AnimalType.Elephant))
            {
                price = 1000;
                maxlvl1 = 1;
                maxlvl2 = 1;
                animalRep = 12;
                animalPrice = 300;
                upgrlvl1 = 2000;
                upgrlvl2 = 5000;
            }
            else if (animalType.Equals(AnimalType.Giraffe))
            {
                price = 1200;
                maxlvl1 = 1;
                maxlvl2 = 1;
                animalRep = 8;
                animalPrice = 200;
                upgrlvl1 = 1500;
                upgrlvl2 = 3000;
            }
            else if (animalType.Equals(AnimalType.Gorilla))
            {
                price = 1500;
                maxlvl1 = 1;
                maxlvl2 = 1;
                animalRep = 10;
                animalPrice = 250;
                upgrlvl1 = 1500;
                upgrlvl2 = 3500;
            }
            else if (animalType.Equals(AnimalType.Lion))
            {
                price = 1000;
                maxlvl1 = 1;
                maxlvl2 = 4;
                animalRep = 8;
                animalPrice = 200;
                upgrlvl1 = 2000;
                upgrlvl2 = 3000;
            }
            else
            {
                throw new Exception("Cant recognize the animal(s)");
            }
        }
    }
}
