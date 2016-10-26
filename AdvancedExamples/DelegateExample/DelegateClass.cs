using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedExamples.DelegateExample
{
    //Delegate VS Interface. Delegate design better than Interface design if one or more of these conditions are true:
    //• Interface defines only a single method.
    //• Multicast capability is needed.
    //• The subscriber needs to implement the interface multiple times. 
    //(If we want many different behaviour we have to define one class implementing same Interface for every different behavior)
    //Delegates are useful sometimes to substitute Interfaces

    public class DelegateClass
    {
        //Delegate: object that knows how to call a method. Reference to a method.
        //Delegate type: defines the kind of method that delegate instances can call (return type and its parameter types)
        //1) Define a new Delegate Type "Transformer" which can call int ...(int)
        public delegate int Transformer(int x);

        public delegate int Trans1(int x);
        public delegate int Trans2(int x);

        public delegate void TransObj(string x);
        public delegate object TransObjRet();

        public DelegateClass()
        {
            Transformer tr = new Transformer(Square); // 3a) Create delegate instance

            Transformer t = Square; // 3b SHORTCUT) Create delegate instance (assigning a method to a delegate variable)
                                    //Square = method group (without brackets or arguments). If overloaded, C# will pick the correct overload based on the signature of the delegate
                                    
            int resultr = tr.Invoke(3); //4a) Invoke delegate
            int result = t(3); // 4b SHORTCUT) Invoke delegate


            //Caller invokes the delegate, and then the delegate calls the target method. 
            //This indirection decouples the caller from the target method.


            Console.WriteLine (result); // 9

            //Perform method calls in the order they have been added to delegate
            Transformer tmulti = null;
            tmulti += Square;
            tmulti += SquareAndAdd;
            tmulti -= Square;
            
            int[] squareNumbers = {1,2,3,4};
            int[] squareResult = SquareNumber(squareNumbers, Square);
            Console.WriteLine(string.Concat(squareResult));
            
            //Delegate types are all incompatible each others
            Trans1 tr1 = Square;
            Trans2 tr2 = new Trans2(tr1); //It works!
            //Trans2 tr2 = tr1; //Doesn't works! Compile-time error
            tr2(3);
            
            Console.WriteLine(string.Concat(tr.Method.ToString()));
            Console.WriteLine(string.Concat(t.Method.ToString()));

            //Possibile assignement with more generic argument type (object rather than string) -> Contravariance
            TransObj tobj = new TransObj(SquareObj);

            //Possible method return type more specific than delegate return type  -> Covariance
            TransObjRet tobjret = SquareObjRet;
            object stringResult = tobjret();
        }
        
        //2) Compatible method to Transformer delegate
        public int Square(int x) { 
            return x * x; 
        }
        public int SquareAndAdd(int x)
        {
            return x * x + x;
        }

        public int[] SquareNumber(int[] numbers, Transformer t) {
            int[] squareNumbers = new int[numbers.Length];
            for(int i=0;i<numbers.Length;i++) {
                squareNumbers[i] = t(numbers[i]);
            }
            return squareNumbers;
        }

        //private string printIntArray(int[] printArray) {
        //    string.Concat(
        //    foreach (int number in printArray) { 
                
        //    }    
        //}

        public void SquareObj(object x)
        {
            int newx = Convert.ToInt32(x);
            //return newx * newx;
        }

        public string SquareObjRet()
        {
            return "Covariance";
        }
    }
}
