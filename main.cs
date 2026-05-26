 using System;
using System.Net;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("HTTP Listener started on port 8002");

        string[] s3Facts =
        {
            "Scale storage resources to meet fluctuating needs with 99.999999999% (11 9s) of data durability.",
            "Store data across Amazon S3 storage classes to reduce costs without upfront investment or hardware refresh cycles.",
            "Protect your data with unmatched security, compliance, and audit capabilities.",
            "Easily manage data at any scale with robust access controls, flexible replication tools, and organization-wide visibility.",
            "Run big data analytics, artificial intelligence (AI), machine learning (ML), and high performance computing (HPC) applications.",
            "Meet Recovery Time Objectives (RTO), Recovery Point Objectives (RPO), and compliance requirements with S3's robust replication features."
        };

        var listener = new HttpListener();
        listener.Prefixes.Add("http://*:8002/");

        listener.Start();

        try
        {
            while (true)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();
                HttpListenerResponse response = ctx.Response;

                response.StatusCode = (int)HttpStatusCode.OK;
                response.StatusDescription = "OK";
                response.ContentType = "text/plain";

                int index = Random.Shared.Next(s3Facts.Length);

                Console.WriteLine(index);
                Console.WriteLine(s3Facts[index]);

                string message = $"{DateTime.Now.TimeOfDay} - {s3Facts[index]}";

                byte[] buffer = Encoding.UTF8.GetBytes(message);

                response.ContentLength64 = buffer.Length;

                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                response.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            listener.Close();
        }
    }
}
