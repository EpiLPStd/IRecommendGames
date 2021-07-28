using System.Diagnostics;
using System.IO;

namespace IRecommendGames.Model
{
    class DatabaseFix
    {
        public string message = "Wciśnij przycisk 'Rozpocznij', aby przygotować \nlżejszą bazę danych oraz plik atrybutów.";

        public DatabaseFix() { }

        public void DoFixDatabase()
        {
            try
            {
                string bat = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                bat = bat.Replace("Files\\Program", "Files");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WorkingDirectory = bat;
                startInfo.FileName = "DatabaseFixOpen.bat";
                
                Process process = Process.Start(startInfo);
                process.Close();

                message = "Baza danych przefiltrowana, utworzono plik atrybutów.";
            }
            catch
            { 
                message = "Nie udało się utworzyć listy atrybutów.";
            }
        }
    }
}