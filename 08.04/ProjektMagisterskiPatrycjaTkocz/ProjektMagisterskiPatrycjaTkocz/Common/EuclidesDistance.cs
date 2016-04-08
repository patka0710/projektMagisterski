using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    public static class EuclidesDistance
    {
        public static double[] CalculateEuclidesDistance(double[] Zp, double[] Mi, int numberOfClasses, int dimension)
        {
            double[] result = new double[numberOfClasses];
            for (int i = 0; i < numberOfClasses; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < Zp.Length; ++j)
                {
                    sum += Math.Pow((Zp[j] - Mi[(i * dimension) + j]), 2);
                }
                result[i] = Math.Sqrt(sum);
            }
            return result;
        }
    }
}
