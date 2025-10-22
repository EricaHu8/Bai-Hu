using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingScript : MonoBehaviour
{
    public static FadingScript instance;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1.0f;
    public bool fadeRunning;
    public bool fadeIn;
    public List<AudioSource> audioSources;

    public float volumeStart;
    public int numSounds;
    

    [SerializeField] private GameObject fox;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        FadeIn();

        
    }

    public void FadeIn()
    {   
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    public IEnumerator FadeOutAndInHelper()
    {
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
        fadeRunning = true;
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }
    public IEnumerator FadeOutAndInHelper(Vector3 foxPos)
    {
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
        fadeRunning = true;
        fox.transform.position = foxPos;
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        
        fadeRunning = true;
        float elapsedTime = 0.0f;
        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            for (int i=0; i<numSounds; i++)
            {
                
                audioSources[i].volume = Mathf.Lerp(0, volumeStart, elapsedTime / duration);
            }
            yield return null;
        }
        cg.alpha = end;
        fadeRunning = false;
    }
}
