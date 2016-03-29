using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gbest_PSO_Clustering
{
    class Swarm
    {
        List<double[]> Z;
        int dimension;
        int swarm;
        int clusterCount;
        double min;
        double max;
        int maxIteration;

        Particle[] particles;
        public double[] bestGlobalPosition;
        double bestGlobalFitness;
        public double[] clusterZp;
        double[] positionMeans;


        public Swarm(List<double[]> Z, int swarm, int dimension, int clusterCount, double min, double max, int maxIteration, double[] positionMeans)
        {
            this.Z = Z;
            this.dimension = dimension;
            this.swarm = swarm;
            this.clusterCount = clusterCount;
            this.min = min;
            this.max = max;
            this.maxIteration = maxIteration;
            this.positionMeans = positionMeans;
            particles = new Particle[swarm];
            bestGlobalPosition = new double[clusterCount * dimension];
            bestGlobalFitness = double.MaxValue;
            clusterZp = new double[Z.Count];
            
        }

        private double test()
        {
            int counterDataVector = 0;
            List<double[]> listConitainEuclidesDistanceForOneParticle = new List<double[]>();
            foreach (var Zp in Z)
            {
                double[] result = EuclidesDistance(Zp, positionMeans);
                listConitainEuclidesDistanceForOneParticle.Add(result);

                double minDistance = result.Min();
                for (int l = 0; l < result.Length; l++)
                {
                    if (result[l] == minDistance)
                    {
                        clusterZp[counterDataVector] = l;
                        break;
                    }
                }
                counterDataVector++;
            }

            return fitnessFunction(listConitainEuclidesDistanceForOneParticle, clusterZp);
        }


        private void InitSwarm()
        {
            Random rnd = new Random();
            double firstFitness = 0.0;
            for (int i = 0; i < particles.Length; ++i)
            {
                double[] randomPosition = new double[clusterCount * dimension];
                if (i == 0)
                {
                    positionMeans.CopyTo(randomPosition, 0);
                    firstFitness = test();
                }
                else
                {
                    for (int j = 0; j < randomPosition.Length; ++j)
                    {
                        randomPosition[j] = rnd.NextDouble() * (max - min) + min;
                    }
                }
                double[] randomVelocity = new double[clusterCount * dimension];
                for (int j = 0; j < randomVelocity.Length; ++j)
                {
                    double lo = min * 0.1;
                    double hi = max * 0.1;
                    randomVelocity[j] = rnd.NextDouble() * (hi - lo) + lo;
                }

                particles[i] = new Particle(randomPosition, randomVelocity, randomPosition, firstFitness, firstFitness);
            }
        }

        private double[] EuclidesDistance(double[] Zp, double[] Mi)
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

        private double fitnessFunction(List<double[]> distance, double[] clusterZp)
        {
            double result = 0.0;

            for (int i = 0; i < clusterCount; i++)
            {
                bool clusterContainDataVector = false;
                int theNumberDataVectorInCluster = 0;
                double sum = 0.0;
                for (int j = 0; j < clusterZp.Length; j++)
                {
                    if (clusterZp[j] == i)
                    {
                        theNumberDataVectorInCluster++;
                        sum += distance.ElementAt(j).Min(); // => particles.Min());
                        clusterContainDataVector = true;
                    }
                }
                if (clusterContainDataVector)
                {
                    result += sum / theNumberDataVectorInCluster;
                }
            }
            //todo
            return result / clusterCount;
        }


        private void UpdateVelocity(Particle currentParticle)
        {
            double w = 0.729;
            double c1 = 1.49445;
            double c2 = 1.49445;
            double r1, r2;
            Random rnd = new Random(0);
            double[] updateVelocity = new double[dimension * clusterCount];

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

        private void UpdatePosition(Particle currentParticle)
        {
            double[] updatePosition = new double[dimension * clusterCount];

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

        private void Solve()
        {
            double[] temporaryClusterZp = new double[Z.Count];
           // clusterZp.CopyTo(bestClusterZp, 0); //zapamiętanie grupowania Zp przez K-means;

            for (int t = 0; t < maxIteration; t++)
            {
                for (int i = 0; i < swarm; i++)
                {

                    Particle currentParticle = particles[i];

                    int counterDataVector = 0;
                    List<double[]> listConitainEuclidesDistanceForOneParticle = new List<double[]>();
                    foreach (var Zp in Z)
                    {
                        double[] result = EuclidesDistance(Zp, currentParticle.position);
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

                    currentParticle.fitness = fitnessFunction(listConitainEuclidesDistanceForOneParticle, temporaryClusterZp);
                    //Console.WriteLine(currentParticle.fitness);

                    //todo update global best position and locla best position //for particle
                    if (currentParticle.fitness < currentParticle.bestFitness)
                    {//??
                        currentParticle.position.CopyTo(currentParticle.personalBestPosition, 0);
                        currentParticle.bestFitness = currentParticle.fitness;
                    }

                    if (currentParticle.fitness < bestGlobalFitness)
                    {//??
                        currentParticle.position.CopyTo(bestGlobalPosition, 0);
                        bestGlobalFitness = currentParticle.fitness;
                        temporaryClusterZp.CopyTo(clusterZp, 0);
                    }

                    //todo update the cluster centroid za pomocą wzorów
                    UpdateVelocity(currentParticle);
                    UpdatePosition(currentParticle);
                }
            }
        }

        public void Result()
        {
            InitSwarm();
            Solve();
        }


    }
}
