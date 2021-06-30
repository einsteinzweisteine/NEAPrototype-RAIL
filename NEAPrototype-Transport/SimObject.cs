using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    public abstract class SimObject:Object
    {
        //Global List will hold all objects
        private static Dictionary<SimObject,List<Event>> simObjects = new Dictionary<SimObject,List<Event>> { };
        private const int timeStep = 1; //In milliseconds
        private static readonly Server ServerMain = new Server();

        public SimObject()
        {
            simObjects.Add(this, new List<Event> { });
        }

        protected abstract void Process();

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
        /// NetworkEvent facilitates communication between trains, stations, and the server
        /// </summary>
        public class NetworkEvent : Event
        {
            /// <param name="trainTrsm">Train object that is sending the message</param>
            /// <param name="trainRecv">Train object that is receiving the message</param>
            /// <param name="msg">NtwMsg object that represents the type of signal sent</param>
            /// <param name="payload">Any corresponding data sent</param>
            public NetworkEvent(Train trainTrsm, Train trainRecv, NtwMsg msg, object[] payload = null)
            {
                // data will contain the transmitting train, the receiving train, and the message respectively
                this.data = new object[4] { trainTrsm, trainRecv, msg, payload };
                Attach(trainRecv);
            }

            /// <param name="trainTrsm">Train object that is sending the message</param>
            /// <param name="serverMain">Server object that is receiving the message</param>
            /// <param name="msg">NtwMsg object that represents the type of signal sent</param>
            /// <param name="payload">Any corresponding data sent</param>
            public NetworkEvent(Train trainTrsm, Server serverMain, NtwMsg msg, object[] payload = null)
            {
                // data will contain the transmitting train, the receiving server, and the message respectively
                this.data = new object[4] { trainTrsm, serverMain, msg, payload };
                Attach(serverMain);
            }

            /// <param name="serverMain">Server object that is sending the message</param>
            /// <param name="trainRecv">Train object that is receiving the message</param>
            /// <param name="msg">NtwMsg object that represents the type of signal sent</param>
            /// <param name="payload">Any corresponding data sent</param>
            public NetworkEvent(Server serverMain, Train trainRecv, NtwMsg msg, object[] payload = null)
            {
                // data will contain the transmitting server, the receiving train, and the message respectively
                this.data = new object[4] { serverMain, trainRecv, msg, payload };
                Attach(trainRecv);
            }

            /// <param name="statTrsm">Station object that is sending the message</param>
            /// <param name="trainRecv">Train object that is receiving the message</param>
            /// <param name="msg">NtwMsg object that represents the type of signal sent</param>
            /// <param name="payload">Any corresponding data sent</param>
            public NetworkEvent(Station statTrsm, Train trainRecv, NtwMsg msg, object[] payload = null)
            {
                // data will contain the transmitting station, the receiving train, and the message respectively
                this.data = new object[4] { statTrsm, trainRecv, msg, payload };
                Attach(trainRecv);
            }
        }

        public enum NtwMsg
        {

        }

        public class PssgrCreateEvent:Event
        {
            public PssgrCreateEvent(Station currentStation, Station targetStation)
            {
                this.data = new object[1] { new Passenger(currentStation,targetStation) };
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
        #endregion
    }
}
