﻿namespace HrAspire.Employees.Business.Documents;

public record DocumentDetailsServiceModel(
    int Id,
    string Title,
    string? Description,
    string FileName,
    string CreatedById,
    string CreatedByFullName,
    DateTime CreatedOn);
