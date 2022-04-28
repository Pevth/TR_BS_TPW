using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ModelAPI ModelAPI { get; set; } = ModelAPI.CreateApi();
        private int _numberOfBalls = 5;
        private Task updating;
        public ObservableCollection<BallModel> Balls { get; set; }

        public ICommand ButtonClick { get; set; }

        public int numberOfBalls
        {
            get
            {
                return _numberOfBalls;
            }
            set
            {
                if (value.Equals(_numberOfBalls))
                    return;
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

        public void UpdatePosition()
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

            }
        }

        private void ClickHandler()
        {
            ModelAPI.createBalls(5);
            ModelAPI.moveBalls();

            updating = new Task(UpdatePosition);
            updating.Start();

        }

    }
}
