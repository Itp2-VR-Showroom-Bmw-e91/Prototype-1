using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


public class EnterCar : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin;
    public Transform seatPosition;
    public Image fadeImage;

    [Header("Movement")]
    public ContinuousTurnProvider moveProvider;
    public ContinuousTurnProvider turnProvider;

    [Header("Settings")]
    public float fadeDuration = 0.3f;
    public float seatOffsetUp = 0.3f;

    private CharacterController cc;

    public void Enter()
    {
        StartCoroutine(EnterRoutine());
    }

    IEnumerator EnterRoutine()
    {
        // 🔵 Fade out
        yield return StartCoroutine(Fade(0, 1));

        // 🔴 XR MOVEMENT KOMPLETT STOPPEN
        if (moveProvider != null) moveProvider.enabled = false;
        if (turnProvider != null) turnProvider.enabled = false;

        // 🔥 WICHTIG: XR Origin Bewegung einfrieren
        xrOrigin.enabled = false;

        yield return null;

        // 🚗 Teleport
        Vector3 pos =
            seatPosition.position +
            seatPosition.up * seatOffsetUp;

        xrOrigin.transform.SetPositionAndRotation(seatPosition.position, seatPosition.rotation);

        yield return null;
        yield return null;

        // 🔵 XR wieder aktivieren
        xrOrigin.enabled = true;

        if (moveProvider != null) moveProvider.enabled = true;
        if (turnProvider != null) turnProvider.enabled = true;

        // 🔵 Fade in
        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(start, end, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, end);
    }
}