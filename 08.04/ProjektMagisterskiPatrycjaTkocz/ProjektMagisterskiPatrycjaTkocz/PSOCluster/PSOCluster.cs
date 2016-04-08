using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    class PSOCluster : IPSOCluster
    {
        //bool hybrid = false;
        Parameters parameters;
        int numberOfCluster;
        List<double[]> Z;
        int dimension;
        double min;
        double max;
        int[] clusteringZ_PSO;
        //double[] means;
        int maxIteration;
        Results result;
        double[] meansK_means;
        Particle[] particles;
        double[] bestGlobalPosition;
        double bestGlobalFitness;
        int numberOfParticles;

        /* public PSOCluster(Parameters allParameters, List<double[]> dataVector)
         {
             result = new Results();
         }*/

        public PSOCluster(Parameters allParameters, List<double[]> dataVector, Results results)
        {
            parameters = allParameters;
            Z = dataVector;
            result = results;
            numberOfCluster = parameters.getNumberOfCluster();
            dimension = parameters.getDimension();
            min = parameters.getMinValue();
            max = parameters.getMaxValue();
            clusteringZ_PSO = new int[Z.Count];
            numberOfParticles = parameters.getNumberOfParticles();
            meansK_means = result.getMeansK_means();
            particles = new Particle[numberOfParticles];
            //hybrid = true;
            bestGlobalPosition = new double[numberOfCluster * dimension];
            maxIteration = parameters.getMaxIterationPSO();
            bestGlobalFitness = double.MaxValue;

        }

        public void RandomlyInitializeSwarm()
        {
            Random rnd = new Random();
            double firstFitness = 0.0;
            for (int i = 0; i < particles.Length; ++i)
            {
                double[] randomPosition = new double[numberOfCluster * dimension];
                if (i == 0)
                {
                    meansK_means.CopyTo(randomPosition, 0);
                    firstFitness = result.getFitnessFunctionK_means();
                }
                else
                {
                    for (int j = 0; j < randomPosition.Length; ++j)
                    {
                        randomPosition[j] = rnd.NextDouble() * (max - min) + min;
                    }
                }
                double[] randomVelocity = new double[numberOfCluster * dimension];
                for (int j = 0; j < randomVelocity.Length; ++j)
                {
                    double lo = min * 0.1;
                    double hi = max * 0.1;
                    randomVelocity[j] = rnd.NextDouble() * (hi - lo) + lo;
                }

                particles[i] = new Particle(randomPosition, randomVelocity, randomPosition, firstFitness, firstFitness);
            }
        }

        public void UpdatePosition(Particle currentParticle)
        {
            double[] updatePosition = new double[dimension * numberOfCluster];

            for (int j = 0; j < currentParticle.position.Length; j++)
            {
                updatePosition[j] = currentParticle.position[j] + currentParticle.velocity[j];
                if (updatePosition[j] < min)
                {
                    updatePosition[j] = min;
                }
                else if (updatePosition[j] > max)
                {
                    updatePosition[j] = max;
                }
            }
            updatePosition.CopyTo(currentParticle.position, 0);
        }

        public void UpdateVelocity(Particle currentParticle)
        {
            double w = 0.729;
            double c1 = 1.49445;
            double c2 = 1.49445;
            double r1, r2;
            Random rnd = new Random(0);
            double[] updateVelocity = new double[dimension * numberOfCluster];

            for (int j = 0; j < currentParticle.velocity.Length; j++)
            {
                r1 = rnd.NextDouble();
                r2 = rnd.NextDouble();

                updateVelocity[j] = (w * currentParticle.velocity[j]) +
                  (c1 * r1 * (currentParticle.personalBestPosition[j] - currentParticle.position[j])) +
                  (c2 * r2 * (bestGlobalPosition[j] - currentParticle.position[j]));
            }
            updateVelocity.CopyTo(currentParticle.velocity, 0);
        }

        public Results Run()
        {
            RandomlyInitializeSwarm();
            int[] temporaryClusterZp = new int[Z.Count];

            for (int t = 0; t < maxIteration; t++)
            {
                for (int i = 0; i < numberOfParticles; i++)
                {

                    Particle currentParticle = particles[i];

                    int counterDataVector = 0;
                    List<double[]> listConitainEuclidesDistanceForOneParticle = new List<double[]>();
                    foreach (var Zp in Z)
                    {
                        double[] result = EuclidesDistance.CalculateEuclidesDistance(Zp, currentParticle.position, numberOfCluster, dimension);
                        listConitainEuclidesDistanceForOneParticle.Add(result);

                        double minDistance = result.Min();
                        for (int l = 0; l < result.Length; l++)
                        {
                            if (result[l] == minDistance)
                            {
                                temporaryClusterZp[counterDataVector] = l;
                                break;
                            }
                        }
                        counterDataVector++;
                    }

                    currentParticle.fitness = FitnessFunction.CalculateFitnessFunction(listConitainEuclidesDistanceForOneParticle, temporaryClusterZp, numberOfCluster);

                    if (currentParticle.fitness < currentParticle.bestFitness)
                    {
                        currentParticle.position.CopyTo(currentParticle.personalBestPosition, 0);
                        currentParticle.bestFitness = currentParticle.fitness;
                    }

                    if (currentParticle.fitness < bestGlobalFitness)
                    {
                        currentParticle.position.CopyTo(bestGlobalPosition, 0);
                        bestGlobalFitness = currentParticle.fitness;
                        temporaryClusterZp.CopyTo(clusteringZ_PSO, 0);
                    }

                    UpdateVelocity(currentParticle);
                    UpdatePosition(currentParticle);
                }
            }

            result.setClusteringZPSO(clusteringZ_PSO);
            result.setFitnessFunctionPSO(bestGlobalFitness);
            result.setMeansPSO(bestGlobalPosition);
            return result;
        }
    }
}
