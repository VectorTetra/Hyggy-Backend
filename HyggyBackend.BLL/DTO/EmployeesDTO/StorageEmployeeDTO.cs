namespace HyggyBackend.BLL.DTO.EmployeesDTO
{
	public class StorageEmployeeDTO : EmployeeDTO
	{
		// public virtual StorageDTO Storage { get; set; }
		public virtual MainStorageDto Storage { get; set; } = new MainStorageDto();

	}
}
