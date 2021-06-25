using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    public class Train
    {
        private static int idGlobal = 0;
        private readonly int id;

        public Place currentPlace;
        public int dist;
        private const int capacity = 6;
        private List<Passenger> passengersCurrent;
        private List<Passenger> passengersTarget;
        public int speed;
       
        public Train(Place currentPlace)
        {
            id = idGlobal;
            idGlobal++;
            this.currentPlace = currentPlace;
        }

        public void GetOff(Passenger passenger)
        {
            passengersCurrent.Remove(passenger);
        }

        public void GetOn(Passenger passenger)
        {
            passengersCurrent.Add(passenger);
            passengersTarget.Remove(passenger);
        }
    }
}
