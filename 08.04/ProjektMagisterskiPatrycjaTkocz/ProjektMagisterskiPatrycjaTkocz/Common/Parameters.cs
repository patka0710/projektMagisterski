using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    class Parameters
    {
        private int numberOfCluster { get; set;}
        private int numberOfIterationInKmeans {get;set;}
        private string nameFile { get; set; }
        private double maxValue { get; set; }
        private double minValue { get; set; }
        private int dimension { get; set; }
        private int maxIterationK_means { get; set; }
        private int maxIterationPSO { get; set; }
        private int numberOfParticles { get; set; }

        public Parameters()
        {
        }

        public int getNumberOfCluster()
        {
            return numberOfCluster;
        }

        public void setNumberOfCluster(int value)
        {
            numberOfCluster = value;
        }

        public string getNameFile()
        {
            return nameFile;
        }

        public void setNameFile(string value)
        {
            nameFile = value;
        }

        public double getMaxValue()
        {
            return maxValue;
        }

        public void setMaxValue(double value)
        {
            maxValue = value;
        }

        public double getMinValue()
        {
            return minValue;
        }

        public void setMinValue(double value)
        {
            minValue = value;
        }

        public int getDimension()
        {
            return dimension;
        }

        public void setDimension(int value)
        {
            dimension = value;
        }

        public int getMaxIterationK_means()
        {
            return maxIterationK_means;
        }

        public void setMaxIterationK_means(int value)
        {
            maxIterationK_means = value;
        }

        public int getMaxIterationPSO()
        {
            return maxIterationPSO;
        }

        public void setMaxIterationPSO(int value)
        {
            maxIterationPSO = value;
        }

        public int getNumberOfParticles()
        {
            return numberOfParticles;
        }

        public void setNumberOfParticles(int value)
        {
            numberOfParticles = value;
        }
    }
}
