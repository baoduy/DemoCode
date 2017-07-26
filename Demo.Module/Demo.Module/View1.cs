using System.Collections.Generic;
using System.ComponentModel.Composition;
using HBD.Mef.WinForms;

namespace Demo.Module
{
    [Export]
    public partial class View1 : ViewBase
    {
        public override string Text => "View 1";

        public View1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.NavigationManager.NavigateTo(typeof(View2),
                new Dictionary<string, object> { ["Message"] = "Navigated from View 1" });
        }
    }
}