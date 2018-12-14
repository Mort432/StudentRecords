using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mock
{
    public class MockRepo<T> : IRepo<T> where T : Identifiable
    {
        public MockRepo()
        {
        }

        //Use MockDatabase.
        protected MockDatabase data = new MockDatabase();

        //Internal item list.
        public List<T> Items = new List<T>();
        //Identity sequence for insert IDs
        public int mockIdentityTracker = 1;

        //Base delete.
        public void Delete(T item)
        {
            Items.Remove(item);
        }

        //Base insert.
        public void Insert(T item)
        {
            item.Id = mockIdentityTracker++;
            Items.Add(item);
        }

        //Base insert set.
        public void InsertSet(IEnumerable<T> items)
        {
            foreach(T item in items)
            {
                Insert(item);
            }
        }

        //Base select from predicate.
        public async Task<IEnumerable<T>> Select(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(Items.Where(predicate.Compile()));
        }

        //Base select all.
        public async Task<IEnumerable<T>> SelectAll()
        {
            return await Task.FromResult(Items);
        }

        //Base select by Id.
        public async Task<T> SelectById(object id)
        {
            return await Task.FromResult(Items.Where(x => x.Id.Equals(id)).SingleOrDefault());
        }

        //Base update.
        public void Update(T item)
        {
            int index = Items.IndexOf(Items.Where(x => x.Id.Equals(item.Id)).FirstOrDefault());
            Items[index] = item;
        }
    }
}
