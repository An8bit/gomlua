using System;
using System.Text.RegularExpressions;
using xNet;

namespace ConsoleApp1
{
	// Token: 0x02000008 RID: 8
	internal class WorkJob
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000047B8 File Offset: 0x000029B8
		public string work(string id, string cookie, string useragent, string o)
		{
			Login login = new Login();
			string text = this.Get_Fb("https://mbasic.facebook.com/", cookie, useragent);
			string text2 = this.Get_Fb("https://mbasic.facebook.com/checkpoint/1501092823525282/?next=https%3A%2F%2Fm.facebook.com%2F", cookie, useragent);
			bool flag = text.Contains("Đăng nhập Facebook") || text.Contains("Log in to Facebook") || text.Contains("Facebook - Log In or Sign Up") || text.Contains("Facebook - Đăng nhập hoặc đăng ký");
			bool flag2 = text2.Contains("Chúng tôi đã tạm ngừng tài khoản của bạn") || text2.Contains("We Suspended Your Account");
			string result;
			if (flag)
			{
				result = "die";
			}
			else if (flag2)
			{
				result = "282";
			}
			else
			{
				string text3 = this.Geturl("https://mbasic.facebook.com/" + id, cookie);
				if (text3 == "" || text3.Contains(id))
				{
					result = "URL";
				}
				else
				{
					string text4 = this.Get_Fb(text3, cookie, useragent).ToString();
					if (text4.Contains("Bạn tạm thời bị chặn") || text4.Contains("You’re Temporarily Blocked"))
					{
						result = "Block";
					}
					else
					{
						string text5 = Regex.Match(text4, "/reactions/picker/?.*?(?<react>.*?)\"").Groups["react"].ToString().Replace("amp;", "");
						if (text5 == "")
						{
							result = "NODE";
						}
						else
						{
							try
							{
								string text6 = login.Getfb("https://mbasic.facebook.com/reactions/picker/" + text5, cookie);
								if (text6.Contains("Bạn tạm thời bị chặn") || text6.Contains("You’re Temporarily Blocked"))
								{
									result = "Block";
								}
								else
								{
									string str = Regex.Matches(text6, "/ufi/reaction/?.*?(?<cx>.*?)\"")[int.Parse(o)].Groups["cx"].ToString().Replace("amp;", "");
									//HttpRequest httpRequest = new HttpRequest();
									//httpRequest.AddHeader("authority", "mbasic.facebook.com");
									//httpRequest.UserAgent = useragent;
									//httpRequest.AddHeader("accept-language", "vi,vi-VN;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
									//httpRequest.AddHeader("upgrade-insecure-requests", "1");
									//httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,/**;q=0.8,application/signed-exchange;v=b3;q=0.9");
									//httpRequest.AddHeader("cookie", cookie);
									string text7 = login.Getfb("https://mbasic.facebook.com/ufi/reaction/" + str, cookie).ToString();
									if (text7.Contains("Tài khoản của bạn hiện bị hạn chế") || text7.Contains("Your account is restricted at the moment") || text7.Contains("Giờ bạn chưa dùng được tính năng này") || text7.Contains("You Can't Use This Feature Right Now") || text7.Contains("You can't use this feature at the moment"))
									{
										result = "Block_tuongtac";
									}
									else
									{
										result = "OK";
									}
								}
							}
							catch (ArgumentOutOfRangeException)
							{
								result = "NODE";
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00004A9C File Offset: 0x00002C9C
		public string Geturl(string url, string cookie)
		{
			string result;
			try
			{
				string userAgent;
				if (cookie.Contains("|"))
				{
					string[] array = cookie.Split(new char[]
					{
						'|'
					});
					cookie = array[0];
					string text = array[1];
					userAgent = ((text == "") ? "Mozilla/5.0 (iPhone; CPU iPhone OS 11_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1" : text);
				}
				else
				{
					cookie = cookie.Trim();
					userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 11_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1";
				
				}
				HttpRequest httpRequest = new HttpRequest
				{
					KeepAlive = true,
					AllowAutoRedirect = true,
					Cookies = new CookieDictionary(false),
					UserAgent = userAgent
				};
				Login login = new Login();
				login.AddCookie(httpRequest, cookie);
				httpRequest.AddHeader("authority", "mbasic.facebook.com");
				httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,/**;q=0.8,application/signed-exchange;v=b3;q=0.9");
				Uri address = httpRequest.Get(url, null).Address;
				result = address.ToString();
			}
			catch (HttpException)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00004B90 File Offset: 0x00002D90
		public string Get_Fb(string url, string cookie, string useragent)
		{
			HttpRequest httpRequest = new HttpRequest();
            httpRequest.AddHeader("authority", "mbasic.facebook.com");
            httpRequest.AddHeader("scheme", "https");
            httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            httpRequest.AddHeader("accept-language", "en-US,en;q=0.9,vi;q=0.8");
            httpRequest.AddHeader("cache-control", "max-age=0");
            httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
            httpRequest.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            httpRequest.AddHeader("sec-fetch-dest", "document");
            httpRequest.AddHeader("sec-fetch-mode", "navigate");
            httpRequest.AddHeader("sec-fetch-site",
                    "none");
            httpRequest.AddHeader("sec-fetch-user",
                    "?1");
            httpRequest.AddHeader("upgrade-insecure-requests",
                    "1");
           
            httpRequest.UserAgent = useragent;			
			httpRequest.AddHeader("cookie", cookie);
			return httpRequest.Get(url,null).ToString();
		   
		}
	}
}
