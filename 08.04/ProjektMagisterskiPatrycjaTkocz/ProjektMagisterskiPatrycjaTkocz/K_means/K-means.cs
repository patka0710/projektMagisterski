using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektMagisterskiPatrycjaTkocz
{
    class K_means: IK_means
    {
        Parameters parameters;
        int numberOfCluster;
        List<double[]> Z;
        int dimension;
        double min;
        double max;
        int[] clusteringZ;
        double[] means;
        int maxIteration;
        Results result;


        public K_means(Parameters allParameters, List<double[]> dataVector)
        {
            parameters = allParameters;
            numberOfCluster =  parameters.getNumberOfCluster();
            Z = dataVector;
            dimension = parameters.getDimension();
            min = parameters.getMinValue();
            max = parameters.getMaxValue();
            maxIteration = parameters.getMaxIterationK_means();

            clusteringZ = new int[Z.Count];
            means = new double[dimension * numberOfCluster];
            result = new Results();
        }


        public void RandomlyInitializeMeans()
        {
            Random rnd = new Random();

            for (int i = 0; i < means.Length; i++)
            {
                //TODO może losować double zamiast intów ???
                means[i] = rnd.Next((int)min, (int)max);
            }
        }

        public void RandomlyDataVectorZpAddToCluster()
        {
            Random rnd = new Random();

            for (int i = 0; i < clusteringZ.Length; i++)
            {
                clusteringZ[i] = rnd.Next(0, numberOfCluster);
            }
        }

        public bool UpdateMeans()
        {
            int[] numberOfElemenstsInCluster = new int[numberOfCluster];

            for (int i = 0; i < Z.Count; ++i)
            {
                int cluster = clusteringZ[i];
                ++numberOfElemenstsInCluster[cluster];
            }

            for (int i = 0; i < numberOfCluster; i++)
            {
                if (numberOfElemenstsInCluster[i] == 0)
                {
                    return false;
                }

            }

            for (int i = 0; i < numberOfCluster; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    double sum = 0.0;
                    int counterZp = 0;

                    foreach (var Zp in Z)
                    {
                        if (clusteringZ[counterZp] == i)
                        {
                            sum += Zp[j];
                        }
                        counterZp++;
                    }
                    if (sum != 0.0)
                    {
                        means[(dimension * i) + j] = sum / numberOfElemenstsInCluster[i];
                    }
                }

            }
            return true;
        }

        public bool UpdateClustering()
        {
            int result = 0;
            double[] temporaryClustering = new double[Z.Count];
            clusteringZ.CopyTo(temporaryClustering, 0);

            int counterDataVector = 0;
            foreach (var Zp in Z)
            {
                double[] distance = EuclidesDistance.CalculateEuclidesDistance(Zp, means, numberOfCluster, dimension);

                double minDistance = distance.Min();
                for (int j = 0; j < distance.Length; j++)
                {
                    if (distance[j] == minDistance)
                    {
                        clusteringZ[counterDataVector] = j;
                        break;
                    }
                }
                counterDataVector++;
            }

            for (int i = 0; i < clusteringZ.Length; i++)
            {
                if (clusteringZ[i] != temporaryClustering[i])
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

        public double CalculateFitnessFunction()
        {
            int counterDataVector = 0;
            List<double[]> listConitainEuclidesDistanceForOneParticle = new List<double[]>();
            foreach (var Zp in Z)
            {
                double[] result = EuclidesDistance.CalculateEuclidesDistance(Zp, means, numberOfCluster, dimension);
                listConitainEuclidesDistanceForOneParticle.Add(result);

                double minDistance = result.Min();
                for (int l = 0; l < result.Length; l++)
                {
                    if (result[l] == minDistance)
                    {
                        clusteringZ[counterDataVector] = l;
                        break;
                    }
                }
                counterDataVector++;
            }

            return FitnessFunction.CalculateFitnessFunction(listConitainEuclidesDistanceForOneParticle, clusteringZ, numberOfCluster);
        }

        public Results Run()
        {
            int counter = 0;
            bool successUpdateMeans = true;
            bool changedInUpdateClustering = true;

            RandomlyInitializeMeans();
            RandomlyDataVectorZpAddToCluster();

            while (successUpdateMeans == true && changedInUpdateClustering && counter < maxIteration)
            {
                successUpdateMeans = UpdateMeans();
                changedInUpdateClustering = UpdateClustering();
                counter++;
            }

            result.setClusteringZK_means(clusteringZ);
            result.setMeansK_means(means);
            result.setFitnessFunctionK_means(CalculateFitnessFunction());

            //MessageBox.Show(CalculateFitnessFunction().ToString());
            return result;
        }

    }
}
