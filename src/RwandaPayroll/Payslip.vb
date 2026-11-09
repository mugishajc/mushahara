Namespace RwandaPayroll

    Public Structure PayrollInput
        Public ReadOnly EmployeeName As String
        Public ReadOnly BasicSalary As Decimal
        Public ReadOnly TransportAllowance As Decimal
        Public ReadOnly HousingAllowance As Decimal
        Public ReadOnly OtherTaxableAllowances As Decimal

        Public Sub New(
            ByVal employeeName As String,
            ByVal basicSalary As Decimal,
            Optional ByVal transportAllowance As Decimal = 0D,
            Optional ByVal housingAllowance As Decimal = 0D,
            Optional ByVal otherTaxableAllowances As Decimal = 0D)

            Me.EmployeeName = employeeName
            Me.BasicSalary = basicSalary
            Me.TransportAllowance = transportAllowance
            Me.HousingAllowance = housingAllowance
            Me.OtherTaxableAllowances = otherTaxableAllowances
        End Sub
    End Structure

    Public Structure Payslip
        Public EmployeeName As String
        Public GrossSalary As Decimal
        Public Paye As Decimal
        Public PensionEmployee As Decimal
        Public PensionEmployer As Decimal
        Public MaternityEmployee As Decimal
        Public MaternityEmployer As Decimal
        Public Cbhi As Decimal
        Public NetSalary As Decimal
        Public EmployerCost As Decimal
    End Structure

    ''' <summary>
    ''' Builds a payslip from a PayrollInput, in the order mandated by RRA and RSSB:
    ''' gross then PAYE then RSSB pension and maternity (employee side), then CBHI
    ''' on the net before CBHI, then final net.
    ''' </summary>
    Public Module PayslipCalculator

        Public Function Compute(ByVal input As PayrollInput) As Payslip
            Dim gross As Decimal = input.BasicSalary +
                                  input.TransportAllowance +
                                  input.HousingAllowance +
                                  input.OtherTaxableAllowances

            Dim payeTax As Decimal = Paye.Compute(gross)
            Dim pension = Contributions.PensionFor(input.BasicSalary)
            Dim maternity = Contributions.MaternityFor(input.BasicSalary)

            Dim netBeforeCbhi As Decimal = gross - payeTax - pension.Employee - maternity.Employee
            Dim cbhi As Decimal = Contributions.CbhiFor(netBeforeCbhi)
            Dim net As Decimal = Money.Positive(netBeforeCbhi - cbhi)

            Dim employerCost As Decimal = gross + pension.Employer + maternity.Employer

            Return New Payslip With {
                .EmployeeName = input.EmployeeName,
                .GrossSalary = Money.Round2(gross),
                .Paye = payeTax,
                .PensionEmployee = pension.Employee,
                .PensionEmployer = pension.Employer,
                .MaternityEmployee = maternity.Employee,
                .MaternityEmployer = maternity.Employer,
                .Cbhi = cbhi,
                .NetSalary = Money.Round2(net),
                .EmployerCost = Money.Round2(employerCost)
            }
        End Function

    End Module

End Namespace
