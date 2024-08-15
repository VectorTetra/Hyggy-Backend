namespace HyggyBackend.DAL.Entities.Employes
{
    public abstract class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public virtual Proffession Proffession { get; set; }
    }
}
