using System;
using System.Collections.Generic;
using System.Linq;
using Books.Data;
using Books.Logic;
using Books.Tests.Extensions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Books.Tests.Logic
{
    [TestFixture]
    public class Given_a_new_contractor_when_saving_the_contractor : Specification<ContractorService>
    {
        private IRepository<Contractor> _contractorRepo;
        private Contractor _newContractor;
        private IDateService _dateService;
        private DateTime _firstDayOfCurrentWeek;

        protected override ContractorService Given()
        {
            _contractorRepo = MockRepository.GenerateStub<IRepository<Contractor>>();
            _dateService = MockRepository.GenerateStub<IDateService>();

            _newContractor = new Contractor
            {
                FirstName = "George",
                LastName = "Washington",
                SocialSecurityNumber = "123-45-6789",
                Address = new Address
                {
                    Street = "123 Fourth St",
                    City = "Five Palms",
                    State = "CA",
                    ZipCode = "67890"
                }
            };
            _firstDayOfCurrentWeek = "01/01/2001".ToDate();
            _dateService.Stub(s => s.FirstDayOfCurrentWeek()).Return(_firstDayOfCurrentWeek);

            return new ContractorService(_contractorRepo, _dateService);
        }

        protected override void When()
        {
            Subject.Add(_newContractor);
        }

        [Test]
        public void The_the_started_on_date_should_be_set_to_the_current_week_start_if_it_is_null()
        {
            Assert.AreEqual(_firstDayOfCurrentWeek, _newContractor.StartedOn);
        }
    }

    [TestFixture]
    public class Given_an_existing_contractor_when_saving_with_null_started_on : Specification<ContractorService>
    {
        private IRepository<Contractor> _contractorRepo;
        private Contractor _theContractor;

        protected override ContractorService Given()
        {
            _contractorRepo = MockRepository.GenerateStub<IRepository<Contractor>>();

            _theContractor = new Contractor
            {
                FirstName = "George",
                LastName = "Washington",
                SocialSecurityNumber = "123-45-6789",
                Address = new Address
                {
                    Street = "123 Fourth St",
                    City = "Five Palms",
                    State = "CA",
                    ZipCode = "67890"
                },
                StartedOn = "02/02/2002".ToDate()
            };

            return new ContractorService(_contractorRepo, MockRepository.GenerateStub<IDateService>());
        }

        protected override void When()
        {
            _theContractor.StartedOn = null;
        }

        [Test, ExpectedException(typeof(ValidationException))]
        public void Then_a_validation_exception_should_be_thrown()
        {
            Subject.Save(_theContractor);
        }
    }

    [TestFixture]
    public class Given_many_contractors_when_querying_for_a_list_of_active_contractors : Specification<ContractorService>
    {
        private IRepository<Contractor> _contractorRepo;
        private IDateService _dateService;
        private Contractor[] _contractors;
        private IEnumerable<Contractor> _result;

        protected override ContractorService Given()
        {
            _contractorRepo = MockRepository.GenerateStub<IRepository<Contractor>>();
            _contractors = new[]
                {
                    new Contractor { Status = ContractorStatus.Active}, 
                    new Contractor { Status = ContractorStatus.Active}, 
                    new Contractor { Status = ContractorStatus.Active}, 
                    new Contractor { Status = ContractorStatus.Inactive}, 
                    new Contractor { Status = ContractorStatus.Active}, 
                    new Contractor { Status = ContractorStatus.Active}, 
                    new Contractor { Status = ContractorStatus.Active}, 
                    new Contractor { Status = ContractorStatus.Active}, 
                };
            //_contractorRepo.Stub(r => r.Query(null))
                //.WhenCalled()
                           //.Callback((Func<Contractor, bool> p) => _contractors.AsQueryable().Where(p));


            return new ContractorService(_contractorRepo, MockRepository.GenerateStub<IDateService>());
        }

        protected override void When()
        {
            _result = Subject.GetActive();
        }

        [Test, Ignore]
        public void Then_the_list_of_contractors_returned_should_be_the_expected_list()
        {
            Assert.AreSame(_contractors, _result);
        }
    }
}