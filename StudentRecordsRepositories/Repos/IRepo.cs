using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    //Declares basic functionality that all repositories must declare.
    public interface IRepo<T> where T : Identifiable
    {
        Task<T> SelectById(object id);
        Task<IEnumerable<T>> SelectAll();
        //Select based on a given predicate.
        Task<IEnumerable<T>> Select(Expression<Func<T, bool>> predicate);
        void Insert(T item);
        void Delete(T item);
        void Update(T item);
    }
}
