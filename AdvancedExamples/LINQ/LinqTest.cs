using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedExamples.LINQ
{
    //LINQ: Language Integrated Query. Set of language and framework features for writing structured type-safe queries over:
    //- Local object collections: any coll implementing IEnumerable<T> (an array, list, or XML DOM) in memory
    //- Remote data sources: tables in SQL Server. 
    //Benefits: compile-time type checking and dynamic query composition.
    //Core types namespaces: System.Linq and System.Linq.Expressions
    public class LinqTest
    {
        //LINQ basic units of data:
        //- Sequences: object that implements IEnumerable<T>
        //- Elements: each item in the sequence

        //Query operator: method that transforms a sequence. Accepts an input sequence and emits a transformed output sequence. 
        //Standard QO: in Enumerable class in System.Linq, all implemented as static extension methods.
        
        //LINQ-to-objects (Local Queries): operate over local sequences
        //Sequence from Remote queries: sequences additionally implement the IQueryable<T> interface and use standard query operators in the Queryable class.
        
        
        public LinqTest() { 
        
            //Query: expression that, when enumerated, transforms sequences with query operators. (sequence + operator)
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" }; //one sequence
            //one operator Where with Lambda Exp as argument with: 
            //- Input argument (n, string) is an input element, represents each name in the array, one a time (not whole sequence).
            //- Expression "body": must return a bool value. If true, the element should be included in the output sequence
            //Predicate: Lambda that has value IN and bool OUT
            IEnumerable<string> filteredNamesFluentSyntax = names.Where(n => n.Length >= 4); //Fluent Syntax
            //System.Linq.Enumerable.Where(names, n => n.Length >= 4); //explicit ext method form (compiler translation)

            IEnumerable<string> filteredNamesQueryExpression = from n in names
                                                               where n.Length >= 4
                                                               select n; //Query Expression Syntax

            foreach (string n in filteredNamesFluentSyntax)
                Console.WriteLine (n); //Dick Harry

            //Query operator general signature (is possible define a more complex method to create a delegate instance and
            //call query operator with it without lambda expressions):
            //public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource,bool> predicate)


            //Chaining Query Operators: where->order by->select->NEW sequence. Input arg "n" scope is internal every query operator
            IEnumerable<string> query = names
            .Where(n => n.Contains("a")) //Filter
            .OrderBy(n => n.Length) //Sorter
            .Select(n => n.ToUpper()); //Projector
            foreach (string name in query) Console.WriteLine(name);

            //Chaining Query Operators in Query Expression Syntax: compiler translate it always in Fluent Syntax
            IEnumerable<string> queryExpressionSyntax =
                from n in names //"n"=range variable (refers current element in sequence)
                where n.Contains("a") // Filter elements (works on "n" in array)
                orderby n.Length // Sort elements (works on "n" in sequence result after where operation)
                //possible to add "let" or "join" as well
                select n.ToUpper(); // Translate each element (project)
                //finish with "select" or "group by"
            foreach (string name in queryExpressionSyntax) Console.WriteLine(name);

            //Query Syntax VS Fluent Syntax
            //Deuce: use of Where, OrderBy, Select operators
            //Adv QS: 1) "Let" clause (introduce new variable alongside the range variable) 
            //        2) "SelectMany", "Join", or "GroupJoin", followed by an outer range variable reference
            //Adv FS: 1) Better for queries that comprise a single operator
            //        2) Many operators have no keyword in query syntax

            //Mixed Syntax Queries: possible mix "query" and "fluent" syntax. Only rule is query syntax must be complete
            int matches = (from n in names where n.Contains("a") select n).Count();

            //Deferred (Lazy) Execution: query operator executes when its returned enumerable is enumerated (MoveNext called on its enumerator).
            //No Lazy Execution operators (sudden execution): 
            //- Single element or scalar value (First or Count)
            //- Use conversion op on returned enumerable (ToArray, ToList, ToDictionary, ToLookup)
                        
            //How works: query operator return a "Decorator sequence" (rather than collection sequence). It wraps:
            //- a reference to real sequence that will be used at runtime
            //- lamda expression (ex: a predicate)
            //- optional external arguments
            //Proxy: return rather than a decorator if the output sequence performed no transformation
            IEnumerable<int> lessThanTen = new int[] { 5, 12, 3 }.Where (n => n < 10);
        }

        //Where implementation (FILTER)
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (TSource element in source)
                if (predicate(element))
                    yield return element;
        }

        //Select implementation (PROJECT): TSource IN, TResult OUT (equal or different from TSource).
        //Can "transform" elements. Compiler INFERS lambda return type (TResult) from expression itself
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            throw new NotImplementedException();
        }

        //OrderBy implementation (SORT): TSource IN, TKey OUT (is type of sorting key, infers from lambda expression) 
        public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        //Query operator execute when we do foreach on result sequence.
        private void ReEvaluation() { 
            var numbers = new List<int>() { 1, 2 };
            IEnumerable<int> query = numbers.Select (n => n * 10);

            foreach (int n in query) Console.Write (n + "|"); // 10|20|
            numbers.Clear();
            foreach (int n in query) Console.Write (n + "|"); // <nothing>

        //For freeze result sequence or don't reexecute when using foreach on same enumerable -> Conversion Operator    
        //- ToArray: copies the output of a query to an array
        //- ToList: copies to a generic List<T>
            numbers = new List<int>() { 1, 2 };
            List<int> timesTen = numbers.Select (n => n * 10).ToList(); // Executes immediately into a List<int>
            numbers.Clear();
            Console.WriteLine (timesTen.Count); // Still 2
        }

        private void QueryOperatorList()
        {
            //Query operator
            int[] numbers = { 10, 9, 8, 7, 6 };
            
            //Natural Order
            //Take: outputs the first x elements, discarding the rest:
            IEnumerable<int> firstThree = numbers.Take(3); // { 10, 9, 8 }
            
            //Skip: ignores the first x elements and outputs the rest:
            IEnumerable<int> lastTwo = numbers.Skip(3); // { 7, 6 }
            
            //Reverse: invert the order:
            IEnumerable<int> reversed = numbers.Reverse(); // { 6, 7, 8, 9, 10 }


            //One element return
            int firstNumber = numbers.First(); // 10
            int lastNumber = numbers.Last(); // 6
            int secondNumber = numbers.ElementAt(1); // 9
            int secondLowest = numbers.OrderBy(n=>n).Skip(1).First(); // 7

            //Aggregation operators return a scalar value; usually of numeric type:
            int count = numbers.Count(); // 5;
            int min = numbers.Min(); // 6;

            //Quantifiers return a bool value(don’t return collection, you can’t call further query operators):
            bool hasTheNumberNine = numbers.Contains (9); // true
            bool hasMoreThanZeroElements = numbers.Any(); // true
            bool hasAnOddElement = numbers.Any (n => n % 2 != 0); // true

            //Two input sequences. 
            //Concat: appends one sequence to another, and Union, which does the same but with duplicates removed:
            int[] seq1 = { 1, 2, 3 };
            int[] seq2 = { 3, 4, 5 };
            IEnumerable<int> concat = seq1.Concat (seq2); // { 1, 2, 3, 3, 4, 5 }
            IEnumerable<int> union = seq1.Union (seq2); // { 1, 2, 3, 4, 5 }
        }
    }
}
