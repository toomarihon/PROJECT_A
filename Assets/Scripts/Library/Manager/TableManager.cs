using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeserializable
{
	int GetKey();
}

public class DialogInfo : IDeserializable
{
	public int Number { get; set;}
	public int NextNumber{ get; set;}
	public string Actor{ get; set;}
	public string Text{ get; set;}

	public DialogInfo()
	: this(0, 0, "", "")
	{
	}

	public DialogInfo(int number, int nextNumber, string actor, string text)
	{
		Number = number;
		NextNumber = nextNumber;
		Text = text;
	}

	public int GetKey()
	{
		return Number;
	}
}

public class BuildableObject 
{
	//Need to add contents.
}
	
public class PlantInfo : BuildableObject, IDeserializable
{
	public enum PlantType
	{
		PLANT1,
		PLANT2,
		PLANT3
	}

	public int Id;
	public string Name;
	public int GrowthStage;
	public int Age;

//	public PlantInfo
//
//	public PlantInfo(int id, string name, int growthStage, int age)
//	{
//		Init (id, name, growthStage, age);
//	}

	public void Init(int id, string name, int growthStage, int age)
	{
		Id = id;
		Name = name;
		GrowthStage = growthStage;
		Age = age;
	}

	public int GetKey()
	{
		return Id;
	}
}

public class PlantGroupInfo : IDeserializable
{
	public int Id { get; set;}
	public int GroupRange { get; set;}
	public float ProperHumidity{ get; set;}
	public float ProperTemperature{ get; set;}
	public List<int> Plants = new List<int> ();		//this list include plant id.

	public PlantGroupInfo(int id, int groupRange, float humidity, float temperature)
	{
		Id = id;
		GroupRange = groupRange;
		ProperHumidity = humidity;
		ProperTemperature = temperature;
	}

	public int GetKey()
	{
		return Id;
	}
}

public class TableManager : SingletonBase<TableManager> 
{
	[HideInInspector] public Dictionary<int, DialogInfo> DialogList { private set; get;}
	[HideInInspector] public Dictionary<int, PlantInfo> PlantList{ private set; get;}
	[HideInInspector] public Dictionary<int, PlantGroupInfo> PlantGroupList{ private set; get;}

	public void Awake()
	{
		SetPlantList ();
//		DialogList.Values.
	}

	public void SetDialogList ()
	{
//		List<string[]> dialogList = Util.LoadFile (ResourcesPath.CSV_FolderFath + ResourcesPath.CSV_Dialog);
//
//		if(dialogList == null)
//		{
//			return;
//		}
//
//		if(DialogList == null)
//		{
//			DialogList = new Dictionary<int, DialogInfo> ();
//		}
//		else
//		{
//			DialogList.Clear ();
//		}
//
//		for(int i = 0; i < dialogList.Count; i++)
//		{
//			string[] line = dialogList [i];
//			if(line == null)
//			{
//				JDebugger.Log (this, "SetDialogList () - string is null");
//				return;
//			}
//
//			DialogList.Add (int.Parse (line [0]), new DialogInfo(int.Parse(line[0]), int.Parse(line[1]), line[2], line[3]));
//		}
	}

	//[JNE] this is temporary function.
	public void SetPlantList()
	{
		if(PlantList == null)
		{
			PlantList = new Dictionary<int, PlantInfo> ();
		}
		else
		{
			PlantList.Clear ();
		}

		PlantList = JUtil.CSVToDicationary<PlantInfo> ("CSV/PlantInfoTable");


//		string jsonStr = JUtil.CSVToJson("CSV/PlantInfoTable");

//		List<PlantInfo> info = JsonFx.Json.JsonReader.Deserialize<List<PlantInfo>> (jsonStr); 

//		PlantList = JUtil.ByteToDictionary<PlantInfo> (data);

//		PlantList.Add (1, new PlantInfo (1, "습지에서 자라는 풀", 1, 1));
//		PlantList.Add (2, new PlantInfo (2, "습지에서 자라는 풀2", 1, 1));
//		PlantList.Add (3, new PlantInfo (3, "습기가 덜한 풀", 1, 1));
//		PlantList.Add (4, new PlantInfo (4, "습기 없는 곳에서 자라는 풀", 1, 1));
	}

	//[JNE] this is temporary function.
	public void SetPlantGroupList()
	{
//		public PlantGroupInfo(int id, int groupRange, float humidity, float temperature)
//		{
//			Id = id;
//			GroupRange = groupRange;
//			ProperHumidity = humidity;
//			ProperTemperature = temperature;
//		}

//		PlantGroupInfo info = new PlantGroupInfo (1, 1, 0.4f, 25f);
//		info.Plants.Add (1);
//		info.Plants.Add (2);
//
		//this way isnt efficient
	}
}
