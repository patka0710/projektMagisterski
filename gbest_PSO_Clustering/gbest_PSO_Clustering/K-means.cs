using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gbest_PSO_Clustering
{
    interface EuclidesDistance
    {
        double[] EuclidesDistance(double[] Zp, double[] Mi);
    }

    class K_means : EuclidesDistance
    {
        List<double[]> Z;
        int dimension;
        int clusterCount;
        int[] clusterZp;
        double[] means;
        double min;
        double max;

        public K_means(List<double[]> dataVector, int dimension, int clusterCount, double min, double max)
        {
            this.Z = dataVector;
            this.dimension = dimension;
            this.clusterCount = clusterCount;
            this.min = min;
            this.max = max;

            this.clusterZp = new int[Z.Count];
            this.means = new double[dimension * clusterCount];
        }


         void InitMeansRandom()
        {
            Random rnd = new Random();

            for(int i=0; i< means.Length; i++)
            {
                means[i] = rnd.Next((int)min, (int)max);
            }
        }

        void InitZpToCluster()
        {
            Random rnd = new Random();

            for (int i = 0; i < clusterZp.Length; i++)
            {
                clusterZp[i] = rnd.Next(0, clusterCount);
            }
        }

        public double[] EuclidesDistance(double[] Zp, double[] Mi)
        {
            double[] result = new double[clusterCount];
            for (int k = 0; k < clusterCount; k++)
            {
                double sum = 0.0;
                for (int j = 0; j < Zp.Length; ++j)
                {
                    sum += Math.Pow((Zp[j] - Mi[(k * dimension) + j]), 2);
                }
                result[k] = Math.Sqrt(sum);
            }
            return result;
        }

        private bool UpdateMeans()
        {
            
            int[] clusterCounts = new int[clusterCount];

            for (int i = 0; i < Z.Count; ++i)
            {
                int cluster = clusterZp[i];
                ++clusterCounts[cluster];
            }

            for(int i=0; i< clusterCount; i++)
            {
                if (clusterCounts[i] == 0)
                {
                    return false;
                }

            }

            for (int i = 0; i < clusterCount; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    double sum = 0.0;
                    int counterZp = 0;

                    foreach (var Zp in Z)
                    {
                        if (clusterZp[counterZp] == i)
                        {
                            sum += Zp[j];
                        }
                        counterZp++;
                    }
                    if (sum != 0.0)
                    {
                        means[(dimension * i) + j] = sum / clusterCounts[i];
                    }
                }
               
            }
            return true;
        }

        private bool UpdateClustering()
        {
            int result = 0;
            double[] temporaryRememberClustering = new double[Z.Count];

            clusterZp.CopyTo(temporaryRememberClustering, 0);

            int counterDataVector = 0;
            foreach (var Zp in Z)
            {
                double[] distance = EuclidesDistance(Zp, means);

                double minDistance = distance.Min();
                for (int l = 0; l < distance.Length; l++)
                {
                    if (distance[l] == minDistance)
                    {
                        clusterZp[counterDataVector] = l;
                        break;
                    }
                }
                counterDataVector++;
            }

            for (int i = 0; i < clusterZp.Length; i++)
            {
                if (clusterZp[i] != temporaryRememberClustering[i])
                {
                    result++;
                }
            }

            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        public double[] Solved(int maxIteration)
        {
            int counter = 0;
            bool successUpdateMeans = true;
            bool changedInUpdateClustering = true;

            InitMeansRandom();

            InitZpToCluster();

            while (successUpdateMeans == true && changedInUpdateClustering && counter < maxIteration)
            {
                successUpdateMeans = UpdateMeans();
                changedInUpdateClustering = UpdateClustering();
                counter++;
            }

            Console.WriteLine("Przyporządkowane klastry");
            foreach (var tmp in clusterZp)
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

            return means;
        }
    }
}
