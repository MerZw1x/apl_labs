using LinearAlgebra;

namespace LinearAlgebra.Tests;

public class MathVectorTests
{
    [Fact]
    public void Constructor_Array_CopiesCoordinates()
    {
        double[] source = { 3, 4 };
        IMathVector vector = new MathVector(source);
        source[0] = 100;
        Assert.Equal(3.0, vector[0]);
    }

    [Fact]
    public void Constructor_List_CopiesCoordinates()
    {
        List<double> source = new List<double> { 3, 4 };
        IMathVector vector = new MathVector(source);
        source[0] = 100;
        Assert.Equal(new double[] { 3, 4 }, vector);
    }

    [Fact]
    public void Constructor_Vector_CopyIsIndependent()
    {
        MathVector source = new MathVector(new double[] { 3,4 });
        IMathVector copy = new MathVector(source);
        source[0] = 100;
        Assert.Equal(new double[] { 3, 4 }, copy);
        copy[1] = 200;
        Assert.Equal(4.0, source[1]);
    }

    [Fact]
    public void Constructor_Null_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new MathVector(null!));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Dimensions_ReturnsCoordinateCount(int count)
    {
        IMathVector vector = new MathVector(new double[count]);
        Assert.Equal(count, vector.Dimensions);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(3, 4, 5)]
    [InlineData(-3, -4, 5)]
    [InlineData(1, 1, 1.4142135623730951)]
    public void Length_ReturnsExpected(double x, double y, double expected)
    {
        MathVector vector = new MathVector(new double[] { x, y });
        Assert.Equal(expected, vector.Length, 10);
    }

    [Fact]
    public void Length_EmptyVector_ReturnsZero()
    {
        MathVector vector = new MathVector(new double[] {  });
        Assert.Equal(0.0, vector.Length);
    }

    [Theory]
    [InlineData(0, 3, 4)]
    [InlineData(1, 4, 3)]
    public void Indexer_ValidIndex_ReadsAndWrites(int index, double expected, double untouched)
    {
        MathVector vector = new MathVector(new double[] { 3,4 });
        Assert.Equal(expected, vector[index]);
        vector[index] = 9;
        Assert.Equal(9.0, vector[index]);
        Assert.Equal(untouched, vector[1 - index]);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void Indexer_InvalidIndex_ThrowsForReadAndWrite(int index)
    {
        MathVector vector = new MathVector(new double[] { 3,4 });
        Assert.Throws<IndexOutOfRangeException>(() => { double value = vector[index]; });
        Assert.Throws<IndexOutOfRangeException>(() => { vector[index] = 9; });
    }

    [Fact]
    public void Indexer_EmptyVector_Throws()
    {
        MathVector vector = new MathVector(new double[] {  });
        Assert.Throws<IndexOutOfRangeException>(() => { double value = vector[0]; });
        Assert.Throws<IndexOutOfRangeException>(() => { vector[0] = 9; });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Enumeration_ReturnsCoordinatesInOrder(int count)
    {
        IMathVector vector = new MathVector(new double[count]);
        List<double> actual = new List<double>();
        foreach (double coordinate in vector)
        {
            actual.Add(coordinate);
        }
        Assert.Equal(new double[count], actual);
    }

    [Fact]
    public void Enumeration_DifferentCoordinates_KeepsOrder()
    {
        MathVector vector = new MathVector(new double[] { 3,-4,2 });
        List<double> actual = new List<double>();
        foreach (double coordinate in vector)
        {
            actual.Add(coordinate);
        }
        Assert.Equal(new double[] { 3, -4, 2 }, actual);
    }

    [Theory]
    [InlineData(2, 5, -2)]
    [InlineData(-2, 1, -6)]
    [InlineData(0, 3, -4)]
    [InlineData(0.5, 3.5, -3.5)]
    public void SumNumber_ReturnsExpected(double number, double x, double y)
    {
        MathVector vector = new MathVector(new double[] { 3,-4 });
        IMathVector result = vector.SumNumber(number);
        Assert.Equal(new double[] { x, y }, result);
    }

    [Fact]
    public void SumNumber_ResultIsNewAndIndependent()
    {
        MathVector vector = new MathVector(new double[] { 3,4 });
        IMathVector result = vector.SumNumber(2);
        Assert.NotSame(vector, result);
        Assert.Equal(new double[] { 3,4 }, vector);
        result[0] = 100;
        Assert.Equal(3.0, vector[0]);
    }

    [Theory]
    [InlineData(2, 6, -8)]
    [InlineData(-2, -6, 8)]
    [InlineData(0, 0, 0)]
    [InlineData(0.5, 1.5, -2)]
    public void MultiplyNumber_ReturnsExpected(double number, double x, double y)
    {
        MathVector vector = new MathVector(new double[] { 3,-4 });
        IMathVector result = vector.MultiplyNumber(number);
        Assert.Equal(new double[] { x, y }, result);
    }

    [Fact]
    public void MultiplyNumber_ResultIsNewAndIndependent()
    {
        MathVector vector = new MathVector(new double[] { 3,4 });
        IMathVector result = vector.MultiplyNumber(2);
        Assert.NotSame(vector, result);
        Assert.Equal(new double[] { 3,4 }, vector);
        result[0] = 100;
        Assert.Equal(3.0, vector[0]);
    }

    [Theory]
    [InlineData(2, 1.5, -2)]
    [InlineData(-2, -1.5, 2)]
    [InlineData(0.5, 6, -8)]
    public void DivideNumber_ReturnsExpected(double number, double x, double y)
    {
        MathVector vector = new MathVector(new double[] { 3,-4 });
        IMathVector result = vector.DivideNumber(number);
        Assert.Equal(new double[] { x, y }, result);
    }

    [Fact]
    public void DivideNumber_ResultIsNewAndIndependent()
    {
        MathVector vector = new MathVector(new double[] { 3,4 });
        IMathVector result = vector.DivideNumber(2);
        Assert.NotSame(vector, result);
        Assert.Equal(new double[] { 3,4 }, vector);
        result[0] = 100;
        Assert.Equal(3.0, vector[0]);
    }

    [Theory]
    [InlineData(0.0)]
    public void DivideNumber_Zero_Throws(double zero)
    {
        MathVector vector = new MathVector(new double[] { 3,4 });
        Assert.Throws<DivideByZeroException>(() => vector.DivideNumber(zero));
    }

    [Fact]
    public void Sum_ReturnsNewIndependentResult()
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        MathVector right = new MathVector(new double[] { 1,2 });
        IMathVector result = left.Sum(right);
        Assert.Equal(new double[] { 4,6 }, result);
        Assert.NotSame(left, result);
        Assert.NotSame(right, result);
        Assert.Equal(new double[] { 3,4 }, left);
        Assert.Equal(new double[] { 1,2 }, right);
        result[0] = 100;
        Assert.Equal(3.0, left[0]);
        Assert.Equal(1.0, right[0]);
    }

    [Fact]
    public void Multiply_ReturnsNewIndependentResult()
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        MathVector right = new MathVector(new double[] { 1,2 });
        IMathVector result = left.Multiply(right);
        Assert.Equal(new double[] { 3,8 }, result);
        Assert.NotSame(left, result);
        Assert.NotSame(right, result);
        Assert.Equal(new double[] { 3,4 }, left);
        Assert.Equal(new double[] { 1,2 }, right);
        result[0] = 100;
        Assert.Equal(3.0, left[0]);
        Assert.Equal(1.0, right[0]);
    }

    [Fact]
    public void Divide_ReturnsNewIndependentResult()
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        MathVector right = new MathVector(new double[] { 1,2 });
        IMathVector result = left.Divide(right);
        Assert.Equal(new double[] { 3,2 }, result);
        Assert.NotSame(left, result);
        Assert.NotSame(right, result);
        Assert.Equal(new double[] { 3,4 }, left);
        Assert.Equal(new double[] { 1,2 }, right);
        result[0] = 100;
        Assert.Equal(3.0, left[0]);
        Assert.Equal(1.0, right[0]);
    }

    [Theory]
    [InlineData(1, 2, 11)]
    [InlineData(-3, -4, -25)]
    [InlineData(4, -3, 0)]
    public void ScalarMultiply_ReturnsExpected(double x, double y, double expected)
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        MathVector right = new MathVector(new double[] { x,y });
        Assert.Equal(expected, left.ScalarMultiply(right), 10);
    }

    [Theory]
    [InlineData(3, 4, 0)]
    [InlineData(0, 0, 5)]
    [InlineData(1, 2, 2.8284271247461903)]
    public void CalcDistance_ReturnsExpected(double x, double y, double expected)
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        MathVector right = new MathVector(new double[] { x,y });
        Assert.Equal(expected, left.CalcDistance(right), 10);
    }

    [Theory]
    [InlineData("Sum")]
    [InlineData("Multiply")]
    [InlineData("Divide")]
    [InlineData("ScalarMultiply")]
    [InlineData("CalcDistance")]
    public void VectorMethod_Null_Throws(string method)
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        Assert.Throws<ArgumentNullException>(() =>
        {
            switch (method)
            {
                case "Sum": left.Sum(null!); break;
                case "Multiply": left.Multiply(null!); break;
                case "Divide": left.Divide(null!); break;
                case "ScalarMultiply": left.ScalarMultiply(null!); break;
                case "CalcDistance": left.CalcDistance(null!); break;
            }
        });
    }

    [Theory]
    [InlineData("Sum", 1)]
    [InlineData("Sum", 3)]
    [InlineData("Multiply", 1)]
    [InlineData("Multiply", 3)]
    [InlineData("Divide", 1)]
    [InlineData("Divide", 3)]
    [InlineData("ScalarMultiply", 1)]
    [InlineData("ScalarMultiply", 3)]
    [InlineData("CalcDistance", 1)]
    [InlineData("CalcDistance", 3)]
    public void VectorMethod_DifferentDimensions_Throws(string method, int count)
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        IMathVector right = new MathVector(new double[count]);
        Assert.Throws<ArgumentException>(() =>
        {
            switch (method)
            {
                case "Sum": left.Sum(right); break;
                case "Multiply": left.Multiply(right); break;
                case "Divide": left.Divide(right); break;
                case "ScalarMultiply": left.ScalarMultiply(right); break;
                case "CalcDistance": left.CalcDistance(right); break;
            }
        });
    }

    [Theory]
    [InlineData("Sum")]
    [InlineData("Multiply")]
    [InlineData("Divide")]
    [InlineData("ScalarMultiply")]
    [InlineData("CalcDistance")]
    [InlineData("SumNumber")]
    [InlineData("MultiplyNumber")]
    [InlineData("DivideNumber")]
    public void Method_EmptyVector_ReturnsEmptyOrZero(string method)
    {
        MathVector left = new MathVector(new double[] {  });
        MathVector right = new MathVector(new double[] {  });
        switch (method)
        {
            case "Sum": Assert.Equal(0, left.Sum(right).Dimensions); break;
            case "Multiply": Assert.Equal(0, left.Multiply(right).Dimensions); break;
            case "Divide": Assert.Equal(0, left.Divide(right).Dimensions); break;
            case "ScalarMultiply": Assert.Equal(0.0, left.ScalarMultiply(right)); break;
            case "CalcDistance": Assert.Equal(0.0, left.CalcDistance(right)); break;
            case "SumNumber": Assert.Equal(0, left.SumNumber(2).Dimensions); break;
            case "MultiplyNumber": Assert.Equal(0, left.MultiplyNumber(2).Dimensions); break;
            case "DivideNumber": Assert.Equal(0, left.DivideNumber(2).Dimensions); break;
        }
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(1, 0)]
    public void Divide_ZeroCoordinate_ThrowsAndKeepsInputs(double x, double y)
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        MathVector right = new MathVector(new double[] { x,y });
        Assert.Throws<DivideByZeroException>(() => left.Divide(right));
        Assert.Equal(new double[] { 3,4 }, left);
        Assert.Equal(new double[] { x,y }, right);
    }

    [Fact]
    public void Divide_ZeroNumerator_ReturnsZero()
    {
        MathVector left = new MathVector(new double[] { 0,0 });
        MathVector right = new MathVector(new double[] { 1,2 });
        IMathVector result = left.Divide(right);
        Assert.Equal(new double[] { 0,0 }, result);
    }

    [Theory]
    [InlineData("+", 4, 6)]
    [InlineData("-", 2, 2)]
    [InlineData("*", 3, 8)]
    [InlineData("/", 3, 2)]
    public void Operator_Vector_ReturnsExpectedAndKeepsInputs(string operation, double x, double y)
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        IMathVector right = new MathVector(new double[] { 1, 2 });
        IMathVector result;
        switch (operation)
        {
            case "+": result = left + right; break;
            case "-": result = left - right; break;
            case "*": result = left * right; break;
            case "/": result = left / right; break;
            default: throw new ArgumentException("Неизвестный оператор");
        }
        Assert.Equal(new double[] { x,y }, result);
        Assert.Equal(new double[] { 3,4 }, left);
        Assert.Equal(new double[] { 1,2 }, right);
    }

    [Theory]
    [InlineData("+", 5, 6)]
    [InlineData("-", 1, 2)]
    [InlineData("*", 6, 8)]
    [InlineData("/", 1.5, 2)]
    public void Operator_Number_ReturnsExpectedAndKeepsInput(string operation, double x, double y)
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        IMathVector result;
        switch (operation)
        {
            case "+": result = left + 2; break;
            case "-": result = left - 2; break;
            case "*": result = left * 2; break;
            case "/": result = left / 2; break;
            default: throw new ArgumentException("Неизвестный оператор");
        }
        Assert.Equal(new double[] { x,y }, result);
        Assert.Equal(new double[] { 3,4 }, left);
    }

    [Fact]
    public void Operator_Percent_ReturnsScalarProduct()
    {
        MathVector left = new MathVector(new double[] { 3,4 });
        MathVector right = new MathVector(new double[] { 1,2 });
        Assert.Equal(11.0, left % right);
    }

}
