using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication3
{

    /* 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */
    class Users
    {
        public string role {get;set;}
        public string name{get;set;}
        public int age{get;set;}
        public int salary{get;set;}
        public string login{get;set;}
        public string password{get;set;}

    }

    class Tovar
    {
        public string name { get; set; }
        public string group { get; set; }
        public int count { get; set; }
        public int cost { get; set; }
    }

    class Order
    {
        public int usr { get; set; }
        public List<int> lst { get; set; }
        public List<int> cnt{ get; set; }
        public string date { get; set; }

        public string compl { get; set; }

    }
    class Salers
    {
       
        List<Users> user=new List<Users>();

        List<Tovar> ware = new List<Tovar>();

        List<Order> ordr = new List<Order>();

        void Start()
        {
            if (File.Exists("users.txt"))
            {
                using (StreamReader sr = new StreamReader("users.txt", System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Users tmp = new Users();
                        tmp.role = line;
                        tmp.name=sr.ReadLine();
                        tmp.age=Convert.ToInt32(sr.ReadLine());
                        tmp.salary=Convert.ToInt32(sr.ReadLine());
                        tmp.login=sr.ReadLine();
                        tmp.password=sr.ReadLine();
                        user.Add(tmp);
                    }
                }
            }
            else
            {
                Users tmp = new Users();
                Console.Write("Пользователи не обнаружены\nЗарегистрируйте администратора:\n ФИО: ");
                tmp.name=Console.ReadLine();
                tmp.role = "admin";
                Console.Write(" Возраст: ");
                tmp.age = Convert.ToInt32(Console.ReadLine());
                Console.Write(" Зарплата: ");
                tmp.salary = Convert.ToInt32(Console.ReadLine());
                Console.Write(" Логин: ");
                tmp.login = Console.ReadLine();
                Console.Write(" Пароль: ");
                tmp.password = Console.ReadLine();
                user.Add(tmp);
            }
            if (File.Exists("ware.txt"))
            {
                using (StreamReader sr2 = new StreamReader("ware.txt", System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr2.ReadLine()) != null)
                    {
                        Tovar tmp = new Tovar();
                        tmp.name= line;
                        tmp.group = sr2.ReadLine();
                        tmp.count = Convert.ToInt32(sr2.ReadLine());
                        tmp.cost = Convert.ToInt32(sr2.ReadLine());
                        ware.Add(tmp);
                    }
                }
            }
            if (File.Exists("order.txt"))
            {
                using (StreamReader sr3 = new StreamReader("order.txt", System.Text.Encoding.Default))
                {
                    string line;
                    int n;
                    while ((line = sr3.ReadLine()) != null)
                    {
                        Order tmp = new Order();
                        tmp.usr = Convert.ToInt32(line);
                        n = Convert.ToInt32(sr3.ReadLine());
                        tmp.lst = new List<int>();
                        tmp.cnt = new List<int>();
                        for (int j = 0; j < n; j++)
                        {
                            tmp.lst.Add(Convert.ToInt32(sr3.ReadLine()));
                            tmp.cnt.Add(Convert.ToInt32(sr3.ReadLine()));
                        }

                        tmp.date = sr3.ReadLine();
                        tmp.compl = sr3.ReadLine();
                       
                        ordr.Add(tmp);
                    }
                }
            }
        }

        void SaveUsers()
        {
            using (StreamWriter sw = new StreamWriter("users.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < user.Count; i++)
                {
                    sw.WriteLine(user[i].role);
                    sw.WriteLine(user[i].name);
                    sw.WriteLine(user[i].age);
                    sw.WriteLine(user[i].salary);
                    sw.WriteLine(user[i].login);
                    sw.WriteLine(user[i].password);
                }
            }
            using (StreamWriter sw2 = new StreamWriter("ware.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < ware.Count; i++)
                {
                    sw2.WriteLine(ware[i].group);
                    sw2.WriteLine(ware[i].name);
                    sw2.WriteLine(ware[i].count);
                    sw2.WriteLine(ware[i].cost);
                }
            }
            using (StreamWriter sw3 = new StreamWriter("order.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < ordr.Count; i++)
                {     
                    sw3.WriteLine(ordr[i].usr);
                    sw3.WriteLine(ordr[i].lst.Count);
                    for (int j = 0; j < ordr[i].lst.Count; j++)
                    {
                        sw3.WriteLine(ordr[i].lst[j]);
                        sw3.WriteLine(ordr[i].cnt[j]);
                    }
                     sw3.WriteLine(ordr[i].date);
                    sw3.WriteLine(ordr[i].compl);
                }
            }
        }

        string FindRole(string login)
        {
            for (int i = 0; i < user.Count; i++)
            {
                if (login == user[i].login) return user[i].role;
            }            
            return "";
        }

        string GetPass(string login)
        {
            for (int i = 0; i < user.Count; i++)
                if (login == user[i].login) return user[i].password;
            return "";
        }

       int GetInd(string login)
        {
            for (int i = 0; i < user.Count; i++)
                if (login == user[i].login) return i;
            return -1;
        }

        public void StartProg()
        {
            Start();
            MainMenu();
            SaveUsers();
        }

       void AdminMenu()
        {
          
            //Администратор: добавление, редактирование и удаление пользователей (ФИО, логин, пароль)(кроме покупателей). Просмотр ВСЕХ данных
            int t = 0;
            string login;
            bool fl;
            do
            {
                Console.Clear();
                Console.Write("1. Добавление\n2. Редактирование\n3. Удаление\n4. Просмотр всех пользователей\n0. Выход\n\n>>");
                t=Convert.ToInt32(Console.ReadLine());
                switch (t)
                {
                    case 1: 

                         Users tmp = new Users();
                         Console.Write("ФИО: ");
                         tmp.name=Console.ReadLine();
                         do
                         {
                             Console.Write("Роль: \n 1. Администратор\n 2. Кладовщик\n 3. Кадровый сотрудник\n 4. Кассир-продавец\n 5. Бухгалтер\n\n>>");
                             t = Convert.ToInt32(Console.ReadLine());
                         } while (t < 1 || t > 4);

                         switch (t)
                         {
                             case 1: tmp.role = "admin"; break;
                             case 2: tmp.role = "hr"; break;
                             case 3: tmp.role = "warehouse"; break;
                             case 4: tmp.role = "cashier"; break;
                             case 5: tmp.role = "bookkeeping"; break;
                         }

                         Console.Write(" Возраст: ");
                         tmp.age = Convert.ToInt32(Console.ReadLine());
                         Console.Write(" Зарплата: ");
                         tmp.salary = Convert.ToInt32(Console.ReadLine());
                         do
                         {
                             Console.Write(" Логин: ");
                             tmp.login = Console.ReadLine();
                         } while (GetPass(tmp.login) != "");
                         Console.Write(" Пароль: ");
                         tmp.password = Console.ReadLine();
                         user.Add(tmp);
                        
                         break;

                    case 2:
                        fl = false;
                         Console.Write("Логин: ");
                         login=Convert.ToString(Console.ReadLine());
                         for (int i = 0; i < user.Count; i++)
                         {
                             if (login == user[i].login)
                             {
                                 Console.Write(" Возраст: ");
                                 user[i].age = Convert.ToInt32(Console.ReadLine());
                                 Console.Write(" Зарплата: ");
                                 user[i].salary = Convert.ToInt32(Console.ReadLine());
                                 fl= true;
                             }
                         }
                         if (!fl) Console.WriteLine("Не найден такой пользователь ");

                         break;

                    case 3:
                         fl = false;
                         Console.Write("Логин: ");
                         login = Convert.ToString(Console.ReadLine());
                         for (int i = 0; i < user.Count; i++)
                         {
                             if (login == user[i].login)
                             {
                                 user.RemoveAt(i);
                                 fl = true;
                                 break;
                             }
                         }
                         if (!fl) Console.WriteLine("Не найден такой пользователь ");
                         break;
                    case 4:
                         if (user.Count > 0)
                         {
                             Console.WriteLine("Логин\tПароль    \tДолжность\tЗарплата\tВозраст\tФИО");
                             for (int i = 0; i < user.Count; i++)
                             {
                                 Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", user[i].login, user[i].password, user[i].role, user[i].salary, user[i].age, user[i].name);
                             }
                         }
                         
                         break;
                }
                Console.ReadKey();
            } while (t != 0);
                  }

        void HRMenu()
        {

            //Кадры: просмотр всех данных сотрудников (кроме пароля), редактирования данных, увольнение сотрудников.
            int t = 0;
            string login;
            bool fl;
            do
            {
                Console.Clear();
                Console.Write("1. Редактирование\n2. Увольнение\n3. Просмотр сотрудников\n0. Выход\n\n>>");
                t = Convert.ToInt32(Console.ReadLine());
                switch (t)
                {
                    
                    case 1:
                        fl = false;
                        Console.Write("Логин: ");
                        login = Convert.ToString(Console.ReadLine());

                        for (int i = 0; i < user.Count; i++)
                        {
                            if (login == user[i].login)
                            {
                                do
                                {
                                    Console.Write("Роль: \n 1. Администратор\n 2. Кладовщик\n 3. Кадровый сотрудник\n 4. Кассир-продавец\n 5. Бухгалтер\n\n>>");
                                    t = Convert.ToInt32(Console.ReadLine());
                                } while (t < 1 || t > 4);

                                switch (t)
                                {
                                    case 1: user[i].role = "admin"; break;
                                    case 2: user[i].role = "hr"; break;
                                    case 3: user[i].role = "warehouse"; break;
                                    case 4: user[i].role = "cashier"; break;
                                    case 5: user[i].role = "bookkeeping"; break;
                                }

                               
                                Console.Write(" Возраст: ");
                                user[i].age = Convert.ToInt32(Console.ReadLine());
                                Console.Write(" Зарплата: ");
                                user[i].salary = Convert.ToInt32(Console.ReadLine());
                                fl = true;
                            }
                        }
                        if (!fl) Console.WriteLine("Не найден такой пользователь ");

                        break;

                    case 2:
                        fl = false;
                        Console.Write("Логин: ");
                        login = Convert.ToString(Console.ReadLine());
                        for (int i = 0; i < user.Count; i++)
                        {
                            if (login == user[i].login)
                            {
                                user.RemoveAt(i);
                                fl = true;
                                break;
                            }
                        }
                        if (!fl) Console.WriteLine("Не найден такой пользователь ");
                        break;
                    case 3:
                        if (user.Count > 0)
                        {
                            Console.WriteLine("Логин\tДолжность\tЗарплата\tВозраст\tФИО");
                            for (int i = 0; i < user.Count; i++)
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", user[i].login, user[i].role, user[i].salary, user[i].age, user[i].name);
                            }
                        }

                        break;
                }
                Console.ReadKey();
            } while (t != 0);


        }

        void ShowWare()
        {
            if (ware.Count > 0)
            {
                Console.WriteLine("\tТовар\tКатегория\tКоличество\tЦена");
                for (int i = 0; i < ware.Count; i++)
                {
                    Console.WriteLine("{0})\t{1}\t{2}\t{3}\t{4}", i, ware[i].name, ware[i].group, ware[i].count, ware[i].cost);
                }

            }
            else Console.WriteLine("Нет товаров");
        }
        void WareMenu()
        {
           // Кладовщик: оформление поставки, перемещения товара со склада, брак, просмотр товара по категории.

            
            int t = 0;
            string str;
            bool fl;
            do
            {
                Console.Clear();
                Console.Write("1. Оформление поставки\n2. Перемещения товара со склада/брак\n3. Просмотр товаров\n0. Выход\n\n>>");
                t = Convert.ToInt32(Console.ReadLine());
                switch (t)
                {

                    case 1:

                        Console.Write("Название: ");
                        str = Console.ReadLine();
                        for (t = 0; t < ware.Count; t++)
                        {
                            if (ware[t].name == str)
                            {
                                Console.Write("Сколько добавить: ");
                                ware[t].count += Convert.ToInt32(Console.ReadLine());
                                break;
                            }

                        }
                        if (t == ware.Count)
                        {
                            Tovar tmp = new Tovar();
                            tmp.name = str;
                            Console.Write("Категория: ");
                            tmp.group = Console.ReadLine();
                            Console.Write("Новая цена: ");
                            tmp.cost = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Количество: ");
                            tmp.count = Convert.ToInt32(Console.ReadLine());
                            ware.Add(tmp);
                        }
                        

                        break;

                    case 2:

                        Console.Write("Название: ");
                        str = Console.ReadLine();
                        for (t = 0; t < ware.Count; t++)
                        {
                            if (ware[t].name == str)
                            {
                                Console.Write("На складе - {0} ", ware[t].count);
                                Console.Write("Сколько списать: ");
                                ware[t].count += Convert.ToInt32(Console.ReadLine());
                                if (ware[t].count < 1) ware.RemoveAt(t);
                                break;
                            }

                        }
                        if (t == ware.Count) Console.Write("Товар не найден ");


                        break;
                    case 3:
                        ShowWare();

                        break;
                }
                Console.ReadKey();
            } while (t != 0);


        }

        void CashMenu()
        {
            //Кассир-продавец: оформления-отмена заказа (проверка на наличия товара на складе).


            int t = 0,n;
            string str;
            bool fl;
            do
            {
                Console.Clear();
                Console.Write("1. Оформления-отмена заказа\n2. Просмотр товаров\n3. Просмотр заказов\n0. Выход\n\n>>");
                t = Convert.ToInt32(Console.ReadLine());
                switch (t)
                {

                    case 1:
                        if (ordr.Count > 0)
                        {
                            do
                            {
                                Console.Write("Номер заказа: ");
                                n = Convert.ToInt32(Console.ReadLine());
                            } while (n < 0 && n >= ordr.Count);


                            Console.Write("Новый статус: ");
                            ordr[n].compl = Console.ReadLine();
                            Console.Write("Статус заказа изменен на: {0}\n", ordr[n].compl);
                        }
                        else Console.WriteLine("Заказов нет");


                        break;

                    case 2:

                        if (ware.Count > 0)
                        {
                            Console.WriteLine("Товар\tКатегория\tКоличество\tЦена");
                            for (int i = 0; i < ware.Count; i++)
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}", ware[i].name, ware[i].group, ware[i].count, ware[i].cost);
                            }
                        }
                        else Console.WriteLine("Товаров нет");

                        break;
                    case 3:
                        if (ordr.Count > 0)
                        {
                            for (int i = 0; i < ordr.Count; i++)
                            {
                                Console.WriteLine("{0} - {1} - {2}", user[ordr[i].usr].name, ordr[i].date, ordr[i].compl);
                                for (int j = 0; j < ordr[i].lst.Count; j++)
                                {
                                    Console.WriteLine("\t{0} - {1}",ware[ordr[i].lst[j]],ordr[i].cnt[j]);
                                }
                            }
                        }
                        else Console.WriteLine("Заказов нет");
                        break;
                }
                Console.ReadKey();
            } while (t != 0);


        }


        void BookMenu()
        {
//            Бухгалтерия: информация о доходах за покупки, общий бюджет компании.


            int t = 0, n,i;
            string str;
            bool fl;
            do
            
            {
                Console.Clear();
                Console.Write("1. Просмотр заказов\n2. Рассчет заказа\n3. Заработная плата\n0. Выход\n\n>>");
                t = Convert.ToInt32(Console.ReadLine());
                switch (t)
                {

                    case 1:

                        if (ordr.Count > 0)
                        {
                            for (i = 0; i < ordr.Count; i++)
                            {
                                Console.WriteLine("{0} - {1} - {2}", user[ordr[i].usr].name, ordr[i].date, ordr[i].compl);
                                for (int j = 0; j < ordr[i].lst.Count; j++)
                                {
                                    Console.WriteLine("\t{0} - {1}", ware[ordr[i].lst[j]].name, ordr[i].cnt[j]);
                                }
                            }
                        }
                        else Console.WriteLine("Заказов нет");
                        
                        
                        break;

                    case 2:
                        if (ordr.Count > 0)
                        {
                            do
                            {
                                Console.Write("Номер заказа: ");
                                n = Convert.ToInt32(Console.ReadLine());
                            } while (n < 0 || n >= ordr.Count);
                            int sum=0;
                            Console.WriteLine("{0} - {1} - {2}", user[ordr[n].usr].name, ordr[n].date, ordr[n].compl);
                            for (int j = 0; j < ordr[n].lst.Count; j++)
                            {
                                Console.WriteLine("\t{0} - {1}шт. - {2}р.", ware[ordr[n].lst[j]].name, ordr[n].cnt[j], ware[ordr[n].lst[j]].cost * ordr[n].cnt[j]);
                                sum+=ware[ordr[n].lst[j]].cost * ordr[n].cnt[j];
                            }

                            Console.WriteLine("Общая стоимость заказа = {0}p.", sum);

                        }
                        else Console.WriteLine("Заказов нет");

                        break;
                    case 3:
                        Console.Write("ФИО: ");
                        str = Console.ReadLine();
                        for (i = 0; i < user.Count; i++)
                            {
                                if (user[i].name == str)
                                {
                                    Console.WriteLine("{0} - {1} - {2}p.", user[i].name, user[i].age, user[i].salary);
                                    break;
                                }
                            }
                        if (i == 0) Console.WriteLine("Не найден такой сотрудник");
                        break;
                }
                Console.ReadKey();
            } while (t != 0);


        }

        void AddNewUserOrder(int log)
        {
            int n;
            if (ware.Count > 0)
            {
                ShowWare();
                do
                {
                    Console.WriteLine("\nНомер товара");
                    n = Convert.ToInt32(Console.ReadLine());
                } while (n < 0 || n >= ware.Count);

                Order tmp = new Order();
                tmp.usr = log;
                tmp.lst = new List<int>();
                tmp.lst.Add(n);
                Console.Write("Количество: ");
                tmp.cnt = new List<int>();
                tmp.cnt.Add(Convert.ToInt32(Console.ReadLine()));
                tmp.compl = "Собрано";
                Console.Write("Дата: ");
                tmp.date = Console.ReadLine();
                ordr.Add(tmp);
            }
            else Console.WriteLine("Нет товаров в базе. Обратитесь к администратору");
        }
        void UserMenu(int log)
        {
            //Покупатель: добавить, удалить товары из корзины, изменить количество товаров.
            int t = 0, n, i, k;
            string str;
            bool fl;

            n = 0;


        f1:
            fl = false;
            int sum = 0;
            k = 0;
            Console.Clear();
            Console.WriteLine("Вы вошли как {0} - {1}", user[log].name, user[log].age);
            for (i = 0; i < ordr.Count; i++)
            {
                if (ordr[i].usr == log)
                {
                    Console.WriteLine("{0}) {1}", k++, ordr[i].date);
                    for (int j = 0; j < ordr[i].lst.Count; j++)
                    {
                        Console.WriteLine("\t{0} - {1}шт. - {2}р.", ware[ordr[i].lst[j]].name, ordr[i].cnt[j], ware[ordr[i].lst[j]].cost * ordr[i].cnt[j]);
                        sum += ware[ordr[i].lst[j]].cost * ordr[i].cnt[j];
                    }
                }
            }
            if (k == 0)
            {
                Console.WriteLine("Заказов нет\nДобавить новый заказ? (1-да,иначе-выход)");
                n = Convert.ToInt32(Console.ReadLine());
                if (n == 1)
                {
                    AddNewUserOrder(log);
                    goto f1;
                }
            }
            else
            {

                Console.WriteLine("Общая стоимость заказов = {0}p.\n", sum);

                Console.Write("1. Добавить новый заказ\n2. Редактировать заказ\n0. Выход\n\n>>");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1: AddNewUserOrder(log); break;
                    case 2:
                        do
                        {
                            Console.Write("Номер заказа: ");
                            n = Convert.ToInt32(Console.ReadLine());
                        } while (n < 0 || n >= k);
                        k = 0;
                        for (i = 0; i < ordr.Count; i++)
                        {
                            if (ordr[i].usr == log)
                            {
                                if (k == n)
                                {
                                    Console.Write("1. Редактировать\n2. Удалить \n3. Добавить товар\n0. Выход\n\n>>");
                                    n = Convert.ToInt32(Console.ReadLine());
                                    switch (n)
                                    {
                                        case 1:
                                            for (int j = 0; j < ordr[i].lst.Count; j++)
                                                Console.WriteLine("{0})\t{1} - {2}шт. - {3}р.", j, ware[ordr[i].lst[j]].name, ordr[i].cnt[j], ware[ordr[i].lst[j]].cost * ordr[i].cnt[j]);
                                            do
                                            {
                                                Console.Write("Номер товара: ");
                                                n = Convert.ToInt32(Console.ReadLine());
                                            } while (n < 0 || n >= ordr[i].lst.Count);
                                            Console.WriteLine("Количество: ");
                                            ordr[i].cnt[n] = Convert.ToInt32(Console.ReadLine());

                                            break;
                                        case 2: ordr.RemoveAt(i); goto f1; break;
                                        case 3:
                                            ShowWare();
                                            do
                                            {
                                                Console.WriteLine("\nНомер товара");
                                                n = Convert.ToInt32(Console.ReadLine());
                                            } while (n < 0 || n >= ware.Count);
                                            ordr[i].lst.Add(n);
                                            Console.Write("Количество: ");
                                            ordr[i].cnt.Add(Convert.ToInt32(Console.ReadLine()));

                                            break;
                                    }

                                }
                                k++;
                            }
                        }
                        break;

                }
            }


            Console.ReadKey();
        }



        void MainMenu()
        {
            string role = "",tmp="";
           int t;
           do
           {
               Console.Clear();
               Console.Write("1 - Регистрация покупателя\n2 - Вход\n0 - Завершение программы\n\n>> ");
               t = Convert.ToInt32(Console.ReadLine());
               if (t == 1)
               {
                   Users usr = new Users();
                   usr.role = "user";
                   Console.Write("ФИО: ");
                   usr.name = Console.ReadLine();

                   Console.Write(" Возраст: ");
                   usr.age = Convert.ToInt32(Console.ReadLine());
                    usr.salary = 0;
                   do
                   {
                       Console.Write(" Логин: ");
                       usr.login = Console.ReadLine();
                   } while (GetPass(usr.login) != "");
                   Console.Write(" Пароль: ");
                   usr.password = Console.ReadLine();
                   user.Add(usr);
               }
               else if (t == 2)
               {

                   t = 3;

                   while (t > 0)
                   {
                       Console.Write("Введите логин ({0} попыток): ", t);
                       tmp = Console.ReadLine();
                       role = FindRole(tmp);
                       if (role == "") t--;
                       else break;
                   }
                   if (t == 0) Console.WriteLine("\nПрограмма будет завершена\n");
                   else 
                   {

                       t = 3;
                       string pass = GetPass(tmp);
                       string log = tmp;
                       while (t > 0)
                       {
                           Console.Write("Введите пароль ({0} попыток): ", t);
                           tmp = Console.ReadLine();
                           if (pass != tmp) t--;
                           else break;
                       }
                       if (t == 0)
                       {
                           Console.WriteLine("\nПрограмма будет завершена\n");
                       }
                       else
                       {
                           switch (role)
                           {
                               case "admin": AdminMenu();
                                   break;
                               case "hr": HRMenu();
                                   break;
                               case "warehouse": WareMenu();
                                   break;
                               case "cashier": CashMenu();
                                   break;
                               case "bookkeeping": BookMenu();
                                   break;
                               case "user": UserMenu(GetInd(log));
                                   break;
                           }
                       }
                   }
               }
               Console.ReadKey();

           } while (t != 0);
    
        }

    }



    class Program
    {
        static void Main(string[] args)
        {

            Salers sl = new Salers();
            sl.StartProg();
        }
    }
}
