using MongoDB.Driver;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mongo
{
    public class MongoModuleRunRepo : MongoRepo<ModuleRun>, IModuleRunRepo
    {
        protected override IMongoCollection<ModuleRun> Collection => ModuleRuns;
    }
}
