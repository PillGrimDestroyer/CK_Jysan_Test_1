using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CK_Jysan_Test_1
{
    internal class Program
    {
        private static string FILE_NAME = @"Test.txt";

        private static List<BaseEmployee> employeeList = new List<BaseEmployee>();
        private static List<BaseEmployee> sortedList = new List<BaseEmployee>();

        private static void Main(string[] args)
        {
            try
            {
                GenerateEmployees();

                SaveToFile(FILE_NAME);
                ReadFromFile(FILE_NAME);

                SortData();
                PrintData();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка:");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static void SortData()
        {
            var sortedData = employeeList.OrderByDescending(el => el.GetMonthSalary());
            bool haveDuplicateNames = employeeList.GroupBy(el => el.Salary).Any(g => g.Count() > 1);

            if (haveDuplicateNames)
            {
                sortedData = sortedData.ThenBy(el => el.Name);
            }

            sortedList = sortedData.ToList();
        }

        private static void PrintData()
        {
            List<BaseEmployee> firstFifeEmployees = sortedList.Take(5).ToList();
            List<BaseEmployee> lastThreeEmployees = sortedList.GetRange(employeeList.Count - 3, 3);

            Console.WriteLine("Отсортирование список");

            foreach (BaseEmployee employee in sortedList)
            {
                Console.WriteLine($"ID: {employee.Id}; Name: {employee.Name}; Month salary: {employee.GetMonthSalary()}");
            }

            Console.WriteLine();
            Console.WriteLine("Первые 5 имён");

            foreach (BaseEmployee employee in firstFifeEmployees)
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Последние 3 ID");

            foreach (BaseEmployee employee in lastThreeEmployees)
            {
                Console.WriteLine(employee.Id);
            }
        }

        private static void GenerateEmployees()
        {
            employeeList.Clear();
            Random random = new Random(DateTime.Now.Millisecond);

            int count = random.Next(10, 20);

            for (int i = 0; i < count; i++)
            {
                BaseEmployee employee;
                bool isHourlyEmployee = random.Next(10) > 4;
                bool isNeedDuplicate = random.Next(10) < 3;

                if (isHourlyEmployee)
                {
                    employee = new HourlyEmployee();
                }
                else
                {
                    employee = new FixedEmployee();
                }

                employee.Id = i;
                employee.Name = RandomString(random.Next(5, 11), new Random(i));
                employee.Salary = isNeedDuplicate ? 150.55 : Math.Round(random.NextDouble() * random.Next(150, 480), 2);

                employeeList.Add(employee);
            }
        }

        public static string RandomString(int length, Random random)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static void SaveToFile(string fileName)
        {
            IEnumerable<string> data = employeeList.Select(el => el.ToString());

            File.WriteAllLines(fileName, data, new UTF8Encoding(false));
        }

        private static void ReadFromFile(string fileName)
        {
            employeeList.Clear();
            List<string> fileLines = ReadAllLines(fileName);

            foreach (string line in fileLines)
            {
                employeeList.Add(BaseEmployee.GetFromString(line));
            }
        }

        private static List<string> ReadAllLines(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($@"Файл ""{fileName}"" не найден");
            }

            return new List<string>(File.ReadAllLines(fileName));
        }
    }
}
