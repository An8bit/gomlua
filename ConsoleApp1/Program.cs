using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("cokies: ");
            string a = Console.ReadLine();
            runing(a);
        }
        static void runing(string cokies)
        {
            Login logfb = new Login();
            logfb.Getfb("http://m.facebook.com/",cokies);
            string text = cokies;
            if (text == "")
            {
                return;
            }
            else
            {
                Login login = new Login();
                List_Job job = new List_Job();
                WorkJob WorkJob = new WorkJob();
                string text2 = login.Gl(text);             
                string userangent = login.useragent;
                string cookies = login.cookies;
                int num2 = 25;
                int num3 = 15;              
                string sl = job.size;

                if (text2 != "")
                {
                    job.GetData("https://gomlua.com/user/info?os=web", text2, userangent).ToString();
                    Console.WriteLine("dang nhap xong");
                    for (; ; )
                    {                     
                        Console.Clear();                                                                                                             
                        do
                        {
                            Random random = new Random();
                            int u2 = random.Next(num3, num2) * 1000;
                            Console.WriteLine("chờ: " + u2 / 1000 + "ms");
                            string a1 = job.Job(text2, userangent);
                            Console.WriteLine(a1);
                          job.GetData("https://gomlua.com/cpi/likeSuccess?os=web&link_id=" + job.id_job + "&like_old = undefined", text2, userangent);
                            if (a1 == null)
                            {
                                Console.WriteLine(a1);
                                return;
                            }                          

                            if (a1 == "OK" && job.timx(job.link_id)==false )
                            {
                                job.list.Add(job.link_id);
                                Console.WriteLine(job.num + " đồng");
                                Console.Write("so job\t" + job.size);
                                Console.Write("\tdang lam job: " + job.link_id);                              
                                Console.Write("\ttrang thai:" + job.react_type + "\n");

                                int num4;
                                if (job.react_type == "LIKE")
                                    num4 = 0;
                                else if (job.react_type == "LOVE")
                                    num4 = 1;
                                else if (job.react_type == "CARE")
                                    num4 = 2;
                                else if (job.react_type == "HAHA")
                                    num4 = 3;
                                else if (job.react_type == "WOW")
                                    num4 = 4;
                                else
                                    num4 = 5;
                                
                                string a2 = WorkJob.work(job.id_job, cookies, userangent, num4.ToString());                              

                                if (a2 == "NODE" || a2 == "URL")
                                {

                                    job.GetData(job.link_report + "LINK", text2, userangent);
                                    Console.WriteLine("report");

                                }
                                else if (a2 == "OK")
                                {
                                    for (int i = u2 / 1000; i > 0; i--)
                                    {
                                        Thread.Sleep(TimeSpan.FromSeconds(1.0));
                                        Console.Clear();
                                        Console.Write("chờ ");
                                        Console.WriteLine(i);

                                    }
                                    string data = job.GetData(job.link_revice, text2, userangent);
                                    Console.WriteLine("đang nhận");                                  

                                    if (data == "500")
                                    {
                                        job.GetData(job.link_report + "LINK", text2, userangent);
                                    }
                                    JObject r = JObject.Parse(data);
                                    Console.WriteLine(r["message"].ToString());                                                                    
                                   if (r["message"].ToString() == "Có lỗi xảy ra, xin vui lòng thử \"Nhận lúa\" lại!")
                                    {
                                        for (int i = 1; i < 4; i++)
                                        {
                                            data = job.GetData("https://gomlua.com/cpi/likeSuccess?os=web&link_id=" + job.id_job + "&like_old = undefined", text2, userangent);
                                            Console.WriteLine("dang lam lai lan " + i);
                                            Console.WriteLine(r["message"].ToString());
                                            if (r["status"].ToString() == "1")
                                            {
                                                Console.WriteLine(r["message"].ToString());
                                                return;
                                            }                                        
                                            Thread.Sleep(6000);
                                        }
                                        data = job.GetData("https://gomlua.com/cpi/reportBug?site=web&cpi_id=" + job.link_id + "&type=LIKE_TOKEN&report_type=", text2, userangent);
                                        job.GetData("https://gomlua.com/cpi/listCampaignFacebook?os=web&type=like_post", text2, userangent).ToString();
                                        Console.WriteLine("đã report");
                                        Thread.Sleep(u2);
                                    }
                                    else if (r["message"].ToString() == "Chiến dịch đã kết thúc")
                                    {
                                        job.GetData("https://gomlua.com/cpi/reportBug?site=web&cpi_id=" + job.link_id + "&&type=LIKE_TOKEN&report_type=OTHER", text2, userangent);
                                        job.GetData("https://gomlua.com/cpi/listCampaignFacebook?os=web&type=like_post", text2, userangent).ToString();
                                        Console.WriteLine("đã report");
                                        Thread.Sleep(u2);
                                    }

                                }

                            }
                        }
                        while(sl != "0" );
                      

                    }

                }
            }
        }
    }
}
