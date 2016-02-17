using System;
using System.Collections.Generic;
using System.IO;

namespace MAS
{

    class Statistic
    {
        /// <summary>
        /// The number of rabbits
        /// </summary>
        int nmbOfInhabitants = 0; //+
        /// <summary>
        /// The number of viruses
        /// </summary>
        int nmbOfViruses = 0; //+
        /// <summary>
        /// The number of conflictes Infected-Infected
        /// </summary>   
        float avgOfConflictsII = 0; 
        /// <summary>
        /// The average number of conflicts Infected-Noninfected
        /// </summary>
        float avgOfConflictsINI = 0; 
        /// <summary>
        /// The average number of conflicts NonInfected-NonInfected
        /// </summary>
        float avgOfConflictsININ = 0;// +
        /// <summary>
        /// The average number of relocation for infected agents
        /// </summary>
        float nmbRelocationInfected = 0;// +
        /// <summary>
        /// The average number of relocation for noninfected agents
        /// </summary>
        float nmbRelocationNonInfected = 0; // +
        /// <summary>
        /// The average number of infeceted agents
        /// </summary>
        float avgInfected = 0; // на каждой итерации, для каждого вируса
        /// <summary>
        /// The average number of infections for each agent
        /// </summary>
        float avgNumberOfInfections = 0; 
        /// <summary>
        /// The maximum number of infections for each agent
        /// </summary>
        float maxNumberOfInfections = 0;
        /// <summary>
        /// The minimum number of infections for each agent
        /// </summary>
        float minNumberOfInfections = 0;
        /// <summary>
        /// The total number years of living of all viruses
        /// </summary>
        int nmbOfVirusesYearsTotal = 0;
        /// <summary>
        /// The total number of viruses
        /// </summary>
        int nmbOfVirusesTotal = 0;
        //----------Viruses stats--------------
        /// <summary>
        /// The average lifetime for viruses
        /// </summary>
        float avgVirusesLifetime = 0;
        /// <summary>
        /// The average number of infected agents
        /// </summary>
        float avgInfetedByVirus = 0;
        /// <summary>
        /// The number viruses on each iteration
        /// </summary>
        int[] cntDeletedViruses;
        /// <summary>
        /// The number new infected agent on each iteration
        /// </summary>
        int[] cntLivingViruses;
        /// <summary>
        /// The number of dead viruses
        /// </summary>
        int[] cntCurInfected;
        int[] cntNonInfected;
        int[] cntInfected;
        /// <summary>
        /// The average probability of infection
        /// </summary>
        float avgProbabilityOfInfection = 0;
        string timeStamp = "";
        public long ElapsedTime { get; set; }

        /**
        * Initiates the object for collecting statistic
        * \param[in] time duration of the simulation
        * \paran[in] nmbOfInhabitants the constant number of inhabitants
        */
        public Statistic(int time, int nmbOfInhabitants)
        {
            cntDeletedViruses = new int[time];
            cntLivingViruses = new int[time];
            cntCurInfected = new int[time];
            cntNonInfected = new int[time];
            cntInfected = new int[time];
            this.nmbOfInhabitants = nmbOfInhabitants;
            timeStamp = GetTimeStamp(DateTime.Now);
            ElapsedTime = -1; 

        }

        /** the 
        * Updates viruses's data
        * \param[in] v an instance of Virus class
        **/
        public void AddVirusData(Virus v)
        {
            avgProbabilityOfInfection += v.Contagiousness;
            avgVirusesLifetime += v.Lifetime; //nmbOfVirusesYearsTotal for total infected
            nmbOfViruses++;
        }


        /**
        * Updates viruses's data
        * \param[in] nmbOfDeads the number of died viruses
        * \param[in] nmbLiving the number of alive inhabitants
        * \param[in] nmbInfectedOnTheIteration the number of infected inhabitants on an iteration
        * \param[in] nmbInf the total number of infected inhabitants on an iteration
        * \param[in] nmbNonInf the total number of susceptible inhabitants on an iteration
        * \param[in] time the number of iteration
        **/
        public void AddVirusData(int nmbOfDeads, int nmbLiving, int nmbInfectedOnTheIteration,int nmbInf, int nmbNonInf, int time)
        {
            cntDeletedViruses[time] += nmbOfDeads;
            cntLivingViruses[time] += nmbLiving;
            cntCurInfected[time] += nmbInfectedOnTheIteration;
            cntInfected[time] += nmbInf;
            cntNonInfected[time] += nmbNonInf;
       }   

        /** 
         * Updates information about viruses at the end of the simulation
         * \param[in] expert the instance of Expert class
         */
        public void AddVirusData(Expert expert)
        {
            List<Virus>[] viruses = expert.viruses;
            foreach(List<Virus> vlist in viruses)
            {
                if (vlist != null)
                    foreach (Virus v in vlist)
                    {
                        avgProbabilityOfInfection += v.Contagiousness;
                        avgVirusesLifetime += v.Lifetime; 
                        nmbOfViruses++;
                    }
            }
        }

        /**
         * Calculate statistic on viruses data that was collected during the simulation
         * \param[in] expert the instance of Expert class
         */
        public float[] CalculateVirusesStatistic(Expert expert)
        {
            AddVirusData(expert);

           /* avgViruses = 0;
            int avgInfected = 0;
            maxNumberOfInfections = -1;

            minNumberOfInfections = int.MaxValue;

            foreach (int cnt in cntViruses)
                avgViruses += cnt;
            foreach (int cnt in cntInfected)
            {
                avgInfected += cnt;
                if (maxNumberOfInfections < cnt)
                    maxNumberOfInfections = cnt;
                if (minNumberOfInfections > cnt)
                    minNumberOfInfections = cnt;
            }
            avgProbabilityOfInfection /= nmbTotalViruses;*/
            return new float[2] { nmbOfViruses, avgProbabilityOfInfection/nmbOfViruses };
        }

        /**
         * Calculate statistic on inhabitant data that was collected during the simulation
         * \param[in] expert the instance of Expert class
         */
        public float [] CalculateInhabitantsStatistic(Inhabitant[,] ca)
        {
            int durationOfDesease = 0;
            int nmbOfRelocationInfected = 0;
            int nmbOfRelocationNonInfected = 0; ;
            int nmbOfConflictsII = 0;
            int nmbOfConflictsINI = 0;
            int nmbOfConflictsININ = 0;
            float recoveryRate = 0;
            int cnt = 0;
            for (int i = 0; i < ca.GetLength(0); i++)
                for (int j = 0; j < ca.GetLength(1); j++)
                {
                    if (ca[i,j]!=null)
                    {
                        cnt++;
                        durationOfDesease += ca[i, j].Quarantine;
                        nmbOfRelocationInfected += ca[i, j].NmbTotalRelocationInfected;
                        nmbOfRelocationNonInfected += ca[i, j].NmbTotalRelocationNonInfected;
                        nmbOfConflictsII += ca[i, j].NmbOfConflictsII;
                        nmbOfConflictsINI += ca[i, j].NmbOfConflictsINI;
                        nmbOfConflictsININ += ca[i, j].NmbOfConflictsININ;
                        recoveryRate += ca[i, j].RecoveryRate;
                    }
               
                }
            return new float[8] { cnt, durationOfDesease, nmbOfRelocationInfected, nmbOfRelocationNonInfected , nmbOfConflictsII, nmbOfConflictsINI, nmbOfConflictsININ, recoveryRate / cnt};

        }

        /**
        * Calculates statistic and writes results to file
        * \param[in] expert the instance of Expert class
        * \param[in] infRate the rate of inhabitants
        * \param[in] virRate the rate of viruses
        */
        public void WriteResults(Expert expert, float infRate, float virRate)
        {
            float[] inhStats;
            float[] virStats; 
            if (expert.SimulationTime % 2 == 0)
                inhStats = CalculateInhabitantsStatistic(expert.ca);
            else
                inhStats = CalculateInhabitantsStatistic(expert.ca1);

            virStats = CalculateVirusesStatistic(expert);        
           
            using (StreamWriter file = new StreamWriter("stats.csv", true))
            {
                //file.WriteLine("timeStamp; elapsed time;width;height;rateInh;rateVir;iterations; cognitive;"
                //      + "NmbOfInhabitants; durationOfDesease; nmbOfRelocationInfected; nmbOfRelocationNonInfected; nmbOfConflictsII; nmbOfConflictsINI; nmbOfConflictsININ; recoveryRate / cnt;"
                //     + "nmbOfViruses; avgProbabilityOfInfection");
                file.WriteLine(timeStamp + ";" + ElapsedTime + ";" + expert.Width + ";" + expert.Height + ";" + infRate + ";" + virRate + ";" + expert.SimulationTime + ";" + expert.Cognitive + ";" + expert.VirusesLifetime + ";"
                    + string.Join(";", inhStats) + ";" + /*"0;"+expert.InfectionRate);// */string.Join(";", virStats));
            }

            using (StreamWriter file = new StreamWriter("stats_log.csv", true))
            {
                file.WriteLine(timeStamp + ";" + "cntDeletedViruses" + ";" + string.Join(";", cntDeletedViruses));
                file.WriteLine(timeStamp + ";" + "cntLivingViruses" + ";" + string.Join(";", cntLivingViruses));
                file.WriteLine(timeStamp + ";" + "cntCurInfected" + ";" + string.Join(";", cntCurInfected));
                file.WriteLine(timeStamp + ";" + "cntInfected" + ";" + string.Join(";", cntInfected));
                file.WriteLine(timeStamp + ";" + "cntNonInfected" + ";" + string.Join(";", cntNonInfected));
            }      
        }

        public static String GetTimeStamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssf");
        }
    }
}