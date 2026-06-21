using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class VRSeat : MonoBehaviour
{
    public Transform seatPoint;
    public GameObject xrOrigin;
    public CharacterController characterController;
    [SerializeField] private PlayerMovementController playerMovementController;

    [Header("Collider zum Deaktivieren")]
    public Collider[] carCollider;
    public Collider driverSeatCollider;

    [Header("Locomotion Provider")]
    [SerializeField] private MonoBehaviour moveProvider;
    [SerializeField] private MonoBehaviour turnProvider;

    private bool isSeated = false;
    private Transform xrCamera;
    private XRSimpleInteractable seatInteractable;
    private Behaviour snapVolume;
    private Collider[] seatColliders;

    public bool IsSeated => isSeated;

    private void Awake()
    {
        seatInteractable = GetComponent<XRSimpleInteractable>();
        seatColliders = GetComponents<Collider>();

        if (driverSeatCollider == null && seatColliders.Length > 0)
            driverSeatCollider = seatColliders[0];

        foreach (MonoBehaviour behaviour in GetComponents<MonoBehaviour>())
        {
            if (behaviour != null && behaviour.GetType().Name == "SnapVolume")
            {
                snapVolume = behaviour;
                break;
            }
        }
    }

    private void Start()
    {
        if (xrOrigin != null)
        {
            Camera mainCam = xrOrigin.GetComponentInChildren<Camera>();

            if (mainCam != null)
            {
                xrCamera = mainCam.transform;
            }
        }
    }

    private void LateUpdate()
    {
        if (isSeated)
            SetSeatCollidersEnabled(false);
    }

    public void SitDown(SelectEnterEventArgs selectArgs = null)
    {
        if (isSeated) return;

        if (xrOrigin != null &&
            seatPoint != null &&
            characterController != null &&
            xrCamera != null)
        {
            if (moveProvider != null) moveProvider.enabled = false;
            if (turnProvider != null) turnProvider.enabled = false;

            if (carCollider != null)
            {
                foreach (Collider col in carCollider)
                {
                    if (col != null)
                        col.enabled = false;
                }
            }
            if (playerMovementController != null)
            {
                playerMovementController.DisableMovement();
                Debug.Log("movement deaktiviert");
            }

            Vector3 cameraOffset = xrCamera.position - xrOrigin.transform.position;
            xrOrigin.transform.position = seatPoint.position - cameraOffset;
            xrOrigin.transform.rotation = seatPoint.rotation;

            Physics.SyncTransforms();

            isSeated = true;
            StartCoroutine(FinalizeSeatInteraction());

            Debug.Log("Sitz-Modus erfolgreich! Augen sind auf dem Target fixiert.");
        }
    }

    private IEnumerator FinalizeSeatInteraction()
    {
        yield return null;

        // SnapVolume/Interactable zuerst aus, damit SelectExit den Collider nicht wieder einschaltet.
        SetSeatInteractionEnabled(false);
        ClearSeatInteractionState();
        SetSeatCollidersEnabled(false);

        yield return null;

        ClearSeatInteractionState();
        SetSeatCollidersEnabled(false);
    }

    private void ClearSeatInteractionState()
    {
        if (seatInteractable == null)
            return;

        XRInteractionManager manager = seatInteractable.interactionManager;
        if (manager == null)
            return;

        var selecting = new List<IXRSelectInteractor>(seatInteractable.interactorsSelecting);
        foreach (IXRSelectInteractor interactor in selecting)
            manager.SelectExit(interactor, seatInteractable);

        var hovering = new List<IXRHoverInteractor>(seatInteractable.interactorsHovering);
        foreach (IXRHoverInteractor interactor in hovering)
            manager.HoverExit(interactor, seatInteractable);
    }

    private void SetSeatInteractionEnabled(bool enabled)
    {
        if (seatInteractable != null)
            seatInteractable.enabled = enabled;

        if (snapVolume != null)
            snapVolume.enabled = enabled;
    }

    private void SetSeatCollidersEnabled(bool enabled)
    {
        if (seatColliders == null)
            return;

        foreach (Collider col in seatColliders)
        {
            if (col != null)
                col.enabled = enabled;
        }
    }

    public void StandUp(Transform exitPoint)
    {
        if (!isSeated || exitPoint == null) return;

        xrOrigin.transform.position = exitPoint.position;
        xrOrigin.transform.rotation = exitPoint.rotation;

        Physics.SyncTransforms();

        if (carCollider != null)
        {
            foreach (Collider col in carCollider)
            {
                if (col != null)
                    col.enabled = true;
            }
        }

        if (moveProvider != null) moveProvider.enabled = true;
        if (turnProvider != null) turnProvider.enabled = true;

        if (playerMovementController != null)
        {
            playerMovementController.EnableMovement();
            Debug.Log("movement aktiviert");
        }

        isSeated = false;
        SetSeatInteractionEnabled(true);

        foreach (DoorInteractable door in FindObjectsByType<DoorInteractable>(FindObjectsSortMode.None))
            door.ApplySeatColliderState();
    }
}
