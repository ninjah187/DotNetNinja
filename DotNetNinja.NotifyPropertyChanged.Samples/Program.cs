using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.NotifyPropertyChanged.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sample = new SampleNotifier();

            sample.PropertyChanged += (s, e) =>
            {
                Console.WriteLine($"> {e.PropertyName} changed");
            };

            sample.Property = 1;
            sample.Property2 = 2;
            sample.Property3 = 3;

            Console.ReadKey();
        }
    }
}
