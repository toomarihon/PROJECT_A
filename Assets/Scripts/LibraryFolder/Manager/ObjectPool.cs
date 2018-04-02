using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//isModified
public class ObjectPool
{
    private Transform _root;
	private Dictionary<string, GameObject> _prefabList;
    private Dictionary<string, List<GameObject>> _objectPool = new Dictionary<string, List<GameObject>>();

	public void Init(Dictionary<string, GameObject> prefabs)
	{
		if(_root == null)
		{
			_root = new GameObject ("ObjectPool").transform;
		}

		_prefabList = prefabs;
	}

    public GameObject GetObject(string name)
    { 
        List<GameObject> objList = null;
        GameObject curObj = null;

        if(_objectPool.TryGetValue(name, out objList))
        {
            for (int i = 0; i < objList.Count; i++)
            {
                curObj = objList[i];

                if (!curObj.activeSelf)
                {
                    curObj.SetActive(true);
                    return curObj;
                }
            }
        }

        curObj = AddObject(name, (objList == null ? 2 : (int)(objList.Count * 0.5)));
        curObj.SetActive(true);
       
        return curObj;
    }

    //return minimum index of objectList
    private GameObject AddObject(string name, int count)
    {
        List<GameObject> objList = null;
        GameObject frontObj = null;

        if (_objectPool.TryGetValue(name, out objList))
        {
            frontObj = AddObjectEx(objList, name, count);
        }
        else
        {
            objList = new List<GameObject>(count);

            frontObj = AddObjectEx(objList, name, count);

            _objectPool.Add(name, objList);
        }

        return frontObj;
    }

    private GameObject AddObjectEx(List<GameObject> list, string name, int count)
    {
        Transform rootTrans = GetRootTransform(name);
        GameObject obj = null;

		for (int i = count -1; i >= 0; i--)
        {
			GameObject prefab = null;
			if(!_prefabList.TryGetValue(name, out prefab))
			{
				JDebugger.Log ("prefab : " + name + " doesnt exist in prefabList. return null");
				return null;
			}

			obj = GameObject.Instantiate(prefab, rootTrans);
            obj.SetActive(false);
            list.Add(obj);
        }

		return obj;
    }

    private Transform GetRootTransform(string name)
    {
        Transform root = _root.transform.Find(name);
        if (root == null)
        {
            root = new GameObject(name).transform;
            root.SetParent(_root);
        }

        return root;
    }
}
