using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollView : MonoBehaviour 
{
	public GameObject _prefab;

	[Header("Already Set")]
	public RectTransform _contents;
	public RectTransform _grid;
	public RectTransform _scrollBarHorizontalTransform;
	public RectTransform _scrollBarVerticalTransform;

	private ScrollRect _scrollRect;
	private RectTransform _rectTransform;

	public System.Action<int, GameObject> OnUpdateContentAction{ get; set;}
	private List<GameObject> _contentList = new List<GameObject>();

	public void Awake()
	{
		_scrollRect = this.GetComponent<ScrollRect> ();
		_rectTransform = this.GetComponent<RectTransform> ();
	}

	public void Start()
	{
		if (_prefab == null) 
		{
			JDebugger.Log (this, "UI Scroll View : Prefab is not allocated by user");
			return;
		}

		GridLayoutGroup grid = _grid.GetComponent<GridLayoutGroup> ();
		grid.cellSize = _prefab.GetComponent<RectTransform> ().sizeDelta;

		// scrollRect -> horizontal ? grid -> vertical
		// scrollRect -> vertical ? grid -> horizontal
		if(_scrollRect.horizontal)
		{
			grid.startAxis = GridLayoutGroup.Axis.Vertical;
		}
		else
		{
			grid.startAxis = GridLayoutGroup.Axis.Horizontal;
		}

		Vector2 contentSize = _rectTransform.sizeDelta;
		contentSize.y -= _scrollBarHorizontalTransform.sizeDelta.y;
		contentSize.x -= _scrollBarVerticalTransform.sizeDelta.x;

		if(_scrollRect.horizontal && grid.cellSize.y > contentSize.y)
		{
			contentSize.y = grid.cellSize.y;
		}
		else if(_scrollRect.vertical && grid.cellSize.x > contentSize.x)
		{
			contentSize.x = grid.cellSize.x;
		}

		_contents.sizeDelta = contentSize;
		_grid.sizeDelta = contentSize;

		SetContentsRectSize ();
	}

	public void SetContentsRectSize()
	{
		if(IsEmpty())
		{
			return;
		}

		_contents.anchoredPosition = Vector3.zero;

		int contentsCount = _contentList.Count;
		Vector2 final = _contents.sizeDelta;
		Vector2 spacing = _grid.GetComponent<GridLayoutGroup> ().spacing;
		Vector2 objSize = _prefab.GetComponent<RectTransform> ().sizeDelta;

		if(_scrollRect.horizontal)
		{
			int colums = (int)Mathf.Ceil ((float)contentsCount / (int)(_rectTransform.sizeDelta.y / (spacing.y + objSize.y)));
			final.x = colums * (objSize.x + spacing.x);
		}
		else
		{
			int rows = (int)Mathf.Ceil((float)contentsCount / (int)(_rectTransform.sizeDelta.x / (spacing.x + objSize.x)));
			final.y = rows * (objSize.y + spacing.y);
		}
	
		_contents.sizeDelta = final;
		_grid.sizeDelta = final;
	}
		
	public void AddContent()
	{
		GameObject obj = GameObject.Instantiate (_prefab, _grid.transform);
		_contentList.Add (obj);
		Refresh ();
	}

	public void RemoveContent(int idx)
	{
		if(idx >= _contentList.Count)
		{
			JDebugger.Log (this, "RemoveContent parameter idx is out of range");
			return;
		}

		_contentList.RemoveAt (idx);
		Refresh ();
	}

	public void RemoveContent(GameObject obj)
	{
		for(int i = 0; i < _contentList.Count; i++)
		{
			if(obj == _contentList[i])
			{
				RemoveContent (i);
				return;
			}
		}

		JDebugger.Log (this, "RemoveContent can't find obj ");
	}

	public void RemoveContent(System.Predicate<GameObject> predicate)
	{
		_contentList.RemoveAll (predicate);

		Refresh ();
	}

	public void Sort(System.Comparison<GameObject> comparison)
	{
		_contentList.Sort (comparison);
	}

	public void Refresh()
	{
		SetContentsRectSize ();
		RefreshEX ();

		Sort ((lhs, rhs) => {
			return 0;
		});
	}

	private void RefreshEX()
	{
		for(int i = 0; i < _contentList.Count; i++)
		{
			if(OnUpdateContentAction != null)
			{
				OnUpdateContentAction (i, _contentList [i]);
			}
		}
	}

	public bool IsEmpty()
	{
		return _contentList.Count == 0;
	}
}
