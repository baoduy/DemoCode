using HBD.WPF.Shell.ViewModels;
using System.ComponentModel.Composition;

namespace WPF.Demo.Module.ViewModels
{
    [Export]
    public class ViewModel1 : ViewModelBase
    {
        /// <summary>
        /// Set View tile and header.
        /// </summary>
        /// <param name="viewTitle"></param>
        /// <param name="viewHeader"></param>
        protected override void SetViewTitle(out string viewTitle, out string viewHeader)
        {
            viewTitle = "View 1";
            viewHeader = "The View 1";
        }
    }
}
