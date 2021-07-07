using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    public class Train : SimObject
    {
        private static int idGlobal = 0;
        private readonly int id;
        private static List<Train> trList = new List<Train> { };

        private const int capacity = 6;

        private List<Passenger> passengersCurrent;
        private List<Passenger> passengersTarget;

        public Place currentPlace;
        public int dist;
        public int speed;

        public static List<Train> TrList
        {
            get
            {
                return trList;
            }
        }
        public Train(Place currentPlace)
        {
            id = idGlobal;
            idGlobal++;
            this.currentPlace = currentPlace;
            trList.Add(this);
        }

        public void GetOff(Passenger passenger)
        {
            passengersCurrent.Remove(passenger);
            passengersTarget.Remove(passenger);
        }

        public void GetOn(Passenger passenger)
        {
            passengersCurrent.Add(passenger);
        }

        public bool isCloseToStation(Station stat)
        {
            if ((currentPlace.GetType().Name == "Station")&&(currentPlace == stat))
            {
                return true;

            }
            else if ((currentPlace.GetType().Name == "Station")&&(this.dist <= Station.trsmRange))
            {
                return true;
            }
            return false;
        }
        protected override void Process(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.GetType().Name)
                {
                    case "NetworkEvent":
                        switch (e.Data[2])
                        {
                            case NtwMsg.NPR:
                                NtwMsg msg;
                                if (passengersTarget.Count < capacity)
                                {
                                    msg = NtwMsg.NPA;
                                }
                                else
                                {
                                    msg = NtwMsg.NPD;
                                }
                                NetworkEvent ntwEvnt = new NetworkEvent(this, SimObject.ServerMain,msg,new object[] { });
                                break;
                            case NtwMsg.PRS:

                                break;
                        }
                        break;
                    case "PssgrRemoveEvent":
                        passengersCurrent.Remove((Passenger)e.Data[0]);
                        passengersTarget.Remove((Passenger)e.Data[0]);
                        break;
                }
            }
        }
    }
}
