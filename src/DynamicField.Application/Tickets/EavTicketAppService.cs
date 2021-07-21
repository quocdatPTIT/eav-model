using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Uow;
using Abp.ObjectMapping;
using DynamicField.EAV;
using DynamicField.EntityFrameworkCore;
using DynamicField.Tickets.Models.Request;

namespace DynamicField.Tickets
{
    public class EavTicketAppService : DynamicFieldAppServiceBase, IEavTicketAppService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<EavAttribute> _eavAttributeRepository;
        private readonly IRepository<EavEntityType> _eavEntityTypeRepository;
        private readonly IRepository<EavAttributeOption> _eavAttributeOptionRepository;
        private readonly IRepository<EavAttributeOptionValue> _eavAttributeOptionValueRepository;

        public EavTicketAppService(
            IObjectMapper objectMapper,
            IRepository<EavAttribute> eavAttributeRepository,
            IRepository<EavEntityType> eavEntityTypeRepository,
            IRepository<EavAttributeOption> eavAttributeOptionRepository,
            IRepository<EavAttributeOptionValue> eavAttributeOptionValueRepository)
        {
            _objectMapper = objectMapper;
            _eavAttributeRepository = eavAttributeRepository;
            _eavEntityTypeRepository = eavEntityTypeRepository;
            _eavAttributeOptionRepository = eavAttributeOptionRepository;
            _eavAttributeOptionValueRepository = eavAttributeOptionValueRepository;
        }

        public async Task<bool> CreateAttribute(CreateAttributeReq res)
        {
            var optionTypes = new List<string> {"DROPDOWN", "MULTISELECT", "CHECKBOX"};

            // Add attribute
            var entityTypeFromRepo =
                await _eavEntityTypeRepository.FirstOrDefaultAsync(e => e.EntityTypeCode == res.EntityTypeCode);

            if (entityTypeFromRepo is null)
                return await Task.FromResult(false);
        
            
            var newAttribute = _objectMapper.Map<EavAttribute>(res);
            newAttribute.EavEntityTypeId = entityTypeFromRepo.Id;
            newAttribute.EavEntityType = entityTypeFromRepo;

            var attributeId = await _eavAttributeRepository.InsertAndGetIdAsync(newAttribute);

            // Add options
            if (!optionTypes.Contains(res.BackendType)) return await Task.FromResult(true);
            
            var option = new EavAttributeOption {AttributeId = attributeId, SortOrder = res.Option.SortOrder};
            var optionId = await _eavAttributeOptionRepository.InsertAndGetIdAsync(option);

            var optionValues = new List<EavAttributeOptionValue>();
            
            res.Option.OptionValues.ForEach(item =>
            {
                optionValues.Add(new EavAttributeOptionValue
                {
                    AttributeOptionId = optionId,
                    Value = item
                });
            });
            
            var dbContext = await CurrentUnitOfWork.GetDbContextAsync<DynamicFieldDbContext>();
            await dbContext.EavAttributeOptionValues.AddRangeAsync(optionValues);
            
            return await Task.FromResult(true);
        }
    }
}