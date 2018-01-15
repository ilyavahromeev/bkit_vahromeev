using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_6._1
{
    delegate int addOrSubtract(int n1, int n2);   

    class Program
    {
        static int addUp(int n1, int n2) {
            return n1 + n2;
        }
        static int subtract(int n1, int n2) {
            return n1 - n2;
        }

        static void method(int n1, int n2, addOrSubtract param) {
            string res = param(n1, n2).ToString();
            string s1 = n1.ToString();
            string s2 = n2.ToString();
            if (param == addUp) {
                Console.WriteLine("Параметр addUp:    " + s1 + " + " + s2 + " = " + res);
            } else if (param == subtract) {
                Console.WriteLine("Параметр subtract: " + s1 + " - " + s2 + " = " + res);
            } else {
                Console.WriteLine("Результат: " + res);
            }
        }

        static void function(int n1, int n2, Func<int, int, int> param) {
            string res = param(n1, n2).ToString();
            string s1 = n1.ToString();
            string s2 = n2.ToString();
            if (param == addUp) {
                Console.WriteLine("Параметр addUp:    " + s1 + " + " + s2 + " = " + res);
            }
            else if (param == subtract) {
                Console.WriteLine("Параметр subtract: " + s1 + " - " + s2 + " = " + res);
            } else {
                Console.WriteLine("Результат: " + res);
            }
        }

        static void Main(){
            int i1 = 10;
            int i2 = 1;

            Console.WriteLine("Метод, принимающий делегат в качестве параметра:");
            method(i1, i2, addUp);
            method(i1, i2, subtract);

            Console.WriteLine("\nВызов метода, передавая лямбда-выражения (сложение):");
            method(i1, i2, (int x, int y) => {
                return x + y;
            });

            Console.WriteLine("Вызов метода, передавая лямбда-выражения (вычитание):");
            method(i1, i2, (int x, int y) => {
                return x - y;
            });

            Console.WriteLine("\n\nИспользование обобщенного делегата Func<>:");
            Console.WriteLine("Функция, принимающая метод в качестве параметра:");
            function(i1, i2, addUp);
            function(i1, i2, subtract);

            Console.WriteLine("Вызов функции, передавая лямбда-выражения (сложение):");
            function(i1, i2, (int x, int y) => {
                return x + y;
            });

            Console.WriteLine("Вызов функции, передавая лямбда-выражения (вычитание):");
            function(i2, i2, (int x, int y) => {
                return x - y;
            });

            Console.ReadLine();
        }
    }
}
