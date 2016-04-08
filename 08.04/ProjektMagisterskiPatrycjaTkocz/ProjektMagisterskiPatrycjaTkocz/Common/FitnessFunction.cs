using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    public static class FitnessFunction
    {
        public static double CalculateFitnessFunction(List<double[]> distance, int[] clusterZp, int numberOfCluster)
        {
            double result = 0.0;

            for (int i = 0; i < numberOfCluster; i++)
            {
                bool clusterContainDataVector = false;
                int theNumberDataVectorInCluster = 0;
                double sum = 0.0;
                for (int j = 0; j < clusterZp.Length; j++)
                {
                    if (clusterZp[j] == i)
                    {
                        theNumberDataVectorInCluster++;
                        sum += distance.ElementAt(j).Min();
                        clusterContainDataVector = true;
                    }
                }
                if (clusterContainDataVector)
                {
                    result += sum / theNumberDataVectorInCluster;
                }
            }
            //todo
            return result / numberOfCluster;
        }
    }
}
