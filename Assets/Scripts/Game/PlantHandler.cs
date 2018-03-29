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
	public void CreatePlant(List<PlantGroup> plantGroup, MapTileInfo[,] mapTiles)
	{
		for(int c_i = 0; c_i < MapTileInfo.ColumnCount; c_i++)
		{
			for(int r_i = 0; r_i < MapTileInfo.RowCount; r_i++)
			{
				if(mapTiles[c_i, r_i].TileType == MapTileInfo.Type.Water)
				{
					continue;
				}

				float maxChance = 0.1f;
				if(JUtil.NextFloat() > maxChance)
				{
					continue;
				}

				float humidity = mapTiles [c_i, r_i].Humidity;
				PlantGroup pg = new PlantGroup ();
				pg.Init (1);

			}
		}

	}
}
