using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Structures.Interfaces
{
	public interface IGateway
	{
		string GetJson(string url);
		Task<string> GetJsonAsync(string url, Dictionary<string, string> headers = null);
		Task<string> PostJsonToUrlAsync(string url, object json, Dictionary<string, string> headers = null);
	}
}
