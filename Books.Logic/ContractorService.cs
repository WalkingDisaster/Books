using System;
using System.Collections.Generic;
using Books.Data;

namespace Books.Logic
{
    public class ContractorService : DataManagementService<Contractor>
    {
        private readonly IDateService _dateService;

        public ContractorService(IRepository<Contractor> repository, IDateService dateService)
            : base(repository)
        {
            _dateService = dateService;
        }

        public override Contractor BeforeAdding(Contractor toAdd)
        {
            if (toAdd.StartedOn == null)
                toAdd.StartedOn = _dateService.FirstDayOfCurrentWeek();
            return toAdd;
        }

        protected override void Validate(Contractor toValidate, ValidationType validationType)
        {
            if (toValidate.StartedOn == null)
                throw new ValidationException("Start date is required for a contractor.");
        }

        public IEnumerable<Contractor> GetActive()
        {
            var result = Query(c => c.Status == ContractorStatus.Active);
            return result;
        }
    }
}