using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace L1_conccurent_programming
{
    class Rmonitor
    {
        private static readonly object lockObject = new object();
        private int n;
        private field[] store;

        public Rmonitor(int size)
        {
            n = 0;
            store = new field[size];
        }


        public field Get(int i) { return store[i]; }
        /// <summary>
        /// Gets the counter of apple tree data (method overloading)
        /// </summary>
        /// <returns>Number of trees</returns>
        public int GetSize() { return n; }
        /// <summary>
        /// Add a tree to the garden i.e. to the end of array
        /// </summary>
        /// <param name="atree">Tree to be added</param>



        public void Add(field a)
        {
            lock (lockObject)
            {
                int insertionIndex = n;

                // Find the correct position to insert the new element using insertion sort
                while (insertionIndex > 0 && a.CompareTo(store[insertionIndex - 1]) < 0)
                {
                    store[insertionIndex] = store[insertionIndex - 1];
                    insertionIndex--;
                }

                // Insert the new element at the correct position
                store[insertionIndex] = a;
                n++;
            }
        }



    }

}