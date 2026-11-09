Namespace RwandaPayroll

    ''' <summary>
    ''' Pay As You Earn income tax for Rwanda.
    '''
    ''' Monthly bands used by Rwanda Revenue Authority:
    '''   0        .. 60 000       at  0 %
    '''   60 001   .. 100 000      at 10 %
    '''   100 001  .. 200 000      at 20 %
    '''   above 200 000            at 30 %
    '''
    ''' Each band is applied only to the slice of income that falls inside it.
    ''' </summary>
    Public Module Paye

        Public ReadOnly Bands As PayeBand() = {
            New PayeBand(0D, 60000D, 0D),
            New PayeBand(60000D, 100000D, 0.1D),
            New PayeBand(100000D, 200000D, 0.2D),
            New PayeBand(200000D, Decimal.MaxValue, 0.3D)
        }

        Public Function Compute(ByVal taxableIncome As Decimal) As Decimal
            If taxableIncome <= 0D Then Return 0D
            Dim tax As Decimal = 0D
            For Each band In Bands
                If taxableIncome <= band.LowerExclusive Then Exit For
                Dim upper = If(taxableIncome < band.UpperInclusive, taxableIncome, band.UpperInclusive)
                tax += (upper - band.LowerExclusive) * band.Rate
            Next
            Return Money.Round2(tax)
        End Function

    End Module

    Public Structure PayeBand
        Public ReadOnly LowerExclusive As Decimal
        Public ReadOnly UpperInclusive As Decimal
        Public ReadOnly Rate As Decimal

        Public Sub New(ByVal lower As Decimal, ByVal upper As Decimal, ByVal rate As Decimal)
            Me.LowerExclusive = lower
            Me.UpperInclusive = upper
            Me.Rate = rate
        End Sub
    End Structure

End Namespace
