namespace CK_Jysan_Test_1
{
    /// <summary>
    /// Сотрудник с почасовой оплатой
    /// </summary>
    public class HourlyEmployee : BaseEmployee
    {
        public static string EMPLOYEE_TYPE_NAME = "Hourly";

        public HourlyEmployee()
        {
            EmployeeType = EMPLOYEE_TYPE_NAME;
        }

        public override double GetMonthSalary()
        {
            return 20.8 * 8 * Salary;
        }
    }
}
