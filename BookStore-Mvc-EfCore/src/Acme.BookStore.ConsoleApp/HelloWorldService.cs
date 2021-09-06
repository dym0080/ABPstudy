using System;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.ConsoleApp
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("\tHello World!");
        }
    }
}
