using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedExamples.DelegateExample
{
    public class GenericDelegate
    {
        public delegate T Transformer<T>(T arg);

        public GenericDelegate()
        {
            Transformer<int> t = Square;           
            int result = t(3); // Invoke delegate
            
            Console.WriteLine (result); // 9

            int[] squareNumbers = {1,2,3,4};
            TransformNumber(squareNumbers, Square);
            Console.WriteLine(string.Concat(squareNumbers));

            double[] squareDoubleNumbers = { 1.0, 2.5, 3.4, 4.6 };
            TransformNumber(squareDoubleNumbers, Square);
            Console.WriteLine(string.Concat(squareDoubleNumbers));
        }
        
        //Compatible method to Transformer delegate
        static int Square(int x) { 
            return x * x; 
        }

        static double Square(double x) {
            return x * x;
        }

        public static void TransformNumber<T>(T[] values, Transformer<T> t)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = t(values[i]);
        }
    }
}
