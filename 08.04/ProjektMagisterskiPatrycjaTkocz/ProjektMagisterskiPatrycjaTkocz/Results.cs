using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    class Results
    {
        private int[] clusteringZK_means;
        private double[] meansK_means;
        private double fitnessFunctionK_means { get; set;}

        private int[] clusteringZPSO;
        private double[] meansPSO;
        private double fitnessFunctionPSO { get; set; }

        public Results()
        { }

        public void setClusteringZK_means(int[] value)
        {
            clusteringZK_means = value;
        }

        public int[] getClusteringZK_means()
        {
            return clusteringZK_means;
        }

        public void setMeansK_means(double[] value)
        {
            meansK_means = value;
        }

        public double[] getMeansK_means()
        {
            return meansK_means;
        }

        public void setFitnessFunctionK_means(double value)
        {
            fitnessFunctionK_means = value;
        }

        public double getFitnessFunctionK_means()
        {
            return fitnessFunctionK_means;
        }
        //
        public void setClusteringZPSO(int[] value)
        {
            clusteringZPSO = value;
        }

        public int[] getClusteringZPSO()
        {
            return clusteringZPSO;
        }

        public void setMeansPSO(double[] value)
        {
            meansPSO = value;
        }

        public double[] getMeansPSO()
        {
            return meansPSO;
        }

        public void setFitnessFunctionPSO(double value)
        {
            fitnessFunctionPSO = value;
        }

        public double getFitnessFunctionPSO()
        {
            return fitnessFunctionPSO;
        }
    }
}
