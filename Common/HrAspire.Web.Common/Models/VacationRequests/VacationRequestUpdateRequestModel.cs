﻿namespace HrAspire.Web.Common.Models.VacationRequests;

using System.ComponentModel.DataAnnotations;

using HrAspire.Vacations.Data.Models;

public class VacationRequestUpdateRequestModel
{
    [Required]
    public VacationRequestType? Type { get; set; }

    // TODO: validate from date < to date + from date >= today
    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public string? Notes { get; set; }
}