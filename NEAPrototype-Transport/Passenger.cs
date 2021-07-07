using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    /// <summary>
    /// Passenger class represents passengers - physical entities that are:
    /// <list type="number">
    /// <item>Spawned at a Station</item>
    /// <item>Assigned a target Station</item>
    /// <item>Assigned a Train</item>
    /// <item>Picked up by said Train</item>
    /// <item>Deposited at the target Station before being disposed</item>
    /// </list>
    /// </summary>
    public class Passenger:SimObject
    {
        public enum State
        {
            Travelling,
            Waiting,
            Idle
        }

        public State state;
        private static int globalPassId = 0;
        private readonly int passId = 0;
        Station currentStation;
        Station targetStation;
        Train trainAssigned;
        private bool onTrain;

        public Passenger(Station currentStation, Station targetStation)
        {
            this.currentStation = currentStation;
            this.targetStation = targetStation;
            state = State.Idle;
            onTrain = false;
            passId = globalPassId;
            globalPassId++;
            new PssgrCreateEvent(currentStation, targetStation);
        }

        private void GetOn()
        {
            if (1==1)
            {

            }
        }

        private void GetOff()
        {
            if (onTrain && trainAssigned.speed == 0 && trainAssigned.dist == 0 && trainAssigned.currentPlace == targetStation)
            {
                trainAssigned.GetOff(this);
                targetStation = null;
                trainAssigned = null;
                state = State.Idle;
                
            }
        }

        protected override void Process(List<Event> events)
        {
            foreach (Event e in events)
            {
                if (e.GetType().Name == "PssgrAssignEvent")
                {
                    trainAssigned = (Train)e.Data[0];
                }
            }
        }
    }
}
