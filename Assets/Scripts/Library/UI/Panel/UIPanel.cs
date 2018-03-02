using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : UIObject 
{
	public enum PopupType
	{
		None = -1,
		Popup,
		System,
	}
		
	[Header("Common Attribute")]
	public PopupType _curPopupType = PopupType.Popup;
    [HideInInspector] public System.Action<UIPanel> OnCompleteAction;

	public abstract void StartOpenPopup ();
	public abstract void StartClosePopup ();
    public virtual void ClosePopup()
    {
		if(OnCompleteAction == null)
		{
			return;
		}

		OnCompleteAction (this);
    }
}
