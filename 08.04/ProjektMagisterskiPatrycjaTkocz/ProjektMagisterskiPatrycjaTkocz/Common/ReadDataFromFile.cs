using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    class ReadDataFromFile
    {
        private string fileName;
        private double maxValue { get; set;}
        private double minValue { get; set;}
        private int dimension { get; set; }
        private double[] dataVector;

        public ReadDataFromFile(string name)
        {
            fileName = name;
        }

        public List<double[]> GetDataFromFile()
        {
            List<double[]> DataVectors = new List<double[]>();
            maxValue = minValue = 1;
            try
            {
                StreamReader file = new StreamReader(fileName);
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] data = line.Split(',');
                    int i = 0;
                    dataVector = new double[data.Count()];
                    foreach (string value in data)
                    {
                        dataVector[i] = Convert.ToDouble(value);
                        i++;
                    }

                    if (maxValue < dataVector.Max())
                    {
                        maxValue = dataVector.Max();
                    }
                    if (minValue > dataVector.Min())
                    {
                        minValue = dataVector.Min();
                    }
                    DataVectors.Add(dataVector);
                }
                dimension = dataVector.Count();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            return DataVectors;
        }

        public double GetMaxValue()
        {
            return maxValue;
        }

        public double GetMinValue()
        {
            return minValue;
        }

        public int GetDimension()
        {
            return dimension;
        }
    }
}
