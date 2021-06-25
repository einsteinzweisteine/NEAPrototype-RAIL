using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    public class Passenger
    {
        public enum State
        {
            Travelling,
            Waiting,
            Idle
        }

        public State state;
        private static int passId = 0;
        Station currentStation;
        Station targetStation;
        Train trainAssigned;
        private bool onTrain;

        public Passenger(Station currentStation)
        {
            this.targetStation = currentStation;
            this.state = State.Idle;
            this.onTrain = false;
        }

        public void BookJourney(Station targetStation, Train trainAssigned)
        {
            this.targetStation = targetStation;
            this.trainAssigned = trainAssigned;
        }

        private void GetOn()
        {
            if ()
        }

        private void GetOff()
        {
            if (onTrain && trainAssigned.speed == 0 && trainAssigned.dist == 0 && trainAssigned.currentPlace == targetStation)
            {
                this.trainAssigned.GetOff(this);
                this.targetStation = null;
                this.trainAssigned = null;
            }
        }
    }
}
