using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockUserRepo : MockRepo<User>, IUserRepo
    {
        #region mock datasource

        public MockUserRepo()
        {
            InsertSet(data.UsersCollection);
        }
        #endregion
    }
}
