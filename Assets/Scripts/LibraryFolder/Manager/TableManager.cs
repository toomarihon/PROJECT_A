using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//READ ME!
//TableInfo class must inherited IDeserializable

public interface IDeserializable
{
	int GetKey();
}
//relate with Builable Object ->
public class DialogTableInfo : IDeserializable
{
	public int Number { get; set;}
	public int NextNumber{ get; set;}
	public string Actor{ get; set;}
	public string Text{ get; set;}

	public int GetKey()
	{
		return Number;
	}
}
	
public class PlantTableInfo : IDeserializable
{
	public int Id;
	public string Name;
	public int GrowthStage;
	public int MaxGrowthStage;
	public int Age;
	public int MaxAge;
	public float ProperHumidity;
	public float ProperTemperature;

	public int GetKey()
	{
		return Id;
	}
}

public class PlantGroupTableInfo : IDeserializable
{
	public int Id;
	public int GroupRange;
	public int MinPlantCount;
	public int MaxPlantCount;
	public float Chance;
	public float ProperHumidity;
	public float ProperTemperature;
	public List<int> Plants = new List<int> ();		//this list include plant id.

	public int GetKey()
	{
		return Id;
	}
}

public class TableManager : SingletonBase<TableManager> 
{
	public string PlantInfoTablePath = "CSV/PlantInfoTable";
	public string PlantGroupInfoTablePath = "CSV/PlantGroupInfoTable";

	[HideInInspector] public Dictionary<int, DialogTableInfo> DialogList { private set; get;}
	[HideInInspector] public Dictionary<int, PlantTableInfo> PlantList{ private set; get;}
	[HideInInspector] public Dictionary<int, PlantGroupTableInfo> PlantGroupList{ private set; get;}

	public void Awake()
	{
		LoadTable ();
	}
		
	//[JNE] this is temporary function.
	public void LoadTable()
	{
		PlantList = JUtil.CSVToDicationary<PlantTableInfo> (PlantInfoTablePath);
		PlantGroupList = JUtil.CSVToDicationary<PlantGroupTableInfo> (PlantGroupInfoTablePath);
	}

	public PlantGroupTableInfo GetAdaptablePlantGroup(float temperature, float humididy)
	{
		var iter = PlantGroupList.GetEnumerator ();
		List<PlantGroupTableInfo> list = new List<PlantGroupTableInfo> ();

		while(iter.MoveNext())
		{
			PlantGroupTableInfo info = iter.Current.Value;
			if(IsAdaptableEnvironment(temperature, humididy, info.ProperTemperature, info.ProperHumidity))
			{
				list.Add (info);
			}
		}

		return list.Count > 0 ? list [JUtil.NextInt (0, list.Count)] : null;
	}

	public bool IsAdaptableEnvironment(float temperature, float humidity, float baseTemperature, float baseHumidity)
	{
		float minRange = 0.9f;
		float maxRange = 1.1f;

		if(JUtil.IsRangeOf(temperature, baseTemperature * minRange, baseTemperature * maxRange) &&
			JUtil.IsRangeOf(humidity, baseHumidity * minRange, baseHumidity * maxRange))
		{
			return true;
		}

		return false;
	}
}
