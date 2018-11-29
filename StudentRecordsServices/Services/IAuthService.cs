using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;

namespace StudentRecordsServices.Services
{
    public interface IAuthService
    {
        User authorisedUser { get; set; }
    }
}
