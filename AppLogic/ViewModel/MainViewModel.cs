using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using AppLogic.Services;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.ComponentModel;

namespace AppLogic.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private bool _changeProvided;
        private IFileService _fileService;
        private User _user;

        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get { return _user; } set { _user = value; RemoveCommand.RaiseCanExecuteChanged();} }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }


        public MainViewModel(IFileService fileService)
        {
            _fileService = fileService;
            
            

            SaveCommand = new RelayCommand(
                () => Save(),
                () => !AnyValueHasErrors() && _changeProvided);

            CancelCommand = new RelayCommand(
                () => Cancel(), 
                () => _changeProvided);

            AddCommand = new RelayCommand(
                () => Add());

            RemoveCommand = new RelayCommand(
                () => Remove(), 
                () => AnyUserSelected());

            Loadusers();
            _changeProvided = false;

        }

        private void Loadusers()
        {
            Users = new ObservableCollection<User>();
            Users.CollectionChanged += Users_CollectionChanged;

            foreach (var user in _fileService.LoadAllUsers())
            {
                Users.Add(user);
            }

        }

        private void Users_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
                }
            }
            _changeProvided = true;
            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _changeProvided = true;
            Trace.WriteLine("Value of property has changed");
            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }

        private void Cancel()
        {
            Loadusers();

            _changeProvided = false;
            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            _fileService.SaveToFile(Users.ToList());

            _changeProvided = false;
            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }

        private void Add()
        {
            Users.Add(new User());


            _changeProvided = true;

            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }

        private void Remove()
        {
            Users.Remove(SelectedUser);


            _changeProvided = true;

            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }


        private bool AnyValueHasErrors()
        {
            Trace.WriteLine(Users.Any(x => x.HasErrors));
            return Users.Any(x => x.HasErrors);
        }

        private bool AnyUserSelected()
            => SelectedUser == null ? false : true;

    }
}
