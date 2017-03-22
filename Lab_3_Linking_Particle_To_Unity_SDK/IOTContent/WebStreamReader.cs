using UnityEngine;
using System.Collections.Generic;
using System.Net;

public class WebStreamReader : System.IDisposable
{
	volatile bool running = false;
	string url = "";
	System.Threading.Thread thread = null;
	Queue<string> buffer = new Queue<string>();
	object lockHandle = new object();

	~WebStreamReader()
	{
		Dispose();
	}

	public void Start(string aURL)
	{
		if (!running)
		{
			url = aURL;
			thread = new System.Threading.Thread(Run);
			thread.Start();
		}
	}
	private void Run()
	{
		running = true;
		ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
		var request = System.Net.HttpWebRequest.Create(url);
		var response = request.GetResponse();
		var stream = response.GetResponseStream();
		byte[] data = new byte[2048];
		while (running)
		{
			int count = stream.Read(data, 0, 2048);
			if (count > 0)
			{
				lock(lockHandle)
				{
					string message = System.Text.UTF8Encoding.UTF8.GetString(data, 0, count);
					buffer.Enqueue(message);
				}
			}
		}
	}

	public string GetNextBlock()
	{
		string tmp = "";
		lock(lockHandle)
		{
			if (buffer.Count > 0)
			{
				tmp = buffer.Dequeue();
			}
		}
		return tmp;
	}

	public void Dispose()
	{
		running = false;
		thread.Abort();
	}
}