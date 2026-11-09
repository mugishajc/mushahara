Imports Xunit

Namespace RwandaPayroll.Tests

    Public Class PayslipTests

        <Fact>
        Public Sub FullPayslipForAMidRangeSalary()
            Dim input As New PayrollInput(
                employeeName:="Uwase",
                basicSalary:=500000D,
                transportAllowance:=30000D)

            Dim p = PayslipCalculator.Compute(input)

            Assert.Equal(530000D, p.GrossSalary)
            ' PAYE: 40 000 @ 10 % + 100 000 @ 20 % + 330 000 @ 30 % = 4 000 + 20 000 + 99 000 = 123 000
            Assert.Equal(123000D, p.Paye)
            Assert.Equal(30000D, p.PensionEmployee)         ' 6 % of 500 000
            Assert.Equal(40000D, p.PensionEmployer)         ' 8 % of 500 000
            Assert.Equal(1500D, p.MaternityEmployee)        ' 0.3 % of 500 000
            Assert.Equal(1500D, p.MaternityEmployer)
            ' Net before CBHI: 530 000 - 123 000 - 30 000 - 1 500 = 375 500
            ' CBHI: 0.5 % = 1 877.5
            Assert.Equal(1877.5D, p.Cbhi)
            Assert.Equal(373622.5D, p.NetSalary)
            ' Employer cost: gross + employer pension + employer maternity
            Assert.Equal(571500D, p.EmployerCost)
        End Sub

        <Fact>
        Public Sub NetIsNeverNegative()
            Dim input As New PayrollInput("Edge", 10000D)
            Dim p = PayslipCalculator.Compute(input)
            Assert.True(p.NetSalary >= 0D)
        End Sub

    End Class

End Namespace
