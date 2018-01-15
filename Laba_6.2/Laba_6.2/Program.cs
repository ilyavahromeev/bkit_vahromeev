using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Laba_6._2
{
    class Program
    {
        public class newAtt : Attribute {
            public newAtt() { }
            public newAtt(string param) {
                desc = param;
            }

            public string desc { get; set; }
        }

        public class toInspect {
            public toInspect() { }
            public toInspect(int n) { }
            public toInspect(string param) { }

            public int addUp(int n1, int n2) {
                return n1 + n2;
            }
            public int subtract(int n1, int n2) {
                return n1 - n2;
            }

            [newAtt("Описание переменной prop1")]
            public string prop1 {
                get { return _prop1; }
                set { _prop1 = value; }
            }
            private string _prop1;

            public int prop2 { get; set; }

            [newAtt("Описание переменной prop3")]
            public double prop3 {
                get; private set;
            }

            public int pubInt;
            public float pubFloat;
            internal static object t;
        }

        public static bool getAtt(PropertyInfo checkType, Type attType, out object att) {
            bool res = false;
            att = null;

            var search = checkType.GetCustomAttributes(attType, false);
            if (search.Length > 0) {
                res = true;
                att = search[0];
            }

            return res;
        }

        static void Main() {
            Console.WriteLine("Вывод информации о конструкторах, свойствах, методах:\n");
            Type t = typeof(toInspect);
            Console.WriteLine("Название класса: toInspect");
            Console.WriteLine("Полное название класса: " + t.FullName);
            Console.WriteLine("Класс унаследован от: " + t.BaseType.FullName);
            Console.WriteLine("Класс находится в пространстве: " + t.Namespace);
            Console.WriteLine("Класс находится в сборке: " + t.AssemblyQualifiedName);

            Console.WriteLine("\nИнформация о конструкторах, выведенная в цикле:");
            foreach (var constructor in t.GetConstructors()) {
                Console.WriteLine(constructor);
            }

            Console.WriteLine("\nИнформация о методах, выведенная в цикле: ");
            int methodCount = 1;
            foreach (var meth in t.GetMethods()) {
                Console.WriteLine("Название " + methodCount.ToString() + " метода: " + meth.Name);
                Console.WriteLine("Тип " + methodCount.ToString() + " метода: " + meth.DeclaringType);
                Console.WriteLine("Тип возврата " + methodCount.ToString() + " метода: " + meth.ReturnType);
                methodCount = methodCount + 1;
            }

            Console.WriteLine("\nИнформация о свойствах, выведенная в цикле: ");
            int propCount = 1;
            foreach (var prop in t.GetProperties()) {
                Console.WriteLine("Свойство " + propCount.ToString() + " : " + prop);
                propCount = propCount + 1;
            }

            Console.WriteLine("\nИнформация о свойствах с аттрибутами, выведенная в цикле: ");
            int attCount = 1;
            foreach (var prop in t.GetProperties()) {
                object attObj;
                if (getAtt(prop, typeof(newAtt), out attObj)) {
                    newAtt att = attObj as newAtt;
                    Console.WriteLine("Свойство " + attCount.ToString() + " имеет аттрибут!");
                    Console.WriteLine("Название свойства " + attCount.ToString() + " : " + prop.Name);
                    Console.WriteLine("Аттрибут свойства " + attCount.ToString() + " : " + att.desc);
                }
                
                attCount = attCount + 1;
            }

            Console.WriteLine("\nВызов метода через рефлексию: ");
            toInspect method = (toInspect)t.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[] { });
            object[] param = new object[] { 10, 1 };
            object addUpRes = t.InvokeMember("addUp", BindingFlags.InvokeMethod, null, method, param);
            object subtractRes = t.InvokeMember("subtract", BindingFlags.InvokeMethod, null, method, param);

            Console.WriteLine("Параметры (через запятую): " + param[0].ToString() + ", " + param[1].ToString());
            Console.WriteLine("Сложение:  " + addUpRes.ToString());
            Console.WriteLine("Вычитание: " + subtractRes.ToString());

            Console.ReadLine();
        }
    }
}
