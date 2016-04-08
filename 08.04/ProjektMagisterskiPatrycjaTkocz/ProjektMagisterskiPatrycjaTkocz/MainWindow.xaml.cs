using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjektMagisterskiPatrycjaTkocz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker m_oBackgroundWorker = null;
        List<Data> Data { get; set; }
        public bool InvokeRequired { get; private set; }

        Parameters parameters = new Parameters();
        public MainWindow()
        {
            Data = new List<Data>();

            Data.Add(new Data("iris.data"));
            Data.Add(new Data("wine.data"));
            Data.Add(new Data("test.data"));
            InitializeComponent();
            comboBox.ItemsSource = Data;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox.Text = comboBox.SelectedValue.ToString();
            listBoxResults.Items.Clear();
            //Main main = new Main(comboBox.SelectedValue.ToString());
        }

        private void run_Click(object sender, RoutedEventArgs e)
        {
            parameters.setNumberOfCluster(int.Parse(numberOfCluster.Text.ToString()));
            parameters.setNameFile(comboBox.SelectedValue.ToString());
            parameters.setMaxIterationK_means(int.Parse(numberOfIteration.Text.ToString()));
            parameters.setMaxIterationPSO(int.Parse(numberOfIterationPSO.Text.ToString()));
            parameters.setNumberOfParticles(int.Parse(numberOfParticles.Text.ToString()));

            if (null == m_oBackgroundWorker)
            {
                m_oBackgroundWorker = new BackgroundWorker();
                m_oBackgroundWorker.DoWork +=
                    new DoWorkEventHandler(m_oBackgroundWorker_DoWork);
                m_oBackgroundWorker.RunWorkerCompleted +=
                    new RunWorkerCompletedEventHandler(
                    m_oBackgroundWorker_RunWorkerCompleted);
                //m_oBackgroundWorker.ProgressChanged +=
                    //new ProgressChangedEventHandler(m_oBackgroundWorker_ProgressChanged);
                m_oBackgroundWorker.WorkerReportsProgress = true;
                m_oBackgroundWorker.WorkerSupportsCancellation = true;
            }
            //listBoxResults.Items.Add("Uruchomiono");
            m_oBackgroundWorker.RunWorkerAsync();

        }

        private void m_oBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //listBoxResults.Items.Add("Przerwano");
            }
            else
            {
                //listBoxResults.Items.Add("Zakończono");
            }
        }

        private void m_oBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            Main main = new Main(this, parameters);
            main.Run();
        }

        public void AppendListBox(string value)
        {
            Action<string> workMethod = (i) => listBoxResults.Items.Add(value);
            listBoxResults.Dispatcher.BeginInvoke(DispatcherPriority.Background, workMethod, value);
        }

    }
}
