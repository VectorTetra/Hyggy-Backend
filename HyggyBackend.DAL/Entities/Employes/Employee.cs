using Microsoft.AspNetCore.Identity;

namespace HyggyBackend.DAL.Entities.Employes
{
    public abstract class Employee : User
    {
        public long Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual Proffession Proffession { get; set; }
    }
}
