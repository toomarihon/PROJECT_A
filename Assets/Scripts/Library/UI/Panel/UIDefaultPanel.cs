using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDefaultPanel : UIPanel 
{
	public enum Direction
	{
		None,
		Left,
		Top,
		Right,
		Bottom
	}

	[SerializeField] private Image _image;

	[Header("Default Panel")]
	public JTween.EaseType _openTransitionStyle = JTween.EaseType.Linear;
	public JTween.EaseType _closeTransitionStyle = JTween.EaseType.Linear;
	public Direction _inDirection = Direction.None;
	public Direction _outDirection = Direction.None;
	public float _movingAmount = 50;
	public float _transitionTime = 0.15f;

	private Vector3 originPosition = Vector3.back;

	public override void StartOpenPopup ()
	{
		//Init originPosition of panel.
		if(originPosition == Vector3.back)
		{
			originPosition = this.transform.localPosition;
		}

		//Init
		SetAlphaValue (0);

		//Start move and change alpha value.
		switch(_inDirection)
		{
		case Direction.Left:
			this.transform.localPosition = originPosition + new Vector3 (_movingAmount, 0, 0);
			break;
		case Direction.Top:
			this.transform.localPosition = originPosition - new Vector3 (0, _movingAmount, 0);
			break;
		case Direction.Right:
			this.transform.localPosition = originPosition - new Vector3 (_movingAmount, 0, 0);
			break;
		case Direction.Bottom:
			this.transform.localPosition = originPosition + new Vector3 (0, _movingAmount, 0);
			break;
		}

		JTween.MoveTo (this.gameObject, originPosition, _transitionTime, 0, _openTransitionStyle);
		JTween.ValueTo (this.gameObject, 0, 1, _transitionTime, 0, SetAlphaValue, null, _openTransitionStyle);
	}

	public override void StartClosePopup ()
	{
		SetAlphaValue (1);

		Vector3 targetPos = Vector3.zero;
		switch(_outDirection)
		{
		case Direction.Left:
			targetPos = originPosition - new Vector3 (_movingAmount, 0, 0);
			break;
		case Direction.Top:
			targetPos = originPosition + new Vector3 (0, _movingAmount, 0);
			break;
		case Direction.Right:
			targetPos = originPosition + new Vector3 (_movingAmount, 0, 0);
			break;
		case Direction.Bottom:
			targetPos = originPosition - new Vector3 (0, _movingAmount, 0);
			break;
		}

		JTween.MoveTo (this.gameObject, targetPos, _transitionTime, 0, _openTransitionStyle);
		JTween.ValueTo (this.gameObject, 1, 0, _transitionTime, 0, SetAlphaValue, OnCompleteTransition, _openTransitionStyle);
	}

	//if you use this panel, this method must be override
	public virtual void SetAlphaValue(float alpha)
	{
		Color color = _image.color;
		color.a = alpha;
		_image.color = color;
	}

	public virtual void OnCompleteTransition(float alpha)
	{
		this.gameObject.SetActive (false);
	}
}
