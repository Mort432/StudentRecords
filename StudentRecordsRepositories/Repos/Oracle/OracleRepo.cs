using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    public abstract class OracleRepo<T> : IRepo<T> where T : Identifiable
    {
        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(T item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Select(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> SelectById(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
