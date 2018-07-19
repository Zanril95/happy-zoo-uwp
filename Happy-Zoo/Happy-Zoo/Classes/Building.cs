using System;

namespace Happy_Zoo
{
    /// <summary>
    /// CLASS IS NOT USED
    /// </summary>
    public abstract class Building
    {
        private float price;
        private int reputation;
        private String Name;

        public Building(String name){
            this.Name = name;
        }

        public String getName()
        {
            return Name;
        }

        public float getPrice(){
            return price;
        }

        private  void setPrice(float fee){
            price = fee;
        }

        public virtual int getReputation()
        {
            return reputation;
        }

        protected void setReputation(int rep)
        {
            reputation = rep;
        }

    }
}
