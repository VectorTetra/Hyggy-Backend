namespace HyggyBackend.BLL.DTO.EmployeesDTO
{
	public abstract class EmployeeDTO
	{
       // public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? Name { get; set; } = string.Empty;
		public string? Surname { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
        public long ProfessionId { get; set; }
    }
}
