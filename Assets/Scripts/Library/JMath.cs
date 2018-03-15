using System.Collections;
using System.Collections.Generic;
using System;

public class JMath 
{
	private static Random _random;

	static JMath()
	{
		_random = new Random ();
	}
		
	public static int NextInt(int min, int max)
	{
		return _random.Next (min, max);
	}

	public static float NextFloat(float min, float max)
	{
		return min + (max - min) * (float)_random.NextDouble ();
	}

	public static double NextDouble(double min, double max)
	{
		return min + (max - min) * _random.NextDouble ();
	}

	public static double NextDouble()
	{
		return _random.NextDouble ();
	}
}
