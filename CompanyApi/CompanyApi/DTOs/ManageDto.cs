namespace CompanyApi.Dto;

public record ManageDto(Guid EmployeeId, int DepartmentId, DateOnly? Since);