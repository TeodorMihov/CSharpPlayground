using System;

using static Algorithms.ConsolePrinter;

namespace Algorithms._01_Introduction
{
    public static class MathematicalConcepts_BookExamples
    {
        /// <summary>
        /// Делението с частно и остатък може да се използва за намиране на броя на цифрите на
        /// дадено естествено число n. Алгоритъмът е следният: Последователно делим (докато е възможно)
        /// целочислено n на 10. Очевидно, при всяко такова деление цифрите на n намаляват с една. Така,
        /// броят на цифрите на n се определя от броя на деленията, които извършваме, докато n стане равно
        ///     на нула:
        /// </summary>
        public static  void Task_1_12()
        {
            PrintCondition("Намерете броя на цифрите на дадено число.");
            var digits = 0;
            var n = 422242;
            for (int i = n; i > 0; i /= 10)
            {
                digits++;
            }

            PrintSolution($"Числото {n} съдържа {digits} цифри");
        }
    }
}