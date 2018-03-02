using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour 
{
	public enum State
	{
		Idle,
		Move
	}

	public float _moveSpeed;

	protected Animator _animator;
	protected State _curState;

	public void Awake()
	{
		_animator = this.GetComponent<Animator> ();
	}

	public void ChangeState(State state)
	{
		if(_curState == state)
		{
			_curState = state;
			return;
		}

		Debug.Log ("JNE " + state.ToString ());
		_animator.SetInteger ("State", (int)state);
		_curState = state;
	}

	public void Update()
	{
		MoveCharacter ();
	}

	protected void MoveCharacter()
	{

	}
}
