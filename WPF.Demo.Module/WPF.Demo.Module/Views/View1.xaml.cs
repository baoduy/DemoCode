using System.ComponentModel.Composition;
using System.Windows.Controls;
using WPF.Demo.Module.ViewModels;

namespace WPF.Demo.Module
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class View1 : UserControl
    {
        public View1()
        {
            InitializeComponent();
        }

        [Import]
        public ViewModel1 Model { set { this.DataContext = value; } }
    }
}
