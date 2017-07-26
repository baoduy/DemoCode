using HBD.Mef.Shell.Services;
using System.ComponentModel.Composition;
using HBD.Mef.Shell.Navigation;
using System.Collections.Generic;
using HBD.WPF.Shell.Modularity;
using HBD.Mef.Shell;
using HBD.WPF.Shell;

namespace WPF.Demo.Module
{
    [Prism.Mef.Modularity.ModuleExport(typeof(DemoStartup))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DemoStartup : WpfModuleBase
    {
        /// <summary>
        /// The list of ViewType will be open automatically when app started.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IViewInfo> GetStartUpViewTypes()
        {
            yield return new ViewInfo(typeof(View1));
            yield break;
        }

        /// <summary>
        /// The Main menu configuration.
        /// </summary>
        /// <param name="menuSet"></param>
        protected override void MenuConfiguration(IShellMenuService menuSet)
        {
            base.MenuConfiguration(menuSet);

            menuSet.Menu("Demo")
                .Children
                    .AddNavigation("View 1")
                    .For<View1>();

        }
    }
}
