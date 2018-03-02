using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour 
{
	public static float GetAxis(string name)
	{
		return Input.GetAxis (name);
	}
}
