using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using static Algorithms.ConsolePrinter;
    
namespace Algorithms._01_Introduction
{
    public static class MathematicalConcepts_Excercises
    {
        public static void Task_1()
        {
            PrintCondition("Дадени са множествата A = {1,2,4,5,7} и B = {2,3,4,5,6}. Да се определят множествата: AnB, AuB, A\\B, B\\A.");
            
            var a = new List<int>() {1, 2, 3, 4, 5, 7};
            var b = new List<int>() {2, 3, 4, 5, 6};
            PrintSolution("AuB = ", Union(a, b));
            PrintSolution("AnB = ", Intersect(a, b));
            PrintSolution("A\\B  = ", Except(a, b));
            PrintSolution("B\\A  = ", Except(b, a));
        }

        public static void Task_2()
        {
            PrintCondition("Дадени са две множества A и B и е известно, че AnB = A. Какво можете да кажете за множеството B?");
            PrintSolution("-- Общите елементи между А и B. ");
        }
        
        public static void Task_3()
        {
            PrintCondition("Операцията симетрична разлика + на A и B се дефинира така: A+B = (АuB) \\ (AnB). Да се определи симетричната разлика на множествата A = {1,2,4,5,7} и B = {2,3,4,5,6}.");
            var a = new List<int>() {1, 2, 3, 4, 5, 7};
            var b = new List<int>() {2, 3, 4, 5, 6};
            var union = Union(a, b);
            var intersect = Intersect(a, b);
            var simetricalDistance = Except(union, intersect); // TODO: add correct english name
            PrintSolution("Симетричната разлика е: ", simetricalDistance);
        }
        
        public static void Task_4()
        {
            PrintCondition("Една операция се нарича симетрична, ако A * B = B * A. Кои от изброените операции са симетрични: AuB, AnB, A\\B, B\\A, AB?");
            PrintSolution("АuB и АnB - двете са симетрични, защото независимо дали ще ги извикаме така или BuA/BnA пак ще получим същия резултат");
        }

        
        public static void Task_6()
        {
            PrintCondition("Да се намерят частното и остатъка от делението на m на n, ако (m,n) е: (7,3), (-7,3), (7,-3), (-7,-3), (3, 7), (-3,7), (3, -7), (-3,-7)");
            PrintSolution($"Остатък от (7,3) = {7 % 3}");
            PrintSolution($"Частно от (7,3) = {7 / 3}");
            PrintSolution($"Остатък от (-7,3) = {-7 % 3}");
            PrintSolution($"Частно от (-7,3) = {-7 / 3}");
            PrintSolution($"Остатък от (-7,-3) = {-7 % -3}");
            PrintSolution($"Частно от (-7,-3) = {-7 / -3}");
            PrintSolution($"Остатък от (3,7) = {3 % 7}");
            PrintSolution($"Частно от (3,7) = {3 / 7}");
            PrintSolution($"Остатък от (-3,7) = {-3 % 7}");
            PrintSolution($"Частно от (-3,7) = {-3 / 7}");
        }
        
        // подмножество - АeB
        private static bool IsSubset(IEnumerable<int> a, IEnumerable<int> b) => a.All(b.Contains);

        // обединение - AuB
        private static IEnumerable<int> Union(IEnumerable<int> a, IEnumerable<int> b) => a.Union(b);
        
        // сечение - AnB
        private static IEnumerable<int> Intersect(IEnumerable<int> a, IEnumerable<int> b) => a.Intersect(b);

        // разлика - A\B
        private static IEnumerable<int> Except(IEnumerable<int> a, IEnumerable<int> b) => a.Except(b);
    }
}