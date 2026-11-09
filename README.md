# vbnet-rwanda-payroll

A VB.NET class library that computes a Rwandan monthly payslip: PAYE income tax across the RRA bands, RSSB pension and maternity contributions, CBHI (Mutuelle) on the net before CBHI, final take-home pay and total employer cost.

Rwanda-specific but the shape is general enough to lift to Kenya, Uganda or Tanzania by swapping the bands and rates.

## The rules it encodes

PAYE on monthly taxable income:

| Band (RWF)             | Rate |
|------------------------|------|
| 0 to 60 000            | 0 %  |
| 60 001 to 100 000      | 10 % |
| 100 001 to 200 000     | 20 % |
| above 200 000          | 30 % |

Each band taxes only the slice of income that falls inside it.

Statutory contributions, employee and employer:

- RSSB pension: 6 % + 8 % of basic salary
- RSSB maternity: 0.3 % + 0.3 % of basic salary
- CBHI: 0.5 % of net before CBHI (employee only)

## Use

```vb
Imports RwandaPayroll

Dim p = PayslipCalculator.Compute(
    New PayrollInput(employeeName:="Uwase", basicSalary:=500000D, transportAllowance:=30000D))

Console.WriteLine($"Gross: {p.GrossSalary:N0}")
Console.WriteLine($"PAYE:  {p.Paye:N0}")
Console.WriteLine($"Net:   {p.NetSalary:N0}")
Console.WriteLine($"Employer cost: {p.EmployerCost:N0}")
```

## Build and test

```bash
dotnet build
dotnet test
```

Tests cover each PAYE band individually, cumulative tax at band boundaries, a full payslip walkthrough with every statutory deduction, and the invariant that net salary cannot be negative.

## Why VB.NET

A lot of small East African accounting shops still run on VB code. Making the payroll rules legible to those developers is more useful than hiding them inside a service they can't read.
