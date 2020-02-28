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
            var persistenceContainer = new ContainerConfig<PersistenceContainerModule>();

            Inject(builder,
                typeof(BaseBusinessService<JobTitle, JobTitleEntity>),
                typeof(IBusinessService<JobTitleEntity>), new BaseBusinessServiceInitializer
                {
                    MapperConfiguration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddExpressionMapping();
                        cfg.CreateMap<JobTitle, JobTitleEntity>()
                            .ForMember(dest => dest.Name, member => member.MapFrom(map => map.NameJobTitle));
                        cfg.CreateMap<JobTitleEntity, JobTitle>();
                    }).CreateMapper()
                },
                persistenceContainer.Container.Resolve<IDataService<JobTitle>>());

            Inject(builder,
                typeof(BaseBusinessService<EmpPromotion, EmployeePromotionEntity>),
                typeof(IBusinessService<EmployeePromotionEntity>), new BaseBusinessServiceInitializer
                {
                    MapperConfiguration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddExpressionMapping();
                        cfg.CreateMap<EmpPromotion, EmployeePromotionEntity>()
                            .ForMember(dest => dest.EntityId,
                                member => member.MapFrom(map => map.EmpPromotionId.ToString()))
                            .ForMember(dest => dest.JobTitle, member => member.MapFrom(map => map.JobTitle))
                            .ForMember(dest => dest.Employee, member => member.MapFrom(map => map.Employee))
                            .ForMember(dest => dest.HireDate, member => member.MapFrom(map => map.HireDate))
                            .ForMember(dest => dest.EmployeeId,
                                member => member.MapFrom(map => map.Employee.EmployeeId))
                            .ForMember(dest => dest.JobTitleId,
                                member => member.MapFrom(map => map.JobTitle.JobTitleId))
                            .ForMember(dest => dest.Salary, member => member.MapFrom(map => map.Salary));
                        cfg.CreateMap<EmployeePromotionEntity, EmpPromotion>();
                    }).CreateMapper()
                },
                persistenceContainer.Container.Resolve<IDataService<EmpPromotion>>());

            Inject(builder,
                typeof(BaseBusinessService<Employee, EmployeeEntity>),
                typeof(IBusinessService<EmployeeEntity>), new BaseBusinessServiceInitializer
                {
                    MapperConfiguration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddExpressionMapping();
                        cfg.CreateMap<Employee, EmployeeEntity>()
                            .ForMember(dest => dest.EntityId, member => member.MapFrom(map => map.EmployeeId))
                            .ForMember(dest => dest.Inn, member => member.MapFrom(map => map.INN));
                        cfg.CreateMap<EmployeeEntity, Employee>();
                    }).CreateMapper()
                },
                persistenceContainer.Container.Resolve<IDataService<Employee>>());
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