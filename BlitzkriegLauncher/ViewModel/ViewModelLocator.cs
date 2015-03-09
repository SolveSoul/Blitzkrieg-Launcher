using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace BlitzkriegLauncher.ViewModel
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<LauncherVM>();
        }

        public LauncherVM LauncherVM
        {
            get { return ServiceLocator.Current.GetInstance<LauncherVM>(); }
        }
    }
}