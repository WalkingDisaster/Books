using NUnit.Framework;

namespace Books.Tests.Logic
{
    public abstract class Specification<T>
    {
        protected T Subject { get; private set; }
        
        [SetUp]
        public void SetUp()
        {
            Subject = Given();
            When();
        }

        protected abstract T Given();
        protected abstract void When();
    }
}