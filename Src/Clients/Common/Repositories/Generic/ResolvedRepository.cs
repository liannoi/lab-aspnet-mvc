using ApplicationGeneric.DependencyInjection.Core;
using Autofac;
using Autofac.Core;

namespace HumanResources.Common.Repositories.Generic
{
    public class ResolvedRepository<TModule, TResolve> : IResolvedRepository<TResolve> where TModule : IModule, new()
    {
        private readonly ContainerConfig<TModule> _containerConfig;

        protected ResolvedRepository(ContainerConfig<TModule> containerConfig)
        {
            _containerConfig = containerConfig;
        }

        public TResolve Resolve()
        {
            return _containerConfig.Container.Resolve<TResolve>();
        }
    }
}