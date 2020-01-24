using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApi
{
  public  class JobJson
    {
        //место хранения данных котегории. Можно подключить БД
        private const string FilePath = @"TempDate.txt";

        string tempResulJson;


        /// <summary>
        /// получение данный Json из строки
        /// </summary>
        /// <param name="pathGetJson"></param>
        /// <returns></returns>
        public string GetDataJson(string pathGetJson)
        {
            string str = ""; //!!!Новая сторока
            try
          
            {
             // WebRequest req = WebRequest.Create(@"http://api.openweathermap.org/data/2.5/weather?q=Moscow&APPID=a5ca6c5692978d29d84474e9f351648c");
            WebRequest req = WebRequest.Create(pathGetJson);
            req.Method = "POST";
            //Вот тут пришлось погуглить так как не редко тип у каждого свой, в итоге нашел в одном из проектов на github.
            req.ContentType = "application/x-www-urlencoded";

            //  openweather openweather;//!!!Новая сторока
          
            WebResponse response = req.GetResponse();
            using (Stream s = response.GetResponseStream()) //Пишем в поток.
            {
                using (StreamReader r = new StreamReader(s)) //Читаем из потока.
                {
                    str = r.ReadToEnd(); //!!!Изменения
                }
            }
                tempResulJson = str;// для сохранения данных
                return str;

            }
            catch (Exception ex)
            {
                 str+=$"Произошла ошибка при олучении Json файла{ex}";
            }
            
            return str;
        }

        /// <summary>
        /// сохранение поученных Json данных
        /// </summary>
        /// <param name="preliminarilyGob"></param>
        /// <returns></returns>
        public string SaveChanges(string preliminarilyGob)
        {
            string logJob;

            if (string.IsNullOrEmpty(preliminarilyGob))
            {
                logJob = "Поле ввода не должно быть пустм и содержать одни пробелы";
                return logJob;
            }

            else
            {
                try
                {

                    var data = JsonConvert.SerializeObject(preliminarilyGob);

                  //  File.WriteAllText(FilePath, data); //сохраняем 

                    using (StreamWriter sw = new StreamWriter(FilePath, true, System.Text.Encoding.Default))

                    // using (StreamWriter sw = new StreamWriter(myPachDir + @"texLog.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(data); // запись

                    }

                    return logJob = "Данные удачно записанны";

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при записи полученного файла Json ");
                    return logJob = $"Данные НЕ записанны \t\n{ex}";
                }

            }
        }


        public void SaveLinkJson(string pathLink)
        {
            string tempPathDir = @"Log\"; // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            using (StreamWriter sw = new StreamWriter(@"pathLinkJson.txt", true, System.Text.Encoding.Default))

            // using (StreamWriter sw = new StreamWriter(myPachDir + @"texLog.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(pathLink); // запись

            }
        }


        //Открытие сохраненного файла
    public void OpehSaveResult()
    {

    }


    }



}
