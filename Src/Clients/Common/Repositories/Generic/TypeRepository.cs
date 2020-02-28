using ApplicationGeneric.DependencyInjection;

namespace HumanResources.Common.Repositories.Generic
{
    public class TypeRepository<TResolve> : ResolvedRepository<ApplicationContainerModule, TResolve>,
        ITypeRepository<TResolve>
    {
        protected TypeRepository() : base(new ContainerConfig<ApplicationContainerModule>())
        {
        }

        public TypeRepository(ContainerConfig<ApplicationContainerModule> containerConfig) : base(containerConfig)
        {
        }

        public TResolve Result { get; private set; }

        public new void Resolve()
        {
            Result = base.Resolve();
        }
    }
}