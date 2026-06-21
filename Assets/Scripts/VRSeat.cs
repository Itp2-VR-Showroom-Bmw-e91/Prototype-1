using UnityEngine;

public class VRSeat : MonoBehaviour
{
    public Transform seatPoint;
    public GameObject xrOrigin; // Dein XR Origin (XR Rig)
    public CharacterController characterController;

    [Header("Collider zum Deaktivieren")]
    public Collider[] carCollider;
    public Collider driverSeatCollider;

    [Header("Locomotion Provider")]
    [SerializeField] private MonoBehaviour moveProvider;
    [SerializeField] private MonoBehaviour turnProvider;

    private bool isSeated = false;
    private Transform xrCamera;

    private void Start()
    {
        // Holt sich automatisch die Main Camera aus deinem Rig
        if (xrOrigin != null)
        {
            Camera mainCam = xrOrigin.GetComponentInChildren<Camera>();

            if (mainCam != null)
            {
                xrCamera = mainCam.transform;
            }
        }
    }

    public void SitDown()
    {
        if (isSeated) return;

        if (xrOrigin != null &&
            seatPoint != null &&
            characterController != null &&
            xrCamera != null)
        {
            // Bewegung deaktivieren
            if (moveProvider != null) moveProvider.enabled = false;
            if (turnProvider != null) turnProvider.enabled = false;

            // Auto-Collider deaktivieren
            if (carCollider != null)
            {
                foreach (Collider col in carCollider)
                {
                    if (col != null)
                        col.enabled = false;
                }
            }

            // Sitz-Trigger deaktivieren
            if (driverSeatCollider != null)
                driverSeatCollider.enabled = false;

            // Kamera exakt auf den Sitzpunkt setzen
            Vector3 cameraOffset = xrCamera.position - xrOrigin.transform.position;
            xrOrigin.transform.position = seatPoint.position - cameraOffset;
            xrOrigin.transform.rotation = seatPoint.rotation;

            Physics.SyncTransforms();

            isSeated = true;

            Debug.Log("Sitz-Modus erfolgreich! Augen sind auf dem Target fixiert.");
        }
    }

    public void StandUp(Transform exitPoint)
    {
        if (!isSeated || exitPoint == null) return;

        // Ausstiegspunkt
        xrOrigin.transform.position = exitPoint.position;
        xrOrigin.transform.rotation = exitPoint.rotation;

        Physics.SyncTransforms();

        // Auto-Collider wieder aktivieren
        if (carCollider != null)
        {
            foreach (Collider col in carCollider)
            {
                if (col != null)
                    col.enabled = true;
            }
        }

        // Sitz-Trigger wieder aktivieren
        if (driverSeatCollider != null)
            driverSeatCollider.enabled = true;

        // Bewegung wieder aktivieren
        if (moveProvider != null) moveProvider.enabled = true;
        if (turnProvider != null) turnProvider.enabled = true;

        isSeated = false;
    }
}