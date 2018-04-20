using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTo
{
    class User
    {
        public string Name { set; get; }
        public int Age { get; set; }
        public List<string> Language { get; set; }
        public User()
        {
            new List<string>();
        }
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }




    class Program
    {
        static void Main(string[] args)
        {
            string[] teams = { "Бавария","Борусия","Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            int[] numbers = { 1, 23, 45, 5, 7, 878, 989, 6, 67, 4, 7, 12, 5 };
            List<User> users = new List<User> {
                new User {Name="Bob", Age = 14, Language = new List<string> {"Английский","Арабский" } },
                new User {Name="Tom", Age = 24, Language = new List<string> {"Английский","Француский" } },
                new User {Name="Jon", Age = 14, Language = new List<string> {"Английский","португальский" } },
                new User {Name="Bob", Age = 30, Language = new List<string> { "Арабский", "Француский" } }
            };

            List<Person> person = new List<Person>()
            {
                new Person() { Name="Andrey", Age=24,City="Kyiv"},
                new Person() { Name="Liza", Age=18,City="Moscow"},
                new Person() { Name="Oleg", Age=15,City="lONDON"},
                new Person() { Name="Sergey", Age=55,City="Kyiv"},
                new Person() { Name="Sergey", Age=32,City="Kyiv"},
            };
            Console.WriteLine("---1");
            var select1 = (from p in person
                           where p.Age > 25
                           select p).Distinct();
            foreach (var item in select1)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("---1.1");
            var select2 = person.Where(p => p.Age > 25);
            foreach (var item in select2)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("---2");
            var select3 = from p in person
                          where p.City!= "Kyiv"
                          select p;
            foreach (var item in select3)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("---2.1");
            var select4 = person.Where(p => p.City != "Kyiv");
            foreach (var item in select4)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("---3");
            var select5 = from p in person
                          where p.City == "Kyiv"
                          select p.Name;
            foreach (var item in select5)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---3.1");
            var select6 = person.Where(p => p.City == "Kyiv").Select(/*p=>p.Name*/p=>new { p.Name,p.City});
            foreach (var item in select6)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("---4");
            var select7 = from p in person
                          where p.Age>35 && p.Name == "Sergey"
                          select p;
            foreach (var item in select7)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("---4.1");
            var select8 = person.Where(p => p.Age > 35 && p.Name == "Sergey");
            foreach (var item in select8)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("---5");
            var select9 = from p in person
                          where p.City == "Moscow"
                          select p;
            foreach (var item in select9)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("---5.1");
            var select10 = person.Where(p => p.City == "Moscow");
            foreach (var item in select10)
            {
                Console.WriteLine(item.Name);
            }













            var selectdTeam2 = teams.Where(team => team.StartsWith("Б")).Select(t => t.ToUpper());

            var selectedUsers = from user in users
                                where user.Age > 23
                                select user;











            //Основы LINQ
            Console.WriteLine("--------Основы LINQ--------");
            MethodLinQ(teams);

            //Фильтрация
            Console.WriteLine("--------Основы Фильтрация--------");
            MethodLinQ2(numbers);

            //Выборка сложных обьектов
            Console.WriteLine("--------Выборка сложных обьектов--------");
            MethodLinQ3(users);

            //Проэкция
            Console.WriteLine("--------Проэкция--------");
            MethodLinQ4(users);
        }


        //Проэкция
        private static void MethodLinQ4(List<User> users)
        {
            Console.WriteLine("----");
            var names = from user in users select user.Name;
            foreach (var item in names)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----");
            var namesAge = from user in users
                           select new { FirstName = user.Name, UserAge = user.Age };
            foreach (var item in namesAge)
            {
                Console.WriteLine(item.FirstName+" "+item.UserAge);
            }

            ///через метод розширения
            //выборка по имени

            Console.WriteLine("----");
            foreach (var item in users.Select(u=>u.Name))
            {
                Console.WriteLine(item);
            }

            //выборка обьекта анонимного типа
            Console.WriteLine("----");
            var items = users.Select(u => new { u.Name,u.Age });
            foreach (var item in items)
            {
                Console.WriteLine(item.Name+" "+item.Age);
            }



        }

        //Выборка сложных обьектов
        private static void MethodLinQ3(List<User> users)
        {
            var selectedUsers = from user in users
                                where user.Age > 23
                                select user;
            foreach (var item in selectedUsers)
            {
                Console.WriteLine(item.Name+" "+item.Age);
            }
            Console.WriteLine("----");
            var selectedUsersLang = from user in users
                                    from lang in user.Language
                                    where user.Age > 23
                                    where lang == "Английский"
                                    select user;
            foreach (var item in selectedUsersLang)
            {
                Console.WriteLine(item.Name+" "+item.Age);
            }

            Console.WriteLine("----");
            var selectedUsersLangM = users.SelectMany(user => user.Language, (u, l) => new { User = u, lang = l }).
                Where(u => u.lang == "Английский" && u.User.Age > 23).Select(u => u.User);
            foreach (var item in selectedUsersLangM)
            {
                Console.WriteLine(item.Name+" "+item.Age);
            }



        }


        //Фильтрация
        private static void MethodLinQ2(int[] numbers)
        {
            IEnumerable<int> result = from number in numbers
                                      where number > 10 && number % 2 == 0
                                      select number;
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            //средствами розширеных методов
            IEnumerable<int> result2 = numbers.Where(number => number > 10 && number % 2 == 0).OrderBy(t => t);
            foreach (var item in result2)
            {
                Console.WriteLine(item);
            }

            //or
            foreach (var item in numbers.Where(number => number > 10 && number % 2 == 0))
            {
                Console.WriteLine(item);
            }
        }


        //Основы LINQ
        private static void MethodLinQ(string[] teams)
        {
            /*IEnumerable<string>*/
            var selectedTeam = from team in teams //опеределяем каждый обьект из teams как team
                               where team.StartsWith("Б") //фильтрация по критериям
                               orderby team //сортируем по возрастанию
                               select team/*.ToUpper()*/; //выбираем обьект

            //Console.WriteLine(selectedTeam.GetType().Name);
            foreach (var item in selectedTeam)
            {
                Console.WriteLine(item.GetType().Name);
                Console.WriteLine(item);
            }

            ///синтаксис методов
            //or            var selectdTeam2 = teams.Where((string team) => { return team.StartsWith("Б"); });
            var selectdTeam2 = teams.Where(team => team.StartsWith("Б")).Select(t => t.ToUpper());
            foreach (var item in selectdTeam2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
