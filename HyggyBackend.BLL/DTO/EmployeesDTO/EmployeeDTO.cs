namespace HyggyBackend.BLL.DTO.EmployeesDTO
{
	public abstract class EmployeeDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
	}
}
