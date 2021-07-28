using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace IRecommendGames.Model
{
    class Games
    {      
        public Dictionary<string, List<string>> Attributes = new Dictionary<string, List<string>>();

        public Games()
        {
            AttributesInitialize();
            LoadAttributes();
        }

        private void AttributesInitialize()
        {
            Attributes.Add("categories", new List<string>());
            Attributes.Add("developer", new List<string>());
            Attributes.Add("genres", new List<string>());
            Attributes.Add("platforms", new List<string>());
            Attributes.Add("publisher", new List<string>());
            Attributes.Add("steamspy_tags", new List<string>());
        }

        public void LoadAttributes()
        {
            try
            {
                string path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                path = path.Replace("Files\\Program", "Files\\attributes.json");

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    Attributes = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
                }
            }
            catch { }
        }

        public void SavePrefs(ObservableCollection<string> userPreferences)
        {
            string path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            path = path.Replace("Files\\Program", "Files\\");
            string json = JsonConvert.SerializeObject(userPreferences);

            File.WriteAllText(path + "UserPrefs.json", json);
        }
    }
}