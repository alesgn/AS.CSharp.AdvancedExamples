using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedExamples.DelegateExample;
using AdvancedExamples.LambdaExpExample;
using AdvancedExamples.EnumeratorExample;

namespace AdvancedExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //CallDelegate();
            //CalllambdaExp();
            CallEnumerator();
            CallExtension();
            Console.In.ReadLine();


        }

        private static void CallDelegate() {
            Console.WriteLine("Delegate Tests");
            DelegateClass dc = new DelegateClass();

            MulticastDelegateClass mdc = new MulticastDelegateClass();

            //Methods call in the order they were added to delegate instance (+= or -= create a new istance of delegate)
            //If no methods are assigned, delegate istance = null
            //If return type no void and more than one method is assigned to delegate, it return only last called method result
            //(preceding methods are still called, but their only return values are discarded)
            ProgressReporter pr = mdc.WriteProgressToConsole;
            pr += mdc.WriteProgressToFile; //combine
            //pr -= mdc.WriteProgressToConsole; //remove
            var methodClass = pr.Target; //Method class instance (only if method assigned to delegate is an instance method)
            var method = pr.Method; //Method

            mdc.HardWork(pr);

            Console.WriteLine("Generic Delegate");
            GenericDelegate gd = new GenericDelegate();
        }

        private static void CalllambdaExp()
        {
            Console.WriteLine("LambdaExp Tests");
            LambdaExp le = new LambdaExp();
            le.CapturedVariables();
        }

        private static void CallEnumerator()
        {
            Console.WriteLine("Enumerator Tests");
            EnumeratorTest le = new EnumeratorTest();
            
        }

        private static void CallExtension()
        {
            Console.WriteLine("Extension Tests");
            

        }
    }
}
