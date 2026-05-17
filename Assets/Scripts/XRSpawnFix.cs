using UnityEngine;
using Unity.XR.CoreUtils;

public class XRSpawnFix : MonoBehaviour
{
    public XROrigin xrOrigin;
    public CharacterController characterController;
    public Transform groundSpawnPoint;

    void Start()
    {
        if (xrOrigin == null || groundSpawnPoint == null)
        {
            Debug.LogError("XRSpawnFix: Referenzen fehlen!");
            return;
        }

        if (characterController != null)
            characterController.enabled = false;

        xrOrigin.transform.SetPositionAndRotation(
            groundSpawnPoint.position,
            groundSpawnPoint.rotation
        );

        if (characterController != null)
            characterController.enabled = true;

        Debug.Log("Spieler gespawnt bei: " + groundSpawnPoint.position);
    }
}