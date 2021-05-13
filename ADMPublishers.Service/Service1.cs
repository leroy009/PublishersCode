using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace ADMPublishers.Service
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public Service1()
        {
            InitializeComponent();
        }
        //Using this since its a pain to debug services
        public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);


            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44307/Admin/Author/");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("GetAllArray");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<Author[]>();
            //        readTask.Wait();

            //        var authors = readTask.Result;
            //        WriteToFile("Service is writing authors at " + DateTime.Now);
            //        WriteCSV(authors);
            //        WriteToFile("Service has completed writing authors at " + DateTime.Now);
            //    }
            //}

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is about to fetch author from api at " + DateTime.Now);

            try
            {
                using (var client = new HttpClient())
                {
                    WriteToFile("HttpClient from api at " + DateTime.Now);

                    client.BaseAddress = new Uri("https://localhost:44307/Admin/Author/");
                    //HTTP GET
                    var responseTask = client.GetAsync("GetAllArray");
                    responseTask.Wait();
                    WriteToFile("responseTask.Wait(); from api at " + DateTime.Now);

                    var result = responseTask.Result;
                    WriteToFile("result from api at " + DateTime.Now);

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Author[]>();
                        readTask.Wait();

                        var authors = readTask.Result;
                        WriteToFile("Service is writing authors at " + DateTime.Now);
                        WriteCSV(authors);
                        WriteToFile("Service has completed writing authors at " + DateTime.Now);
                    }
                }
            }
            catch(Exception ex)
            {
                WriteToFile("Error: " + ex.Message);
            }            
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        public void WriteCSV<T>(IEnumerable<T> items)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .OrderBy(p => p.Name);

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Date.ToString("yyyyMMdd");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Guid id = Guid.NewGuid();
            string filepath = path +"\\"+ id + "_" + DateTime.Now.Date.ToString("yyMMddHHss") + ".csv";

            if (File.Exists(filepath))
            {
                using (var writer = new StreamWriter(filepath))
                {
                    foreach (var item in items)
                    {
                        writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                    }
                }
            }
            else
            {
                using (var writer = new StreamWriter(filepath))
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                    foreach (var item in items)
                    {
                        writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                    }
                }
            }                
        }
    }
}
