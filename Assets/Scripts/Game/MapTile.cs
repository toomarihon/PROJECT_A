using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour 
{
	public int Xi { get; set;}
	public int Yi { get; set;}
	private MapTileInfo.Type _type;

	public void SetTile(int xi, int yi, MapTileInfo.Type type)
	{
		Xi = xi;
		Yi = yi;
		_type = type;
	}
}
