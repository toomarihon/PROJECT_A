using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PanelStackManager
public class PanelStackManager : SingletonBase<PanelStackManager> 
{
	public enum PopupName
	{
		None = -1,

		Test,

		Max
	}
		
	public List<GameObject> _pupupPrefabList;

	private static float _distanceBetweenPopup = 50.0f;
	private List<GameObject> _popupStack = new List<GameObject>(20);
	private Dictionary<PopupName, List<GameObject>> _usedPopups = new Dictionary<PopupName, List<GameObject>>();
	private Transform _activatedGroup;
	private Transform _deactivatedGroup;

	private void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		Camera[] cameras = Camera.allCameras;

		for(int i = 0; i < Camera.allCameras.Length; i++)
		{
			if(cameras[i].CompareTag("UICamera"))
			{
				_activatedGroup = cameras [i].transform.Find ("UI_Canvas").Find ("Activated");
				_deactivatedGroup = cameras [i].transform.Find ("UI_Canvas").Find ("Deactivated");
			}
		}
	}

	/// <summary>
	/// Push the specified popupObj.
	/// 
	/// Set local z position and hierarchy
	/// </summary>
	/// <param name="popupObj">Popup object.</param>
	public void Push(GameObject popupObj)
	{
		UIPanel popup = popupObj.GetComponent<UIPanel> ();
        popup.OnCompleteAction = ActionAtPopPanel;
		Transform popupTrans = popup.transform;

		Vector3 position = popupTrans.localPosition;
		position.z = -Count () * _distanceBetweenPopup;
	
		popupTrans.SetParent (GetActivatedGroupTrans());
		popupTrans.localPosition = position;

		popupObj.SetActive (true);
        popup.StartOpenPopup();
        _popupStack.Add (popupObj);
    }

    public void Push(PopupName name)
	{
		//1) find popup in __usedPopups -> Init -> Push
		//2) if didnt find popop in _usedPopups? instantiate popup in popupList -> Init -> Add Used Popups -> Push
		List<GameObject> popupObjs = null;
		if(_usedPopups.TryGetValue(name, out popupObjs))
		{
			Debug.Log ("popupObjs.Count : " + popupObjs.Count);

			for(int i = 0; i < popupObjs.Count; i++)
			{
				if(!popupObjs[i].activeSelf)
				{
					Push (popupObjs [i]);
					return;
				}
			}
		}
			
		GameObject popupObj = GameObject.Instantiate<GameObject> (_pupupPrefabList [(int)name], GetActivatedGroupTrans());

		if(popupObjs == null)
		{
			popupObjs = new List<GameObject> ();
			popupObjs.Add (popupObj);
			_usedPopups.Add (name, popupObjs);
		}
		else
		{
			popupObjs.Add (popupObj);
		}

		Push (popupObj);
	}

	public void Pop()
	{
		if(Empty())
		{
			JDebugger.Log (this, "PanelStackManager Is Empty");
			return;
		}

		int lastIdx = _popupStack.Count - 1;
        
        UIPanel popup = _popupStack[lastIdx].GetComponent<UIPanel>();
        popup.StartClosePopup();
		popup.ClosePopup ();

        _popupStack.RemoveAt(lastIdx);
    }

	public void PopAll()
	{
		for(int i = 0; i < _popupStack.Count; i++)
		{
			Pop ();
		}
	}

	public int Count()
	{
		return _popupStack.Count;
	}

	public bool Empty()
	{
		return _popupStack.Count == 0;
	}

	public Transform GetActivatedGroupTrans()
	{
		return _activatedGroup;
	}

	public Transform GetDeactivatedGroupTrans()
	{
		return _deactivatedGroup;
	}

	public void ActionAtPopPanel(UIPanel popup)
    {
        Transform popupTrans = popup.transform;
        Vector3 position = popupTrans.localPosition;
        position.z = 0;

        popupTrans.SetParent(GetDeactivatedGroupTrans());
        popupTrans.localPosition = position;
    }
}
