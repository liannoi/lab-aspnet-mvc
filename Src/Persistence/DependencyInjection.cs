using System;
using System.Data.Entity;
using ApplicationGeneric.Services.DataServices;
using ApplicationGeneric.Services.Infrastructure.Helpers;
using ApplicationGeneric.Services.Infrastructure.Initializers;
using ApplicationGeneric.Services.Infrastructure.Keys;
using Autofac;

namespace HumanResources.Persistence
{
    /// <summary>
    ///     Dependency injection at the Persistence level.
    /// </summary>
    public sealed class PersistenceContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Inject(builder, typeof(HumanResourcesDbContext), typeof(DbContext));

            InjectWithOneKeyAttribute<Employee>(builder, typeof(BaseDataService<Employee>),
                typeof(IDataService<Employee>), nameof(Employee.EmployeeId));

            InjectWithOneKeyAttribute<EmpPromotion>(builder, typeof(BaseDataService<EmpPromotion>),
                typeof(IDataService<EmpPromotion>), nameof(EmpPromotion.EmpPromotionId));

            InjectWithOneKeyAttribute<JobTitle>(builder, typeof(BaseDataService<JobTitle>),
                typeof(IDataService<JobTitle>), nameof(JobTitle.JobTitleId));
        }

        private void InjectWithOneKeyAttribute<TEntity>(ContainerBuilder builder, Type registerType, Type asType,
            string propertyName) where TEntity : class, new()
        {
            Inject(builder, registerType, asType, new BaseDataServiceInitializer<TEntity>
            {
                TypeTools = new TypeTools<TEntity>
                {
                    FirstKeyAttribute = new EntityKeyAttribute
                    {
                        PropertyName = propertyName
                    }
                }
            });
        }

        private void Inject(ContainerBuilder builder, Type registerType, Type asType)
        {
            builder.RegisterType(registerType).As(asType);
        }

        private void Inject<TEntity>(ContainerBuilder builder, Type registerType, Type asType,
            IDataServiceInitializer<TEntity> dataServiceInitializer) where TEntity : class, new()
        {
            builder.RegisterType(registerType).As(asType).WithParameter(dataServiceInitializer.ParameterName,
                dataServiceInitializer.TypeTools);
        }
    }
}