using Autofac;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Öncelikle Assembl ler alınır.


            //Üzerinde çalıştığın katmanın assembly sini al
            var apiAssembly = Assembly.GetExecutingAssembly();


            //AppDbContext.cs classının bulunduğu katmanın Assembly sini al
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));


            //CategoryService.cs classının bulunduğu katmanın Assembly sini al
            var serviceAssembly = Assembly.GetAssembly(typeof(CategoryService));


            //Assembly leri tara ve "Repository" ile biten class ve Interfaceleri ekle (InstancePerLifetimeScope program.cs de svope a karşılık gelir.) 
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //Assembly leri tara ve "Service" ile biten class ve Interfaceleri ekle (InstancePerLifetimeScope program.cs de svope a karşılık gelir.) 
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            //GenericRepository.cs ve IGenericRepository interfaceyi ekle
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            //Service.cs ve IService interfaceyi ekle
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            //UnitOfWork.cs ve IUnitOfWork interfaceyi ekle
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
        }
    }
}
