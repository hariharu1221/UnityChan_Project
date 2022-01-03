using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : Singleton<LoadingSceneManager>
{
    public static string nextScene;

    public void LoadSceneFade(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(FadeWithChange());
    }

    IEnumerator FadeWithChange()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        UIManager.Instance.Fade(1.0f, true);
        float timer = 0f;
        while(!op.isDone)
        {
            timer += Time.deltaTime;
            if (timer > 1.0f)
                op.allowSceneActivation = true;
            yield return null;
        }
        UIManager.Instance.Fade(1.0f, false);
        yield return new WaitForSeconds(1);
    }

    public void LoadSceneBar(string sceneName)
    {

    }
}