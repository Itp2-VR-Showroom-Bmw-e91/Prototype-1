using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class EnterCar : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin;
    public Transform seatPosition;
    public Image fadeImage;
    public CharacterController characterController;

    [Header("Movement")]
    public ActionBasedContinuousMoveProvider moveProvider;
    public ActionBasedContinuousTurnProvider turnProvider;

    [Header("Settings")]
    public float fadeDuration = 0.3f;
    public float seatOffsetUp = 0.3f;

    public void Enter()
    {
        StartCoroutine(EnterRoutine());
    }

    IEnumerator EnterRoutine()
    {
        yield return StartCoroutine(Fade(0, 1));

        if (moveProvider != null) moveProvider.enabled = false;
        if (turnProvider != null) turnProvider.enabled = false;

        // CharacterController deaktivieren damit Teleport funktioniert
        if (characterController != null)
            characterController.enabled = false;

        yield return null;

        Vector3 pos = seatPosition.position + seatPosition.up * seatOffsetUp;
        xrOrigin.transform.SetPositionAndRotation(pos, seatPosition.rotation);

        yield return null;

        if (characterController != null)
            characterController.enabled = true;

        if (moveProvider != null) moveProvider.enabled = true;
        if (turnProvider != null) turnProvider.enabled = true;

        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        if (fadeImage == null) yield break;

        float time = 0;
        while (time < fadeDuration)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(start, end, time / fadeDuration));
            time += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, end);
    }
}