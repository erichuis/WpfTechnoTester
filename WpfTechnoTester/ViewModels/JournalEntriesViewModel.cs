using Domain.Models;
using System.Collections.ObjectModel;
using WpfTechnoTester.Commands;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.ViewModels
{
    public class JournalEntriesViewModel : ViewModelBase
    {
        private readonly IJournalEntryService _journalEntryService;
        private readonly IWindowService _windowService;

        public ObservableCollection<JournalEntry> SelectedJournalEntries { get; set; }
        public ObservableCollection<JournalEntry> JournalEntries { get; set; }
        public JournalEntry? SelectedJournalEntry { get; set; }
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand LoadCommand { get; }

        public JournalEntriesViewModel(IJournalEntryService todoItemService,
            IWindowService windowService)
        {
            JournalEntries = [];
            SelectedJournalEntries = [];
            _journalEntryService = todoItemService;
            _windowService = windowService;

            AddCommand = new RelayCommand((param) => AddItem());
            DeleteCommand = new RelayCommand((param) => DeleteItem());
            EditCommand = new RelayCommand((param) => EditItem());
            LoadCommand = new RelayCommand((param) => RetrieveItems());
        }
      

        private async void RetrieveItems()
        {
            JournalEntries?.Clear();

            var items = await _journalEntryService.GetAllAsync();
            foreach (var item in items) JournalEntries?.Add(item);
        }

        private void AddItem()
        {
            SelectedJournalEntry = new JournalEntry() { Entry = string.Empty, Category = string.Empty };
            var dialogResult = _windowService.ShowJournalEntryEditViewDialog(SelectedJournalEntry);

            if (!dialogResult)
                return;

            JournalEntries.Add(SelectedJournalEntry);
            //RetrieveJournalEntrys();
        }

        private void EditItem()
        {
            if (SelectedJournalEntry == null)
                return;

            _windowService.ShowJournalEntryEditViewDialog(SelectedJournalEntry!);
            RetrieveItems();
        }

        private async void DeleteItem()
        {
            if (SelectedJournalEntries.Count == 0)
            {
                return;
            }

            var itemsToDelete = SelectedJournalEntries.ToList();

            foreach (var item in itemsToDelete)
            {
                await _journalEntryService.DeleteAsync(item.JournalEntryId);
                JournalEntries.Remove(item);
            }
        }
    }
}
