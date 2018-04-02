using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSavePath
{
	public static string Test = "Test";
}

public class LocalSaveManager
{
	public static void SetString(string key, string value)
	{
		PlayerPrefs.SetString (key, value);
		PlayerPrefs.Save();
	}

	public static void SetInt(string key, int value)
	{
		PlayerPrefs.SetInt (key, value);
		PlayerPrefs.Save ();
	}

	public static void SetFloat(string key, float value)
	{
		PlayerPrefs.SetFloat (key, value);
		PlayerPrefs.Save ();
	}

	public static void SetInts(string key, int[] values)
	{
		string packet = "";
		for(int i = 0; i < values.Length; i++)
		{
			if(i == values.Length - 1)
			{
				packet += values [i].ToString ();
				break;
			}

			packet += values[i].ToString() + ",";
		}

		SetString (key, packet);
	}

	public static void SetBool(string key, bool flag)
	{
		if(flag)
		{
			SetInt (key, 1);
		}
		else
		{
			SetInt (key, 0);
		}
	}

	public static string GetString(string key)
	{
		if(PlayerPrefs.HasKey(key))
		{
			return PlayerPrefs.GetString (key);
		}

		return null;
	}

	public static int GetInt(string key)
	{
		if(PlayerPrefs.HasKey(key))
		{
			return PlayerPrefs.GetInt (key);
		}

		return 0;
	}

	public static bool GetInts(string key, ref int[] output)
	{
		if(!HasKey(key))
		{
			return false;
		}

		string packet = GetString (key);
		string[] values = packet.Split(',');

		if(output.Length != values.Length)
		{
			return false;
		}

		for(int i = 0; i < values.Length; i++)
		{
			output [i] = int.Parse(values [i]);
		}

		return true;
	}

	public static float GetFloat(string key)
	{
		if(PlayerPrefs.HasKey(key))
		{
			return PlayerPrefs.GetFloat (key);
		}

		return 0;
	}

	public static bool GetBool(string key)
	{
		return GetInt (key) == 1;
	}

	public static bool HasKey(string key)
	{
		return PlayerPrefs.HasKey (key);
	}

	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}
}