using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour 
{
	public int Xi { get; set;}
	public int Yi { get; set;}

	private MapTileInfo.Type _type;
    private SpriteRenderer _spriteRenderer;

    public void SetTile(int xi, int yi, MapTileInfo.Type type)
    {
        if(_spriteRenderer == null)
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        Xi = xi;
        Yi = yi;
        _type = type;

        switch (type)
        {
            case MapTileInfo.Type.Ground:
                _spriteRenderer.sprite = ResourcesManager.Instance.GetSprite("Tile_Ground");
                break;
            case MapTileInfo.Type.Water:
                _spriteRenderer.sprite = ResourcesManager.Instance.GetSprite("Tile_Water");
                break;
        }
    }
}
