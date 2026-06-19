using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSeat : MonoBehaviour
{
    public Transform seatPoint;
    public GameObject xrOrigin;   // Dein XR Origin (XR Rig)
    public CharacterController characterController; 

    [SerializeField] private MonoBehaviour moveProvider;
    [SerializeField] private MonoBehaviour turnProvider;

    private bool isSeated = false;

    // Wir merken uns die originale Tracking-Einstellung, um sie beim Aufstehen wiederherzustellen
    private Unity.XR.CoreUtils.XROrigin unityXROrigin;

    private void Start()
    {
        if (xrOrigin != null)
        {
            unityXROrigin = xrOrigin.GetComponent<Unity.XR.CoreUtils.XROrigin>();
        }
    }

    public void SitDown()
    {
        if (isSeated) return;

        if (xrOrigin != null && seatPoint != null && characterController != null)
        {
            // 1. Bewegung & Physik kurz stoppen
            characterController.enabled = false;

            // 2. VR-Kamera exakt auf den Sitz zwingen (Ignoriert ab jetzt die echte Körpergröße)
            if (unityXROrigin != null)
            {
                unityXROrigin.RequestedTrackingOriginMode = Unity.XR.CoreUtils.XROrigin.TrackingOriginMode.Device;
            }

            // 3. Exakte Position und Rotation setzen
            xrOrigin.transform.position = seatPoint.position;
            xrOrigin.transform.rotation = seatPoint.rotation;

            // 4. Physik updaten und Controller wieder anwerfen
            Physics.SyncTransforms();
            characterController.enabled = true;

            // 5. Bewegung blockieren
            if (moveProvider != null) moveProvider.enabled = false;
            if (turnProvider != null) turnProvider.enabled = false;

            isSeated = true;
            Debug.Log("Bombenfest auf Sitz fixiert – Augen sind jetzt exakt auf dem SeatPoint!");
        }
    }

    public void StandUp(Transform exitPoint)
    {
        if (!isSeated) return;

        if (xrOrigin != null && exitPoint != null && characterController != null)
        {
            characterController.enabled = false;

            // Beim Aufstehen wieder auf den echten Hallenboden (Körpergröße) zurückschalten
            if (unityXROrigin != null)
            {
                unityXROrigin.RequestedTrackingOriginMode = Unity.XR.CoreUtils.XROrigin.TrackingOriginMode.Floor;
            }

            xrOrigin.transform.position = exitPoint.position;

            Physics.SyncTransforms();
            characterController.enabled = true;

            if (moveProvider != null) moveProvider.enabled = true;
            if (turnProvider != null) turnProvider.enabled = true;
        }

        isSeated = false;
        Debug.Log("Aufgestanden – Normale Fortbewegung aktiv.");
    }
}