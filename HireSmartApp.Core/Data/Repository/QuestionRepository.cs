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
    public class QuestionRepository : GenericRepository<Questions>, IQuestionRepository
    {
        private readonly DataContext context;

        public QuestionRepository(DataContext context) : base(context)
        {
            this.context = context;
        }        
    }
}
