using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using SendSms.Core.Models;
using SendSms.EntityFramework;
using SendSms.Helpers;
using Windows.UI.Xaml.Controls;

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

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(DeleteTemplate);
                }

                return _deleteCommand;
            }
        }

        private async void DeleteTemplate()
        {
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Вы действительно хотите удалить шаблон?",
                Content = "Восстановление будет не возможным. Продолжить?",
                PrimaryButtonText = "Удалить",
                CloseButtonText = "Отмена"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (Selected?.Id != 0)
                {
                    using (var db = new SendSmsContext())
                    {
                        db.Templates.Remove(Selected);
                        db.SaveChanges();
                    }
                }
                Source.Remove(Selected);
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
                var data = await db.Templates.AsNoTracking().OrderBy(x => x.Title).ToListAsync();
                foreach (var item in data)
                {
                    Source.Add(item);
                }
            }
        }
    }
}
