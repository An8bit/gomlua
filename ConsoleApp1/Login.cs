using System;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using xNet;

namespace ConsoleApp1
{
	// Token: 0x02000004 RID: 4
	internal class Login
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002434 File Offset: 0x00000634
		public string Gl(string cookie)
		{
			string str = "";
			this.useragent = "";
			if (cookie.Contains("|"))
			{
				string[] array = cookie.Split(new char[]
				{
					'|'
				});
				cookie = array[0];
				string text = array[1];
				this.useragent = ((text == "") ? "Mozilla/5.0 (iPhone; CPU iPhone OS 11_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1" : text);
			}
			else
			{
				cookie = cookie.Trim();
				this.useragent = "Mozilla/5.0 (iPhone; CPU iPhone OS 11_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1";
			}
			this.cookies = cookie;
			HttpRequest httpRequest = new HttpRequest
			{
				KeepAlive = true,
				AllowAutoRedirect = true,
				Cookies = new CookieDictionary(false),
				UserAgent = this.useragent
			};
			this.AddCookie(httpRequest, cookie);
			httpRequest.AddHeader("authority", "m.facebook.com");
			httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
			string text2 = httpRequest.Get("https://m.facebook.com/v2.9/dialog/oauth?app_id=901999673240219&cbt=1621310784808&channel_url=https%3A%2F%2Fstaticxx.facebook.com%2Fx%2Fconnect%2Fxd_arbiter%2F%3Fversion%3D46%23cb%3Df37e196fc0296%26domain%3Dgomlua.com%26origin%3Dhttps%253A%252F%252Fgomlua.com%252Ff1784b0562c04f4%26relation%3Dopener&client_id=901999673240219&display=touch&domain=gomlua.com&e2e=%7B%7D&enable_profile_selector=true&fallback_redirect_uri=https%3A%2F%2Fgomlua.com%2F%23%2Fauth%2Flogin&locale=en_US&logger_id=f2df7cbb4ea2afc&origin=2&redirect_uri=https%3A%2F%2Fstaticxx.facebook.com%2Fx%2Fconnect%2Fxd_arbiter%2F%3Fversion%3D46%23cb%3Df29221fe01f5f%26domain%3Dgomlua.com%26origin%3Dhttps%253A%252F%252Fgomlua.com%252Ff1784b0562c04f4%26relation%3Dopener%26frame%3Df27e7cb7f4262e4&response_type=token%2Csigned_request%2Cgraph_domain&return_scopes=true&scope=email%2Cpublic_profile&sdk=joey&version=v2.9", null).ToString();
			if (text2.Contains("access_token="))
			{
				str = Regex.Match(text2, "access_token=.*?(?<token>.*?)&").Groups["token"].ToString();
			}
			if (text2.Contains("Đăng nhập bằng Facebook") || text2.Contains("Log in With Facebook"))
			{
				string text3 = Regex.Match(text2, "name=\"fb_dtsg\" value=\".*?(?<fb>.*?)\"").Groups["fb"].ToString();
				string text4 = Regex.Match(text2, "name=\"jazoest\" value=\".*?(?<jaz>.*?)\"").Groups["jaz"].ToString();
				string text5 = Regex.Match(text2, "name=\"encrypted_post_body\" value=\".*?(?<encry>.*?)\"").Groups["encry"].ToString();
				string text6 = Regex.Match(text2, "name=\"logger_id\" value=\".*?(?<logger>.*?)\"").Groups["logger"].ToString();
				string text7 = Regex.Match(text2, "name=\"cbt\" value=\".*?(?<cbt>.*?)\"").Groups["cbt"].ToString();
				string text8 = string.Concat(new string[]
				{
					"jazoest=",
					text4,
					"&fb_dtsg=",
					text3,
					"&from_post=1&auth_type=&auth_nonce=&default_audience=&fbapp_pres=&ref=&ret=&return_format=return_scopes%2Cdenied_scopes%2Csigned_request%2Cgraph_domain%2Caccess_token%2Cbase_domain&dialog_type=gdp_v4&scope=email%2Cpublic_profile&sso_device=&logger_id=",
					text6,
					"&sheet_name=initial&display=popup&sdk=joey&facebook_sdk_version=&sdk_version=&user_code=&install_nonce=&loyalty_program_id=&referral_code=&messenger_page_id=&page_id_account_linking=&reset_messenger_state=&aid=&deferred_redirect_uri=&code_redirect_uri=&tp=unspecified&nonce=&fx_app=&code_challenge=&code_challenge_method=&encrypted_post_body=",
					text5,
					"&cbt=",
					text7,
					"&__CONFIRM__=1"
				});
				HttpRequest httpRequest2 = new HttpRequest();
				httpRequest2.AddHeader("User-Agent", this.useragent);
				httpRequest2.AddHeader("Cookie", cookie ?? "");
				string input = httpRequest2.Post("https://m.facebook.com/v4.0/dialog/oauth/confirm/", text8, "application/x-www-form-urlencoded").ToString();
				str = Regex.Match(input, "access_token=.*?(?<token>.*?)&").Groups["token"].ToString();
			}
			HttpRequest httpRequest3 = new HttpRequest();
			httpRequest3.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36");
			httpRequest3.AddHeader("Accept", "application/json, text/plain, */*");
			string text9 = httpRequest3.Get("https://gomlua.com/user/login?os=web&facebook_token=" + str, null).ToString();
			JObject jobject = JObject.Parse(text9);
			return (jobject["message"].ToString() == "Thanh cong") ? jobject["data"]["app_token"].ToString() : "";
		}
		// Token: 0x06000009 RID: 9 RVA: 0x00002770 File Offset: 0x00000970
		public string Getfb(string url, string cookie)
		{
			this.useragent = "";
			if (cookie.Contains("|"))
			{
				string[] array = cookie.Split(new char[]
				{
					'|'
				});
				cookie = array[0];
				string text = array[1];
				this.useragent = ((text == "") ? "Mozilla/5.0 (iPhone; CPU iPhone OS 11_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1" : text);
			}
			else
			{
				cookie = cookie.Trim();
				this.useragent = "Mozilla/5.0 (iPhone; CPU iPhone OS 11_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1";
			}
			HttpRequest httpRequest = new HttpRequest
			{
				KeepAlive = true,
				AllowAutoRedirect = true,
				Cookies = new CookieDictionary(false),
				UserAgent = this.useragent
			};
			this.AddCookie(httpRequest, cookie);
			httpRequest.AddHeader("accept-language", "vi,vi-VN;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
			httpRequest.AddHeader("upgrade-insecure-requests", "1");
			httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,/**;q=0.8,application/signed-exchange;v=b3;q=0.9");
			return httpRequest.Get(url, null).ToString();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002858 File Offset: 0x00000A58
		public void AddCookie(HttpRequest http, string cookie)
		{
			string[] array = cookie.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					'='
				});
				if (array3.Count<string>() > 1)
				{
					try
					{
						http.Cookies.Add(array3[0], array3[1]);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x04000004 RID: 4
		public string useragent = "";

		// Token: 0x04000005 RID: 5
		public string cookies = "";
	}
}
