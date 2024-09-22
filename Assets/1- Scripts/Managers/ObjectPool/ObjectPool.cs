using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> _objects = new Queue<T>();
    private readonly T _prefab;
    private readonly Transform _parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (_objects.Count > 0)
        {
            T obj = _objects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T obj = Object.Instantiate(_prefab, _parent);
            return obj;
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Enqueue(obj);
    }
}
