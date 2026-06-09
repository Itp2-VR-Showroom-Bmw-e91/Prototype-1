using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;

public class SoundManager : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    [Header("Audio")]
    public AudioSource start;
    public AudioSource idle;
    public AudioSource end;
    public float fadeTime = 0.15f;

    private bool isRunning = false;
    private Coroutine fadeRoutine;

    protected override void Awake()
    {
        base.Awake();
        idle.volume = 0f;
        idle.loop = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (!isRunning)
        {
            StartSound();
            isRunning = true;
        }
        else
        {
            StopSound();
            isRunning = false;
        }
    }

    void StartSound()
    {
        start.Play();
        if (!idle.isPlaying)
            idle.Play();
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
        idle.Stop();
        idle.volume = 0f;
        end.volume = 1f;
    }
}