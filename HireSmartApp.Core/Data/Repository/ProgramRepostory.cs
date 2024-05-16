using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Models.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Data.Repository
{
    public class ProgramRepository : GenericRepository<ApplicationProgram>, IProgramRepository
    {
        private readonly DataContext context;

        public ProgramRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
