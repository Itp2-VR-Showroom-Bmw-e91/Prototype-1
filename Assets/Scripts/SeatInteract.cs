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

        // Event abfangen, wenn der Controller den Sitz "anklickt"
        simpleInteractable.selectEntered.AddListener(OnSelect);
    }

    void OnDestroy()
    {
        if (simpleInteractable != null)
            simpleInteractable.selectEntered.RemoveListener(OnSelect);
    }

    // NUR NOCH EINE Methode hier lassen:
    void OnSelect(SelectEnterEventArgs args)
    {
        Debug.Log("SITZ WURDE GEKLICKT!"); 
        if (vrSeat != null)
        {
            vrSeat.SitDown();
        }
    }
}