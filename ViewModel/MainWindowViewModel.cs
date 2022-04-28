using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;


namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ModelAPI ModelAPI { get; set; } = ModelAPI.CreateApi();
        private string _numberOfBalls;
        private Task updating;

        public ObservableCollection<BallModel> Balls { get; set; }

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
            Balls = new ObservableCollection<BallModel>();
            ButtonClick = new RelayCommand(() => ClickHandler());
        }

        public void UpdateBalls()
        {
            while (true)
            {
                ObservableCollection<BallModel> treadList = new ObservableCollection<BallModel>();
                foreach (BallModel ball in ModelAPI.ListOfBallModel)
                {
                    treadList.Add(ball);
                }

                Balls = treadList;
                RaisePropertyChanged(nameof(Balls));
                Thread.Sleep(10);
            }
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
            ModelAPI.createBalls(readNumberOfBalls());
            ModelAPI.moveBalls();

            updating = new Task(UpdateBalls);
            updating.Start();

        }

    }
}
