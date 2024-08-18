using Microsoft.AspNetCore.Identity;

namespace HyggyBackend.DAL.Entities.Employes
{
    public abstract class Employee : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public virtual Proffession Proffession { get; set; }
    }
}
