using HireSmartApp.Core.Data.Repository.IRepository;
using HireSmartApp.Core.Models.Domain.Authorization;

namespace HireSmartApp.Core.Data.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly DataContext context;

        public RoleRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
        
    }
}