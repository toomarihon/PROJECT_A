using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        _tileHandler.CreateMapInfo(_worldData.MapTiles);
		_tileHandler.CreateMap(_worldData.MapTiles, this.gameObject);
		_plantHandler.CreatePlant(_worldData.PlantGroup, _worldData.MapTiles);
		_animalHandler.CreateMonsters (_worldData.AnimalGroup, _worldData.MapTiles);
    }
}
