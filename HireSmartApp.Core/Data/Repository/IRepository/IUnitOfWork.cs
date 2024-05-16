using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IProgramRepository ProgramRepository { get; }

        Task SaveAsync();
    }
}
