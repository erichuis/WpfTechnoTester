using Domain.Models;
using System.ComponentModel.DataAnnotations;
using WpfTechnoTester.Services;

namespace WpfTechnoTester.ViewModels
{
    public class JournalEntryEditViewModel : ViewModelBase
    {
        private readonly IJournalEntryService _journalEntryService;
        private readonly JournalEntriesViewModel _journalEntriesViewModel;

        public JournalEntryEditViewModel(IJournalEntryService journalEntryService, JournalEntriesViewModel journalEntriesViewModel)
        {
            _journalEntryService = journalEntryService;
            _journalEntriesViewModel = journalEntriesViewModel;
            if(_journalEntriesViewModel.SelectedJournalEntry == null)
            {
                throw new ArgumentException("The selected journalEntry can not be null");
            }
            ValidationCtx = new ValidationContext(JournalEntry);
        }

        private JournalEntry JournalEntry
        {
            get
            {
                if (_journalEntriesViewModel.SelectedJournalEntry == null)
                {
                    throw new Exception("Selected journal entry is null"); 
                }
                return _journalEntriesViewModel.SelectedJournalEntry;
            }
        }

        public string Entry
        {
            get => JournalEntry.Entry;
            set
            {
                if (JournalEntry.Entry != value)
                {
                    JournalEntry.Entry = value;
                    OnPropertyChangedExt(nameof(Entry), value);
                }
            }
        }
        public DateTime? DateEntry
        {
            get => JournalEntry.DateEntry;
            set
            {
                if (JournalEntry.DateEntry != value)
                {
                    JournalEntry.DateEntry = value;
                    OnPropertyChangedExt(nameof(DateEntry), value);
                }
            }
        }
        public string Category
        {
            get => JournalEntry.Category;
            set
            {
                if (JournalEntry.Category != value)
                {
                    JournalEntry.Category = value;
                    OnPropertyChangedExt(nameof(Category), value);
                }
            }
        }
        protected override void DoAction()
        {
            if (JournalEntry.JournalEntryId != Guid.Empty)
            {
                var response = _journalEntryService.UpdateAsync(JournalEntry).GetAwaiter().GetResult();
                if (!response )
                {
                    return;
                    //Todo do logging and show error dialog...with logging ;-)
                }
            }
            else
            {
                var journalEntry = new JournalEntry()
                {
                    Entry = Entry,
                    DateEntry = DateEntry,
                    Category = Category
                };
    
                var response = _journalEntryService.CreateAsync(journalEntry).GetAwaiter().GetResult();
                if (response == null)
                {
                    return;
                    //Todo do logging and show error dialog...with logging ;-)
                }
            }

            ActionSucceeded = true;
        }
    }
}
