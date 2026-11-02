using LinearAlgebra;

double[] coordinates = { 3, 4 };
IMathVector v1 = new MathVector(coordinates);
IMathVector v2 = new MathVector(new List<double> { 1, 2 });
IMathVector copy = new MathVector(v1);

Console.WriteLine("Исходные векторы:");
Console.WriteLine($"v1 (из массива): ({v1[0]}, {v1[1]})");
Console.WriteLine($"v2 (из списка): ({v2[0]}, {v2[1]})");
Console.WriteLine($"Копия v1: ({copy[0]}, {copy[1]})");
Console.WriteLine($"Размерность v1: {v1.Dimensions}");
Console.WriteLine($"Длина v1: {v1.Length}");

Console.Write("Перебор координат v1: ");
foreach (double coordinate in v1)
{
    Console.Write($"{coordinate} ");
}
Console.WriteLine();

Console.WriteLine($"Первая координата: {v1[0]}");
v1[0] = 10;
Console.WriteLine($"После изменения через индексатор: {v1[0]}");
Console.WriteLine($"У копии первая координата осталась прежней: {copy[0]}");
v1[0] = 3;

coordinates[0] = 100;
Console.WriteLine($"После изменения исходного массива v1[0] осталось: {v1[0]}");

Console.WriteLine("\nМетоды:");
IMathVector result;

result = v1.SumNumber(2);
Console.WriteLine($"v1.SumNumber(2): ({result[0]}, {result[1]})");

result = v1.MultiplyNumber(2);
Console.WriteLine($"v1.MultiplyNumber(2): ({result[0]}, {result[1]})");

result = v1.DivideNumber(2);
Console.WriteLine($"v1.DivideNumber(2): ({result[0]}, {result[1]})");

result = v1.Sum(v2);
Console.WriteLine($"v1.Sum(v2): ({result[0]}, {result[1]})");

result = v1.Multiply(v2);
Console.WriteLine($"v1.Multiply(v2): ({result[0]}, {result[1]})");

result = v1.Divide(v2);
Console.WriteLine($"v1.Divide(v2): ({result[0]}, {result[1]})");

Console.WriteLine($"Скалярное произведение: {v1.ScalarMultiply(v2)}");
Console.WriteLine($"Расстояние: {v1.CalcDistance(v2)}");


Console.WriteLine("\nОператоры:");

result = (MathVector)v1 + 2;
Console.WriteLine($"v1 + 2: ({result[0]}, {result[1]})");

result = (MathVector)v1 + v2;
Console.WriteLine($"v1 + v2: ({result[0]}, {result[1]})");

result = (MathVector)v1 - 2;
Console.WriteLine($"v1 - 2: ({result[0]}, {result[1]})");

result = (MathVector)v1 - v2;
Console.WriteLine($"v1 - v2: ({result[0]}, {result[1]})");

result = (MathVector)v1 * 2;
Console.WriteLine($"v1 * 2: ({result[0]}, {result[1]})");

result = (MathVector)v1 * v2;
Console.WriteLine($"v1 * v2: ({result[0]}, {result[1]})");

result = (MathVector)v1 / 2;
Console.WriteLine($"v1 / 2: ({result[0]}, {result[1]})");

result = (MathVector)v1 / v2;
Console.WriteLine($"v1 / v2: ({result[0]}, {result[1]})");

Console.WriteLine($"v1 % v2: {(MathVector)v1 % v2}");
Console.WriteLine($"Исходный v1 после операций: ({v1[0]}, {v1[1]})");
Console.WriteLine($"Исходный v2 после операций: ({v2[0]}, {v2[1]})");

Console.WriteLine("\nПримеры обработки ошибок:");

try
{
    v1.DivideNumber(0);
}
catch (DivideByZeroException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

try
{
    v1.Divide(new MathVector(new double[] { 1, 0 }));
}
catch (DivideByZeroException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

try
{
    v1.Sum(new MathVector(new double[] { 1, 2, 3 }));
}
catch (ArgumentException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

try
{
    Console.WriteLine(v1[2]);
}
catch (IndexOutOfRangeException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

try
{
    Console.WriteLine(v1[-1]);
}
catch (IndexOutOfRangeException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

try
{
    v1[2] = 5;
}
catch (IndexOutOfRangeException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

try
{
    v1.Sum(null!);
}
catch (ArgumentNullException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

try
{
    _ = new MathVector(null!);
}
catch (ArgumentNullException error)
{
    Console.WriteLine($"Ошибка: {error.Message}");
}

Console.WriteLine("\nДемонстрация завершена.");
