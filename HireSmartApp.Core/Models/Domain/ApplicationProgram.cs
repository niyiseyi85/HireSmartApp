
namespace HireSmartApp.Core.Models.Domain
{
    public class ApplicationProgram : Entity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ProgramDescription { get; set; }       
        public List<User>? Users { get; set; }
    }   
}
