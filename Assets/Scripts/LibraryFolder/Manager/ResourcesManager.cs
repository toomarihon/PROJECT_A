using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPath
{
	public static string SpriteFolderPath = "Sprites/";
	public static string WorldPrefabFolerPath = "Prefabs/World";

	public static string CSV_FolderFath = "CSV/";
	public static string CSV_Dialog = "Dialog";
}

public class ResourcesManager : SingletonBase<ResourcesManager> 
{
	private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
	private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
	private ObjectPool _objectPool = new ObjectPool ();

	public void Awake()
	{
        DontDestroyOnLoad(this.gameObject);

        AddSpriteAll();
		AddPrefabAll ();

		_objectPool.Init (_prefabs);
    }

	public Sprite GetSprite(string spriteName)
	{
		Sprite sprite = null;
		if(!_sprites.TryGetValue(spriteName, out sprite))
		{
			JDebugger.Log(this, "Can't Find Sprite " + spriteName);
		}

		return sprite;
	}

	public GameObject GetObject(string name)
	{
		return _objectPool.GetObject(name);
	}

	public void AddSpriteAll()
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>(ResourcesPath.SpriteFolderPath);

		for(int i = 0; i < sprites.Length; i++)
		{
			_sprites.Add(sprites[i].name, sprites[i]);
		}
	}

	public void AddPrefabAll()
	{
		AddPrefab (ResourcesPath.WorldPrefabFolerPath);
	}

	public void AddPrefab(string folerPath)
	{
		GameObject[] obj = Resources.LoadAll<GameObject> (folerPath);

		for(int i = 0; i < obj.Length; i++)
		{
			_prefabs.Add (obj [i].name, obj [i]);
		}
	}
}
