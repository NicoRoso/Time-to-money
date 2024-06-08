using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_Screen : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScene;

    [SerializeField] private Slider scale;

    public void Loading(int index)
    {
        Time.timeScale = 1;
        LoadingScene.SetActive(true);
        StartCoroutine(LoadAsync(index));
    }

    IEnumerator LoadAsync(int index)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(index);
        loadAsync.allowSceneActivation = false;

        while (!loadAsync.isDone)
        {
            scale.value = loadAsync.progress;

            if (loadAsync.progress >= 0.9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(2.2f);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
