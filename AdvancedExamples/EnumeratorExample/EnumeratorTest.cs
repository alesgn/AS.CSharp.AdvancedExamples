using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedExamples.EnumeratorExample
{
    //Enumerator: read-only, forward-only cursor over a sequence of values. Implements:
    //- System.Collections.IEnumerator
    //- System.Collections.Generic.IEnumerator<T>
    
    //Enumerable: object, logical representation of a sequence. It produces cursors over itself.
    //- Implements IEnumerable or IEnumerable<T>
    //- Has a method named GetEnumerator that returns an enumerator (to access to its sequence of values)
    
    public class EnumeratorTest
    {
        public EnumeratorTest() {
            
            //Initialization
            List<int> list = new List<int> { 1, 2, 3 }; //fast mode
            list.Add(4); //normal mode (compiler transform former code in this)

            //Foreach: enumerator consumer.
            foreach (int fib in Fibs(6))
                Console.Write (fib + " "); //OUTPUT: 1 1 2 3 5 8

            foreach (string foo in Foo(true))
                Console.WriteLine(foo + " ");

            //Sequence composition: call nested iterators
            foreach (int fib in EvenNumbersOnly(Fibs(6)))
                Console.Write(fib + " "); //OUTPUT: 2 8
        }

        //Iterator: enumerator producer. Method, property, or indexer that contains one or more yield statements.
        //Must return one of the 4 previous interfaces (otherwise, compiler error). Iterator method:
        static IEnumerable<int> Fibs (int fibCount) {
            for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++) {
                
                //Return statement: “Here’s the value you asked me to return from this method,” 
                //Yield return statement: “Here’s the next element you asked me to yield from this enumerator.” 
                //On each yield, control's returned to the caller (foreach), but callee’s (Fibs) state is maintained so can
                //continue executing until caller has finished enumerating
                yield return prevFib;
                int newFib = prevFib+curFib;
                prevFib = curFib;
                curFib = newFib;
            }   
        }

        //1) Multiple yield staments permitted
        //2) Earlier exit with break permitted
        //3) try block permitted only with finally block (no catch, too complex to handle, compiler translate Iterators as private classes.
        static IEnumerable<string> Foo(bool breakEarly) {
            try {
                yield return "One";
                yield return "Two";
                if (breakEarly)
                    yield break;
                yield return "Three"; 
            } finally { 
                Console.WriteLine("Everything's OK!");
            }
        }

        static IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
        {
            foreach (int x in sequence)
                if ((x % 2) == 0)
                    yield return x;
        }
    }
}
