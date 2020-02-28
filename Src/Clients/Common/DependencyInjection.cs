using System;
using ApplicationGeneric.DependencyInjection;
using ApplicationGeneric.Services.BusinessServices;
using ApplicationGeneric.Services.DataServices;
using ApplicationGeneric.Services.Infrastructure.Initializers;
using Autofac;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using HumanResources.Domain.Entities;
using HumanResources.Persistence;

namespace HumanResources.Common
{
    /// <summary>
    ///     Dependency injection at the Presentation level.
    /// </summary>
    public sealed class ApplicationContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var persistanceContainer = new ContainerConfig<PersistenceContainerModule>();

            Inject(builder,
                typeof(BaseBusinessService<Employee, EmployeeEntity>),
                typeof(IBusinessService<EmployeeEntity>), new BaseBusinessServiceInitializer
                {
                    MapperConfiguration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddExpressionMapping();
                        cfg.CreateMap<Employee, EmployeeEntity>()
                            .ForMember(nameof(EmployeeEntity.EntityId), o => o.MapFrom(s => s.EmployeeId))
                            .ForMember(nameof(EmployeeEntity.Inn), o => o.MapFrom(s => s.INN));
                        cfg.CreateMap<EmployeeEntity, Employee>();
                    }).CreateMapper()
                },
                persistanceContainer.Container.Resolve<IDataService<Employee>>());

            Inject(builder,
                typeof(BaseBusinessService<EmpPromotion, EmployeePromotionEntity>),
                typeof(IBusinessService<EmployeePromotionEntity>), new BaseBusinessServiceInitializer
                {
                    MapperConfiguration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddExpressionMapping();
                        cfg.CreateMap<EmpPromotion, EmployeePromotionEntity>()
                            .ForMember(nameof(EmployeePromotionEntity.EntityId),
                                o => o.MapFrom(s => s.EmpPromotionId.ToString()))
                            .ForMember(nameof(EmployeePromotionEntity.JobTitle.Name),
                                o => o.MapFrom(s => s.JobTitle.NameJobTitle))
                            .ForMember(nameof(EmployeePromotionEntity.EmployeeId),
                                o => o.MapFrom(s => s.EmployeeId.ToString()))
                            .ForMember(nameof(EmployeePromotionEntity.Salary),
                                o => o.MapFrom(s => Convert.ToInt32(s.Salary)));
                        cfg.CreateMap<EmployeePromotionEntity, EmpPromotion>();
                    }).CreateMapper()
                },
                persistanceContainer.Container.Resolve<IDataService<EmpPromotion>>());

            Inject(builder,
                typeof(BaseBusinessService<JobTitle, JobTitleEntity>),
                typeof(IBusinessService<JobTitleEntity>), new BaseBusinessServiceInitializer
                {
                    MapperConfiguration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddExpressionMapping();
                        cfg.CreateMap<JobTitle, JobTitleEntity>()
                            .ForMember(nameof(JobTitleEntity), o => o.MapFrom(s => s.NameJobTitle));
                        cfg.CreateMap<JobTitleEntity, JobTitle>();
                    }).CreateMapper()
                },
                persistanceContainer.Container.Resolve<IDataService<JobTitle>>());
        }

        private void Inject<TEntity>(ContainerBuilder builder, Type registerType, Type asType,
            IBusinessServiceInitializer businessServiceInitializer, IDataService<TEntity> dataService)
            where TEntity : class, new()
        {
            builder.RegisterType(registerType).As(asType)
                .WithParameter(businessServiceInitializer.ParameterName, businessServiceInitializer.MapperConfiguration)
                .WithParameter("dataService", dataService);
        }
    }
}