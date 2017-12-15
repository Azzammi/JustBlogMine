using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

using JustBlog.Core.Objects;
using NHibernate.Tool.hbm2ddl;

namespace JustBlog.Core.Repository
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>().ToMethod(
                    e => Fluently.Configure()
                         //Setting the database connection string
                         .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c =>
                            c.FromConnectionStringWithKey("JustBlogDbConnString")))
                         //Setting a provider to cache the queries (cache)
                         .Cache(c => c.UseQueryCache().ProviderClass<HashtableCacheProvider>())
                         //Specifying the assembly where the domain & mapping classes exists (mappings)
                         .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Post>())
                         //Ask NHibernate to create tables from te classes (Expose Configuration)
                         .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false))
                         .BuildConfiguration()
                         .BuildSessionFactory()
                )
                .InSingletonScope();

            Bind<ISession>()
                .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }
    }
}
