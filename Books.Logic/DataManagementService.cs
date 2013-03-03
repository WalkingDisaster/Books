using System;
using System.Collections.Generic;
using Books.Data;

namespace Books.Logic
{
    public class DataManagementService<T>
        where T : Entity
    {
        private readonly IRepository<T> _repository;

        public DataManagementService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Get(int id)
        {
            return _repository.Get(id);
        }

        public void Add(T toAdd)
        {
            toAdd = BeforeAdding(toAdd);
            Validate(toAdd, ValidationType.Insert);
            _repository.Insert(toAdd);
        }

        public void Save(T toSave)
        {
            Validate(toSave, ValidationType.Update);
            _repository.Update(toSave);
        }

        protected IEnumerable<T> Query(Func<T, bool> predicate)
        {
            return _repository.Query(predicate);
        }

        public virtual T BeforeAdding(T toAdd)
        {
            return toAdd;
        }

        protected virtual void Validate(T toValidate, ValidationType validationType)
        {
            // hook
        }
    }
}