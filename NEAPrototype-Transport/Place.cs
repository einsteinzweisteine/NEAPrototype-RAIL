using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEAPrototype_Transport
{
    public abstract class Place:SimObject
    {
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

        public void addConn(Place place)
        {
            connPlace.Add(place);
        }
    }

    public class Station:Place
    {
        private static int statIdGlobal = 0;
        private readonly int statId;

        private List<Station> statList = new List<Station> { };

        private int[] pos;
        private string name;
        private List<Track> trackConn;
        public string Name
        {
            get
            {
                return name;
            }
        }

        public Station(string name, int[] pos, List<Track> trackConn)
        {
            statId = statIdGlobal;
            statIdGlobal++;

            this.name = name;
            this.pos = pos;
            this.trackConn = trackConn;
        }

        protected override void Process()
        {

        }
    }

    public class Track:Place
    {
        private static int trckIdGlobal = 0;
        private readonly int trckId;

        private static List<Track> trckList = new List<Track> { };

        private Place lwrConn;
        private Place uppConn;


        public Track ()
        {
            trckId = trckIdGlobal;
            trckIdGlobal++;
            trckList.Add(this);
        }
        
        public void addlwrConn(Place lwrConn)
        {
            this.lwrConn = lwrConn;
        }

        protected override void Process()
        {
        }
    }
}
