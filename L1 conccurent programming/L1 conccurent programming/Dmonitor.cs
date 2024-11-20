using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L1_conccurent_programming
{
    class Dmonitor
    {
        private static readonly object signal = new object();
        private static readonly object signal1 = new object();
        private int n;
        private  field[] store;



        public Dmonitor(int size)
        {
            n = 0;
            store = new field[size];
        }



        public field Get(int i) { return store[i]; }

        public int GetSize()
        {
            lock (signal1)
            {
                // Wait until there is at least one item in the store
                while (n == 0)
                {
                    Monitor.Wait(signal1);
                }

                // Return the current value of n
                return n;
            }
        }





        public field Removeitem(int h)
        {
            
            lock (signal)
            {
                if (h >= 0 && h <= n)
                {
                    field g;
                    g = store[h];
                    for (int i = h; i < n - 1; i++)
                    {
                        
                        store[i] = store[i + 1];
                    }
                    store[n - 1] = null;
                    n--;

                    // Signal to waiting threads that an item has been removed
                    Monitor.PulseAll(signal);
                    return g;
                }
                else
                {
                    Console.WriteLine("Invalid index or empty store.");
                    return null;
                }
            }
        }


        



        public void AddItem(field r)
        {



            lock (signal)
            {
                if (store.Length > n)
                {
                    store[n++] = r;
                }
                else
                {
                    // Wait until there is space in 'store'
                    Monitor.Wait(signal);
                    store[n++] = r; // Add the item after waking up
                }

                // If an item is added, signal to waiting threads
                Monitor.PulseAll(signal);

            }
            lock (signal1)
            {
                Monitor.PulseAll(signal1);
            }
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("New students has been added to data monoitor");
            Console.WriteLine(r.ToString());

        }







    }







}

