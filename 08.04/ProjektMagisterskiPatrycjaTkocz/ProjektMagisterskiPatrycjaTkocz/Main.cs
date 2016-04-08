using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    class Main
    {
        double minValue;
        double maxValue;
        ReadDataFromFile readDataFromFile;
        Parameters parameters;
        MainWindow mainWindow;
        Results results;

        public Main(MainWindow window, Parameters allParameters)
        {
            mainWindow = window;
            parameters = allParameters;
            readDataFromFile = new ReadDataFromFile(parameters.getNameFile());
        }

        public void Run()
        {
            List<double[]> dataVector = readDataFromFile.GetDataFromFile();

            maxValue = readDataFromFile.GetMaxValue();
            minValue = readDataFromFile.GetMinValue();

            parameters.setMaxValue(maxValue);
            parameters.setMinValue(minValue);
            parameters.setDimension(readDataFromFile.GetDimension());

            K_means k_means = new K_means(parameters, dataVector);
            results =  k_means.Run();
            //mainWindow.AppendListBox("K - średnich: ");
            //mainWindow.AppendListBox("Wartość funkcji oceny " + results.getFitnessFunctionK_means().ToString());
            mainWindow.AppendListBox("K-średnich: " + String.Format("{0:N2}",results.getFitnessFunctionK_means().ToString()));
            //results.getFitnessFunctionK_means();
            //results.getClusteringZK_means();

            PSOCluster pso_cluster = new PSOCluster(parameters, dataVector, results);
            pso_cluster.Run();
            //mainWindow.AppendListBox("PSO: ");
            //mainWindow.AppendListBox("Wartość funkcji oceny " + results.getFitnessFunctionPSO().ToString());
            mainWindow.AppendListBox("Hybryda:     " + String.Format("{0:N2}", results.getFitnessFunctionPSO().ToString()));
        }
    }
}
