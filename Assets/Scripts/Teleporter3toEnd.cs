using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter3toEnd : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if fox collides with teleporter
        // scene changes to EndScreen.unity

        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ChangeScene());
            //SceneManager.LoadScene("EndScreen");
        }

    }
    public IEnumerator ChangeScene()
    {
        yield return StartCoroutine(FadingScript.instance.FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
        SceneManager.LoadScene("EndScreen");
    }
}

