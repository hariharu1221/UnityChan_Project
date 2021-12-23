using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DontDestroyManager : Singleton<DontDestroyManager>
{
    private Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();

    public void AddDontDestroyObject(string key ,GameObject ob)
    {
        if (gameObjects.ContainsKey(key)) { Debug.Log("중복 되는 키입니다"); return; }
        DontDestroyOnLoad(ob);
        gameObjects.Add(key, ob);
    }

    public void DestroyAllObject()
    {
        foreach(var i in gameObjects)
        {
            Destroy(i.Value);
        }
        gameObjects.Clear();
    }

    public void Destroy(string key)
    {
        Destroy(FIndGameobject(key));
        gameObjects.Remove(key);
    }

    public void SetActiveAllObject(bool active)
    {
        foreach(var i in gameObjects)
            i.Value.SetActive(active);
    }

    public void SetActive(bool active, string key)
    {
        FIndGameobject(key).SetActive(active);
    }

    public KeyValuePair<string, GameObject> FindPair(GameObject ob)
    {
        var pair = gameObjects.FirstOrDefault(x => x.Value == ob);
        return pair;
    }

    public KeyValuePair<string, GameObject> FindPair(string key)
    {
        var pair = gameObjects.FirstOrDefault(x => x.Key == key);
        return pair;
    }

    public GameObject FIndGameobject(string key)
    {
        GameObject ob = gameObjects.TryGetValue(key, out ob) ? ob : null;
        return ob;
    }
}
