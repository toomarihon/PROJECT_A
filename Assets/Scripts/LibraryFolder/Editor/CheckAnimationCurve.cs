using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnimationCurve : MonoBehaviour 
{
	public AnimationCurve _animationCurve;

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < _animationCurve.length; i++)
		{
			Debug.Log ("TIME : " + _animationCurve.keys [i].time + " VALUE : " + _animationCurve.keys [i].value);
		}
	}
}
