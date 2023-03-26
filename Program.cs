using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] columns = { "ФИО", "Возраст", "e-mail", "Программирование", "Математика", "Физика" };  //  Массив с заголовками 1
            string[] resultColumns = { "ФИО", "Общий балл", "Средний балл" };  //  Массив с заголовками 2

            string[] fullName = { "Петрова Василиса Ивановна", "Иванов Майкл Робертович", "Грей Алексанра Батьковна", "Капитан Джек Воробей" };
            string[] eMail = { "petrova88@mail.ru", "imraptor@mail.ru", "sasha@google.com", "cap@black.pearl" };
            int[] age = { 35, 23, 30, 59 };
            float[] programmingScore = { 8.2f, 9.4f, 6.7f, 5.9f };
            float[] mathScore = { 9.0f, 9.5f, 7.1f, 8.8f };
            float[] physicsScore = { 8.7f, 7.7f, 7.3f, 9.9f };

            float[] totalScore = new float[fullName.Length];
            float[] averageScore = new float[fullName.Length];

            List<float[]> scores = new List<float[]> { programmingScore, mathScore, physicsScore }; // Список из массивов с оценками.

            Dictionary<string, string[]> nativeData = new Dictionary<string, string[]> { // Словарь со всеми данными по учащимся.
                { "ФИО", fullName},
                { "Возраст", ToStringArr(age) },
                { "e-mail", eMail },
                { "Программирование", ToStringArr(programmingScore) },
                { "Математика", ToStringArr(mathScore) },
                { "Физика", ToStringArr(physicsScore) }
            };

            ShowInfo(columns, nativeData);
            Console.ReadKey();

            Console.WriteLine("");

            ShowInfo(resultColumns, CalculateScore());
            Console.ReadKey();

            #region Функции
            void ShowInfo(string[] col, Dictionary<string, string[]> data)
            {
                List<int> maxLen = new List<int>(); //  Список, содержащий информацию о максимальной длинне элементов в выводимых данных по каждому столбцу.

                for (int i = 0; i < col.Length; i++)
                {
                    maxLen.Add(col[i].Length);

                    for (int j = 0; j < data[col[i]].Length; j++)
                    {
                        if (maxLen[i] < data[col[i]][j].Length)
                            maxLen[i] = data[col[i]][j].Length;
                    }
                }

                for (int i = 0; i < col.Length; i++)
                {
                    Console.Write($"{col[i]} {string.Concat(Enumerable.Repeat(" ", maxLen[i] - col[i].Length))}");
                }
                Console.Write("\n");

                for (int j = 0; j < fullName.Length; j++)
                {
                    for (int i = 0; i < col.Length; i++)
                    {
                        Console.Write($"{data[col[i]][j]} {string.Concat(Enumerable.Repeat(" ", maxLen[i] - data[col[i]][j].Length))}");
                    }
                    Console.Write("\n");
                }
            }

            Dictionary<string, string[]> CalculateScore()   //  Создание словаря с рассчитанными данными (общий и средний баллы.)
            {
                for (int i = 0; i < fullName.Length; i++)
                {
                    foreach (float[] el in scores)
                        totalScore[i] += el[i];
                }

                for (int i = 0; i < fullName.Length; i++)
                    averageScore[i] = totalScore[i] / scores.Count();

                Dictionary<string, string[]> calcData = new Dictionary<string, string[]> {
                { "ФИО", fullName},
                { "Общий балл", ToStringArr(totalScore) },
                { "Средний балл", ToStringArr(averageScore) }
                };

                return calcData;
            }

            string[] ToStringArr<T>(T[] arr)    //  Преобразование входящего массива к строковому типу.
            {
                string[] arrOut = new string[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    arrOut[i] = float.Parse(arr[i].ToString()).ToString("#.##");

                return arrOut;
            }
            #endregion
        }
    }
}
