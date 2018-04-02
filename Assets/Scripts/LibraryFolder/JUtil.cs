using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using UnityEngine;

public class JUtil 
{
	private static System.Random _random;

	static JUtil()
	{
		_random = new System.Random ();
	}

	public static int NextInt(int min, int max)
	{
		return _random.Next (min, max);
	}

	public static float NextFloat(float min, float max)
	{
		return min + (max - min) * (float)_random.NextDouble ();
	}

	public static float NextFloat()
	{
		return (float)_random.NextDouble ();
	}

	public static double NextDouble(double min, double max)
	{
		return min + (max - min) * _random.NextDouble ();
	}

	public static double NextDouble()
	{
		return _random.NextDouble ();
	}

	public static List<T> ArrayToList<T>(T[] array)
	{
		return new List<T> (array);
	}

	public static byte[] ReadFileAndMakeBinaryFile(string folderPath, string fileName)
	{
		string path = Path.Combine (folderPath, fileName);
		JDebugger.Log ("[ReadFileAndMakeBinaryFile] File path : " + path);

		if (!File.Exists(path))
		{
			JDebugger.Log ("[ReadFileAndMakeBinaryFile] File dosnt exist " + fileName);
			return null;
		}

		return File.ReadAllBytes(path);
	}

	public static Dictionary<int, T> ByteToDictionary<T>(byte[] byteData) where T : IDeserializable
	{
		Stream file = new MemoryStream(byteData);
		Dictionary<int, T> dic = new Dictionary<int, T> ();

		BinaryFormatter bf = new BinaryFormatter();
		List<T> list = (List<T>)bf.Deserialize(file);
		list.ToDictionary (data => data.GetKey (), data => data);

		file.Close ();

		return dic;
	}

	//ResourcesPath.CSVDialog
	public static List<string[]> LoadCSVFile(string filePath)
	{		
		TextAsset fileFullPath = Resources.Load<TextAsset> (filePath);

		Debug.Log (filePath);
		string csvTextData = fileFullPath.text;

		string[] rows = csvTextData.Split('\n');

		List<string[]> stringList = new List<string[]> ();

		for(int i = 0; i < rows.Length; i++)
		{
			string[] element = rows [i].Split (',');
			stringList.Add (element);
		}

		return stringList;
	}

	public static Dictionary<int,V> CSVToDicationary<V>(string filePath) where V : IDeserializable, new()
	{
		List<string[]> stringList = LoadCSVFile (filePath);

		if(stringList.Count < 2)
		{
			JDebugger.Log ("CSVToDicationary stringList count < 2");
			return null;
		}

		Dictionary<int,V> result = new Dictionary<int, V> ();
		string[] header = stringList [0];

		for(int r_i = 1; r_i < stringList.Count; r_i++)
		{
			string[] row = stringList [r_i];

			if(header.Length != row.Length)
			{
				JDebugger.Log ("CSVToDicationary header.Length != row.Length");
				return null;
			}
				
			V value = new V ();

			for(int c_i = 0; c_i < row.Length; c_i++)
			{
				FieldInfo fieldInfo = typeof(V).GetField (FilteringCell(header[c_i]));

				if(fieldInfo == null)
				{
					JDebugger.Log ("[fieldInfo] " + header[c_i] + " is not match witch field name.");
					return null;
				}

				if(fieldInfo.FieldType == typeof(int))
				{
					fieldInfo.SetValue(value, int.Parse(row[c_i]));
				}
				else if(fieldInfo.FieldType == typeof(float))
				{
					fieldInfo.SetValue(value, float.Parse(row[c_i]));
				}
				else if(fieldInfo.FieldType == typeof(long))
				{
					fieldInfo.SetValue(value, long.Parse(row[c_i]));
				}
				else if(fieldInfo.FieldType == typeof(string))
				{
					fieldInfo.SetValue(value, FilteringCell(row[c_i]));
				}
				else if(fieldInfo.FieldType == typeof(List<int>))
				{
					string rowValue = FilteringCell (row [c_i]);
					string[] values = rowValue.Split(':');
					List<int> list = new List<int>() ;
					for(int v_i = 0; v_i < values.Length; v_i++)
					{
						list.Add (int.Parse(values [v_i]));
					}
					fieldInfo.SetValue(value, list);
				}
			}

			result.Add(int.Parse(row[0]), value);
		}

		return result;
	}

	/// <summary>
	/// 이 함수는 csv 파일에서 special character 를 찾아 내용물만 필터링을 하는 함수이다.
	/// </summary>
	/// <returns>The cell.</returns>
	/// <param name="cellValue">Cell value.</param>
	public static string FilteringCell(string cellValue)
	{
		if(cellValue[0] == '\"')
		{
			cellValue = cellValue.Substring (1, cellValue.Length - 2);
		}
		//special character is one character.
		if(cellValue[cellValue.Length - 1] == '\r')
		{
			cellValue = cellValue.Substring (0, cellValue.Length - 1);
		}
		if(cellValue[cellValue.Length - 1] == '\"')
		{
			cellValue = cellValue.Substring (0, cellValue.Length - 1);
		}
		if(cellValue[0] == '[')
		{
			cellValue = cellValue.Substring (1, cellValue.Length - 2);
		}

		return cellValue;
	}

	public static bool IsRangeOf(float cur, float minValue, float maxValue)
	{
		return cur >= minValue && cur <= maxValue;
	}
}
