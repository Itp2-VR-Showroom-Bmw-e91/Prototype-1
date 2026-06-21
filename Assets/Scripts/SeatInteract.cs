using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SeatInteract : MonoBehaviour
{
    public VRSeat vrSeat;
    private XRSimpleInteractable simpleInteractable;

    void Awake()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();

        if (simpleInteractable == null)
        {
            Debug.LogError("XRSimpleInteractable fehlt am Sitz! Bitte im Inspector hinzufügen.", this);
            return;
        }

        simpleInteractable.selectEntered.AddListener(OnSelect);
    }

    void OnDestroy()
    {
        if (simpleInteractable != null)
            simpleInteractable.selectEntered.RemoveListener(OnSelect);
    }

    void OnSelect(SelectEnterEventArgs args)
    {
        if (vrSeat == null || vrSeat.IsSeated)
            return;

        vrSeat.SitDown(args);
    }
}
