using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    /// <summary>
    /// Class represents all of the simulator objects. It handles the pseudo-networking and the stepping of the simulation (as well as events).
    /// </summary>
    public abstract class SimObject:Object
    {
        //Global List will hold all objects and their corresponding events
        private static Dictionary<SimObject,List<Event>> simObjects = new Dictionary<SimObject,List<Event>> { };
        private const int timeStep = 1; //In milliseconds
        protected static readonly Server ServerMain = new Server();

        public SimObject()
        {
            simObjects.Add(this, new List<Event> { });
        }

        /// <summary>
        /// Method handles the operation of a SimObject for each simulation step
        /// </summary>
        /// <param name="events">Contains all of the events concerning the object</param>
        protected abstract void Process(List<Event> events);

        #region Events
        public class Event
        {
            protected object[] data;
            public object[] Data
            {
                get { return data; }
            }

            protected void Attach(SimObject target)
            {
                simObjects[target].Add(this);
            }
        }

        /// <summary>
        /// NetworkEvent facilitates communication between trains, stations, and the server.
        /// </summary>
        public class NetworkEvent : Event
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="trsm">Object transmitting the data</param>
            /// <param name="recv">Object receiving the data</param>
            /// <param name="msg">NtwMsg representing the message type</param>
            /// <param name="payload">Data transmitted</param>
            public NetworkEvent(Object trsm, Object recv, NtwMsg msg, object[] payload = null)
            {
                data = new object[4] { trsm, recv, msg, payload };
            }
        }

        /// <summary>
        /// Enum covers all acronyms that will be used in network communications. Look at README.md for details
        /// </summary>
        public enum NtwMsg
        {
            STS,
            STD,
            STC,
            NPR,
            PDT,
            CLK,
            NPA,
            NPD,
            PSR,
            PRS
        }

        public class PssgrCreateEvent:Event
        {
            public PssgrCreateEvent(Station currentStation, Station targetStation)
            {
                data = new object[1] { new Passenger(currentStation,targetStation) };
                Attach(ServerMain);
            }
        }

        public class PssgrDeleteEvent : Event
        {
            public PssgrDeleteEvent(Passenger pssgr)
            {
                simObjects.Remove(pssgr);
            }
        }

        public class PssgrRemoveEvent : Event
        {
            public PssgrRemoveEvent(Passenger pssgr, Train train)
            {
                data = new object[2] { pssgr, train };
                Attach(train);
            }
        }

        public class PssgrAssignEvent : Event
        {
            public PssgrAssignEvent(Passenger pssgr, Train tr)
            {
                data = new object[] { tr };
                Attach(pssgr);
            }
        }
        #endregion
    }
}
