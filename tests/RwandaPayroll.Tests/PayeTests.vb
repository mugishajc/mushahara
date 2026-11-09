Imports Xunit

Namespace RwandaPayroll.Tests

    Public Class PayeTests

        <Fact>
        Public Sub ZeroIncomeIsZeroTax()
            Assert.Equal(0D, Paye.Compute(0D))
            Assert.Equal(0D, Paye.Compute(-1000D))
        End Sub

        <Fact>
        Public Sub WithinFirstBandIsZeroTax()
            Assert.Equal(0D, Paye.Compute(60000D))
            Assert.Equal(0D, Paye.Compute(30000D))
        End Sub

        <Fact>
        Public Sub SecondBandTaxesOnlyTheSlice()
            ' 80 000 total: first 60 000 free, next 20 000 taxed at 10 % = 2 000
            Assert.Equal(2000D, Paye.Compute(80000D))
        End Sub

        <Fact>
        Public Sub ThirdBandCumulative()
            ' 150 000 total:
            '   40 000 at 10 % = 4 000
            '   50 000 at 20 % = 10 000
            Assert.Equal(14000D, Paye.Compute(150000D))
        End Sub

        <Fact>
        Public Sub TopBandCumulative()
            ' 300 000 total:
            '   40 000 at 10 % = 4 000
            '   100 000 at 20 % = 20 000
            '   100 000 at 30 % = 30 000
            Assert.Equal(54000D, Paye.Compute(300000D))
        End Sub

    End Class

End Namespace
