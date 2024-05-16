using HireSmartApp.Core.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireSmartApp.Core.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository? _userRepository = null;        
        private readonly DataContext _context;
        private IProgramRepository _programRepository;
        private IRoleRepository _roleRepository;
        private IQuestionRepository _questionRepository;
        //constructor
        public UnitOfWork(IUserRepository userRepository, DataContext context, IProgramRepository programRepository, IRoleRepository roleRepository, IQuestionRepository questionRepository)
        {
            _userRepository = userRepository;
            _context = context;
            _programRepository = programRepository;
            _roleRepository = roleRepository;
            _questionRepository = questionRepository;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);

        public IQuestionRepository QuestionRepository => _questionRepository ??= new QuestionRepository(_context);

        public IProgramRepository ProgramRepository => _programRepository ??= new ProgramRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
