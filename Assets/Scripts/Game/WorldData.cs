using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildableObject : MonoBehaviour
{
	protected int _xi;
	protected int _yi;
}
	
public class PlantGroupInfo
{
	private static int _globalGid = 1;	//gid is automatically increased.

	private int _gid;
	private int _xi;
	private int _yi;
	private PlantGroupTableInfo _tableInfo;
	private List<Plant> _plants = new List<Plant>();

	public PlantGroupInfo(PlantGroupTableInfo info, int xi, int yi)
	{
		_gid = _globalGid++;

		_xi = xi;
		_yi = yi;
		_tableInfo = info;
	}

	public void AddPlant(Plant plant)
	{
		_plants.Add (plant);
	}
}

public class WorldData
{
	public MapTileInfo[,] MapTiles { get; private set;}
	public List<PlantGroupInfo> PlantGroup{ get; private set;}

    public WorldData()
    {
        MapTiles = new MapTileInfo[MapTileInfo.ColumnCount, MapTileInfo.RowCount];
		PlantGroup = new List<PlantGroupInfo> ();
    }
}
