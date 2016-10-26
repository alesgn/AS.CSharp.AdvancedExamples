using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedExamples.LambdaExpExample
{
    //Lambda expression: unnamed method written in place of a delegate instance.
    //Compiler immediately converts it to either:
    //- A delegate instance.
    //- An expression tree (Expression<TDelegate>), representing code inside
    //lambda expression in a traversable object model. This allows the lambda expression to be interpreted later at runtime
    public class LambdaExp
    {
        delegate int Transformer(int i);
        delegate int TransformerArgs(int i, int j);
        
        public LambdaExp() {
            //Internally, compiler resolves lambda expr writing a private method filled with expression’s code.
            Transformer sqr = x => x * x; //Expression
            Func<int, int> sqrEq = x => x * x; //Equal but with Function Delegates (in int, out int) 
            Console.WriteLine(sqr(3)); // 9

            sqr = x => { return x * x; }; //Statement block

            //Compiler use type inference to know argument type. We can explicitate it anyway
            Func<int, int> sqrInf = (int x) => x * x; 

            TransformerArgs sqr2 = (x, y) => x * y; //2 input arguments
        }

        public void CapturedVariables() {
            int factor = 2;
            //Closure: Lambda exp that use "factor" (outer variable) capturing it (captured variable)
            Func<int, int> multiplier = n => n * factor;
            factor = 10;
            //Captured variable ("factor") evaluated now, when delegate is invoked, not when delegate was istantiate
            Console.WriteLine(multiplier(3)); //30 not 6

            //We can update Captured variables
            Func<int> UpdCaptured = () => factor++;
            Console.WriteLine(UpdCaptured());
            Console.WriteLine(factor);

            //Captured variable "seed" is out of its scope (where delegate is invoked). 
            //But var is still alive until delegate istance is within its scope
            Console.WriteLine("Out of scope captured variable test");
            Func<int> TestOutOfScope = OutOfScope();
            Console.WriteLine(TestOutOfScope());
            Console.WriteLine(TestOutOfScope());

            //If we capture "i", it is common to all delegate istances.
            //When we invoke them, "i" is evaluated with its last value (3)
            //If we capture "loopScopedi", it is redefined for every delegate istances creation, is different for every delegate.
            //When we invoke them, "loopScopedi" is evaluated with its value in the single delegate scope (0,1, or 2)
            Action[] actions = new Action[3];
            for (int i = 0; i < 3; i++)
            {
                int loopScopedi = i;
                actions[i] = () => Console.Write(loopScopedi);
                //actions[i] = () => Console.Write(i);
            }
            foreach (Action a in actions) a(); // 012
        }

        private Func<int> OutOfScope() {
            int seed = 0;
            return () => seed++;
        }

        private Func<int> AlwaysOutOfScope()
        {
            //captured variable "seed" defined within statement. Statement was fully executed every time delegate is invoked
            return () => { int seed = 0; return seed++; };
        }
    }
}
