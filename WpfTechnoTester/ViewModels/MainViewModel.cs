using System.ComponentModel;
using WpfTechnoTester.State;

namespace WpfTechnoTester.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly INavigator _navigator;

        public event PropertyChangedEventHandler? PropertyChanged;

        public INavigator Navigator
        {
            get { return _navigator; }
        }

        public MainViewModel(
            INavigator navigator)
        {
            _navigator = navigator;
            _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }

        //private bool CanRetrieve()
        //{
        //    return _authenticator.IsLoggedIn;
        //}

        ///// <summary>
        ///// currently works for add, update, delete
        ///// </summary>
        ///// <returns></returns>
        //private bool CanExecute()
        //{
        //    return string.IsNullOrEmpty(_title) &&
        //           string.IsNullOrEmpty(_description);
        //}


        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    //AddTodoItemCommand.OnCanExecuteChanged();
        //}
    }
}