using System.Linq;
using System.Collections;

namespace LinearAlgebra;

public class MathVector : IMathVector
{
    private readonly double[] _coordinates;

    public int Dimensions
    {
        get
        {
            return _coordinates.Length;
        }
    }

    public MathVector(IEnumerable<double> coords)
    {
        ArgumentNullException.ThrowIfNull(coords);

        _coordinates = coords.ToArray();
    }

    public double this[int i] 
    {
        get { return _coordinates[i]; }
        set { _coordinates[i] = value; }
    }

    public double Length 
    { 
        get 
        {
            double sum = 0;
            foreach (double c in _coordinates) 
            {
                sum += (c * c);
            }
            return Math.Sqrt(sum);
        }
    }

    public IEnumerator<double> GetEnumerator()
    {
        return ((IEnumerable<double>)_coordinates).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IMathVector SumNumber(double number)
    {
        double[] newVector = new double[Dimensions];
        
        for (int i = 0; i < Dimensions; i++) 
        {
            newVector[i] = _coordinates[i] + number;
        }

        return new MathVector(newVector);
    }

    public IMathVector MultiplyNumber(double number)
    {
        double[] newVector = new double[Dimensions];
        
        for (int i = 0; i < Dimensions; i++) 
        {
            newVector[i] = _coordinates[i] * number;
        }

        return new MathVector(newVector);
    }

    public IMathVector DivideNumber(double number)
    {
        if (number == 0) 
        {
            throw new DivideByZeroException("Делить на ноль нельзя!");
        }

        double[] newVector = new double[Dimensions];
        
        for (int i = 0; i < Dimensions; i++) 
        {
            newVector[i] = _coordinates[i] / number;
        }

        return new MathVector(newVector);
    }

    private void ValidateVector(IMathVector vector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        if (vector.Dimensions != Dimensions) 
        {
            throw new ArgumentException("Размерность векторов должна совпадать");
        }
    }

    public IMathVector Sum(IMathVector vector)
    {
        ValidateVector(vector);

        double[] newVector = new double[Dimensions];
        
        for (int i = 0; i < Dimensions; i++) 
        {
            newVector[i] = _coordinates[i] + vector[i];
        }

        return new MathVector(newVector);

    }

    public IMathVector Multiply(IMathVector vector)
    {
        ValidateVector(vector);

        double[] newVector = new double[Dimensions];
        
        for (int i = 0; i < Dimensions; i++) 
        {
            newVector[i] = _coordinates[i] * vector[i];
        }

        return new MathVector(newVector);

    }

    public IMathVector Divide(IMathVector vector)
    {
        ValidateVector(vector);

        double[] newVector = new double[Dimensions];
        
        for (int i = 0; i < Dimensions; i++) 
        {
            if (vector[i] == 0) 
            {
                throw new DivideByZeroException("Делить на ноль нельзя!");
            }
            newVector[i] = _coordinates[i] / vector[i];
        }

        return new MathVector(newVector);

    }

    public double ScalarMultiply(IMathVector vector)
    {
        ValidateVector(vector);

        double sum = 0;
        for (int i = 0; i < Dimensions; i++) 
        {
            sum += _coordinates[i]*vector[i];
        }

        return sum;
    }
    
    public double CalcDistance(IMathVector vector)
    {
        ValidateVector(vector);

        double sum = 0;
        for (int i = 0; i < Dimensions; i++) 
        {
            sum += (_coordinates[i] - vector[i]) * (_coordinates[i] - vector[i]);
        }

        return Math.Sqrt(sum);
    }

    public static IMathVector operator +(MathVector left, IMathVector right)
    {
        ArgumentNullException.ThrowIfNull(left);

        return left.Sum(right);
    }

    public static IMathVector operator +(MathVector left, double val)
    {
        ArgumentNullException.ThrowIfNull(left);

        return left.SumNumber(val);
    }

    public static IMathVector operator -(MathVector left, IMathVector right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        right = right.MultiplyNumber(-1);

        return left.Sum(right);
    }

    public static IMathVector operator -(MathVector left, double val)
    {
        ArgumentNullException.ThrowIfNull(left);
        val *= -1;
        return left.SumNumber(val);
    }

    public static IMathVector operator *(MathVector left, IMathVector right)
    {
        ArgumentNullException.ThrowIfNull(left);

        return left.Multiply(right);
    }

    public static IMathVector operator *(MathVector left, double val)
    {
        ArgumentNullException.ThrowIfNull(left);

        return left.MultiplyNumber(val);
    }

    public static IMathVector operator /(MathVector left, IMathVector right)
    {
        ArgumentNullException.ThrowIfNull(left);

        return left.Divide(right);
    }

    public static IMathVector operator /(MathVector left, double val)
    {
        ArgumentNullException.ThrowIfNull(left);

        return left.DivideNumber(val);
    }

    public static double operator %(MathVector left, IMathVector right)
    {
        ArgumentNullException.ThrowIfNull(left);

        return left.ScalarMultiply(right);
    }
}
