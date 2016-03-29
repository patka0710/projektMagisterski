using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gbest_PSO_Clustering
{
    class Particle
    {
        public double[] position;
        public double[] velocity;
        public double[] personalBestPosition;
        public double fitness;
        public double bestFitness;

        public Particle(double[] x, double[] v, double[] y, double fitness, double bestFitness)
        {
            this.position = new double[x.Length];
            x.CopyTo(this.position, 0);
            this.velocity = new double[v.Length];
            v.CopyTo(this.velocity, 0);
            this.personalBestPosition = new double[y.Length];
            y.CopyTo(this.personalBestPosition, 0);
            this.fitness = fitness;
            this.bestFitness = bestFitness;
        }
    }
}
