using System.Collections.Generic;
using UnityEngine;


namespace CustomRandomGenExtension
{
    public static class RandomIndexGenerator
    {
        private static int lastIndex = 0;

        /// <summary>
        /// Generates a new random index every time from a generic set of list  
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="list">A List having any kind of data type</param>
        /// <returns>returns a new random index every time</returns>
        public static int GetRandomIndex<T>(this List<T> list)
        {
            
            int randomIndex = lastIndex;

            while(randomIndex==lastIndex)
            {
                randomIndex = Random.Range(0, list.Count);
            }

            lastIndex = randomIndex;

            return randomIndex;
        }

    }
}

