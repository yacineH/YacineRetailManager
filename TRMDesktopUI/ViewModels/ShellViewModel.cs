using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel:Conductor<object>,IHandle<LogOnEvent>
    {
        //private LoginViewModel _loginVM;
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        //private SimpleContainer _container;

        public ShellViewModel(IEventAggregator events,SalesViewModel salesVM)
                              //SimpleContainer container)
        {
            //_loginVM = loginVM;
            _salesVM = salesVM;
            _events = events;
            //_container = container;

            _events.Subscribe(this);

            //parceque a chaque fois sellview generer on obtien
            //un nouveau LoginViewModel
            //ActivateItem(_loginVM);
            //ActivateItem(_container.GetInstance<LoginViewModel>());

            //remplacer par ca 
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            //_loginVM = _container.GetInstance<LoginViewModel>();
        }
    }
}
