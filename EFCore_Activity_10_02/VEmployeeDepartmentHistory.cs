﻿using System;
using System.Collections.Generic;

namespace EFCore_Activity_10_02;

public partial class VEmployeeDepartmentHistory
{
    public int BusinessEntityId { get; set; }

    public string? Title { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Suffix { get; set; }

    public string Shift { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string GroupName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
