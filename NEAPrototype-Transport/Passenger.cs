using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
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
            this.state = State.Idle;
            this.onTrain = false;
            passId = globalPassId;
            globalPassId++;
        }

        public void BookJourney(Train trainAssigned)
        {
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

        protected override void Process()
        {
        }
    }
}
