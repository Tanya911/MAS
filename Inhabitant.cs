using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    /**
     * \brief Inhabitants are moving agents, susceptible to infection. 
     * 
     * Inhabitants have a radius of moving (a maximal distance of a movement). Each instance also has a specific health state
     * If an instance moves into an occupied cell, a conflict is solved taking into account a status of the disease (infected/noninfected)
     **/

    class Inhabitant : MASObject
    {
        /// <summary>
        /// A probability to recover (difune health state)
        /// </summary>
        float recoveryRate =0; 
        /// <summary>
        /// Share of infected inhabitants in the movement area
        /// </summary>
        float infectedRate;
        /// <summary>
        /// The number of conflictes Infected-Infected
        /// </summary>  
        int nmbOfConflictsII = 0;
        /// <summary>
        /// The number of conflicts Infected-Noninfected
        /// </summary>
        int nmbOfConflictsINI = 0;
        /// <summary>
        /// The number of conflicts NonInfected-NonInfected
        /// </summary>
        int nmbOfConflictsININ = 0;
        /// <summary>
        /// Last coordinates of the successful movement
        /// </summary>
        int oldX, oldY;
        /// <summary>
        /// The number of infections
        /// </summary>
        int nmbOfInfections = 0;
        /// <summary>
        /// The total number of relocation during the simulation when inhabutant is noninfected
        /// </summary>
        int nmbTotalRelocationNonInfected = 0;
        /// <summary>
        /// Weights for viruses and inhabitants density, which are used for selecting the best position in a neighborhood
        /// </summary>
        float wV = 1;
        float wI = (float)0.3;

        public int NmbOfConflictsII
        {
            get { return nmbOfConflictsII; }
        }

        public int NmbOfConflictsINI
        {
            get { return nmbOfConflictsINI; }
        }

        public int NmbOfConflictsININ
        {
            get { return nmbOfConflictsININ; }
        }

        public float RecoveryRate
        {
            get { return recoveryRate; }
        }

        /// <summary>
        /// Indected status
        /// </summary>
        public bool Infected { get; set; }

        /// <summary>
        /// The duration of disease (how long the agent was infected, number of iterations)
        /// </summary>
        public int Quarantine { get; set; }

        public void IncreaseNmbOfInfections()
        {
            nmbOfInfections++; 
        }

        /// <summary>
        /// The total number of relocation during the simulation when inhabutant is infected
        /// </summary>
        int nmbTotalRelocationInfected = 0;
        public int NmbTotalRelocationInfected
        {
            get { return nmbTotalRelocationInfected; }
        }

        public int NmbTotalRelocationNonInfected
        {
            get { return nmbTotalRelocationNonInfected; }
        }

        /**
         * Creates the instance of the Instant class
         * in the random position in unique place for each instance (without collisions with other instances of Inhabitant and Virus classes)
         * \param[in] expert an instance of an Expert class
         * \param[in] id an id of a new-generated instance
         */
        public Inhabitant(Expert expert, int id)
            : base(expert, id)
        {
            this.expert = expert;
            recoveryRate = expert.RecoveryRate;
            if (recoveryRate == -1)
                recoveryRate = (float)expert.mt.genrand_RealInInterval(0, 1);
            int[] coords;
            bool collision = false;
            do
            {
                coords = expert.GetSomeCoordinates();
                collision = expert.DetectInhabitantCollision(coords[0], coords[1]) || expert.DetectVirusCollision(coords[0], coords[1], ref expert.ca);
            } while (collision);

            nmbOfInfections = 0;
            oldX = X = coords[0];
            oldY = Y = coords[1];


            Infected = false;
            SetInfectedRate(expert.ca);
            ChangeInhabitantsEnviroment(1);
        }

        /*
         * Calculates the number of the infected agents in the given rectangular area.
         * \param[in] x0,x1,y0,y1 of a rectangular area
         * \param[in] ca a cellular automata area
         * \param[out] infectedRate the number of infected inhabitants in a given rectangle area
         * */
        int SetInfectedInsideArea(int x0, int x1, int y0, int y1, Inhabitant [,] ca)
        {
            int population = 0;
            int infectedRate = 0;
            for (int i = x0; i <= x1; i++)
                for (int j = y0; j <= y1; j++)
                    if (ca[i, j] != null)
                    {
                        population++;
                        if (ca[i, j].Infected)
                            infectedRate++;
                    }
            return infectedRate;
        }

        /**
         * Calculates the rate of the infected agents in a new heighborhood of destination cell 
         * In the case of boundary positions the calculation is spreading on a space torus like folding
         *\param[in] ca ca a cellular automata area
         */
        void SetInfectedRate(Inhabitant [,] ca)
        {
            int[] coords = expert.GetBorders(X, Y, this.radius);
            int strategy = (coords[2] > coords[3] ? 1 : 0) + (coords[0] > coords[1] ? 2 : 0);
            infectedRate = 0;
            switch (strategy)
            {
                case (0):
                    infectedRate = SetInfectedInsideArea(coords[0], coords[1], coords[2], coords[3], ca);
                    break;
                case (1):
                    infectedRate = SetInfectedInsideArea(coords[0], coords[1], 0, coords[3], ca) +
                        SetInfectedInsideArea(coords[0], coords[1], coords[2], expert.Width - 1, ca);
                    break;
                case (2):
                    infectedRate = SetInfectedInsideArea(0, coords[1], coords[2], coords[3], ca) +
                        SetInfectedInsideArea(coords[0], expert.Height - 1, coords[2], coords[3], ca);
                    break;
                case (3):
                    infectedRate = SetInfectedInsideArea(0, coords[1], 0, coords[3], ca) +
                        SetInfectedInsideArea(coords[0], expert.Height - 1, 0, coords[3], ca) +
                        SetInfectedInsideArea(0, coords[1], coords[2], expert.Width - 1, ca) +
                        SetInfectedInsideArea(coords[0], expert.Height - 1, coords[2], expert.Width - 1, ca);
                    break;
            }
            infectedRate -= ((ca[X, Y] != null) && (ca[X, Y].Infected)) ? 1 : 0;
            infectedRate /= (square - 1);
        }

        /**
         * Resolves conflict, when two agent try to take the same cell.
         * An agent priority is defined by status of agents (noninfected one has the highest priority),
         * in the case of similar statuses agent with the best health (healthStatus value) has the highest priority
         * \param[in] x1,y1 coordimates of a cell
         * \param[in] caFrom,caTo departure and destination aread of a cellular automata
         */
        void ResolveConflict(int x1, int y1,  ref Inhabitant [,] caFrom, ref Inhabitant [,] caTo)
        {
            if (caTo[x1, y1].Infected != Infected)
            {
                nmbOfConflictsINI++;
                if (caTo[x1, y1].Infected)
                {
                    caTo[x1, y1].FindAnotherPlace(ref caTo, ref caTo);
                    caTo[x1, y1] = this;
                    caFrom[X, Y] = null;
                    ChangeInhabitantsEnviroment(-1);
                    oldX = X = x1;
                    oldY = Y = y1;
                    ChangeInhabitantsEnviroment(1);
                }
                else
                    FindAnotherPlace(ref caFrom, ref caTo);
            }
            else
            {
                if (caTo[x1, y1].Infected)
                    nmbOfConflictsII++;
                else
                    nmbOfConflictsININ++;
                if (recoveryRate > caTo[x1, y1].recoveryRate)
                {
                    caTo[x1, y1].FindAnotherPlace(ref caTo, ref caTo);
                    caTo[x1, y1] = this;
                    caFrom[X, Y] = null;
                    ChangeInhabitantsEnviroment(-1);
                    oldX = X = x1;
                    oldY = Y = y1;
                    ChangeInhabitantsEnviroment(1);
                }
                else
                    FindAnotherPlace(ref caFrom, ref caTo);
            }
        }

        /**
         * Looks for another position in a radius of movements
         * If a current neighborhood is overcrowded (more that 2*radius attempts to take a cell), a radius is increased.
         * \param[in] caFrom,caTo departure and destination aread of a cellular automata
         */
        void FindAnotherPlace(ref Inhabitant[,] caFrom, ref Inhabitant[,] caTo)
        {
            bool notRemoved = true;
            int curRadius = radius;
            int attempt = 0;
            do
            {
                notRemoved = Dislocate(curRadius, ref caFrom, ref caTo);
                if (attempt > 2 * curRadius)
                {
                    attempt = 0;
                    curRadius+=radius;
                    if ((curRadius > caTo.GetLength(0) / 2) || (curRadius > caTo.GetLength(1) / 2))
                        curRadius = Math.Min(caTo.GetLength(0), caTo.GetLength(1));
                }
                attempt++;
            } while (notRemoved);
        }

        /**
         * Try to shift to random position in the neighborhood, defined by a given radius
         * \param [in] curRadius radius of the neighborhood, where movements are possible
         * \param[in] caFrom,caTo departure and destination aread of a cellular automata
         * \param[out] bool whether the dislocation was successful
         */
        bool Dislocate(int curRadius, ref Inhabitant[,] caFrom, ref Inhabitant[,] caTo)
        {
            int[] coords = expert.GetNewCoordinates(oldX, oldY, curRadius);
            if ((caTo[coords[0], coords[1]] == null) && (!expert.OccupiedByVirus(coords[0], coords[1])))
            {
                caTo[coords[0], coords[1]] = this;
                caFrom[X, Y] = null;
                ChangeInhabitantsEnviroment(-1);
                oldX = X = coords[0];
                oldY = Y = coords[1];
                ChangeInhabitantsEnviroment(1);
                if (Infected)
                    nmbTotalRelocationInfected++;
                else
                    nmbTotalRelocationNonInfected++;
                return false;
            }
            else
            {
                if (Infected)
                    nmbTotalRelocationInfected++;
                else
                    nmbTotalRelocationNonInfected++;
                return true;
            }              
        }

        /**
         * Moving the agent to a random position inside the neighborhood. If some conflicts happen, another functions for resolving of collisions will be called
         * \param[in] caFrom,caTo departure and destination aread of a cellular automata
         * \param[out] bool whether the inhabitant became susceptible
         * */
        public bool Move(ref Inhabitant[,] caFrom, ref  Inhabitant[,] caTo)
        {
            bool recovered = false;
            if (Infected)
            {
                double rndRecovery = expert.mt.genrand_RealInInterval(0, 1);
                if (rndRecovery < recoveryRate)
                {
                    Infected = false;
                    recovered = true;
                }
                else
                    Quarantine++;
            }

            int[] coords = expert.GetNewCoordinates(oldX, oldY, radius);
            if (caTo[coords[0], coords[1]] != null)
                ResolveConflict(coords[0], coords[1],  ref caFrom, ref caTo);
            else if (expert.OccupiedByVirus(coords[0], coords[1]))
                this.FindAnotherPlace(ref caFrom, ref caTo);
            else
            {
                caTo[coords[0], coords[1]] = this;
                caFrom[X, Y] = null;
                ChangeInhabitantsEnviroment(-1);
                oldX = X = coords[0];
                oldY = Y = coords[1];
                ChangeInhabitantsEnviroment(1);
            }
            return recovered;
        }

        /**
         * Moving the agent to the best position inside the neighborhood. If some conflicts happen, another functions for resolving of collisions will be called
         * \param[in] caFrom,caTo departure and destination aread of a cellular automata
         * \param[out] bool whether the inhabitant became susceptible
         * */
        public bool CognitiveMove(ref Inhabitant[,] caFrom, ref  Inhabitant[,] caTo)
        {
            bool recovered = false;
            if (Infected)
            {
                double rndRecovery = expert.mt.genrand_RealInInterval(0, 1);
                if (rndRecovery < recoveryRate)
                {
                    Infected = false;
                    recovered = true;
                }
                else
                    Quarantine++;
            }
            int[] coords = GetBestCellInNeiborhoood(oldX, oldY, radius, caTo);

            if (caTo[coords[0], coords[1]] != null)
                ResolveConflict(coords[0], coords[1], ref caFrom, ref caTo);
            else
            {
                caTo[coords[0], coords[1]] = this;
                caFrom[X, Y] = null;
                ChangeInhabitantsEnviroment(-1);
                oldX = X = coords[0];
                oldY = Y = coords[1];
                ChangeInhabitantsEnviroment(1);
            }
            return recovered;
        }

        /**
         * Returns coordinates of the most favourable for living cell in a neigborhood
         * \param[in] x, y center of the neigborhood
         * \param[in] radius radius of the neigborhood
         * \param[in] ca area where cells are disposed
         * \param[out] retCoords coordinates of a new cell
        */
        public int[] GetBestCellInNeiborhoood(int x, int y, int radius, Inhabitant [,] ca)
        {
            int[] coords = expert.GetBorders(X, Y, radius);
            int[] retCoords = new int[2] { 0, 0 };
            int strategy = (coords[2] > coords[3] ? 1 : 0) + (coords[0] > coords[1] ? 2 : 0);
            infectedRate = 0;
            float[] out1 = new float[3] { 0, 0, 0 };
            float[] out2 = new float[3] { 0, 0, 0 };
 
            switch (strategy)
            {
                case (0):
                    out1 = GetBestCell(coords[0], coords[1], coords[2], coords[3], wV, wI, ca);
                    retCoords[0] = (int)out1[1];
                    retCoords[1] = (int)out1[2];
                    break;
                case (1):
                    out1 = GetBestCell(coords[0], coords[1], 0, coords[3], wV, wI, ca);
                    out2 = GetBestCell(coords[0], coords[1], coords[2], expert.Width - 1, wV, wI, ca);
                    if (out1[0] > out2[0])
                    {
                        retCoords[0] = (int)out1[1];
                        retCoords[1] = (int)out1[2];
                    }
                    else 
                    {
                        retCoords[0] = (int)out2[1];
                        retCoords[1] = (int)out2[2];
                    }
                    break;
                case (2):
                    out1 = GetBestCell(0, coords[1], coords[2], coords[3], wV, wI, ca);
                    out2 = GetBestCell(coords[0], expert.Height - 1, coords[2], coords[3], wV, wI, ca);
                    if (out1[0] > out2[0])
                    {
                        retCoords[0] = (int)out1[1];
                        retCoords[1] = (int)out1[2];
                    }
                    else 
                    {
                        retCoords[0] = (int)out2[1];
                        retCoords[1] = (int)out2[2];
                    }
                    break;
                case (3):
                    float[] out3 = new float[3] { 0, 0, 0 };
                    out1 = GetBestCell(0, coords[1], 0, coords[3], wV, wI, ca);
                    out2 = GetBestCell(coords[0], expert.Height - 1, 0, coords[3], wV, wI, ca);
                    if (out1[0] > out2[0])
                    {
                        out3[0] = (int)out1[0];
                        out3[1] = (int)out1[1];
                        out3[2] = (int)out1[2];
                    }
                    else
                    {
                        out3[0] = (int)out2[0];
                        out3[1] = (int)out2[1];
                        out3[2] = (int)out2[2];
                    }
                    out1 = GetBestCell(0, coords[1], coords[2], expert.Width - 1, wV, wI, ca);
                    out2 = GetBestCell(coords[0], expert.Height - 1, coords[2], expert.Width - 1, wV, wI, ca);
                    if (out1[0] > out2[0])
                    {
                        if (out1[0] > out3[0])
                        {
                            retCoords[0] = (int)out1[1];
                            retCoords[1] = (int)out1[2];
                        }
                        else
                        {
                            retCoords[0] = (int)out3[1];
                            retCoords[1] = (int)out3[2];
                        }
                    }
                    else
                    {
                        if (out2[0] > out3[0])
                        {
                            retCoords[0] = (int)out2[1];
                            retCoords[1] = (int)out2[2];
                        }
                        else
                        {
                            retCoords[0] = (int)out3[1];
                            retCoords[1] = (int)out3[2];
                        }
                    }
                    break;
            }
            return retCoords;
        }

        /**
        * Returns coordinates of the most favourable for living cell in a rectungle area
        * \param[in] x0, x1, y0, y1 coordinates of the rectangular area
        * \param[in] weightV, weightI - cooficients of the objective function (for estimating cells)
        * \param[in] ca area of a cellular automata
        * \param[out] array an 1x3-dim array that contains an assessment of the best position and its coordinates
        **/
        float[] GetBestCell(int x0, int x1, int y0, int y1, float weightV, float weightI, Inhabitant [,] ca)
        {
            float bestVal = Int16.MinValue, val = Int16.MinValue;
            int xBest = -1, yBest = -1;
            for (int i = x0; i <= x1; i++)
                for (int j = y0; j <= y1; j++)
                {
                    val = - weightI * expert.GetInhabitantDensity(i, j) - weightV * expert.GetVirusesDensity(i, j);
                    if ((val > bestVal) && (!expert.OccupiedByVirus(i, j)))
                    {                   
                            xBest = i;
                            yBest = j;
                            bestVal = val;
                    }
                }
            return new float[3] { bestVal, xBest, yBest };
        }

        /**
        * Change the enviroment information about density in a rectangular area 
        * The corresponding duration of the disease is assigned.
        * \param[in] x0,x1,y0,y1coordinates of the rectangular area
        * \param[in] sign 1/-1 for departure/destination points accordingly
        */
        void InhabitantsInsideArea(int x0, int x1, int y0, int y1, short sign)
        {
            for (int i = x0; i <= x1; i++)
                for (int j = y0; j < y1; j++)
                    expert.ChangeInhabitantDensity(i, j, (float)sign / square);
        }

        /**
         * Change the enviroment information about density in the neigborhood of departure and destination poing
         * In the case of boundary positions the disease is spreading on a space torus like folding
         * \param[in] sign 1/-1 for departure/destination points accordingly
         * */
        public void ChangeInhabitantsEnviroment(short sign)
        {

            int[] coords = expert.GetBorders(X, Y, radius);
            int strategy = (coords[2] > coords[3] ? 1 : 0) + (coords[0] > coords[1] ? 2 : 0);

            switch (strategy)
            {
                case (0):
                    InhabitantsInsideArea(coords[0], coords[1], coords[2], coords[3], sign);
                    break;
                case (1):
                    InhabitantsInsideArea(coords[0], coords[1], 0, coords[3], sign);
                    InhabitantsInsideArea(coords[0], coords[1], coords[2], expert.Width - 1, sign);
                    break;
                case (2):
                    InhabitantsInsideArea(0, coords[1], coords[2], coords[3], sign);
                    InhabitantsInsideArea(coords[0], expert.Height - 1, coords[2], coords[3], sign);
                    break;
                case (3):
                    InhabitantsInsideArea(0, coords[1], 0, coords[3], sign);
                    InhabitantsInsideArea(coords[0], expert.Height - 1, 0, coords[3], sign);
                    InhabitantsInsideArea(0, coords[1], coords[2], expert.Width - 1 - 1, sign);
                    InhabitantsInsideArea(coords[0], expert.Height - 1, coords[2], expert.Width - 1, sign);
                    break;
            }
        }
    }

}
