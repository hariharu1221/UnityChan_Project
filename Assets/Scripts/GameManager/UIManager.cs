using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    IEnumerator Fade(bool isIn)
    {
        yield return new WaitForSeconds(1);
    }
}
