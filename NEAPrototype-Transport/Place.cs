using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    /// <summary>
    /// Class represents the physical location of places (tracks, stations, etc.)
    /// </summary>
    public abstract class Place:SimObject
    {
        //id is used to uniquely identify each Place object
        private static int idGlobal = 0;
        private readonly int id;
        private List<Place> connPlace;

        protected int Id
        {
            get
            {
                return id;
            }
        }

        public Place()
        {
            id = idGlobal;
            idGlobal++;
            connPlace = new List<Place> { };
        }

        /// <summary>
        /// Adds another connection the list of connections. In this prototype, there will only be one connecting place as we are dealing with a circle
        /// </summary>
        /// <param name="place">The Place object that should be connected to this Place</param>
        public void addConn(Place place)
        {
            connPlace.Add(place);
        }
    }

    /// <summary>
    /// Station inherits from Place; it represents locations where passengers are created, picked up and then dropped off by Trains
    /// </summary>
    public class Station:Place
    {
        //statId is used to uniquely identify each Station object
        private static int statIdGlobal = 0;
        private readonly int statId;
        public const int trsmRange = 500;

        private  static List<Station> statList = new List<Station> { };

        //dist is the distance (in metres) from this station to the next station
        private int dist;
        //nickname of the station
        private string name;
        private List<Track> trackConn;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Station(int dist, List<Track> trackConn = null, string name = null)
        {
            statId = statIdGlobal;
            statIdGlobal++;

            statList.Add(this);

            this.name = name;
            this.dist = dist;
            this.trackConn = trackConn;
        }

        /// <summary>
        /// Station transmits its presence to all Train objects nearby
        /// </summary>
        /// <param name="events"></param>
        protected override void Process(List<Event> events)
        {
            foreach (Train tr in Train.TrList)
            {
                if (tr.isCloseToStation(this))
                {
                    new NetworkEvent(this, tr, NtwMsg.PRS, new object[] { this });
                }
            }
        }
    }

    /// <summary>
    /// Track objects represent stretches that Trains can travel down.
    /// </summary>
    public class Track:Place
    {
        private static int trckIdGlobal = 0;
        private readonly int trckId;

        private static List<Track> trckList = new List<Track> { };

        // private Place lwrConn;

        // For the prototype, we will only allow tracks to connect to exclusively two other Station objects
        private Place uppConn;


        public Track ()
        {
            trckId = trckIdGlobal;
            trckIdGlobal++;
            trckList.Add(this);
        }
        
        public void AdduppConn(Place uppConn)
        {
            this.uppConn = uppConn;
        }

        protected override void Process(List<Event> events)
        {
            //No process required (we can treat this object as a constant)
        }
    }
}
