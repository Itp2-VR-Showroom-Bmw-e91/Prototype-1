    using UnityEngine;

    public class VRSeat : MonoBehaviour
    {
        public Transform seatPoint;
        public GameObject xrOrigin;   // Dein XR Origin (XR Rig)
        public CharacterController characterController; 

        [Header("Collider zum Deaktivieren")]
        public Collider carCollider;       
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

            if (xrOrigin != null && seatPoint != null && characterController != null && xrCamera != null)
            {
                // 1. Joysticks blockieren (Kein Laufen mehr möglich, Umschauen bleibt aktiv!)
                if (moveProvider != null) moveProvider.enabled = false;
                if (turnProvider != null) turnProvider.enabled = false;

                // 2. Collider ausschalten (Verhindert das Rausfliegen)
                if (carCollider != null) carCollider.enabled = false;
                if (driverSeatCollider != null) driverSeatCollider.enabled = false;

                // 3. Den Kamera-Offset berechnen und das Rig so verschieben,
                // dass die AUGEN exakt auf dem seatPoint landen!
                Vector3 cameraOffset = xrCamera.position - xrOrigin.transform.position;
                xrOrigin.transform.position = seatPoint.position - cameraOffset;

                // Rotation anpassen
                xrOrigin.transform.rotation = seatPoint.rotation;

                Physics.SyncTransforms();

                isSeated = true;
                Debug.Log("Sitz-Modus erfolgreich! Augen sind auf dem Target fixiert.");
            }
        }

        public void StandUp(Transform exitPoint)
        {
            if (!isSeated || exitPoint == null) return;

            // Normaler Ausstieg
            xrOrigin.transform.position = exitPoint.position;
            xrOrigin.transform.rotation = exitPoint.rotation;
            Physics.SyncTransforms();

            // Collider und Joysticks wieder an
            if (carCollider != null) carCollider.enabled = true;
            if (driverSeatCollider != null) driverSeatCollider.enabled = true;
            if (moveProvider != null) moveProvider.enabled = true;
            if (turnProvider != null) turnProvider.enabled = true;

            isSeated = false;
        }
    }