using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gbest_PSO_Clustering
{
    class Program
    {
        //docelowo dane zostaną zczytane z pliku
        private static List<double[]> initDataVector()
        {
            List<double[]> dataVector = new List<double[]>();

            /* double[] date = { 1, 2 };
             double[] date2 = { 2, 5 };
             double[] date3 = { 7, 4 };
             double[] date4 = { -1, -5 };
             double[] date5 = { -5, -3 };

             dataVector.Add(date);
             dataVector.Add(date2);
             dataVector.Add(date3);
             dataVector.Add(date4);
             dataVector.Add(date5);*/

            /*double[] date = { -7, 5 };
            double[] date1 = { -5, 3 };
            double[] date2 = { -6, 1 };
            double[] date3 = { -3, -1 };
            double[] date4 = { -5, -3 };
            double[] date5 = { 3, 7 };
            double[] date6 = { 2, 2 };
            double[] date7 = { 8, 5 };
            double[] date8 = { 10, 4 };
            double[] date9 = { 2, 1 };
            double[] date10 = { 3, 1 };*/

            double[] date = { 1, 2 };
            double[] date1 = {2, 2 };
            double[] date2 = {1, 1 };
            double[] date3 = {3, 1 };
            double[] date4 = {3, 3 };
            double[] date5 = {4, 4 };
            double[] date6 = {6, 6 };
            double[] date7 = {8, 8 };
            double[] date8 = {7, 9 };
            double[] date9 = {9, 9 };
            double[] date10 = {8, 6 };


            dataVector.Add(date);
            dataVector.Add(date1);
            dataVector.Add(date2);
            dataVector.Add(date3);
            dataVector.Add(date4);
            dataVector.Add(date5);
            dataVector.Add(date6);
            dataVector.Add(date7);
            dataVector.Add(date8);
            dataVector.Add(date9);
            dataVector.Add(date10);

            return dataVector;
        }

        static void Main(string[] args)
        {
            const int clusterCount = 2;
            const int swarmCount = 3;
            const double min = 0;
            const double max = 10;
            int dimension = 2;
            int maxIteration = 10;


            List<double[]> dataVector = initDataVector(); // w przyszlośći zczytamy dane z pliku
            int dataVectorCount = dataVector.Count;
            //Console.WriteLine(dataVectorCount);

            if (dataVectorCount != 0)
            {
                dimension = dataVector.ElementAt(0).Length;
                //Console.WriteLine(dimension);
            }
            else
            {
                Console.WriteLine("Nie znaleziono danych do pogrupowania");
            }


            K_means k_means = new K_means(dataVector, dimension, clusterCount, min, max);

            double[] positionMeans =  k_means.Solved(10);

            Swarm swarm = new Swarm(dataVector, swarmCount, dimension, clusterCount, min, max, maxIteration, positionMeans);

            swarm.Result();

            double[] means = swarm.bestGlobalPosition;
            double[] listDataVectorInNumberCluster = swarm.clusterZp;

            Console.WriteLine("Przyporządkowane klastry");
            foreach(var tmp in listDataVectorInNumberCluster)
            {
                Console.Write(tmp + ", ");
            }
            Console.WriteLine("");

            Console.WriteLine("Pozycje środków klastrów:");
            for (int k = 0; k < clusterCount; k++)
            {
                string position = "( ";
                for (int j = 0; j < dimension; ++j)
                {
                    position += String.Format("{0:N2}", means[(k * dimension) + j]);
                    position += " ";
                }
                Console.WriteLine(position + ")");
            }

            Console.ReadKey();
        }
    }

   

    



}
