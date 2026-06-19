using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

public class VRSeat : MonoBehaviour
{
    public Transform seatPoint;
    public GameObject xrOrigin;
    public CharacterController characterController;

    [Header("Collider zum Deaktivieren")]
    public Collider carCollider;
    public Collider driverSeatCollider;

    private bool isSeated = false;
    private Transform xrCamera;
    private Vector3 targetOriginPos;
    private Transform cameraOffset;
    private LocomotionMediator locomotionMediator;

    private void Start()
    {
        if (xrOrigin != null)
        {
            Camera mainCam = xrOrigin.GetComponentInChildren<Camera>();
            if (mainCam != null) xrCamera = mainCam.transform;
            
            Transform offset = xrOrigin.transform.Find("Camera Offset");
            if (offset != null) cameraOffset = offset;

            locomotionMediator = xrOrigin.GetComponentInChildren<LocomotionMediator>();
        }
    }

    public void SitDown()
    {
        if (isSeated || xrOrigin == null || seatPoint == null) return;

        // 1. Collider aus
        if (carCollider != null) carCollider.enabled = false;
        if (driverSeatCollider != null) driverSeatCollider.enabled = false;

        // 2. LOC-MEDIATOR: Ausschalten + Force-Stop
        if (locomotionMediator != null)
        {
            locomotionMediator.enabled = false;
        }

        // 3. Absolute Positionierung (Der "Fix" für den Arsch der Welt)
        // Wir setzen das Rig direkt auf die Welt-Position des Sitzes
        xrOrigin.transform.position = seatPoint.position;
        xrOrigin.transform.rotation = seatPoint.rotation;

        // 4. Offset-Korrektur (Damit die Kamera exakt im Sitz ist)
        if (cameraOffset != null)
        {
            Vector3 worldCameraPos = xrCamera.position;
            Vector3 worldOriginPos = xrOrigin.transform.position;
            Vector3 delta = worldCameraPos - worldOriginPos;
            delta.y = 0; // Wichtig: Keine Höhenänderung durch die Kamera erzwingen
            
            xrOrigin.transform.position -= delta;
            targetOriginPos = xrOrigin.transform.position; // Ziel für LateUpdate
        }

        Physics.SyncTransforms();
        isSeated = true;
    }

    private void LateUpdate()
    {
        if (isSeated && cameraOffset != null)
        {
            // FESTNAGELN:
            xrOrigin.transform.position = targetOriginPos;

            // ANTI-MOVEMENT-TRICK:
            // Wenn der Mediator wieder aktiv wird (durch Fokus-Wechsel),
            // zwingen wir den Controller hier, jeden Frame auf 0 zu bleiben.
            characterController.Move(Vector3.zero);
        }
    }

    public void StandUp(Transform exitPoint)
    {
        if (!isSeated || exitPoint == null) return;

        isSeated = false;
        
        // Erst aktivieren, dann bewegen
        if (locomotionMediator != null) locomotionMediator.enabled = true;
        
        xrOrigin.transform.position = exitPoint.position;
        xrOrigin.transform.rotation = exitPoint.rotation;

        if (carCollider != null) carCollider.enabled = true;
        if (driverSeatCollider != null) driverSeatCollider.enabled = true;

        Physics.SyncTransforms();
    }
}