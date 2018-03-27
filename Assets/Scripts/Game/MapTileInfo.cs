using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileInfo
{
	public enum Type
	{
		Ground,
		Water,
		Road,
		Sand,
	}

	public static float Width = 10;
	public static float Height = 10;
	public const int ColumnCount = 100;
	public const int RowCount = 100;

	private int _xi;
	private int _yi;
	public int Xi {get{return _xi;}}
	public int Yi {get{return _yi;}}

	public int F { get; set;}
	public int G { get; set;}
	public int H { get; set;}
	public float Humidity { get; set;}
	public float Temperature { get; set;}
	public Type TileType { get; set;}

	public void Init(int xIdx, int yIdx, Type t)
	{
		_xi = xIdx;
		_yi = yIdx;
		TileType = t;
		Temperature = 0;
		Humidity = 0;

		F = 0;
		G = 0;
		H = 0;
	}
}