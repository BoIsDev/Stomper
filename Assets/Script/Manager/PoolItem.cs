using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> objPool = new Dictionary<string, Queue<GameObject>>();
    public Transform Holder;
    private static PoolItem instance;
    public static PoolItem Instance => instance;
    private int countPrefabs = 0;
        private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    protected GameObject CreateNewObj(GameObject obj , Transform pos)
    {
        GameObject newGo = Instantiate(obj, Holder);
        newGo.SetActive(true);
        newGo.transform.position = pos.position;
        newGo.name = obj.name;
        return newGo;
    }

    public GameObject GetObjItem(GameObject gameObject,Transform pos)
    {
        if (objPool.TryGetValue(gameObject.name, out Queue<GameObject> objList))
        {
            if (objList.Count == 0)
            {
                return CreateNewObj(gameObject,pos);
            }
            else
            {
                GameObject newObjList = objList.Dequeue();
                newObjList.SetActive(true);
                newObjList.transform.position = pos.position;
                return newObjList;
            }
        }
        else
        {
            return CreateNewObj(gameObject,pos);
        }
    }

    public void ReturnObjePool(GameObject gameObject)
    {
        if (objPool.TryGetValue(gameObject.name, out Queue<GameObject> objList))
        {
            objList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            newQueue.Enqueue(gameObject);
            objPool.Add(gameObject.name, newQueue);
        }
        gameObject.SetActive(false);

    }

}
