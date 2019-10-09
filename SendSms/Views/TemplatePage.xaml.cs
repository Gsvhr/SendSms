
using SendSms.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SendSms.Views
{
    public sealed partial class TemplatePage : Page
    {
        public TemplateViewModel ViewModel { get; } = new TemplateViewModel();

        public TemplatePage()
        {
            InitializeComponent();
        }
    }
}
