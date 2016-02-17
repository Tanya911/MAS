using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    /*
     * Expert is an istance that knows information about an enviroment and helps to another agents (both of Virus and Inhabitant) 
     * to get some information about parameters and global settings
     * */
    class Expert
    {
        /// <summary>
        /// 2D area  (a cellular automata space)
        /// </summary>
        public Inhabitant[,] ca, ca1;
        /// <summary>
        /// A list of viruses
        /// </summary>
        public List<Virus>[] viruses;
        /// <summary>
        /// Random number generator - Mersenne Twister
        /// </summary>
        public MersenneTwister mt;
        /// <summary>
        /// Statistic collector
        /// </summary>
        public Statistic stats;
        /// <summary>
        /// IDs of inhabitant (used to track agents)
        /// </summary>
        int id_inh;
        /// <summary>
        /// IDs of viruses
        /// </summary>
        int id_vir; //to provide for an unique number for new-generated instances
        /// <summary>
        /// A list of deleted viruses
        /// </summary>
        Stack<Virus>[] deletedViruses;
        /// <summary>
        /// The max rate of new viruses appearing on each iteration
        /// </summary>
        float virusesRate;
        /// <summary>
        /// The rate of new inhabitant
        /// </summary>
        float inhabitantRate;
        /// <summary>
        /// The number of inhabitants
        /// </summary>
        int nmbInhabitants = 0;
        /// <summary>
        /// The number of iteration
        /// </summary>
        int time;
        /// <summary>
        /// Simulation time
        /// </summary>
        int simulationDuration = 0;
        /// <summary>
        /// Cognitive / simple simulation (moving of inhabitants)
        /// </summary>
        bool cognitive;
        /// <summary>
        /// The universal recovery rate
        /// </summary>
        float recoveryRate;
        /// <summary>
        /// Lifetime of viruses
        /// </summary>
        int virusesLifetime = 0;
        /// <summary>
        /// The universal infection rate
        /// </summary>
        float infectionRate;
        bool changable_viruses_nmb;
        public int VirusesLifetime
        {
            get { return virusesLifetime; }
        }
        public float RecoveryRate
        {
            get { return recoveryRate; }
        }
        
        public float InfectionRate
        {
            get { return infectionRate; }
        }

        public bool Cognitive
        {
            get { return cognitive; }
        }
        
        public int SimulationTime
        {
            get { return simulationDuration; }
        }

        float[,] virsusDensity;
        public float GetVirusesDensity(int x, int y)
        {
            return virsusDensity[x, y];
        }

        public void IncreaseVirsusDensity(int x, int y, float val)
        {
            virsusDensity[x, y] += val;
        }


        float[,] inhabitantDensity;
        public float GetInhabitantDensity(int x, int y)
        {
            return inhabitantDensity[x, y];
        }

        public void ChangeInhabitantDensity(int x, int y, float val)
        {
            inhabitantDensity[x, y] += val;
        }

        /// <summary>
        /// Width of the area
        /// </summary>
        public int Width
        {
            get { return ca.GetLength(1); }
        }
        /// <summary>
        /// Height of the area
        /// </summary>
        public int Height
        {
            get { return ca.GetLength(0); }
        }

        public Expert(ref Inhabitant[,] ca, ref Inhabitant[,] ca1, ref  List<Virus>[] viruces, float virusesRate, float inhabitantRate, 
            int simulationDuration, bool cognitive, float infectionRate, float recoveryRate, int lifetime, bool const_viruses_nmb)
        {
            mt = new MersenneTwister();
            deletedViruses = new Stack<Virus>[viruces.GetLength(0)];
            this.ca = ca;
            this.ca1 = ca1;
            this.simulationDuration = simulationDuration;
            virusesLifetime = lifetime;
            this.cognitive = cognitive;
            this.recoveryRate = recoveryRate;
            this.infectionRate = infectionRate;
            this.inhabitantRate = inhabitantRate;
            this.virusesRate = virusesRate;
            virsusDensity = new float[Height, Width];
            inhabitantDensity = new float[Height, Width];
            viruses = viruces;
            id_inh = 0;
            id_vir = 0;
            int nmbViruses = (int)(Height * Width * virusesRate);
            int nmbInhabitants = (int)(Height * Width * inhabitantRate);
            CreateVirusesPopulation(nmbViruses);
            CreateInhabitantPopulation(nmbInhabitants);
            time = 0;        
            stats = new Statistic(simulationDuration, nmbInhabitants);
            changable_viruses_nmb = !const_viruses_nmb;
         }

        /**
         * Creates the viruses population of the given size
         * \param[in] virucesNmb the size of population
         * */
        void CreateVirusesPopulation(int virucesNmb)
        {
            if (virucesNmb == 0)
                virucesNmb = 1;
            for (int i = 0; i < virucesNmb; i++)
            {
                Virus nv = new Virus(this, id_vir++);
                if (viruses[nv.X] == null)
                    viruses[nv.X] = new List<Virus>();
                viruses[nv.X].Add(nv);
            }
        }

        /**
         * Appends a random number of new viruses and adds them to the list of existing viruses
         * \param[out] rndNmb the number of new viruses
         */
        public int AppendToVirusesPopulation()
        {
            int nmbNewViruses = (int)(virusesRate * Height * Width);
            if (nmbNewViruses == 0)
                nmbNewViruses = 1;
            /*int minNumber = (int) (Math.Min((1 - virusesRate*virusesLifetime - inhabitantRate), virusesRate) / virusesLifetime * Height * Width);
            if (minNumber < 0)
                minNumber = 0;*/
            int rndNmb = mt.genrand_IntInInterval(0, nmbNewViruses);

            int cnt = 0;
            for (int i = 0; i < viruses.GetLength(0); i++)
                if (viruses[i] !=null)
                    foreach (Virus v in viruses[i])
                        cnt++;
            if (cnt + rndNmb + nmbInhabitants < this.Height*this.Width*0.6)
            {
                for (int i = 0; i < rndNmb; i++)
                {
                    Virus nv = new Virus(this, id_vir++);
                    if (viruses[nv.X] == null)
                        viruses[nv.X] = new List<Virus>();
                    viruses[nv.X].Add(nv);
                }
                return rndNmb;
            }
            return 0;
        }


        /**
         * Creates the inhabitants population of the given size
         * \param[in] inhabitantNmb the size of population
         * */
        public void CreateInhabitantPopulation(int inhabitantNmb)
        {
            if (inhabitantNmb == 0)
                inhabitantNmb = 1;
            for (int i = 0; i < inhabitantNmb; i++)
            {
                Inhabitant inh = new Inhabitant(this, id_inh++);
                this.ca[inh.X, inh.Y] = inh;
            }
            this.nmbInhabitants = inhabitantNmb;
        }

        /**
         * Gets the heighborhood defined by the center and the radius with a torus like folding
         * \param[in] x, y coordinates of a center
         * \param[in] radius the radius of a heighborhood
         * \param[out] coords an array of extreme points of a square area
         * */
        public int[] GetBorders(int x, int y, int radius)
        {
            int[] coords = new int[4];
            coords[0] = x - radius;
            coords[1] = x + radius;
            coords[2] = y - radius;
            coords[3] = y + radius;
            if (coords[2] < 0)
                coords[2] += Width;
            if (coords[3] >= Width)
                coords[3] -= Width;
            if (coords[0] < 0)
                coords[0] += Height;
            if (coords[1] >= Height)
                coords[1] -= Height;
            return coords;
        }

        /**
         * Checks whether the cell is occupied
         * \param[in] x,y coordinates of the cell
         * \param[in] caCur the area of a cellular automata
         * \param[out] bool a value whether the cell is occupied (true - the cell is occupied, false - otherwise)
         * */
        public bool DetectVirusCollision(int x, int y, ref Inhabitant [,] caCur)
        {
            if (caCur[x, y] != null)
                return true;
            else
                return OccupiedByVirus(x, y);
        }

        /**
         * Checks whether a virus lives in the cell
         * \param[in] x,y coordinates of the cell
         * \param[out] bool value whether collision is founded (true - the cell is occupied by a virus, false - otherwise)
         * */
        public bool OccupiedByVirus(int x, int y)
        {
            if (viruses[x] != null)
                foreach (Virus v in viruses[x])
                    if (v.Y == y)
                        return true;
            return false;
        }

        /**
         * Checks whether an inhabitant lives in the cell
         * \param[in] x,y coordinates of the cell
         * \param[out] bool value whether collision is founded (true - the cell is occupied by another inhabitant, false - otherwise)
         * */
        public bool DetectInhabitantCollision(int x, int y)
        {
            return (ca[x, y] != null);
        }

        /**
          * Returns a random position the the area (cellular automata)
          * \param[out] coords coordinates of a new position
          * */
        public int[] GetSomeCoordinates()
        {
            int[] coords = new int[2];
            coords[0] = mt.genrand_IntInInterval(0, Height - 1);
            coords[1] = mt.genrand_IntInInterval(0, Width - 1);
            return coords;
        }

        /**
          * Returns a random position inside the heighborhood defined by the center and the radius
          * \param[in] x,y coordinates a center
          * \param[in] radius radius of a heighborhood
          * \param[out] coords an array of a new position coordinates 
          * */
        public int[] GetNewCoordinates(int x, int y, int radius)
        {
            int[] coords = new int[2];
            coords[0] = mt.genrand_IntInInterval(-radius, radius) + x;
            coords[1] = mt.genrand_IntInInterval(-radius, radius) + y;

            if (coords[1] < 0)
                coords[1] += Width;
            else if (coords[1] >= Width)
                coords[1] -= Width;
            if (coords[0] < 0)
                coords[0] += Height;
            else if (coords[0] >= Height)
                coords[0] -= Height;
            return coords;
        }

        /**
         * Launches one-time spreading disiases of all viruses 
         * It is a part of one repeated process of "viruses spreading - inhabitants movement"
         * param[out] ca a cellular automata area
         * */
        public int [] VirusesSpreading(Inhabitant [,] ca)
        {
            int nmbDeleted = 0;
            int nmbLiving = 0;
            int nmbInfected = 0;
            for (int i = 0; i < Height; i++)
                if (viruses[i] != null)
                    foreach (Virus v in viruses[i])
                    {
                        v.Diffusion(ca);
                        if (v.Lifetime == 0)
                        {
                            if (deletedViruses[i] == null)
                                deletedViruses[i] = new Stack<Virus>();
                            deletedViruses[i].Push(v);
                            nmbDeleted++;
                        }
                        else
                        {
                            nmbLiving++;
                            nmbInfected += v.CurrentInfected;
                            v.CurrentInfected = 0;
                        }                         
                    }

            for (int i = 0; i < Height; i++)
                if (deletedViruses[i] != null)
                    foreach (Virus v in deletedViruses[i])
                        viruses[i].Remove(v);
            return new int [3] { nmbDeleted, nmbLiving, nmbInfected };
        }


        /**
         * Launches one-time random movements of all inhabitants 
         * It is a part of one repeated process of "viruses spreading - inhabitants movement"
         * \param[in] caFrom, caTo departed and destination areas of a cellular automata
         * \param[out] nmbRecovered the number of recovered inhabitants during an iteration
         * */
        public int InhabitantsActivities(ref Inhabitant[,] caFrom, ref Inhabitant [,] caTo)
        {
            int nmbRecovered = 0;
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    if (caFrom[i, j] != null)
                        if (caFrom[i, j].Move(ref caFrom, ref caTo))
                            nmbRecovered++;
            return nmbRecovered;
        }

        /**
         * Launch one-time movements (considering enviroment conditions) of all inhabitants 
         * Is is a part of one repeated process of "viruses spreading - inhabitants movement"
         * \param[in] caFrom, caTo departed and destination areas of a cellular automata
         * \param[out] nmbRecovered the number of recovered inhabitants during an iteration
         * */

        public int InhabitantsActivitiesCognitive(ref Inhabitant[,] caFrom, ref Inhabitant[,] caTo )
        {
            int nmbRecovered = 0;
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    if (caFrom[i, j] != null)
                        if (caFrom[i, j].CognitiveMove(ref caFrom, ref caTo))
                            nmbRecovered++;
            return nmbRecovered;
        }

        /**
         * Calculates the number of the infected agents and susceptible inhabitants
         * \param[in] coordinates of the rectangular area
         * \param[out] cntInfected, cntNonInfected 1x2-dim an array with the number of infected and susceptible inhabitants
         * */
        public int[] NmbInhabitants(Inhabitant[,] caCur)
        {
            int cntInfected = 0;
            int cntNonInfected = 0;
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    if (caCur[i, j] != null)
                        if (caCur[i, j].Infected)
                            cntInfected++;
                        else
                            cntNonInfected++;
            return new int[2] { cntInfected, cntNonInfected };
        }

        /**
        * An iteration of a "viruses spreading - inhabitants movement" with a random selection cell procedure for inhabitant's movements from area ca to ca1
        **/
        public int[] Next01()
        {
            int[] virusesChanges;
            virusesChanges = VirusesSpreading(ca);
            InhabitantsActivities(ref ca, ref ca1);
            int [] inhStat = NmbInhabitants(ca1);
            if (changable_viruses_nmb)
                AppendToVirusesPopulation();
            stats.AddVirusData(virusesChanges[0], virusesChanges[1], virusesChanges[2], inhStat[0], inhStat[1], time);
            time++;         
            return virusesChanges;
        }

        /**
        * An iteration of a "viruses spreading - inhabitants movement" with a random selection cell procedure for inhabitant's movements from area ca1 to ca
        **/
        public int[] Next10()
        {
            
            int[] virusesChanges = VirusesSpreading(ca1);
            InhabitantsActivities(ref ca1, ref ca);
            int[] inhStat = NmbInhabitants(ca);
            if (changable_viruses_nmb)
                AppendToVirusesPopulation();
            stats.AddVirusData(virusesChanges[0], virusesChanges[1], virusesChanges[2], inhStat[0], inhStat[1], time);
            time++;
            return virusesChanges;
        }

        /**
        * An iteration of a "viruses spreading - inhabitants movement" with an intelligent selection cell procedure for inhabitant's movements from area ca to ca
        **/
        public int[] Next01Cog()
        {
            int[] virusesChanges = VirusesSpreading(ca);
            InhabitantsActivitiesCognitive(ref ca, ref ca1);
            int[] inhStat = NmbInhabitants(ca1);
            if (changable_viruses_nmb)
                AppendToVirusesPopulation();
            stats.AddVirusData(virusesChanges[0], virusesChanges[1], virusesChanges[2], inhStat[0], inhStat[1], time);
            time++;
            return virusesChanges;
        }

        /**
        * An iteration of a "viruses spreading - inhabitants movement" with an intelligent selection cell procedure for inhabitant's movements from area ca1 to ca
        **/
        public int[] Next10Cog()
        {
            int[] virusesChanges = VirusesSpreading(ca1);
            InhabitantsActivitiesCognitive(ref ca1, ref ca);
            int[] inhStat = NmbInhabitants(ca);
            if (changable_viruses_nmb)
                AppendToVirusesPopulation();
            stats.AddVirusData(virusesChanges[0], virusesChanges[1], virusesChanges[2], inhStat[0], inhStat[1], time); 
            time++;
            return virusesChanges;
        }
    }

}