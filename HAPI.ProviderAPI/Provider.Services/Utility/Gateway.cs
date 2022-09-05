using Provider.Structures.Interfaces;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Services.Utility
{
	public class Gateway : IGateway
	{
		public string GetJson(string url)
		{
			return url.GetJsonFromUrl();
		}

		public Task<string> GetJsonAsync(string url, Dictionary<string, string> headers = null)
		{
			return url.GetJsonFromUrlAsync(requestFilter: webReq =>
			{
				if (headers != null && headers.Count > 0)
				{
					foreach (var header in headers)
					{
						webReq.Headers.Add(header.Key, header.Value);
					}
				}
			});
		}

		/// <summary>
		/// Post a json object to an endpoint. If the object is a string, we will post the string as is else it will be serialized to json in camel case.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="json"></param>
		/// <param name="headers"></param>
		/// <returns></returns>
		public Task<string> PostJsonToUrlAsync(string url, object json, Dictionary<string, string> headers = null)
		{
			string jsonString = "";
			if (json.GetType() == typeof(string))
			{
				jsonString = json as string;
			}
			else
			{
				using (JsConfig.With(new Config { TextCase = TextCase.CamelCase, IncludeNullValues = true }))
				{
					jsonString = json.ToJson();
				}
			}

			//Log.Information($"PostJsonToUrlAsync request: {url}\n         {jsonString}");
			Task<string> response = url.PostJsonToUrlAsync(jsonString, requestFilter: webReq =>
			{
				if (headers != null && headers.Count > 0)
				{
					foreach (var header in headers)
					{
						webReq.Headers.Add(header.Key, header.Value);
					}
				}
			});
			//Log.Verbose($"PostJsonToUrlAsync response: {url}\n         {response}");
			return response;
		}
	}
}
