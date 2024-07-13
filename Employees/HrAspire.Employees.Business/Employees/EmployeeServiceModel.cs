﻿namespace HrAspire.Employees.Business.Employees;

public record EmployeeServiceModel(
    string Id,
    string FullName,
    DateOnly DateOfBirth,
    string? Department,
    string Position,
    DateTime CreatedOn);
