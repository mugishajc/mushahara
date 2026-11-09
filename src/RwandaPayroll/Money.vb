Namespace RwandaPayroll

    ''' <summary>
    ''' Money rounded to two decimal places using banker's rounding.
    ''' All payroll amounts flow through this type so rounding stays consistent.
    ''' </summary>
    Public Module Money

        Public Function Round2(ByVal value As Decimal) As Decimal
            Return Math.Round(value, 2, MidpointRounding.ToEven)
        End Function

        Public Function Positive(ByVal value As Decimal) As Decimal
            If value < 0D Then Return 0D
            Return value
        End Function

    End Module

End Namespace
