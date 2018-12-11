﻿using StudentRecordsModels.Models;
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

        protected MockDatabase data = new MockDatabase();

        public List<T> Items = new List<T>();
        public int mockIdentityTracker = 1;

        public void Delete(T item)
        {
            Items.Remove(item);
        }

        public void Insert(T item)
        {
            item.Id = mockIdentityTracker++;
            Items.Add(item);
        }

        public void InsertSet(IEnumerable<T> items)
        {
            foreach(T item in items)
            {
                Insert(item);
            }
        }

        public async Task<IEnumerable<T>> Select(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(Items.Where(predicate.Compile()));
        }

        public async Task<IEnumerable<T>> SelectAll()
        {
            return await Task.FromResult(Items);
        }

        public async Task<T> SelectById(object id)
        {
            return await Task.FromResult(Items.Where(x => x.Id.Equals(id)).SingleOrDefault());
        }

        public void Update(T item)
        {
            int index = Items.IndexOf(Items.Where(x => x.Id.Equals(item.Id)).FirstOrDefault());
            Items[index] = item;
        }
    }
}
