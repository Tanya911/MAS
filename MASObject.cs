using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    /**
     * \brief The basic (parent) class. It represents an agent of the  multi-agent system
     * 
     * This class is used to create classes of specific agents of the  multi-agent system: inhabitants and viruses
     */
    class MASObject
    {
        protected Expert expert;

        /// <summary>
        /// Сoordinates of the object
        /// </summary>
        public int X {get; set;}
        public int Y {get; set;}
        /// <summary>
        /// ID of an object
        /// </summary>
        protected int id;
        /// <summary>
        /// The radius of possible moving or spreading influence (for nonmovable objects)
        /// </summary>
        protected int radius;
        /// <summary>
        /// The square of possible moving or spreading influence (for nonmovable objects)
        /// </summary>
        protected int square;
        public int Id
        {
            get { return id; }
        }

        public MASObject(Expert expert, int id) 
        {
            this.expert = expert;
            this.id = id;
            radius = 3;
            square = (2 * radius + 1) ^ 2;
        }

        /**
         * Generates an uniformly distributed random number in the given range (a,b)
         * \param[in] a Left border
         * \param[in] b Right border 
         * \param[out] float random float
         */
        protected float GetRealNumber(float a, float b)
        {
            return (float)expert.mt.genrand_RealInInterval(a,b);
        }
    }

}
