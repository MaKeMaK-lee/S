using System;

namespace lib
{
    public class AreaCalculator
    {
        /// <summary>
        /// Вычисляет площадь фигуры в зависимости от количества граней. Реализованы только круг и треугольник.
        /// </summary>
        /// <param name="args">Длины сторон или радиус</param>
        /// <returns>Площадь</returns>
        static public double CalculateArea(params double[] args)//Пожалею ресурсы и не стану добавлять отдельно первый параметр для принудительного блокирования вызова без параметров,
                                                                //хотя для удобства использования извне, может и стоило бы.
        {
            if (args.Length == 0)
            {
                throw new Exception("Кол-во аргументов не может быть нулевым");
            }
            if (args.Length == 1)
            {
                if (args[0] < 0)
                    throw new Exception("Радиус не может быть отрицательным");
                return Math.Pow(args[0], 2) * System.Math.PI;
            }
            if (args.Length == 3)
            {
                if (args[0] < 0 || args[1] < 0 || args[2] < 0)
                    throw new Exception("Стороны должны быть представлены положительными числами");
                                
                if (args[0] >= args[1] + args[2] || args[1] >= args[0] + args[2] || args[2] >= args[1] + args[0])
                    throw new Exception("Треугольник не существует");

                double pp = (args[0] + args[1] + args[2]) / 2;

                return Math.Sqrt(pp * (pp - args[0]) * (pp - args[1]) * (pp - args[2]));
            }


            throw new Exception("Фигура не опознана");





        }



    }
}
