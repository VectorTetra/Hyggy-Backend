namespace HyggyBackend.BLL.DTO.EmployeesDTO
{
	public abstract class EmployeeDTO
	{
        public string? Id { get; set; } 
        public string? Email { get; set; }
        public string? Name { get; set; } 
		public string? Surname { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? AvatarPath { get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? RoleName { get; set; }
    }
}
