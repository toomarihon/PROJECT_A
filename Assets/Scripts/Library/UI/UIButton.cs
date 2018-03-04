using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Button 클래스
/// </summary>
public class UIButton : UIObject, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	public bool IsActivated = true;
    public UnityEvent _onButtonClick;

    [Header("[Custom Button Settings]")]
	public JTween.EaseType _buttonStyle = JTween.EaseType.EaseOutBack;
    public float _pressedScale = 0.8f;
	public float _transitionTime = 0.1f;
    
	protected static List<UIButton> _enabledButtons = new List<UIButton> ();

	protected Image _image;
    protected Vector3 _localScale;
	protected RectTransform _rectTransform;
	protected System.Action<GameObject> _onButtonClickAction;

    protected virtual void Awake()
    {
        _image = this.GetComponent<Image>();
		_rectTransform = this.GetComponent<RectTransform> ();
    }

    protected virtual void Start()
    {
        _localScale = this.transform.localScale;
    }

	protected virtual void OnEnable()
	{
		_enabledButtons.Add (this);
	}

	protected virtual void OnDisable()
	{
		_enabledButtons.Remove (this);
	}

	public void DisableExceptThisButton()
	{
		for(int i = 0; i < _enabledButtons.Count; i++)
		{
			if(_enabledButtons[i] == this.gameObject)
			{
				continue;
			}

			_enabledButtons [i].IsActivated = false;
		}
	}

	public void EnableAllButton()
	{
		for(int i = 0; i < _enabledButtons.Count; i++)
		{
			_enabledButtons [i].IsActivated = true;
		}
	}

	public virtual void OnPointerDown(PointerEventData eventData)
    {
		if(!IsActivated)
		{
			return;
		}

        JTween.ScaleTo(this.gameObject, _localScale * _pressedScale, GetTransitionTime(), 0, _buttonStyle);
    }

	public virtual void OnPointerUp(PointerEventData eventData)
    {
		if(!IsActivated)
		{
			return;
		}

        JTween.ScaleTo(this.gameObject, _localScale, GetTransitionTime(), 0, _buttonStyle);
    }

    /// <summary>
    /// 버튼을 눌렀을 때 나오는 애니메이션을 구하는 시간
    /// </summary>
    /// <returns></returns>
	public float GetTransitionTime()
	{
		return _transitionTime;
	}

    public virtual void OnPointerClick(PointerEventData eventData)
    {
		if(!IsActivated)
		{
			return;
		}

        if(_onButtonClick != null)
        {
            _onButtonClick.Invoke();
        }

		if(_onButtonClickAction != null)
		{
			_onButtonClickAction (this.gameObject);
		}
    }

    /// <summary>
    /// 버튼을 클릭했을 때, 추가적인 작업을 추가한다.
    /// </summary>
    /// <param name="action"></param>
	public void SubscribeButton(System.Action<GameObject> action)
	{
		if(action == null)
		{
			return;
		}

		_onButtonClickAction += action;
	}

    /// <summary>
    /// SubScribe 했던 함수를 제외시킨다.
    /// </summary>
    /// <param name="action"></param>
	public void UnSubscribeButton(System.Action<GameObject> action)
	{
		if(action == null)
		{
			return;
		}

		_onButtonClickAction -= action;
	}
}
