using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    interface IK_means
    {
        void RandomlyInitializeMeans();
        void RandomlyDataVectorZpAddToCluster();
        bool UpdateMeans();
        bool UpdateClustering();
        double CalculateFitnessFunction();
    }
}
