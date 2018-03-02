using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : SingletonBase<CameraHandler>
{
	public struct NodeInfo
	{
		Vector3 pos;
		float transitionTime;
	}
	
	private List<NodeInfo> _nodes;
	private Vector3 _targetPos;
	private float _transitionTime;
	private float _curTime;

	private void MovePosition(Vector3 pos)
	{
		if(_targetPos == pos)
		{
			
		}
	}
}
