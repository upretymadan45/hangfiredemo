using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hangfire.Services
{
    public class EmailService
    {
        public void SendEmail(string message)
        {
            Console.WriteLine("Email sent with message: " + message);
        }
    }
}
