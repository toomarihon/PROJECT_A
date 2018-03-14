using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler
{
	public void CreateMap(MapTileInfo[,] tileInfoList)
	{
		int columnCount = MapTileInfo.columnCount;
		int rowCount = MapTileInfo.rowCount;

		tileInfoList = new MapTileInfo[columnCount, rowCount];

		//Create Map First
		for(int x = 0; x < columnCount; x++)
		{
			for(int y = 0; y < rowCount; y++)
			{
				MapTileInfo info = new MapTileInfo ();

				//this is for temp -> almost ground
				MapTileInfo.Type type = JMath.NextInt (0, 5) != 0 ? MapTileInfo.Type.Ground : MapTileInfo.Type.Water;
				info.Init (x, y, type);
				tileInfoList [x,y] = info;
			}
		}

		//Set temperature and humidity
		for(int x = 0; x < columnCount; x++)
		{
			for(int y = 0; y < rowCount; y++)
			{
				if(tileInfoList[x,y].TileType == MapTileInfo.Type.Water)
				{
					for(int tx = x - 2; tx <= x + 2; tx++)
					{
						if(tx < 0)
							continue;
						if(tx >= columnCount)
							break;

						for(int ty = y - 2; ty <= y + 2; ty++)
						{
							if(ty < 0)
								continue;
							if(ty >= rowCount)
								break;
							if (tileInfoList [x, y].TileType == MapTileInfo.Type.Water)
								continue;

							//set humidity and temperature
							float humidity = Mathf.Abs (tx - x) <= 1 && Mathf.Abs (ty - y) <= 1 ? 2 : 1;
							float temperature = 25;

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
}
