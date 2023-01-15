using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace ConsoleApp1
{
	internal class List_Job
	{
        public string link_id = "";

        // Token: 0x04000024 RID: 36
        public string id_job = "";

		// Token: 0x04000025 RID: 37
		public string react_type = "";

		// Token: 0x04000026 RID: 38
		public string link_revice = "";

		// Token: 0x04000027 RID: 39
		public string link_report = "";
		public string size;
		public int num;		
		public List<string> list = new List<string>();
		
		public bool timx(string a)
        {
			if(list.Contains(a)==true)
			return true;
			else return false;

        }
       
        public string GetData(string url, string app_token, string useragent)
		{
			string result;
			try
			{
				HttpRequest httpRequest = new HttpRequest
				{
					KeepAlive = true,
					AllowAutoRedirect = true,
					UserAgent = useragent
				};
				httpRequest.AddHeader("app_token", app_token);
				result = httpRequest.Get(url, null).ToString();
			}
			catch (HttpException)
			{
				result = "500";
			}
			return result;
		}

		public string Job(string app_token, string useragent, string whypay = null)
		{
			string data = this.GetData("https://gomlua.com/cpi/listCampaignFacebook?os=web&type=like_post", app_token, useragent);			
			string result;
			if (data == "500")
			{
				result = "500";
			}
			else
			{
				JObject jobject = JObject.Parse(data);
				 num = int.Parse(jobject["data"]["current_paddy"].ToString());
				if (num >= 10000)
				{
					string data2 = this.GetData("https://gomlua.com/card/transferWhypay?os=web&amount=" + num.ToString() + "&whypay_code=" + whypay, app_token, useragent);
					if (data2.Contains("Thành công"))
					{
						return "withdraw";
					}
				}
				if (jobject["data"]["size"].ToString() == "0")
				{					
					return "hết job";

				}
				else
				{

					size = jobject["data"]["size"].ToString();					
					link_id = jobject["data"]["list"][0]["link_id"].ToString();					
					this.react_type = jobject["data"]["list"][0]["react_type"].ToString();
					this.id_job = jobject["data"]["list"][0]["campaign_id"].ToString();
					int num2 = int.Parse(this.link_id);
					if (num2 < 399999 || num2 < 419999 || num2 > 469999)
					{
						this.link_revice = "https://gomlua.com/cpi/likeSuccess?os=web&link_id=" + this.link_id + "&like_old=undefined";
						//this.link_revice = "https://gomlua.com/cpi/likeSuccess?os=web&link_id" + this.link_id + "&&like_old=1";
						this.link_report = "https://gomlua.com/cpi/reportBug?site=web&cpi_id=" + this.link_id + "&type=LIKE_TOKEN&report_type=";
					}
					else if (num2 < 409999 || num2 < 469999)
					{
						this.link_revice = "https://gomlua.com/likeToken/likeSuccess?os=web&link_id=" + this.link_id + "&like_count=undefined";
						this.link_report = "https://gomlua.com/cpi/reportBug?site=web&cpi_id=" + this.link_id + "&type=LIKE_TOKEN&report_type=";
					}
					result = "OK";
				}
			}
			return result;
		}

	}
}
