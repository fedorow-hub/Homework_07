using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework_07
{
    /// <summary>
    /// структура по работе с данными
    /// </summary>
    struct Notebook
    {
        #region поля блокнота
        /// <summary>
        /// основрой массив для хранения заметок
        /// </summary>
        private Notes[] notes;

        /// <summary>
        /// путь к файлу с данными
        /// </summary>
        private string path;

        /// <summary>
        /// счетчик записей в ежедневнике
        /// </summary>
        int index;

        /// <summary>
        /// массив заголовков
        /// </summary>
        string[] titles;

        #endregion

        /// <summary>
        /// создание блокнота
        /// </summary>
        /// <param name="Path">путь к файлу с данными</param>
        public Notebook(string Path)
        {
            this.path = Path;
            this.index = 0;
            this.titles = new string[0];
            this.notes = new Notes[1];

            this.Load(); // Загрузка данных
        }
        /// <summary>
        /// увеличение размера массива
        /// </summary>
        /// <param name="Flag"></param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.notes, this.notes.Length * 2);
            }
        }
        /// <summary>
        /// добавление записи в блокнот
        /// </summary>
        /// <param name="ConcreteNotes"></param>
        public void Add(Notes ConcreteNotes)
        {
            this.Resize(index >= this.notes.Length);
            this.notes[index] = ConcreteNotes;
            this.index++;
        }
        /// <summary>
        /// загрузка записей в блокнот из файла
        /// </summary>
        public void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split('\t', ',');

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('\t', ',');

                    Add(new Notes(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2], args[3], args[4]));
                }
            }
        }
        /// <summary>
        /// вывод записей из блокнота в консоль
        /// </summary>
        public void PrintDbToConsole()
        {
            Console.WriteLine($"{this.titles[0],10} {this.titles[1],10} {this.titles[2],10} {this.titles[3],15} {this.titles[4],15}");

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.notes[i].Print());
            }
        }
        /// <summary>
        /// добавление записей в файл
        /// </summary>
        public void AddNote(string autor, string note, string remark)
        {                            
                int i = this.index + 1;
                string now = DateTime.Now.ToString();                
                string[] field = new string[5];
                field[0] = $"{i}";                       
                field[1] = now;                    
                field[2] = autor;                     
                field[3] = note;
                field[4] = remark;
                Add(new Notes(Convert.ToInt32(field[0]), Convert.ToDateTime(field[1]), field[2], field[3], field[4]));
                File.Delete(path);
                Save(path);                        
        }
        /// <summary>
        /// удаление записи
        /// </summary>
        /// <param name="index">номер записи для удаления</param>
        public void RemoveNote(int index)
        {
            for (int i = index; i < this.index-1; i++)
            {
                this.notes[i] = this.notes[i + 1];
            }            
            this.index--;
            File.Delete(path);
            Save(path);            
        }
        /// <summary>
        /// сохранение изменений в файл
        /// </summary>
        /// <param name="Path">имя файла</param>
        public void Save(string Path)
        {
            using (StreamWriter sw = new StreamWriter(Path, false, Encoding.Unicode))
            {
                string note = $"{this.titles[0]}\t {this.titles[1]}\t {this.titles[2]}\t {this.titles[3]}\t {this.titles[4]}\t";
                sw.WriteLine(note);

                for (int i = 0; i < this.index; i++)
                {
                    note = string.Empty;

                    note += $"{this.notes[i].Number}\t";
                                        
                    note += $"{this.notes[i].TimeCreateNote}\t";

                    note += $"{this.notes[i].Autor}\t";

                    note += $"{this.notes[i].Note}\t";

                    note += $"{this.notes[i].Remark}\t";

                    sw.WriteLine(note);
                }
            }
        }
        /// <summary>
        /// редактирование записи
        /// </summary>
        /// <param name="index"></param>
        public void EditNote(int position)
        {
            string[] fields = new string[3];
            Console.WriteLine("Введите скорректированное имя автора заметки");
            fields[0] = $"{Console.ReadLine()}";

            Console.WriteLine("Введите скорректированную заметку");
            fields[1] = $"{Console.ReadLine()}";

            Console.WriteLine("Введите скорректированное примечание");
            fields[2] = $"{Console.ReadLine()}";

            this.notes[position] = new Notes(position + 1, this.notes[position - 1].TimeCreateNote, fields[0], fields[1], fields[2]);

            File.Delete(path);
            Save(path);
        }
        /// <summary>
        /// загрузка по в указанном диапазоне дат
        /// </summary>
        /// <param name="start">начальная дата</param>
        /// <param name="end">конечная дата</param>
        public void LoadByDate (DateTime start, DateTime end)
        {
            SortedByDate();
            int a; //переменная для сравнения с начальной датой
            int b; //переменная для сравнения с конечной датой
            int k = 0;//счетчик записей, соответствующих указаному диапазону дат
            for (int i = 0; i < this.index; i++)
            {
               a = start.CompareTo(notes[i].TimeCreateNote);
               b = end.CompareTo(notes[i].TimeCreateNote);
                
                if (a<=0 & b>=0)
                {
                    this.notes[k] = this.notes[i];
                    k++;
                }
            }
            this.index = k;            
        }
        /// <summary>
        /// метод сортировки по дате
        /// </summary>
        public void SortedByDate()
        {
            for (int j = 1; j < this.index; j++)
            {
                if (DateTime.Compare(notes[j].TimeCreateNote, notes[j - 1].TimeCreateNote) < 0)
                {
                    Swap(ref notes[j - 1], ref notes[j]);
                }
            }
        }
        /// <summary>
        /// метод замены двух соседних записей
        /// </summary>
        /// <param name="notes1">первая запись</param>
        /// <param name="notes2">вторая запись</param>
        public void Swap(ref Notes notes1, ref Notes notes2)
        {
            Notes notes;
            notes = notes1;
            notes1 = notes2;
            notes2 = notes;
        }
        /// <summary>
        /// метод сортировки по дате
        /// </summary>        
        public void SortedByNumber()
        {
            for (int j = 1; j < this.index; j++)
            {
                if (notes[j].Number < notes[j - 1].Number)
                {
                    Swap(ref notes[j - 1], ref notes[j]);
                }
            }
        }
        /// <summary>
        /// количество записей в блокноте
        /// </summary>
        public int Count { get { return this.index; } }
    }
}
