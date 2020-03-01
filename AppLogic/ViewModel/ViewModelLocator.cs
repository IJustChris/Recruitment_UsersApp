using AppLogic.IoC;

namespace AppLogic.ViewModel
{
   
    public class ViewModelLocator
    {    
        public ViewModelLocator()
        {
        }

        public MainViewModel Main => Bootstrapper.GetInstance<MainViewModel>();
    }
}