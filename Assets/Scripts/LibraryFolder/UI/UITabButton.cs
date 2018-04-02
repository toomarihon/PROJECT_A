using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITabButton : UIButton
{
    [Header("[Custom Tab Button Settings]")]
	public bool _isDefault;
    public string _spriteOn = null;
    public string _spriteOff = null;
    public Vector2 _sizeOn = new Vector2(1, 1);
    public Vector2 _sizeOff = new Vector2(1, 1);

	private static List<UITabButton> tabButtons = new List<UITabButton>();

	protected override void OnEnable()
	{
		base.OnEnable ();

		if (string.IsNullOrEmpty(_spriteOn))
		{
			_spriteOn = _image.sprite.name;
		}
		if (string.IsNullOrEmpty(_spriteOff))
		{
			_spriteOff = _image.sprite.name;
		}

		tabButtons.Add (this);
		Activate (_isDefault, true);
	}

	protected override void OnDisable()
	{
		base.OnDisable ();

		tabButtons.Remove (this);
	}

	public void Activate(bool flag, bool isForced = false)
    {
		//if cur state is same with next state, didnt do anything.
		//if isForced is true, didnt end this function.
		if(IsActivated == flag && !isForced)
		{
			return;
		}

		IsActivated = flag;
		_image.sprite = ResourcesManager.Instance.GetSprite(flag ? _spriteOn : _spriteOff);

		//if is forced is true, set size directly.
		if(isForced)
		{
			_rectTransform.sizeDelta = flag ? _sizeOn : _sizeOff;
			return;
		}

		Vector2 sizeDelta = this.GetComponent<RectTransform> ().sizeDelta;
        if (flag)
        {
			JTween.ValueTo (this.gameObject, sizeDelta.x, _sizeOn.x, GetTransitionTime (), 0, UpdatedWidth, null, _buttonStyle);
			JTween.ValueTo (this.gameObject, sizeDelta.y, _sizeOn.y, GetTransitionTime (), 0, UpdatedHeight, null, _buttonStyle);
		}
        else
        {
			JTween.ValueTo (this.gameObject, sizeDelta.x, _sizeOff.x, GetTransitionTime (), 0, UpdatedWidth, null, _buttonStyle);
			JTween.ValueTo (this.gameObject, sizeDelta.y, _sizeOff.y, GetTransitionTime (), 0, UpdatedHeight, null, _buttonStyle);
        }
    }

	private void DeactivateOthers(UITabButton curButton)
	{
		for(int i = 0; i < tabButtons.Count; i++)
		{
			if(tabButtons[i] == curButton || !tabButtons[i].IsActivated)
			{
				continue;
			}

			tabButtons [i].Activate (false);
		}
	}

	public override void OnPointerDown (PointerEventData eventData)
	{
		if(IsActivated)
		{
			return;
		}

		JTween.ScaleTo(this.gameObject, _localScale * _pressedScale, GetTransitionTime(), 0, _buttonStyle);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		if(IsActivated)
		{
			return;
		}

		JTween.ScaleTo(this.gameObject, _localScale, GetTransitionTime(), 0, _buttonStyle);
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		if(IsActivated)
		{
			return;
		}

		base.OnPointerClick (eventData);

		Activate (true);
		DeactivateOthers (this);
	}

	private void UpdatedWidth(float width)
	{
		Vector2 sizeDelta = _rectTransform.sizeDelta;
		sizeDelta.x = width;
		_rectTransform.sizeDelta = sizeDelta;
	}

	private void UpdatedHeight(float height)
	{
		Vector2 sizeDelta = _rectTransform.sizeDelta;
		sizeDelta.y = height;
		_rectTransform.sizeDelta = sizeDelta;
	}
}
