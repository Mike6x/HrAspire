﻿namespace HrAspire.Employees.Business.Employees;

using HrAspire.Business.Common;

public interface IEmployeesService
{
    Task<IEnumerable<EmployeeServiceModel>> ListAsync(string currentEmployeeId, int pageNumber, int pageSize);

    Task<IEnumerable<EmployeeServiceModel>> ListManagersAsync();

    Task<IEnumerable<EmployeeServiceModel>> ListManagedEmployeesAsync(string managerId);

    Task<int> GetCountAsync(string currentEmployeeId);

    Task<EmployeeDetailsServiceModel?> GetAsync(string id);

    Task<ServiceResult<string>> CreateAsync(
        string email,
        string password,
        string fullName,
        DateOnly dateOfBirth,
        string position,
        string? department,
        string? managerId,
        string role,
        string createdById);

    Task<ServiceResult> UpdateAsync(
        string id,
        string fullName,
        DateOnly dateOfBirth,
        string position,
        string? department,
        string role,
        string? managerId);

    Task<ServiceResult> DeleteAsync(string id);
}
