using UnityEngine;
using Unity.XR.CoreUtils;

public class XRSpawnFix : MonoBehaviour
{
    public XROrigin xrOrigin;
    public CharacterController characterController;
    public Transform groundSpawnPoint;

    void Start()
    {
        // Controller kurz aus
        characterController.enabled = false;

        // sauber auf Boden setzen
        xrOrigin.transform.SetPositionAndRotation(
            groundSpawnPoint.position,
            groundSpawnPoint.rotation
        );

        // wieder aktivieren
        characterController.enabled = true;
    }
}