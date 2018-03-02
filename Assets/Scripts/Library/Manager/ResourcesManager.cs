using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPath
{
	public static string SpriteFolderPath = "Sprites/";

	public static string CSV_FolderFath = "CSV/";
	public static string CSV_Dialog = "Dialog";
}

public class ResourcesManager : SingletonBase<ResourcesManager> 
{
	private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

	public void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void Start()
	{
		AddSpriteAll();
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

	public void AddSpriteAll()
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>(ResourcesPath.SpriteFolderPath);

		for(int i = 0; i < sprites.Length; i++)
		{
			_sprites.Add(sprites[i].name, sprites[i]);
		}
	}
}
