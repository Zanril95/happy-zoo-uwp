using System;
using System.Collections.Generic;

namespace Happy_Zoo
{
    /// <summary>
    /// The main class.
    /// </summary>
    public class Facility 
    {
        private Enum facilityType;
        private float price;
        private int reputation;
        private String Name;


        public Facility(Enum FacilityType, String name){
            this.facilityType = FacilityType;
            this.Name = name;
            setValues();
        }

        //returns the enum facilitytype
        public Enum getFacilityType()
        {
            return facilityType;
        }

        //returns the name of the facility
        public String getName()
        {
            return Name;
        }

        //returns the price
        public float getPrice()
        {
            return price;
        }

        //sets the price
        private void setPrice(float fee)
        {
            price = fee;
        }

        //returns the reputation
        public virtual int getReputation()
        {
            return reputation;
        }

        //sets the price and reputation of the facility depending on the type the facility is
        private void setValues()
        {

            if (facilityType.Equals(FacilityType.ToiletBuilding))
            {
                price = 750;
                reputation = 8;
            }
            else if (facilityType.Equals(FacilityType.InformationBuilding))
            {
                price = 2000;
                reputation = 20;
            }
            else if (facilityType.Equals(FacilityType.EatAndDrinkBooth))
            {
                price = 1500;
                reputation = 15;
            }   
            else
            {
                throw new Exception("Cant recognize the facility");
            }
        }

    }
}
