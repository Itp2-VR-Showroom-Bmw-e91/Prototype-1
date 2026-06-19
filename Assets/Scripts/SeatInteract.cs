using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SeatInteract : MonoBehaviour
{
    public VRSeat vrSeat;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        if (grab == null)
        {
            Debug.LogError("XRGrabInteractable fehlt am Sitz!", this);
            return;
        }

        grab.selectEntered.AddListener(OnSelect);
    }

    void OnDestroy()
    {
        if (grab != null)
            grab.selectEntered.RemoveListener(OnSelect);
    }

    void OnSelect(SelectEnterEventArgs args)
    {
        vrSeat.SitDown();
    }
}