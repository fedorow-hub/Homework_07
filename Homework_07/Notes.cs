using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    struct Notes
    {
        #region Конструкторы
        /// <summary>
        /// создание заметки в блокноте
        /// </summary>
        /// <param name="Number">порядковый номер</param> 
        /// <param name="TimeCreateNote">дата создания заметки</param>
        /// <param name="Autor">автор заметки</param>
        /// <param name="Note">содержание заметки</param>
        /// <param name="Remark">примечание</param>       
        public Notes(int Number, DateTime TimeCreateNote, string Autor, string Note, string Remark)
        {
            this.number = Number;
            this.timeCreateNote = TimeCreateNote;
            this.autor = Autor;
            this.note = Note;
            this.remark = Remark;            
        }
        #endregion

        #region Методы
        public string Print()
        {
            return $"{this.number,10} {this.timeCreateNote,20} {this.autor,15} {this.note,15} {this.remark,15}";
        }        
        #endregion

        #region Свойства
        /// <summary>
        /// порядковый номер заметки
        /// </summary>
        public int Number { get { return this.number; } set { this.number = value; } }
        /// <summary>
        /// Время создания заметки
        /// </summary>
        public DateTime TimeCreateNote { get { return this.timeCreateNote; } private set { this.timeCreateNote = value; } }

        /// <summary>
        /// Автор
        /// </summary>
        public string Autor { get { return this.autor; } set { this.autor = value; } }

        /// <summary>
        /// Заметка
        /// </summary>
        public string Note { get { return this.note; } set { this.note = value; } }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Remark { get { return this.remark; } set { this.note = value; } }
                
        #endregion

        #region Поля
        /// <summary>
        /// поле "Порядковый номер заметки"
        /// </summary>
        private int number;
        /// <summary>
        /// поле "Время создания заметки"
        /// </summary>
        private DateTime timeCreateNote;

        /// <summary>
        /// поле "Автор"
        /// </summary>
        private string autor;

        /// <summary>
        /// поле "Заметка"
        /// </summary>
        private string note;

        /// <summary>
        /// поле "Примечание"
        /// </summary>
        private string remark;
      
        #endregion

    }
}
