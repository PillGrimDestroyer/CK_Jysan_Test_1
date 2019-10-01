namespace CK_Jysan_Test_1
{
    /// <summary>
    /// Сотрудник с ежемесячным окладом
    /// </summary>
    public class FixedEmployee : BaseEmployee
    {
        public static string EMPLOYEE_TYPE_NAME = "Fixed";

        public FixedEmployee()
        {
            EmployeeType = EMPLOYEE_TYPE_NAME;
        }

        public override double GetMonthSalary()
        {
            return Salary;
        }
    }
}
