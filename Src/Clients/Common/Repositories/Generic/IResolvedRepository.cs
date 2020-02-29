namespace HumanResources.Common.Repositories.Generic
{
    public interface IResolvedRepository<TResolve>
    {
        TResolve Resolve();
    }
}