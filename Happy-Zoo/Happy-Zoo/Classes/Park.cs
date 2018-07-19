using System;
using System.Collections.Generic;
using System.Collections;
using Windows.System.Threading;
using Windows.UI.Core;

namespace Happy_Zoo
{
    /// <summary>
    /// The main class.
    /// </summary>
    public class Park
    {
        private float entranceFee;
        private int totalReputation;
        private float coins;
        private int currentVisitors;
        private List<Animalhouse> animalhouses;
        private List<Facility> facilities;
        private ArrayList visitors;
        private Time time;
        private double restant;
        private bool paused = false;
        private MainGoal goal;
        private Animalhouse selectedAnimalHouse;
        ThreadPoolTimer PeriodicTimer;
        TimeSpan period = TimeSpan.FromSeconds(10);

        public Park()
        {
            entranceFee = 10;
            totalReputation = 0;
            coins = 10000;
            currentVisitors = 0;
            time = new Time();
            visitors = new ArrayList();
            animalhouses = new List<Animalhouse>();
            facilities = new List<Facility>();
            goal = new MainGoal();
            startThread();
        }

        //let thread calculate the reputation and the visitorscount
        private void startThread()
        {
            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    // Your UI update code goes here!

                    calculateReputation();
                    calculateVisitors();
                }
                );
            }, period);
        }
        /*
        //pauzes all theads
        public void pauseThreads()
        {
            time.pauzeStartTime();
            if (paused == false)
            {
                PeriodicTimer.Cancel();
                paused = true;
            }
            else
            {
                startThread();
                paused = false;
            }
        }
        */
        //returns entrancefee
        public float getEntranceFee()
        {
            return entranceFee;
        }

        //sets the entrancefee
        public void setEntranceFee(float fee)
        {
            entranceFee = fee;
        }

        //returns coins
        public float getCoins()
        {
            return coins;
        }

        //returns current visitorscount
        public int getVisitorCount()
        {
            return currentVisitors;
        }

        //returns the date
        public string getDate()
        {
            string date = time.getDay() + "/" + time.getMonth() + "/" + time.getYear();
            return date;
        }

        //adds a given amount of coins
        public void addCoins(float coin)
        {
            coins += coin;
        }

        //decreases a given amount of coins
        public bool decreaseCoins(float coin)
        {
            coins -= coin;
            bool succesfull = true;
            if (coins < 0)
            {
                coins += coin;
                succesfull = false;
                return succesfull;
            }
            return succesfull;
        }

        //returns reputation
        public int getReputation()
        {
            return totalReputation;
        }

        //create an animalhouse
        public Animalhouse createAnimalhouse(Enum animalType, String name)
        {
            Animalhouse temp = new Animalhouse(animalType, name);
            if (decreaseCoins(temp.getPrice()) == true)
            {
                return temp;
            }
            return null;
        }

        //creates a facility
        public Facility createFacility(Enum facilityType, String name)
        {
            Facility temp = new Facility(facilityType, name);
            if (decreaseCoins(temp.getPrice()) == true)
            {
                return temp;
            }
            return null;
        }

        //adds a created animalhouse to the park
        public void addAnimalHouse(Animalhouse building)
        {
            foreach (Animalhouse currentBuilding in animalhouses)
            {
                if (currentBuilding.getName() == building.getName())
                {
                    throw new Exception("Can't add this building. There is already a building with this name.");
                }
            }
            animalhouses.Add(building);
        }

        //adds a created facility to the park
        public void addFacilities(Facility building)
        {
            foreach (Facility currentBuilding in facilities)
            {
                if (currentBuilding.getName() == building.getName())
                {
                    throw new Exception("Can't add this building. There is already a building with this name.");
                }
            }
            facilities.Add(building);
        }

        //upgrades an animalhouse by inserted name
        public bool upgradeAnimalhouse(String name)
        {
            foreach (Animalhouse currentBuilding in animalhouses)
            {
                if (currentBuilding.getName() == name)
                {
                    currentBuilding.setPriceUpgrade();
                    if (currentBuilding.getLevel() < 3)
                    {
                        if (decreaseCoins(currentBuilding.getPrice()) == true)
                        {
                            currentBuilding.increaseLevel();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //adds an animal to an animalhouse by inserted name
        public void addAnimal(String name)
        {
            foreach (Animalhouse currentBuilding in animalhouses)
            {
                if (currentBuilding.getName() == name)
                {
                    if (currentBuilding.getCurrentAnimals() < currentBuilding.getMaxAnimals())
                    {
                        if (decreaseCoins(currentBuilding.getAnimalPrice()) == true)
                        {
                            currentBuilding.addAnimal();
                        }
                    }
                }
            }
        }

        //select an animalhouse by inserted name
        public Animalhouse selectAnimalHouse(string name)
        {
            foreach (Animalhouse currentBuilding in animalhouses)
            {
                if (currentBuilding.getName() == name)
                {
                    selectedAnimalHouse = currentBuilding;
                    return currentBuilding;
                }
            }
            return null;
        }

        //removes animalhouse from park
        public void removeAnimalHouse(String name)
        {
            foreach (Animalhouse currentBuilding in animalhouses)
            {
                if (currentBuilding.getName() == name)
                {
                    animalhouses.Remove(currentBuilding);
                }
            }
        }

        //removes facility from park
        public void removeFacility(String name)
        {
            foreach (Facility currentBuilding in facilities)
            {
                if (currentBuilding.getName() == name)
                {
                    facilities.Remove(currentBuilding);
                }
            }
        }

        //calculates the reputation by taking the sum of the reputation from every animalhouse and facility
        private void calculateReputation()
        {
            totalReputation = 0;
            foreach (Animalhouse currentBuilding in animalhouses)
            {
                totalReputation += currentBuilding.getReputation();
            }
            foreach (Facility currentBuilding in facilities)
            {
                totalReputation += currentBuilding.getReputation();
            }

        }

        //calculates the amount of visitors there should be and create them
        private void calculateVisitors()
        {
            // y == visitors per uur        x == entranceFee      z == totalReputation
            // y = 5000 - 3.6 * (x / 10 * 100) * (2 - z * 0.0016)
            double visitorsAHour = 5000 - 3.6 * (entranceFee / 10 * 100) * (2 - totalReputation * 0.16);
            double visitorsAMin = (visitorsAHour / 60) / 6;
            double restantTemp = visitorsAMin % 1;
            double visitorsAMinRound = visitorsAMin - restantTemp;
            restant += restantTemp;
            restantTemp = 0;
            restantTemp = restant % 1;
            restant = restant - restantTemp;
            visitorsAMinRound += restant;
            restant = restantTemp;

            Console.WriteLine("vitH = " + visitorsAHour + "    visM = " + visitorsAMin + "    visMR = " + visitorsAMinRound + "    res = " + restant + "    resT = " + restantTemp);


            for (int a = 0; a < visitorsAMinRound; a++)
            {
                coins += entranceFee;
                visitors.Add(new Visitor(9));

            }
            ArrayList vistorsToRemove = new ArrayList();
            foreach (Visitor visitor in visitors)
            {
                if (visitor.getBool() == true)
                {
                    vistorsToRemove.Add(visitors.IndexOf(visitor));
                }
            }


            Object[] vistorsToRemoveS = vistorsToRemove.ToArray();
            for (int a = 0; a < vistorsToRemove.Count; a++)
            {

                visitors.RemoveAt(Convert.ToInt32(vistorsToRemoveS[a]));
            }
            currentVisitors = visitors.Count;
        }
    }
}
