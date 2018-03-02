using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    private Transform _root;
    public List<GameObject> _prefabList;
    private Dictionary<string, List<GameObject>> _objectPool = new Dictionary<string, List<GameObject>>();

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _root = new GameObject("ObjectPool").transform;
        _root.SetParent(this.transform);
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
        int prefabIdx = GetPrefabIdx(name);
        if(prefabIdx == -1)
        {
            return null;
        }

        Transform rootTrans = GetRootTransform(name);
        GameObject obj = null;
        GameObject first = null;

        for (int i = 0; i < count; i++)
        {
            obj = GameObject.Instantiate(_prefabList[prefabIdx], rootTrans);
            obj.SetActive(false);
            list.Add(obj);

            if (i == 0)
            {
                first = obj;
            }
        }

        return first;
    }

    private int GetPrefabIdx(string name)
    {
        for(int i = 0; i < _prefabList.Count; i++)
        {
            if(string.Compare(_prefabList[i].name, name) == 0)
            {
                return i;
            }
        }

        JDebugger.Log(this, "Prefab name : " + name + " doesn't exist in prefabList. return -1");
        return -1;
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
