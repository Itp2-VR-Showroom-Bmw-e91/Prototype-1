using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoodOpenVR : MonoBehaviour
{
    public Transform hood;
    public float openAngle = 70f;
    public float speed = 2f;

    private XRGrabInteractable grab;

    private Quaternion closedRot;
    private Quaternion openRot;

    private bool isOpen = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();

        closedRot = hood.localRotation;
        openRot = closedRot * Quaternion.Euler(-openAngle, 0, 0);

        grab.selectEntered.AddListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Toggle: jedes Grab wechselt Zustand
        isOpen = !isOpen;
    }

    void Update()
    {
        Quaternion targetRot = isOpen ? openRot : closedRot;

        hood.localRotation = Quaternion.Slerp(
            hood.localRotation,
            targetRot,
            Time.deltaTime * speed
        );
    }
}