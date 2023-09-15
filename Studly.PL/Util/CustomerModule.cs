using Ninject.Modules;
using Studly.BLL.Interfaces;
using Studly.BLL.Services;

namespace Studly.PL.Util
{
    public class CustomerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomerService>().To<CustomerService>();
        }
    }
}
