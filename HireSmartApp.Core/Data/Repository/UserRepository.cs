using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Models.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;

using User = HireSmartApp.Core.Models.Domain.User;

namespace HireSmartApp.Core.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context) : base(context)
        {
            this.context = context;
        }       
    }
}
