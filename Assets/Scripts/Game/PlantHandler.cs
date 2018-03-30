using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

타일 마다 확률을 검사한다.
만일 확률 적으로 그룹을 설정할 수 있다면 > 현재 타일의 온도와 습도를 가져와 > 식물을 지정한다.
식물이 자라는 환경요건
 */

public class PlantHandler 
{
	public void CreatePlant(List<PlantGroupInfo> plantGroup, MapTileInfo[,] mapTiles)
	{
		float maxChance = 0.01f;

		for(int c_i = 0; c_i < MapTileInfo.ColumnCount; c_i++)
		{
			for(int r_i = 0; r_i < MapTileInfo.RowCount; r_i++)
			{
				if(mapTiles[c_i, r_i].TileType == MapTileInfo.Type.Water)
				{
					continue;
				}

				if(JUtil.NextFloat() > maxChance)
				{
					continue;
				}

				float humidity = mapTiles [c_i, r_i].Humidity;
				PlantGroupTableInfo tableInfo = TableManager.Instance.GetAdaptablePlantGroup (25, humidity);

				//if table info is null -> this means that adaptable plant group dosnt exist in table.
				if(tableInfo == null)
				{
					continue;
				}

				PlantGroupInfo info = new PlantGroupInfo (tableInfo, c_i, r_i);
				AddPlantToGroup (mapTiles, c_i, r_i, info, tableInfo);
			}
		}
	}


	//xi yi -> plant group list's center.
	public void AddPlantToGroup(MapTileInfo[,] mapTiles, int xi, int yi, PlantGroupInfo info, PlantGroupTableInfo tableInfo)
	{
		int range = tableInfo.GroupRange;

		PlantTableInfo plantInfo = null;
		Plant plant = null;

		for(int x_i = xi - range; x_i < xi + range; x_i++)
		{
			if(x_i < 0)
			{
				continue;
			}
			if(x_i >= MapTileInfo.ColumnCount)
			{
				break;
			}

			for(int y_i = yi - range; y_i < yi + range; y_i++)
			{
				if(y_i < 0)
				{
					continue;
				}
				if(y_i >= MapTileInfo.RowCount)
				{
					break;
				}
				if(JUtil.NextFloat() > tableInfo.Chance)
				{
					continue;
				}

				int index = JUtil.NextInt(0,  tableInfo.Plants.Count);
				int plantId = tableInfo.Plants [index];
				MapTileInfo curTile = mapTiles [x_i, y_i];

				if(curTile.GetBuildableObject<BuildableObject>() == null &&
					TableManager.Instance.PlantList.TryGetValue(plantId, out plantInfo))
				{
					if(!TableManager.Instance.IsAdaptableEnvironment(plantInfo.ProperTemperature, plantInfo.ProperHumidity, curTile.Temperature, curTile.Humidity))
					{
						continue;
					}

					GameObject obj = ResourcesManager.Instance.GetObject ("Plant");

					plant = obj.GetComponent<Plant> ();
					plant.SetPlantInfo (plantInfo, x_i, y_i);
					plant.transform.position = MapTile.IndexToPosition (x_i, y_i);

					mapTiles [x_i, y_i].SetBuildableObject (plant);
					info.AddPlant (plant);
				}
			}
		}
	}
}
