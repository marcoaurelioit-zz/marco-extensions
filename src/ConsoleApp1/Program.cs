using Marco.Extensions.ConnectionStringBuilder;
using System;

namespace ConsoleApp1
{
    class Program
    {
        private readonly DefaultConnectionString defaultConnectionString;

        public Program(DefaultConnectionString defaultConnectionString)
        {
            this.defaultConnectionString = defaultConnectionString;
        }

        private void Configuration()
        {
            var catalogDefault = defaultConnectionString.BuildConnectionString();
            var customCatalog = defaultConnectionString.BuildConnectionString("CUSTOMCATALOG");
        }


        static void Main(string[] args)
        {  
            Console.WriteLine("Hello World!");
        }
    }
}
