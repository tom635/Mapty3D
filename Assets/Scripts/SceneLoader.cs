using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadScene(int index)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(_LoadScene(index));
    }

    IEnumerator _LoadScene(int index)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            Debug.Log(progress);

            loadingBar.value = progress;

            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
