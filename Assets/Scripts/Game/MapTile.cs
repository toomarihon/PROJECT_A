using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour 
{
	private MapTileInfo _tileInfo;
	private SpriteRenderer _spriteRenderer;

	//JNE this is for editor display
	public float humidity;

	public int Xi {get{return _tileInfo.Xi;}}
	public int Yi {get{return _tileInfo.Yi;}}

    public void SetTile(MapTileInfo info)
    {
        if(_spriteRenderer == null)
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

		_tileInfo = info;

		switch(info.TileType)
		{
		case MapTileInfo.Type.Ground:
			if(info.Humidity > 0)
			{
				_spriteRenderer.color = new Color (1 - info.Humidity * 0.5f, 1 - info.Humidity, 1);
			}
			else
			{
				_spriteRenderer.color = Color.white;
			}
			break;

		case MapTileInfo.Type.Water:
			_spriteRenderer.color = Color.blue;
			break;
		}

		humidity = info.Humidity;
    }

	public static Vector3 IndexToPosition(int xi, int yi)
	{
		return new Vector3 (MapTileInfo.Width * xi, MapTileInfo.Height * yi, 0);
	}
}
