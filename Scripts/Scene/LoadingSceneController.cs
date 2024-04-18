using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;

public class LoadingSceneController : MonoBehaviour{
    static string nextScene;

    public Slider progressbar;
    public Text loadtext;

    
    [SerializeField]
    Image progressBar;
   
    public static void LoadScene(string sceneName) {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess() {
        /*AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = - 0.5f;
        while (!op.isDone) {
            yield return null;

            if(op.progress < 0f) {
                progressBar.fillAmount = op.progress;
            }
            else {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0f, 1f, timer);
                if(progressBar.fillAmount >= 1f) {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }*/
        yield return null;
        AsyncOperation op2 = SceneManager.LoadSceneAsync(nextScene);
        op2.allowSceneActivation = false;

        float timer2 = -2f;
        while (!op2.isDone) {
            yield return null;
            timer2 += Time.deltaTime/2;
            if (op2.progress >= 0.9f) {
                progressbar.value = Mathf.Lerp(progressbar.value, 1f, timer2);
                if (progressbar.value == 1.0f)
                {
                    op2.allowSceneActivation = true;
                }
            }
            else {

                progressbar.value = Mathf.Lerp(0f, op2.progress, timer2);
                if (progressbar.value >= op2.progress) {
                    timer2 = 0f;
                }
            }
            //float Loading = progressbar.value * 100;
        }
    }
}
