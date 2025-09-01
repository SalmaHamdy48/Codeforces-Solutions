using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using Microsoft.AspNetCore.Http;

namespace CompanyApi.Mapping;

public class ImageUrlResolver : IValueResolver<Employee, EmployeeDto, string?>
{
    private readonly IHttpContextAccessor _http;

    public ImageUrlResolver(IHttpContextAccessor http) => _http = http;

    public string? Resolve(Employee source, EmployeeDto destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(source.ImageFileName)) 
            return null;

        var request = _http.HttpContext?.Request;
        if (request is null) 
            return $"/employee/{source.ImageFileName}";

        var baseUrl = $"{request.Scheme}://{request.Host}";
        return $"{baseUrl}/employee/{source.ImageFileName}";
    }
}