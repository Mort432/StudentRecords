using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mongo
{
    //Declares the behaviour any Mongo Repo must implement.
    public abstract class MongoRepo<T> : IRepo<T> where T : Identifiable
    {
        public MongoRepo()
        {
            //Allows mapping from MongoDB camel case property names to POCO variables
            ConventionRegistry.Register("camelCase", new ConventionPack { new CamelCaseElementNameConvention() }, t => true);
        }

        //Declares connection, database and collections
        private IMongoDatabase Database { get; } = new MongoClient("mongodb://localhost:27017").GetDatabase("student-records");
        protected IMongoCollection<Assignment> Assignments => Database.GetCollection<Assignment>("assignments");
        protected IMongoCollection<Course> Courses => Database.GetCollection<Course>("courses");
        protected IMongoCollection<Module> Modules => Database.GetCollection<Module>("modules");
        protected IMongoCollection<Result> Results => Database.GetCollection<Result>("results");
        protected IMongoCollection<ModuleRun> ModuleRuns => Database.GetCollection<ModuleRun>("moduleruns");
        protected IMongoCollection<User> Users => Database.GetCollection<User>("users");

        protected abstract IMongoCollection<T> Collection { get; }

        //Functionality
        public async Task<IEnumerable<T>> Select(Expression<Func<T, bool>> predicate)
        {
            //Base select from predicate
            return (await SelectAll()).Where(predicate.Compile()).ToList();
        }

        public async Task<IEnumerable<T>> SelectAll()
        {
            //Base select
            return await Task.FromResult(Collection.AsQueryable().ToList());
        }

        public async Task<T> SelectById(object id)
        {
            //Base select by ID
            return (await SelectAll()).Where(x => x.Id.Equals(id)).ToList().FirstOrDefault();
        }

        public virtual async void Update(T item)
        {
            //Base update
            var itemIdFilter = Builders<T>.Filter.Eq(i => i.Id, item.Id);
            var options = new FindOneAndReplaceOptions<T> { ReturnDocument = ReturnDocument.After };

            await Collection.FindOneAndReplaceAsync(itemIdFilter, item, options);
        }

        public virtual async void Delete(T item)
        {
            //Base delete
            var itemIdFilter = Builders<T>.Filter.Eq(i => i.Id, item.Id);

            await Collection.DeleteOneAsync(itemIdFilter);
        }

        public virtual async void Insert(T item)
        {
            //Base insert
            if (item.Id == null) item.Id = ObjectId.GenerateNewId();

            await Collection.InsertOneAsync(item);
        }
    }
}
