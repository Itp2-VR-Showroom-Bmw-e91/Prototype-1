using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSeat : MonoBehaviour
{
    public Transform seatPoint;
    public GameObject xrOrigin;   // XR Rig
    public CharacterController characterController; // falls vorhanden

    private bool isSeated = false;

    public void SitDown()
    {
        if (isSeated) return;

        // Position + Rotation setzen
        xrOrigin.transform.position = seatPoint.position;
        xrOrigin.transform.rotation = seatPoint.rotation;

        // Movement sperren
        if (characterController != null)
            characterController.enabled = false;

        isSeated = true;
    }

    public void StandUp()
    {
        if (!isSeated) return;

        if (characterController != null)
            characterController.enabled = true;

        isSeated = false;
    }
}