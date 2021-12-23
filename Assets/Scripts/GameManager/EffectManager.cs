using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] private PrefabsData effect;

    private void Awake()
    {
        effect = Resources.Load<PrefabsData>("Effects");
    }

    IEnumerator PlayEffectOnceCoroutine(int index, Vector3 position, Vector3 rotation, float size, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject n = GameObject.Instantiate(effect.prefabs[index]);
        n.transform.position = position;
        n.transform.rotation = Quaternion.Euler(rotation);
        n.transform.localScale = Vector3.one * size;
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
        yield return new WaitForSeconds(n.GetComponent<ParticleSystem>().duration);
        Destroy(n);
    }

    public void PlayEffectOnce(int index, Transform transform, Vector3 plusVector, float size = 1, float waitTime = 0)
    {
        StartCoroutine(PlayEffectOnceCoroutine(index, transform, plusVector, size, waitTime));
    }
}