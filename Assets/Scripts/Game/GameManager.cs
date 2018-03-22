using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("-- Map Tile --")]
    public Transform _tileGroup;
	public GameObject _tilePrefab;

	private WorldData _worldData;
	private MapHandler _tileHandler;
	private PlantHandler _plantHandler;
	private AnimalHandler _animalHandler;

	private void Awake()
	{
		//allocate
		_worldData = new WorldData ();
		_plantHandler = new PlantHandler ();
		_tileHandler = new MapHandler ();
        _animalHandler = new AnimalHandler();

    }

    public void Start()
    {
        //init world
        _tileHandler.CreateMap(_worldData.MapTiles);
        _animalHandler.CreateMonsters(_worldData.AnimalGroup, _worldData.MapTiles);
        _plantHandler.CreatePlant(_worldData.PlantGroup, _worldData.MapTiles);

        CreateMap();
    }

    public void Update()
	{
		
	}

    public void CreateMap()
    {
        float width = MapTileInfo.Width = _tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = MapTileInfo.Height = _tilePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        int columnCount = MapTileInfo.ColumnCount;
        int rowCount = MapTileInfo.RowCount;

        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                GameObject copy = GameObject.Instantiate<GameObject>(_tilePrefab, _tileGroup);
                MapTile tile = copy.GetComponent<MapTile>();

                copy.transform.position = new Vector3(i * width, j * height, 0);
                tile.SetTile(i, j, _worldData.MapTiles[i, j].TileType);
            }
        }

    }
}
