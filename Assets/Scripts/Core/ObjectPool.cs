using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPool>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("ObjectPool");
                    _instance = go.AddComponent<ObjectPool>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    // Dictionary to store different pools by prefab
    private Dictionary<GameObject, List<GameObject>> _poolDictionary = new Dictionary<GameObject, List<GameObject>>();
    private Dictionary<GameObject, Transform> _poolParents = new Dictionary<GameObject, Transform>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Initialize a pool for a specific prefab
    /// </summary>
    /// <param name="prefab">The prefab to pool</param>
    /// <param name="initialSize">Initial number of instances to create</param>
    public void InitializePool(GameObject prefab, int initialSize)
    {
        if (prefab == null) return;

        // Create a parent object for this pool
        GameObject poolParent = new GameObject(prefab.name + "Pool");
        poolParent.transform.SetParent(transform);
        _poolParents[prefab] = poolParent.transform;

        // Create the pool list if it doesn't exist
        if (!_poolDictionary.ContainsKey(prefab))
        {
            _poolDictionary[prefab] = new List<GameObject>();
        }

        // Pre-instantiate objects
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = CreateNewInstance(prefab);
            obj.transform.SetParent(poolParent.transform);
            obj.SetActive(false);
            _poolDictionary[prefab].Add(obj);
        }
    }

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <param name="prefab">The prefab to get from the pool</param>
    /// <param name="position">Position to place the object</param>
    /// <param name="rotation">Rotation to apply to the object</param>
    /// <returns>The pooled GameObject</returns>
    public GameObject GetFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null) return null;

        // Initialize the pool if it doesn't exist
        if (!_poolDictionary.ContainsKey(prefab))
        {
            InitializePool(prefab, 10); // Default initial size
        }

        // Find an inactive object in the pool
        foreach (GameObject obj in _poolDictionary[prefab])
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive object is found, create a new one
        GameObject newObj = CreateNewInstance(prefab);
        newObj.transform.position = position;
        newObj.transform.rotation = rotation;
        newObj.transform.SetParent(_poolParents[prefab]);
        _poolDictionary[prefab].Add(newObj);
        return newObj;
    }

    /// <summary>
    /// Return an object to the pool
    /// </summary>
    /// <param name="obj">The object to return</param>
    /// <param name="prefab">The prefab this object was created from</param>
    public void ReturnToPool(GameObject obj, GameObject prefab)
    {
        if (obj == null || prefab == null) return;

        // Make sure the pool exists
        if (!_poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning("Trying to return an object to a non-existent pool");
            Destroy(obj);
            return;
        }

        // Reset the object and deactivate it
        obj.SetActive(false);
        
        // Make sure it's in the right parent
        obj.transform.SetParent(_poolParents[prefab]);

        // Add to pool if not already there
        if (!_poolDictionary[prefab].Contains(obj))
        {
            _poolDictionary[prefab].Add(obj);
        }
    }

    /// <summary>
    /// Create a new instance of a prefab
    /// </summary>
    private GameObject CreateNewInstance(GameObject prefab)
    {
        return Instantiate(prefab);
    }

    /// <summary>
    /// Clear a specific pool
    /// </summary>
    /// <param name="prefab">The prefab pool to clear</param>
    public void ClearPool(GameObject prefab)
    {
        if (!_poolDictionary.ContainsKey(prefab)) return;

        foreach (GameObject obj in _poolDictionary[prefab])
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        _poolDictionary[prefab].Clear();
    }

    /// <summary>
    /// Clear all pools
    /// </summary>
    public void ClearAllPools()
    {
        foreach (var prefab in _poolDictionary.Keys)
        {
            ClearPool(prefab);
        }
        _poolDictionary.Clear();
        _poolParents.Clear();
    }
}