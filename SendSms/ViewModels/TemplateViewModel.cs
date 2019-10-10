
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SendSms.Core.Models;
using SendSms.EntityFramework;
using SendSms.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SendSms.ViewModels
{
    public class TemplateViewModel : Observable
    {
        public ObservableCollection<Template> Source { get; } = new ObservableCollection<Template>();

        private Template _selected;
        public Template Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(AddTemplate);
                }

                return _addCommand;
            }
        }

        private void AddTemplate()
        {
            var t = new Template();
            Source.Add(t);
            Selected = t;
        }

        public TemplateViewModel()
        {
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            using (var db = new SendSmsContext())
            {
                var data = await db.Templates.AsNoTracking().OrderByDescending(x => x.Title).ToListAsync();
                foreach (var item in data)
                {
                    Source.Add(item);
                }
            }
        }
    }
}
