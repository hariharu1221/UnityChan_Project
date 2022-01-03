using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public void Fade(float time, bool isIn)
    {
        StartCoroutine(FadeCor(time, isIn));
    }

    IEnumerator FadeCor(float time, bool isIn)
    {
        yield return new WaitForSeconds(1);
    }
}
