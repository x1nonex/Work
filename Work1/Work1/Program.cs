using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Work1
{
    class Program
    {
        List<Company> companyList = new List<Company>();
        public List<Worker> workersList = new List<Worker>();
        public int workerId;
        public int companyId;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.SelectFuntion();
        }

        void SelectFuntion()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n1) Создать сотрудника\n2) Предприятия \n3) Принять сотрудника \n4) Уволить сотрудника " +
                "\n5) Изменить должность \n6) Перевести в другое предприятие " +
                "\n7) Изменить зарплату сотруднику \n8) Информация о сотрудниках " +
                "\n9) Создать компанию \n\nВведите команду от 1-9, выбрав функцию");

            int command = Convert.ToInt32(Console.ReadLine());

            switch (command)
            {
                case 1:
                    CreateWorkerCommand();
                    break;
                case 2:
                    CompanyList();
                    break;
                case 3:
                    AcceptWorkerCommand();
                    break;
                case 4:
                    DismissWorkerCommand();
                    break;
                case 5:
                    ChangeJobAtCompanyCommand();
                    break;
                case 6:
                    TransferWorkerToAnotherCompanyCommand();
                    break;
                case 7:
                    ChangeSalaryCommand();
                    break;
                case 8:
                    break;
                case 9:
                    CreateCompany();
                    break;

            }
        }

        void Sleep()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nНажмите кнопку чтобы продолжить");
            Console.ReadKey();
            Console.Clear();
            SelectFuntion();
        }

        void ListOfWorkers()
        {
            Console.WriteLine("\nВыберите цифру сотрудника: ");
            for (int i = 0; i < companyList[companyId].workers.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + companyList[companyId].workers[i].info);
            }
            workerId = Convert.ToInt32(Console.ReadLine()) - 1;
        }

        void CreateWorkerCommand()
        {
            Console.WriteLine("\nВведите имя и фамилию сотрудника: ");
            string name = Convert.ToString(Console.ReadLine());
            Worker _worker = new Worker();
            _worker.CreateWorker(name);
            workersList.Add(_worker);
            Sleep();
        }

        void CompanyList()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nКомпании на данный момент: \n");
            for (int i = 0; i < companyList.Count; i++)
            {
                Console.WriteLine(companyList[i].companyName);
            }
            Sleep();
        }

        void SelectCompany()
        {
            Console.WriteLine("\nВыберие предприятие: ");
            for (int i = 0; i < companyList.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + companyList[i].companyName);
            }
            companyId = Convert.ToInt32(Console.ReadLine()) - 1;
        }

        void AcceptWorkerCommand()
        {
            SelectCompany();
            Console.WriteLine("\n Выберите цифру, какого сотрудника принять:");
            for (int i = 0; i < workersList.Count; i++)
            {
                if (workersList[i].company == null)
                {
                    Console.WriteLine(i + 1 + ") " + workersList[i].info);
                }
            }
            workerId = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.WriteLine("Напишите должность: ");
            string job = Convert.ToString(Console.ReadLine());
            companyList[companyId].AcceptToCompany(workersList[workerId], job);
            Sleep();
        }


        void DismissWorkerCommand()
        {
            SelectCompany();
            ListOfWorkers();
            Worker worker = companyList[companyId].workers[workerId];
            companyList[companyId].DissmissWorker(worker);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nСотрудник " + worker.info + " уволен.");
            Sleep();
        }

        void ChangeJobAtCompanyCommand()
        {
            SelectCompany();
            ListOfWorkers();
            Console.WriteLine($"Напишите новую должность сотрудника '{ companyList[companyId].workers[workerId].info}': ");
            string _job = Convert.ToString(Console.ReadLine());
            companyList[companyId].workers[workerId].jobTitle = _job;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nУспешно.");
            Sleep();
        }

        void TransferWorkerToAnotherCompanyCommand()
        {
            SelectCompany();
            ListOfWorkers();
            int oldCompanyId = companyId;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nВ какую компанию перевести сотрудника?");
            SelectCompany();
            companyList[oldCompanyId].TransferToAnotherCompany(companyList[companyId], companyList[oldCompanyId].workers[workerId]);
            Console.Clear();
            Console.WriteLine($"\nНа какую должность перевести сотрудника {companyList[companyId].workers[workerId].info} в компании {companyList[companyId].companyName} ?");
            string _job = Convert.ToString(Console.ReadLine());
            companyList[companyId].workers[workerId].jobTitle = _job;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nРаботник был перевён успешно с должностью: '{companyList[companyId].workers[workerId].jobTitle}'");
            Sleep();
        }

        void ChangeSalaryCommand()
        {
            SelectCompany();
            ListOfWorkers();
            Worker worker = companyList[companyId].workers[workerId];
            Console.WriteLine($"Введите число, указывающее на новую зарплату сотрудника '{worker.info}'");
            int _salary = Convert.ToInt32(Console.ReadLine());
            companyList[companyId].ChangeSalary(worker, _salary);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nУспешно.");
            Sleep();
        }

        void CreateCompany()
        {
            Console.WriteLine("Введите название новой компании: ");
            string _name = Convert.ToString(Console.ReadLine());
            Company newCompany = new Company() { companyName = _name };
            companyList.Add(newCompany);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nКомпания {newCompany.companyName} создана успешно.");
            Sleep();
        }
    }

    class Worker
    {
        public string info;
        public string jobTitle;
        public int salary;
        public string company;

        public void CreateWorker(string _name)
        {
            info = _name;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nСоздан работник: " + info);
        }
    }

    class Company
    {
        public List<Worker> workers = new List<Worker>();
        public string companyName;
        public string[] jobs = new string[] { "Рядовой", "Лох", "Крутой" };
        public int companySalary = 123;

        public void AcceptToCompany(Worker _worker, string _jobTitle)
        {
            this.workers.Add(_worker);
            _worker.jobTitle = _jobTitle;
            _worker.salary = companySalary;
            _worker.company = companyName;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nКомпания: {companyName} \nРаботник: {_worker.info} \nДолжность: {_worker.jobTitle}\nЗарплата: {_worker.salary}");
        }

        public void DissmissWorker(Worker _worker)
        {
            this.workers.Remove(_worker);
            _worker.jobTitle = null;
            _worker.company = null;
            _worker.salary = 0;

        }

        public void ChangeSalary(Worker _worker, int _salary)
        {
            _worker.salary = _salary;
        }

        public void TransferToAnotherCompany(Company _anotherCompany, Worker _worker)
        {
            _worker.company = _anotherCompany.companyName;
            DissmissWorker(_worker);
            _anotherCompany.AcceptToCompany(_worker, "Переведённый");
        }
    }
}
