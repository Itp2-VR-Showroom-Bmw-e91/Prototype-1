using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorInteractable : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float damping = 8f;

    private IXRSelectInteractor _currentInteractor;
    private float _currentAngle = 0f;
    private float _targetAngle = 0f;
    private Vector3 _initialForward;

    protected override void Awake()
    {
        base.Awake();
        _initialForward = doorPivot.forward;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _currentInteractor = args.interactorObject;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _currentInteractor = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase != XRInteractionUpdateOrder.UpdatePhase.Dynamic) return;
        if (_currentInteractor == null) return;

        Vector3 dir = _currentInteractor.transform.position - doorPivot.position;
        dir.y = 0f;
        dir.Normalize();

        float angle = Vector3.SignedAngle(_initialForward, dir, Vector3.up);
        _targetAngle = Mathf.Clamp(angle, minAngle, maxAngle);

        _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * damping);
        doorPivot.rotation = Quaternion.Euler(0f, _currentAngle, 0f) * Quaternion.LookRotation(_initialForward);
    }
}