using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Homework_07
{     
    class Program
    {        
        /// <summary>
        /// автоматическое добавление записей в файл
        /// </summary>
        /// <param name="index">Требуемое количество записей</param>
        public static void AddNotesAuto(int index)
        {
            using (StreamWriter sw = new StreamWriter("db.csv", true, Encoding.Unicode))
            {
                string title = "Номер\t Дата и время создания\t Автор\t Заметка\t Примечание\t";
                sw.WriteLine(title);
                Random random = new Random();
                
                for (int i = 0; i < index; i++)
                {
                    string note = string.Empty;

                    note += $"{i + 1}\t";

                    string now = DateTime.Now.ToString();
                    note += $"{now}\t";

                    note += $"Автор {random.Next(100, 999)}\t";

                    note += $"Заметка {random.Next(100, 999)}\t";

                    note += $"Примечание {random.Next(100, 999)}\t";                   

                    sw.WriteLine(note);
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"db.csv";
            
            if (!File.Exists(path))
            {
                int value;
                bool isInt;
                Console.WriteLine("Введите количество заметок для автоматического создания файла с заметками");

                isInt = Int32.TryParse(Console.ReadLine(), out value);
                if (!isInt || value < 0)
                {
                    Console.WriteLine("недопустимое значение,введите целое число");
                    isInt = Int32.TryParse(Console.ReadLine(), out value);
                }
                AddNotesAuto(value);
            }
              
            Notebook nb = new Notebook(path);
            int i = 0;
            char key = Console.ReadKey(true).KeyChar;
            do
            {
                Console.WriteLine("Выберите пункт меню для дальнейших действий, нажав соответствующую цифру:");
                Console.WriteLine();
                Console.WriteLine("1 - Добавление записей в блокнот");
                Console.WriteLine("2 - Просмотр записей из блокнота в консоли");
                Console.WriteLine("3 - Редактирование записей в блокноте");
                Console.WriteLine("4 - Удаление записей из файла блокнота");
                Console.WriteLine("5 - Сортировка по дате добавления записи");
                Console.WriteLine("6 - Сортировка по номеру записи");
                Console.WriteLine("7 - Загрузка записей по выбранному диапазону дат");
                Console.WriteLine("8 - Выход");

                int value;
                bool isInt;

                isInt = Int32.TryParse(Console.ReadLine(), out value);
                while (!isInt || value < 1 || value > 8)
                {
                    Console.WriteLine("недопустимое значение,введите число  от 1 до 8");
                    isInt = Int32.TryParse(Console.ReadLine(), out value);
                }
                
                switch (value)
                {                    
                    case 1:                                               
                        do
                        {                            
                            Console.Write("\nВведите имя автора заметки: ");
                            string autor = Console.ReadLine();
                            Console.Write("Введите текст заметки: ");
                            string note = Console.ReadLine();
                            Console.Write("Введите примечание: ");
                            string remark = Console.ReadLine();
                            nb.AddNote(autor, note, remark);
                            Console.Write("Ввести следующую заметку н/д?"); key = Console.ReadKey(true).KeyChar;
                        } while (char.ToLower(key) == 'д');                        
                        break;

                    case 2:
                        nb.PrintDbToConsole();
                        break;

                    case 3:                        
                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("Введите номер записи, которую вы хотите редактировать");
                            isInt = Int32.TryParse(Console.ReadLine(), out value);
                            while (!isInt || value < 1 || value > nb.Count)
                            {
                                Console.WriteLine($"недопустимое значение,введите число  от 1 до {nb.Count}");
                                isInt = Int32.TryParse(Console.ReadLine(), out value);
                            }
                            nb.EditNote(value - 1);
                            Console.Write("Редактировать следующую заметку н/д?"); key = Console.ReadKey(true).KeyChar;
                        } while (char.ToLower(key) == 'д');                        
                        break;

                    case 4:
                        do
                        {
                            Console.WriteLine("Введите номер заметки, которую вы хотите удалить");
                            isInt = Int32.TryParse(Console.ReadLine(), out value);
                            while (!isInt || value < 1 || value > nb.Count)
                            {
                                Console.WriteLine($"недопустимое значение,введите число  от 1 до {nb.Count}");
                                isInt = Int32.TryParse(Console.ReadLine(), out value);
                            }
                            nb.RemoveNote(value - 1);
                            Console.Write("Удалить следующую заметку н/д?"); key = Console.ReadKey(true).KeyChar;
                        } while (char.ToLower(key) == 'д');
                        
                        break;

                    case 5:
                        nb.SortedByDate();
                        break;

                    case 6:
                        nb.SortedByNumber();
                        break;
                   
                    case 7:
                        DateTime dateTime;
                        do
                        {
                            Console.WriteLine("Введите начальную дату в формате год/месяц/день");
                            isInt = DateTime.TryParse(Console.ReadLine(), out dateTime);
                            while (!isInt)
                            {
                                Console.WriteLine("Введите корректную начальную дату в формате год/месяц/день");
                                isInt = DateTime.TryParse(Console.ReadLine(), out dateTime);
                            }
                            DateTime start = dateTime;
                            Console.WriteLine("Введите конечную дату в формате год/месяц/день");
                            isInt = DateTime.TryParse(Console.ReadLine(), out dateTime);
                            while (!isInt)
                            {
                                Console.WriteLine("Введите корректную начальную дату в формате год/месяц/день");
                                isInt = DateTime.TryParse(Console.ReadLine(), out dateTime);
                            }
                            DateTime end = dateTime;
                            nb.LoadByDate(start, end);
                            Console.Write("Загрузить данные по другому диапазону дат н/д?"); key = Console.ReadKey(true).KeyChar;
                        } while (char.ToLower(key) == 'д');                        
                        break;
                    case 8:
                        i = 1;
                        break;
                }
                Console.WriteLine();    
                
            } while (i==0);
        }
    }
}
