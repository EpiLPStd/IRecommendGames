using System.ComponentModel;

namespace IRecommendGames.ViewModel.BaseClass
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChange(params string[] properties)
        {
            if (PropertyChanged != null)
            {
                foreach (var property in properties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }
    }
}