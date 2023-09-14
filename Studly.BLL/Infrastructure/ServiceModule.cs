using Ninject.Modules;
using Studly.Interfaces;
using Studly.Repositories;

namespace Studly.BLL.Infrastructure;

public class ServiceModule : NinjectModule
{
    private  readonly string _connectionString;

    public ServiceModule(string connectionString)
    {
        _connectionString = connectionString;
    }

    public override void Load()
    {
        Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(_connectionString);
    }
}