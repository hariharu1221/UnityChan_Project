using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Prefabs effect;
    static EffectManager instance;
    public static EffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                var n = GameObject.Instantiate(Resources.Load<GameObject>("EffectManager"));
                DontDestroyOnLoad(n);
                instance = n.GetComponent<EffectManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    void Update()
    {
        
    }
    
    IEnumerator PlayEffectOnceCoroutine(int index, Vector3 position, Vector3 rotation, float size, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject n = GameObject.Instantiate(effect.prefabs[index]);
        n.transform.position = position;
        n.transform.rotation = Quaternion.Euler(rotation);
        n.transform.localScale = Vector3.one * size;
        //List<ParticleSystem> list = n.GetComponentsInChildren<ParticleSystem>().ToList();
        //list.Add(n.GetComponent<ParticleSystem>());
        yield return new WaitForSeconds(n.GetComponent<ParticleSystem>().duration);
        Destroy(n);
    }

    public void PlayEffectOnce(int index, Vector3 position, Vector3 rotation, float size = 1, float waitTime = 0)
    {
        StartCoroutine(PlayEffectOnceCoroutine(index, position, rotation, size, waitTime));
    }

    IEnumerator PlayEffectOnceCoroutine(int index, Transform transform, Vector3 plusVector, float size, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject n = GameObject.Instantiate(effect.prefabs[index]);
        n.transform.position = transform.position + plusVector;
        n.transform.rotation = transform.rotation;
        n.transform.localScale = Vector3.one * size;
        //List<ParticleSystem> list = n.GetComponentsInChildren<ParticleSystem>().ToList();
        //list.Add(n.GetComponent<ParticleSystem>());
        yield return new WaitForSeconds(n.GetComponent<ParticleSystem>().duration);
        Destroy(n);
    }

    public void PlayEffectOnce(int index, Transform transform, Vector3 plusVector, float size = 1, float waitTime = 0)
    {
        StartCoroutine(PlayEffectOnceCoroutine(index, transform, plusVector, size, waitTime));
    }
}