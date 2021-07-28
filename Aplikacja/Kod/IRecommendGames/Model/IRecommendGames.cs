using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace IRecommendGames.Model
{
    class IRecommendGames
    {
        public string message = "Wciśnij przycisk 'Rozpocznij', by wygenerować listę najlepszych gier.";

        public List<string> Text = new List<string>();

        public IRecommendGames() { }
        
        public void GetRecommendations()
        {            
            try
            {
                string bat = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                bat = bat.Replace("Files\\Program", "Files\\RecommendGames.bat");

                Process process = Process.Start(new ProcessStartInfo(bat)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });

                while ((message = process.StandardOutput.ReadLine()) != null)
                {
                    Text.Add(message);
                }

                for (int i = 0; i < Text.Count; i++)
                {
                    message += Text[i] + "\n";
                }

                process.Close();
            }
            catch
            {
                message = "Nie udało się utworzyć listy polecanych gier.";
            }
        }
    }
}