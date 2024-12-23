using Domain.Models;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WpfTechnoTester.Commands;
using WpfTechnoTester.ViewModels.Helpers;

namespace WpfTechnoTester.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
    public class ViewModelBase : ObservableObject, INotifyDataErrorInfo
    {
        public RelayCommand DoActionCommand => new((param) => DoAction(), (param) => CanDoAction());
        public RelayCommand CancelCommand => new((param) => CancelAction());

        public ViewModelBase() { }
        public ViewModelBase(string displayName)
        {
            DisplayName = displayName;
        }

        private readonly Dictionary<string, List<string>> _errors = new();

        //public Dictionary<string, List<string>> Errors => _errors;

        protected virtual void CancelAction()
        {
            IsCancelled = true;
        }

        protected virtual void DoAction()
        {
        }
        protected virtual bool CanDoAction()
        {
            return true;
        }

        public bool IsCancelled { get; private set; }
        protected bool ActionSucceeded { get; set; }

        public bool CanClose
        {
            get
            {
                return IsCancelled || ActionSucceeded;
            }
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.TryGetValue(propertyName??string.Empty, out var errors))
            {
                return errors;
            }
            return string.Empty;
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public string? DisplayName { get; set; }

        public bool HasErrors => _errors.Count > 0;


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        internal virtual void RaiseCanExecuteChange()
        {
        }

        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, 
            // public, instance property on this object. 
            if (propertyName != "Password" && TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                throw new Exception(msg);
                //else
                //    Debug.Fail(msg);
            }
        }

        protected void OnPropertyChangedExt(string propertyName, object value)
        {
            VerifyPropertyName(propertyName);
            OnPropertyChanged(propertyName);
            RemoveError(propertyName);
            RaiseCanExecuteChange();
            ValidateModel(nameof(propertyName), value);
        }

        protected virtual ValidationContext ValidationCtx { get;}

        protected void ValidateModel(string propertyName, object value)
        {
            ValidationCtx.MemberName = propertyName;

            var results = new List<ValidationResult>();

            // Perform validation
            if (!Validator.TryValidateProperty(value, ValidationCtx, results))
            {
                foreach (var result in results)
                {
                    AddError(propertyName, result.ErrorMessage ?? "An error fix this");
                }
            }
        }

        private void RemoveError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Clear();
            }
            _errors.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
