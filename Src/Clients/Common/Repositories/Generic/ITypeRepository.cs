namespace HumanResources.Common.Repositories.Generic
{
    public interface ITypeRepository<TResolve>
    {
        TResolve Result { get; }
        void Resolve();
    }
}