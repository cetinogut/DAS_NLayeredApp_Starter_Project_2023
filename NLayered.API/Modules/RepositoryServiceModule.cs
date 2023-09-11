using Autofac;
using NLayered.Core.Repositories;
using NLayered.Core.Services;
using NLayered.Core.UnitOfWorks;
using NLayered.Repository.Repositories;
using NLayered.Repository.UnitOfWorks;
using NLayered.Repository;
using NLayered.Service.Mapping;
using NLayered.Service.Services;
using System.Reflection;
using NLayered.Caching;

namespace NLayered.API.Modules
{
    public class RepositoryServiceModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();



            var apiAssembly = Assembly.GetExecutingAssembly(); // burada isim yazarak da assembly alabilirdik ama içindek iclassları yazarak bu üç satırda assembly nameleri aldık
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //autofac InstancePerLifetimeScope --> .net core Scoped
            //autofac InstancePerDependency --> .net core Transient

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


             builder.RegisterType<ProductServiceWithCaching>().As<IProductService>(); // Caching yapabilmek için uyugn servici DI ekliyoruz. ProductService'i değil.

        }
    }
}
