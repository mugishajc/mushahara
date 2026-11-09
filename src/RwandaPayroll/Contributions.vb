Namespace RwandaPayroll

    ''' <summary>
    ''' Statutory contributions on top of PAYE.
    '''
    ''' RSSB pension:        employee 6 %,      employer 8 %     of basic salary
    ''' RSSB maternity:      employee 0.3 %,    employer 0.3 %   of basic salary
    ''' CBHI (Mutuelle):     employee 0.5 %,    employer 0 %     of net before CBHI
    '''
    ''' Only the employee side reduces take-home pay. The employer side is
    ''' reported so a payroll run can compute total cost of employment.
    ''' </summary>
    Public Module Contributions

        Public ReadOnly PensionEmployee As Decimal = 0.06D
        Public ReadOnly PensionEmployer As Decimal = 0.08D
        Public ReadOnly MaternityEmployee As Decimal = 0.003D
        Public ReadOnly MaternityEmployer As Decimal = 0.003D
        Public ReadOnly CbhiEmployee As Decimal = 0.005D

        Public Function PensionFor(ByVal basic As Decimal) As ContributionSplit
            Return New ContributionSplit(
                Money.Round2(basic * PensionEmployee),
                Money.Round2(basic * PensionEmployer))
        End Function

        Public Function MaternityFor(ByVal basic As Decimal) As ContributionSplit
            Return New ContributionSplit(
                Money.Round2(basic * MaternityEmployee),
                Money.Round2(basic * MaternityEmployer))
        End Function

        Public Function CbhiFor(ByVal netBeforeCbhi As Decimal) As Decimal
            Return Money.Round2(netBeforeCbhi * CbhiEmployee)
        End Function

    End Module

    Public Structure ContributionSplit
        Public ReadOnly Employee As Decimal
        Public ReadOnly Employer As Decimal

        Public Sub New(ByVal employee As Decimal, ByVal employer As Decimal)
            Me.Employee = employee
            Me.Employer = employer
        End Sub
    End Structure

End Namespace
