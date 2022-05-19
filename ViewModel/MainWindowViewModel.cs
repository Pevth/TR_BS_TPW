using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;
using System;
using System.Threading.Tasks;


namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase  ///tu jak cos zmienic na inotify
    {
        private ModelAPI ModelAPI { get; set; } = ModelAPI.CreateApi();
        private string _numberOfBalls;

        public ObservableCollection<IBall> Balls { get; set; }

        public ICommand ButtonClick { get; set; }


        public string numberOfBalls
        {
            get
            {
                return _numberOfBalls;
            }
            set
            {
                _numberOfBalls = value;
                RaisePropertyChanged("numberOfBalls");
            }
        }

        public MainWindowViewModel() : this(ModelAPI.CreateApi()) { }

        public MainWindowViewModel(ModelAPI modelAPI)
        {
            ModelAPI = modelAPI;
            Balls = new ObservableCollection<IBall>();
            ButtonClick = new RelayCommand(() => ClickHandler());
            IDisposable observer = ModelAPI.Subscribe(x => Balls.Add(x));
        }



        public int readNumberOfBalls()
        {
            int value;
            if (Int32.TryParse(numberOfBalls, out value))
            {
                value = Int32.Parse(numberOfBalls);
                return value;
            }
            return 0;
        }

        private void ClickHandler()
        {
            ModelAPI.OKLetsGo(readNumberOfBalls());
        }

    }
}
