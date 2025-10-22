using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public CanvasGroup title;
    public CanvasGroup startText;
    public CanvasGroup fadeCanvas;

    [SerializeField] private float fadeDuration = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        title.alpha = 0f;
        startText.alpha = 0f;
        StartCoroutine(fadeTextIn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator fadeTextIn()
    {
        yield return new WaitForSeconds(1.0f);  
        StartCoroutine(FadingScript.instance.FadeCanvasGroup(title, title.alpha, 1, fadeDuration));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadingScript.instance.FadeCanvasGroup(startText, startText.alpha, 1, fadeDuration));
    }

    private IEnumerator ChangeScene()
    {
        yield return StartCoroutine(FadingScript.instance.FadeCanvasGroup(fadeCanvas, fadeCanvas.alpha, 1, fadeDuration));
        SceneManager.LoadScene("CaveRegion");
        yield return null;
    }
}
