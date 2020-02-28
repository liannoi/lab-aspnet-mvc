using ApplicationGeneric.Services.BusinessServices;
using HumanResources.Common.Repositories.Generic;
using HumanResources.Domain.Entities;

namespace HumanResources.Common.Repositories
{
    public class EmployeePromotionRepository : TypeRepository<IBusinessService<EmployeePromotionEntity>>
    {
    }
}