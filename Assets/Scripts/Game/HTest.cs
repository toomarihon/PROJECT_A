using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest : MonoBehaviour 
{
	public GameObject popup;

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.V))
		{
			PanelStackManager.Instance.Push (PanelStackManager.PopupName.Test);
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			PanelStackManager.Instance.Pop ();
		}
	}
}
