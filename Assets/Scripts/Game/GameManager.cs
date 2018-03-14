﻿using System.Collections;
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
		_animalHandler = new AnimalHandler ();

		//init world
		_tileHandler.CreateMap (_worldData.MapTiles);
		_animalHandler.CreateMonsters (_worldData.AnimalGroup, _worldData.MapTiles);
	}
}