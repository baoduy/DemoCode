using System.ComponentModel.Composition;
using System.Linq;
using HBD.Mef.WinForms;
using HBD.Mef.WinForms.Core;


namespace Demo.Module
{
    [Export]
    public partial class View2 : ViewBase
    {
        public override string Text => "View 2";

        public View2()
        {
            InitializeComponent();
        }

        public override void OnNavigatedTo(WinformNavigationContext navigationContext)
        {
            this.label2.Text = navigationContext.NavigationParameters.First().Value.ToString();
        }
    }
}