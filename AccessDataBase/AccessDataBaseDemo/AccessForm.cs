using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessDataBase
{
    public partial class AccessForm : Form
    {
        // конструктор класса формы
        public AccessForm()
        {
            InitializeComponent();
        }

        // обработчик события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // отключаемся от базы данных
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Подключаем базу данных
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb";  //строка соеденения
            //ссылка на экземпляр класса OleDbConnection для соединения с Базой данных
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                  //создаем соеденение
            OleDbDataReader dbReader;
            try
            {
                // Выполянем запрос к БД
                dbConnection.Open();                                            // открываем соеденение
                string query = "SELECT * FROM Worker";                          // строка запроса
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection); // команда
                dbReader = dbCommand.ExecuteReader();                           // считываем данные

                // Проверяем данные
                if (dbReader.HasRows == false)
                {
                    MessageBox.Show("Данные не найдены!", "Ошибка!");
                }
                else
                {
                    // Чтение данных
                    while (dbReader.Read())
                    {
                        // Выводим данные
                        MainDataGridBox.Rows.Add(dbReader["id"], dbReader["Name"], dbReader["Cost"], dbReader["Quantity"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение: {ex.Message}", "Вызвано исключение!");
            }
            finally
            {
                // Закрываем соеденение с БД
                dbConnection.Close();
            }
        }

        private void обПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа подключения Базы данных, также её просмотр и редактирование.", "Об программе");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            // Подключаем базу данных
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb";  //строка соеденения
            //ссылка на экземпляр класса OleDbConnection для соединения с Базой данных
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                  //создаем соеденение
            OleDbDataReader dbReader;
            try
            {
                // Выполянем запрос к БД
                dbConnection.Open();                                            // открываем соеденение
                string query = "SELECT * FROM Worker";                          // строка запроса
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection); // команда
                dbReader = dbCommand.ExecuteReader();                           // считываем данные

                // Проверяем данные
                if (dbReader.HasRows == false)
                {
                    MessageBox.Show("Данные не найдены!", "Ошибка!");
                }
                else
                {
                    // Чтение данных
                    while (dbReader.Read())
                    {
                        // Выводим данные
                        MainDataGridBox.Rows.Add(dbReader["id"], dbReader["Name"], dbReader["Cost"], dbReader["Quantity"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение: {ex.Message}", "Вызвано исключение!");
            }
            finally
            {
                // Закрываем соеденение с БД
                dbConnection.Close();
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            //проверка количества выбранных строк
            if (MainDataGridBox.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            //записываем выбранную строку
            int index = MainDataGridBox.SelectedRows[0].Index;

            //проверка данных в таблице
            if (MainDataGridBox.Rows[index].Cells[0].Value == null ||
                MainDataGridBox.Rows[index].Cells[1].Value == null ||
                MainDataGridBox.Rows[index].Cells[2].Value == null ||
                MainDataGridBox.Rows[index].Cells[3].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            // считаем данные
            string id = MainDataGridBox.Rows[index].Cells[0].Value.ToString();
            string name = MainDataGridBox.Rows[index].Cells[1].Value.ToString();
            string cost = MainDataGridBox.Rows[index].Cells[2].Value.ToString();
            string quantity = MainDataGridBox.Rows[index].Cells[3].Value.ToString();

            // создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            try
            {
                // Выполянем запрос к БД
                dbConnection.Open();                                                                                     //открываем соеденение
                string query = "INSERT INTO Worker VALUES (" + id + ", '" + name + "', " + cost + ", " + quantity + ")"; //строка запроса
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);                                          //команда

                // Выполняем запрос
                if (dbCommand.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
                else
                    MessageBox.Show("Данные добавлены!", "Внимание!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение: {ex.Message}", "Вызвано исключение!");
            }
            finally
            {
                // Закрываем соеденение с БД
                dbConnection.Close();
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            // Проверим количество выбранных строк
            if (MainDataGridBox.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            // Запомним выбранную строку
            int index = MainDataGridBox.SelectedRows[0].Index;

            // Проверим данные в таблицы
            if (MainDataGridBox.Rows[index].Cells[0].Value == null ||
                MainDataGridBox.Rows[index].Cells[1].Value == null ||
                MainDataGridBox.Rows[index].Cells[2].Value == null ||
                MainDataGridBox.Rows[index].Cells[3].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            // Считаем данные
            string id = MainDataGridBox.Rows[index].Cells[0].Value.ToString();
            string name = MainDataGridBox.Rows[index].Cells[1].Value.ToString();
            string cost = MainDataGridBox.Rows[index].Cells[2].Value.ToString();
            string quantity = MainDataGridBox.Rows[index].Cells[3].Value.ToString();

            // Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb"; //строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                 //создаем соеденение

            try
            {
                // Выполянем запрос к БД
                dbConnection.Open();//открываем соеденение
                string query = "UPDATE Worker SET Name = '" + name + "',Cost=" + cost + ",Quantity=" + quantity + " WHERE id = " + id;//строка запроса
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);                                                       //команда

                //Выполняем запрос
                if (dbCommand.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
                else
                {
                    MessageBox.Show("Данные изменены!", "Внимание!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение: {ex.Message}", "Вызвано исключение!");
            }
            finally
            {
                // Закрываем соеденение с БД
                dbConnection.Close();
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            //Проверим количество выбранных строк
            if (MainDataGridBox.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            //Запомним выбранную строку
            int index = MainDataGridBox.SelectedRows[0].Index;

            // Проверим данные в таблицы
            if (MainDataGridBox.Rows[index].Cells[0].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            // Считаем данные
            string id = MainDataGridBox.Rows[index].Cells[0].Value.ToString();

            // Создаем соеденение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb";  // строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                   // создаем соеденение

            try
            {
                // Выполянем запрос к БД
                dbConnection.Open();                                            // открываем соеденение
                string query = "DELETE FROM Worker WHERE id = " + id;       // строка запроса
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection); // команда

                // Выполняем запрос
                if (dbCommand.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
                else
                {
                    MessageBox.Show("Данные удалены!", "Внимание!");
                    // Удаляем данные из таблицы в форме
                    MainDataGridBox.Rows.RemoveAt(index);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show($"Сообщение: {ex.Message}", "Вызвано исключение!");
            }
            finally
            {
                // Закрываем соеденение с БД
                dbConnection.Close();
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа подключения Базы данных, также её просмотр и редактирование.", "Об программе");

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
