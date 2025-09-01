namespace CompanyApi.Dto;

public record DependentDto(int Id, string D_Name, string? Gender, string Relationship, Guid EmployeeId);