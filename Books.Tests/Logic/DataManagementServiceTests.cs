using Books.Data;
using Books.Logic;
using NUnit.Framework;
using Rhino.Mocks;

namespace Books.Tests.Logic
{
    [TestFixture]
    public class Given_a_new_entity_when_I_add_the_entity_to_the_system : Specification<DataManagementService<Contractor>>
    {
        private Contractor _newContractor;
        private IRepository<Contractor> _contractorRepo;

        protected override DataManagementService<Contractor> Given()
        {
            _contractorRepo = MockRepository.GenerateStub<IRepository<Contractor>>();

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

            return new DataManagementService<Contractor>(_contractorRepo);
        }

        protected override void When()
        {
            Subject.Add(_newContractor);
        }

        [Test]
        public void Then_the_entity_should_be_written_to_the_database()
        {
            _contractorRepo.AssertWasCalled(r => r.Insert(_newContractor));
        }
    }

    [TestFixture]
    public class Given_an_existing_entity_when_I_get_it_by_id : Specification<DataManagementService<Contractor>>
    {
        private Contractor _result;
        private IRepository<Contractor> _contractorRepo;
        private Contractor _expected;

        private const int ContractorId = 9598654;

        protected override DataManagementService<Contractor> Given()
        {
            _contractorRepo = MockRepository.GenerateStub<IRepository<Contractor>>();
            _expected = new Contractor
                {
                    Id = ContractorId,
                    FirstName = "John",
                    LastName = "Adams",
                    SocialSecurityNumber = "987-65-4321",
                    Address = new Address
                        {
                            Street = "asd5fwer4",
                            City = "wo;ieruj",
                            State = "UI",
                            ZipCode = "98786"
                        }
                };
            _contractorRepo.Stub(r => r.Get(ContractorId)).Return(_expected);

            return new DataManagementService<Contractor>(_contractorRepo);
        }

        protected override void When()
        {
            _result = Subject.Get(ContractorId);
        }

        [Test]
        public void Then_the_entity_returned_should_be_the_expected_entity()
        {
            Assert.AreEqual(_expected, _result);
        }
    }

    [TestFixture]
    public class Given_an_existing_entity_when_I_save_changes : Specification<DataManagementService<Contractor>>
    {
        private IRepository<Contractor> _contractorRepo;
        private Contractor _theContractor;

        protected override DataManagementService<Contractor> Given()
        {
            _contractorRepo = MockRepository.GenerateStub<IRepository<Contractor>>();
            _theContractor = new Contractor
                {
                    Id = 489645165,
                    FirstName = "Thomas",
                    LastName = "Jefferson",
                    SocialSecurityNumber = "000-00-0001",
                    Address = new Address
                        {
                            Street = "oiuwfdskj",
                            City = "Washington DC",
                            State = "DC",
                            ZipCode = "00001"
                        }
                };

            return new DataManagementService<Contractor>(_contractorRepo);
        }

        protected override void When()
        {
            _theContractor.FirstName = "Alexander";
            _theContractor.LastName = "Hamilton";

            Subject.Save(_theContractor);
        }

        [Test]
        public void Then_the_updated_entity_should_have_been_saved()
        {
            _contractorRepo.AssertWasCalled(r => r.Update(_theContractor));
        }
    }
}