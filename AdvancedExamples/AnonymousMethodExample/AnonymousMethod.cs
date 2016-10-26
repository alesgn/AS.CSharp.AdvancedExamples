using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedExamples.AnonymousMethodExample
{
    delegate int AnonymousDelegate(int i);

    //Anonymous Method: like Lambda Expression but (same captured variable handling)
    public class AnonymousMethod
    {
        public AnonymousMethod() {
            //1) Not implicitly typed input parameters (not only "x", must be "int x")
            //2) Delegate keyword and statement block NEEDED
            //3) Not compiled in Expression tree
            AnonymousDelegate sqr = delegate(int x) { return x * x; };
            Console.WriteLine(sqr(3));

            //
            //Useful for events:

        }
    }
}
