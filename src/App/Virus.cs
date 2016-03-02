using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    /**
     * Viruses are stationary objects, spreading an infection 
     * among instances of the Inhabitant class
     * 
     * Instances of this class may differ the radius of spreading, 
     * the probability of the infection by a contact with noninfected inhabitants, duration of life.
     * The parent class is MASObject
     */
    class Virus : MASObject
    {
        /// <summary>
        /// Probability of the infection
        /// </summary>
        float infectionRate;
        /// <summary>
        /// The number of iteratings during the virus is existing
        /// </summary>
        int lifetime = 0;
        /// <summary>
        /// The total number of infected agents by the virus
        /// </summary> 
        int totalInfected = 0;
        /// <summary>
        /// The number of infected agents by the virus on current iteration (statistic variable)
        /// </summary> 
        public int CurrentInfected { get; set; }

        public float Lifetime
        {
            get { return lifetime; }
        }

        public float Contagiousness
        {
            get { return infectionRate; }
        }

        /**
         * Creates the instance of the Virus class
         * in the random position at unique place for each instance
         * \param[in] expert an instance of the Expert class
         * \param[in] id the unique number of a new-generated virus
         */
        public Virus(Expert expert, int id)
            : base(expert, id)
        {
            this.expert = expert;
            lifetime = expert.VirusesLifetime;
            infectionRate = expert.InfectionRate;
            if (infectionRate == -1)
                infectionRate = base.GetRealNumber(0, (float)0.6);                 
            bool collision = false;
            
            int[] coords;
            do
            {
                coords = expert.GetSomeCoordinates();
                if (this.expert.ca[coords[0], coords[1]] == null)
                    collision = expert.DetectVirusCollision(coords[0], coords[1], ref expert.ca);
            } while (collision);
            X = coords[0];
            Y = coords[1];
            ChangeVirusesEnvirement(1);
        }

        /**
         * Spreads the disease in the given area. Noninfected instanses of Inhabitant class become infected with a given probability of infection. 
         * The corresponding duration of the disease is assigned.
         * \param[in] x0,x1,y0,y1 coordinates of a rectangular area
         * \param[in] ca an area of a cellular automata
         */
        void DiffusionInsideArea(int x0, int x1, int y0, int y1, Inhabitant [,] ca)
        {
            float p = 0;
            for (int i = x0; i <= x1; i++)
                for (int j = y0; j < y1; j++)
                    if (ca[i, j] != null) 
                        if (!ca[i, j].Infected)
                        {
                            p = (float)expert.mt.genrand_real3();
                            if (p < infectionRate)
                               {
                                    ca[i, j].Infected = true;
                                    ca[i, j].IncreaseNmbOfInfections();
                                    CurrentInfected++;
                                }
                        }
            totalInfected += CurrentInfected;
        }

        /*
         * Spreads the disease in the area, that defined by the radius.
         * In the case of boundary positions the disease is spreading on a space torus like folding
         * \param[in] ca an area of a cellular automata
         * */
        public void Diffusion(Inhabitant [,] ca)
        {
            if (lifetime == 0) //virus is dying
            {
                ChangeVirusesEnvirement(-1); //change enviroment information 
                expert.stats.AddVirusData(this); //append data to a statistics collector
            }                
            else 
            {   
                lifetime--; // decrease the remaining lifetime

                int[] coords = expert.GetBorders(X, Y, radius);
                int strategy = (coords[2] > coords[3] ? 1 : 0) + (coords[0] > coords[1] ? 2 : 0); //select the borders of spreading the decease 
                                                                                                  // for torus like folding
                switch (strategy)
                {
                    case (0): // a virus is located inside the area
                        DiffusionInsideArea(coords[0], coords[1], coords[2], coords[3], ca);
                        break;
                    case (1): //a virus is located beside a vertical border
                        DiffusionInsideArea(coords[0], coords[1], 0, coords[3], ca);
                        DiffusionInsideArea(coords[0], coords[1], coords[2], expert.Width - 1, ca);
                        break;
                    case (2):  //a virus is located beside a horisontal border
                        DiffusionInsideArea(0, coords[1], coords[2], coords[3], ca);
                        DiffusionInsideArea(coords[0], expert.Height - 1, coords[2], coords[3], ca);
                        break;
                    case (3): //a virus is located beside a corner
                        DiffusionInsideArea(0, coords[1], 0, coords[3], ca);
                        DiffusionInsideArea(coords[0], expert.Height - 1, 0, coords[3], ca);
                        DiffusionInsideArea(0, coords[1], coords[2], expert.Width - 1 - 1, ca);
                        DiffusionInsideArea(coords[0], expert.Height - 1, coords[2], expert.Width - 1, ca);
                        break;
                }
            }
        }

        /**
        * Updates information about viruses in a given area 
        * Increases rate of viruses, since a new virus appears
        * \param[in] x0,x1,y0,y2 coordinates of the rectangular area
        * \param[in] sign the sign of changes (increasing or decreasing)
        * */
        void ChangeVirusesInArea(int x0, int x1, int y0, int y1, short sign)
        {
            for (int i = x0; i <= x1; i++)
                for (int j = y0; j < y1; j++)
                    expert.IncreaseVirsusDensity(i,j,(float)sign/square);
        }

        /**
         * Updates information about viruses in a given neigborhood 
         * Increases rate of viruses, since a new virus appears
         * \param[in] an area of a cellular automata
         * */
        void ChangeVirusesEnvirement(short sign)
        {
            int[] coords = expert.GetBorders(X, Y, radius);
            int strategy = (coords[2] > coords[3] ? 1 : 0) + (coords[0] > coords[1] ? 2 : 0);

            switch (strategy)
            {
                case (0):
                    ChangeVirusesInArea(coords[0], coords[1], coords[2], coords[3], sign);
                    break;
                case (1):
                    ChangeVirusesInArea(coords[0], coords[1], 0, coords[3], sign);
                    ChangeVirusesInArea(coords[0], coords[1], coords[2], expert.Width - 1, sign);
                    break;
                case (2):
                    ChangeVirusesInArea(0, coords[1], coords[2], coords[3], sign);
                    ChangeVirusesInArea(coords[0], expert.Height - 1, coords[2], coords[3], sign);
                    break;
                case (3):
                    ChangeVirusesInArea(0, coords[1], 0, coords[3], sign);
                    ChangeVirusesInArea(coords[0], expert.Height - 1, 0, coords[3], sign);
                    ChangeVirusesInArea(0, coords[1], coords[2], expert.Width - 1 - 1, sign);
                    ChangeVirusesInArea(coords[0], expert.Height - 1, coords[2], expert.Width - 1, sign);
                    break;
            }
        }

    }
}
