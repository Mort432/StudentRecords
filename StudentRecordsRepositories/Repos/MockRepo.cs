using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockRepo<T> : IRepo<T> where T : Identifiable
    {
        public MockRepo()
        {
        }

        List<T> dataProvider = new List<T>();
        int dataProviderIdentityTracker = 1;

        public void Delete(T item)
        {
            dataProvider.Remove(item);
        }

        public void Insert(T item)
        {
            item.Id = dataProviderIdentityTracker++;
            dataProvider.Add(item);
        }

        public async Task<IEnumerable<T>> Select(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(dataProvider.Where(predicate.Compile()));
        }

        public async Task<IEnumerable<T>> SelectAll()
        {
            return await Task.FromResult(dataProvider);
        }

        public async Task<T> SelectById(object id)
        {
            return await Task.FromResult(dataProvider.Where(x => x.Id == id).SingleOrDefault());
        }

        public void Update(T item)
        {
            dataProvider.Where(t => t.Id == item.Id)
                .Select(x => { x = item; return x; })
                .ToList();
        }
    }
}
