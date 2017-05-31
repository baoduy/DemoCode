#region using

using System.Collections.Generic;
using System.ComponentModel.Composition;
using HBD.Mef.Modularity;
using HBD.Mef.Shell;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.Shell.Services;
using HBD.Mef.WinForms.Modularity;

#endregion

namespace Demo.Module
{
    [PluginExport(typeof(StartupDemoModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StartupDemoModule : WinFormModuleBase
    {
        protected override IEnumerable<IViewInfo> GetStartUpViewTypes()
        {
            yield return new ViewInfo(typeof(View1));
        }

        protected override void MenuConfiguration(IShellMenuService menuSet)
        {
            //Add Main Menu for this module.
            menuSet.Menu("Demo")
                .WithIcon(Resources.DemoIcon)
                .WithToolTip("This is demo menu.")
                .Children
                //Add Navigation for View1
                    .AddNavigation("View 1")
                    .For(new ViewInfo(typeof(View1)))
                    .AndNavigation("View 2")
                    .For(new ViewInfo(typeof(View2)));
        }
    }
}