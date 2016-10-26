using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedExamples.DelegateExample
{
    //Out of class, into the namespace
    public delegate void ProgressReporter(int percentComplete);

    //Multicast delegate: all delegate instances have multicast capability. 
    //A delegate instance can reference a list of target methods
    public class MulticastDelegateClass
    {
        public void HardWork(ProgressReporter p)
        {
            for (int i = 0; i < 10; i++)
            {
                p(i * 10); // Invoke delegate
                System.Threading.Thread.Sleep(100); // Simulate hard work
            }
        }

        public void WriteProgressToConsole(int percentComplete)
        {
            Console.WriteLine(percentComplete);
        }
        public void WriteProgressToFile(int percentComplete)
        {
            System.IO.File.WriteAllText("progress.txt", percentComplete.ToString());
        }
    }
}
