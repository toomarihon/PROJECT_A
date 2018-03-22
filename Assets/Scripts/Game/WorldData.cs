using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGroup 
{
	private int _gId;
	private float _movingRange;
	private List<Animal> _animalList;

	public void Init(int id, float range)
	{
		if(_animalList == null)
		{
			_animalList = new List<Animal> ();
		}
		else
		{
			_animalList.Clear ();
		}

		_gId = id;
		_movingRange = range;
	}

	public void AddAnimal(Animal animal)
	{
		_animalList.Add (animal);
	}
}

public class PlantGroup
{
	private int _gid;
	private float _regenRange;
	private List<Plant> _plantList;

	public void Init(int id, float range)
	{
		if(_plantList == null)
		{
			_plantList = new List<Plant> ();
		}
		else
		{
			_plantList.Clear ();
		}

		_gid = id;
		_regenRange = range;
	}

	public void AddPlant(Plant plant)
	{
		_plantList.Add(plant);
	}
		
}
	
public class WorldData
{
	public MapTileInfo[,] MapTiles { get; set;}
	public List<AnimalGroup> AnimalGroup{ get; set;}
	public List<PlantGroup> PlantGroup{ get; set;}
}
