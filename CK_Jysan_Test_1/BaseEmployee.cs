using System;

namespace CK_Jysan_Test_1
{
    public abstract class BaseEmployee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EmployeeType { get; protected set; }

        public double Salary { get; set; }

        public BaseEmployee()
        {
            EmployeeType = "None";
        }

        public virtual double GetMonthSalary()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $@"Id:{Id};Name:{Name};Salary:{Salary};Type:{EmployeeType}";
        }

        public static BaseEmployee GetFromString(string line)
        {
            string[] splitedData = line.Split(';');

            if (splitedData.Length != 4)
                throw new Exception("Строка имеет неверный формат");

            BaseEmployee employee = default;

            string lineId = splitedData[0].Substring(splitedData[0].IndexOf(":") + 1);
            string lineName = splitedData[1].Substring(splitedData[1].IndexOf(":") + 1);
            string lineSalary = splitedData[2].Substring(splitedData[2].IndexOf(":") + 1);
            string lineType = splitedData[3].Substring(splitedData[3].IndexOf(":") + 1);

            if (lineType == FixedEmployee.EMPLOYEE_TYPE_NAME)
                employee = new FixedEmployee();
            else
                employee = new HourlyEmployee();

            employee.Id = int.Parse(lineId);
            employee.Name = lineName;
            employee.Salary = double.Parse(lineSalary);

            return employee;
        }
    }
}
