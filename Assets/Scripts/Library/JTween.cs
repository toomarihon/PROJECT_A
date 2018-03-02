using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 이 클래스는 iTween 을 상속받은 클래스로, 편의성을 조금 더 개선하려는 목적으로 만든 클래스이다.
 * 
 */
public struct JTweenFactor
{
	//PUBLIC MEMBER
	public JTween.TweenType tweenType;
	public JTween.EaseType easeType;
	public GameObject target;
	public Vector3 start;
	public Vector3 end;
	public float delay;
	public float curTime;
	public float endTime;
	public JTween.TweenAction onUpdateAction;
	public JTween.TweenAction onCompleteAction;

	public float CurTime
	{
		set
		{
			if(value > endTime)
			{
				value = endTime;
			}

			if(value < 0)
			{
				value = 0;
			}

			curTime = value;
		}
		get
		{
			return curTime;
		}
	}

	public JTweenFactor(JTween.TweenType tween, JTween.EaseType ease, GameObject targetObj, Vector3 startVec, Vector3 endVec, float time, float delayTime, JTween.TweenAction onUpdate = null, JTween.TweenAction onComplete = null)
	{
		tweenType = tween;
		easeType = ease;
		target = targetObj;
		start = startVec;
		end = endVec;
		curTime = 0;
		endTime = time;
		delay = delayTime;
		onUpdateAction = onUpdate;
		onCompleteAction = onComplete;
	}
}

public class JTween : MonoBehaviour
{
	public delegate void TweenAction(float value);

	public enum EaseType
	{
		Linear,
		EaseInBack,
		EaseOutBack,
		MAX
	}

	public enum TweenType
	{
		Move,
		Scale,
		Value,
	}

	private static AnimationCurve[] _animationCurve;
	private static List<JTweenFactor> _factorList;

	static JTween()
	{
		_animationCurve = new AnimationCurve[(int)EaseType.MAX] {
			new AnimationCurve (new Keyframe (0, 0), new Keyframe (1, 1)), //Linear
			new AnimationCurve (new Keyframe (0, 0), new Keyframe(0.5063928f, -0.1045021f), new Keyframe (1, 1)), //EaseInBack
			new AnimationCurve (new Keyframe (0, 0), new Keyframe(0.5452878f, 1.137024f), new Keyframe (1, 1)), //EaseOutBack
		};

		_factorList = new List<JTweenFactor> (50);
	}

	public static void ScaleTo(GameObject target, Vector3 scale, float time, float delay = 0, EaseType easeType = EaseType.Linear)
	{
		JTweenFactor factor = new JTweenFactor (TweenType.Scale, easeType, target, target.transform.localScale, scale, time, delay);
		_factorList.Add (factor);
	}

	//this function use vector3's first element
	public static void ValueTo(GameObject target, float startVal, float endVal, float time, float delay, TweenAction updateAction, TweenAction completeAction = null, EaseType easeType = EaseType.Linear)
	{
		if(updateAction == null)
		{
			JDebugger.Log ("JTween ValueTo updateAction must be registered");
			return;
		}

		JTweenFactor factor = new JTweenFactor (TweenType.Value, easeType, target, new Vector3(startVal, 0, 0), new Vector3(endVal, 0, 0), time, delay, updateAction, completeAction);
		_factorList.Add (factor);
	}

	public static void MoveTo(GameObject target, Vector3 pos, float time, float delay = 0, EaseType easeType = EaseType.Linear)
	{
		JTweenFactor factor = new JTweenFactor (TweenType.Move, easeType, target, target.transform.localPosition, pos, time, delay);
		_factorList.Add (factor);
	}

	private void Awake()
	{
		DontDestroyOnLoad (this);
	}

	private void Update()
	{
		if(_factorList.Count == 0)
		{
			return;
		}

		for(int i = 0; i < _factorList.Count; i++)
		{
			JTweenFactor factor = _factorList [i];

			if(UpdateDelay(ref factor))
			{
				continue;
			}

			//Update JTween.
			factor.CurTime += Time.deltaTime;
			switch(factor.tweenType)
			{
			case TweenType.Move:
				UpdatePosition (ref factor);
				break;
			case TweenType.Scale:
				UpdateScale (ref factor);
				break;
			case TweenType.Value:
				UpdateValue (ref factor);
				break;
			}

			_factorList [i] = factor;
		}


		//Remove factor
		_factorList.RemoveAll ((JTweenFactor factor) => {
			return factor.CurTime == factor.endTime;
		});
	}

	//Delay > 0 ? return true, or return false
	private bool UpdateDelay(ref JTweenFactor factor)
	{
		if(factor.delay > 0)
		{
			factor.delay -= Time.deltaTime;

			if(factor.delay < 0)
			{
				factor.delay = 0;
			}

			return true;
		}

		return false;
	}

	private void UpdatePosition (ref JTweenFactor factor)
	{
		factor.target.transform.localPosition = EvalateVector (ref factor);
	}

	private void UpdateScale(ref JTweenFactor factor)
	{
		factor.target.transform.localScale = EvalateVector (ref factor);
	}

	private void UpdateValue(ref JTweenFactor factor)
	{
		Vector3 value = EvalateVector (ref factor);

		if(factor.onUpdateAction != null)
		{
			factor.onUpdateAction (value.x);
		}
		if(factor.onCompleteAction != null && factor.end == value)
		{
			factor.onCompleteAction (value.x);
		}
	}

	private Vector3 EvalateVector(ref JTweenFactor factor)
	{
		float offset = factor.curTime / factor.endTime;

		float x = factor.start.x + (factor.end.x - factor.start.x) * _animationCurve [(int)factor.easeType].Evaluate (offset);
		float y = factor.start.y + (factor.end.y - factor.start.y) * _animationCurve [(int)factor.easeType].Evaluate (offset);
		float z = factor.start.z + (factor.end.z - factor.start.z) * _animationCurve [(int)factor.easeType].Evaluate (offset);

		return new Vector3 (x, y, z);
	}

	public float Evaluate(EaseType type, float time)
	{
		if(time > 1)
		{
			time = 1;
		}
		if(time < 0)
		{
			time = 0;
		}

		return _animationCurve [(int)type].Evaluate (time);
	}
}
