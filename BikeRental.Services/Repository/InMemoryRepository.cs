using BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Services.Repository
{
    public class InMemoryRepository<T> : IRepositoryService<T> where T : class, IEntity<Guid>
    {
        protected static List<T> _set = new List<T>();

        public virtual IQueryable<T> GetAllRecords()
        {
            return _set.AsQueryable<T>();
        }
        public virtual T GetSingle(Guid id)
        {

            var result = _set.FirstOrDefault(r => r.Id == id);

            return result; ;
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _set.AsQueryable<T>().Where(predicate);

            return query;
        }
        public virtual ServiceResult Add(T o)
        {
            _set.Add(o);

            return ServiceResult.CommonResults["OK"];
        }

        public virtual ServiceResult Delete(T obj)
        {
            var toDelete = _set.SingleOrDefault(r => r.Id == obj.Id);

            if (toDelete != null)
                _set.Remove(toDelete);

            return ServiceResult.CommonResults["OK"];
        }

        public virtual ServiceResult Edit(T obj)
        {
            var toUpdate = _set.SingleOrDefault(r => r.Id == obj.Id);

            return ServiceResult.CommonResults["OK"];
        }

        public ServiceResult Save()
        {
            return ServiceResult.CommonResults["OK"];
        }
    }
}
