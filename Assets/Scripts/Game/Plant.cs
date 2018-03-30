using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : BuildableObject 
{
	private PlantTableInfo _plantInfo;

	public void SetPlantInfo(PlantTableInfo info, int xi, int yi)
	{
		_plantInfo = info;
		_xi = xi;
		_yi = yi;
	}
}
