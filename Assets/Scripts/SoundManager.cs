using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource start;
    public AudioSource idle;
    public AudioSource end;

    

    public bool startstop;
    public float fadeTime = 0.15f;

    private bool isRunning;
    private Coroutine fadeRoutine;

    void Start()
    {
        idle.volume = 0f;
        idle.loop = true;
    }

    void Update()
    {
        if (startstop && !isRunning)
        {
            StartSound();
            isRunning = true;
        }
        else if (!startstop && isRunning)
        {
            StopSound();
            isRunning = false;
        }
    }

    void StartSound()
    {
        start.Play();

        if (!idle.isPlaying)
            idle.Play(); // EINMAL starten, dann nie wieder anfassen

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeIdleInAfterStart());
    }

    IEnumerator FadeIdleInAfterStart()
    {
        yield return new WaitForSeconds(start.clip.length - fadeTime);

        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            idle.volume = Mathf.Lerp(0f, 1f, t / fadeTime);
            yield return null;
        }

        idle.volume = 1f;
    }

    void StopSound()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeOutIdleAndPlayEnd());
    }

    IEnumerator FadeOutIdleAndPlayEnd()
    {
        end.volume = 0f;
        end.Play();

        float startVol = idle.volume;
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            idle.volume = Mathf.Lerp(startVol, 0f, t / fadeTime);
            end.volume = Mathf.Lerp(0f, 1f, t / fadeTime);
            yield return null;
        }

        idle.Stop();       // JETZT erst stoppen
        idle.volume = 0f;
        end.volume = 1f;
    }
    private void OnMouseDown()
    {
        if (!startstop)
        {
            startstop = true;
        }
        else
        {
            startstop = false;
        }
        
    }
}
