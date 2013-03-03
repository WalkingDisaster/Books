using System;

namespace Books.Data
{
    public class Contractor : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public Address Address { get; set; }
        public DateTime? StartedOn { get; set; }
        public ContractorStatus Status { get; set; }
    }
}