using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler
{
	public void CreateMapInfo(MapTileInfo[,] tileInfoList)
	{
		int columnCount = MapTileInfo.ColumnCount;
		int rowCount = MapTileInfo.RowCount;

		//tileInfoList = new MapTileInfo[columnCount, rowCount];

		//Create Map First
		for(int x = 0; x < columnCount; x++)
		{
			for(int y = 0; y < rowCount; y++)
			{
				MapTileInfo info = new MapTileInfo ();

				//this is for temp -> almost ground
				MapTileInfo.Type type = JUtil.NextInt (0, 75) != 0 ? MapTileInfo.Type.Ground : MapTileInfo.Type.Water;
				info.Init (x, y, type);
				tileInfoList [x,y] = info;
			}
		}

		int humididyRange = 5;

		//Set temperature and humidity
		for(int x = 0; x < columnCount; x++)
		{
			for(int y = 0; y < rowCount; y++)
			{
				if(tileInfoList[x,y].TileType == MapTileInfo.Type.Water)
				{
					for(int tx = x - humididyRange; tx <= x + humididyRange; tx++)
					{
						if(tx < 0)
							continue;
						if(tx >= columnCount)
							break;

						for(int ty = y - humididyRange; ty <= y + humididyRange; ty++)
						{
							if(ty < 0)
								continue;
							if(ty >= rowCount)
								break;
							if (tileInfoList [tx, ty].TileType == MapTileInfo.Type.Water)
								continue;

							float temperature = 25;
							float humidity = 0;

							if(Mathf.Abs (tx - x) == 5 || Mathf.Abs (ty - y) == 5)
							{
								humidity = 0.1f;
							}
							else if(Mathf.Abs (tx - x) == 4 || Mathf.Abs (ty - y) == 4)
							{
								humidity = 0.2f;
							}
							else if(Mathf.Abs (tx - x) == 3 || Mathf.Abs (ty - y) == 3)
							{
								humidity = 0.3f;
							}
							else if(Mathf.Abs (tx - x) == 2 || Mathf.Abs (ty - y) == 2)
							{
								humidity = 0.4f;
							}
							else if(Mathf.Abs (tx - x) == 1 || Mathf.Abs (ty - y) == 1)
							{
								humidity = 0.5f;
							}

							SetHumidity(tileInfoList[tx, ty], humidity);
							tileInfoList [tx, ty].Temperature = temperature;
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// Sets the humidity.
	/// if value is more bigger than info's humadity, update humadity in info
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetHumidity(MapTileInfo info, float value)
	{
		if(value > info.Humidity)
		{
			info.Humidity = value;
		}
	}

	public void CreateMap(MapTileInfo[,] tileInfoList, GameObject rootObj)
	{
		Transform tileRoot = new GameObject ("TileRoot").transform;
		tileRoot.SetParent (rootObj.transform);

		GameObject tilePrefab = ResourcesManager.Instance.GetObject ("Tile");
		float width = MapTileInfo.Width = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
		float height = MapTileInfo.Height = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
		int columnCount = MapTileInfo.ColumnCount;
		int rowCount = MapTileInfo.RowCount;

		for (int i = 0; i < columnCount; i++)
		{
			for (int j = 0; j < rowCount; j++)
			{
				GameObject copy = GameObject.Instantiate<GameObject>(tilePrefab, tileRoot);
				MapTile tile = copy.GetComponent<MapTile>();

				copy.transform.position = new Vector3(i * width, j * height, 0);
				tile.SetTile(tileInfoList[i, j]);
			}
		}
	}
}
