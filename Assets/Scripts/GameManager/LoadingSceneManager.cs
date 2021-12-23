using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : Singleton<LoadingSceneManager>
{
    public static string nextScene;
    public Image fade;

    public void LoadSceneFade(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(FadeWithChange());
    }

    IEnumerator FadeWithChange()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0f;

        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress > 0.9f)
            {
                
            }
        }
        yield return new WaitForSeconds(1);
    }

    public void LoadSceneBar(string sceneName)
    {

    }
}