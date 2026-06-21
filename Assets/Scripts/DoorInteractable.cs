using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorInteractable : XRBaseInteractable
{
    [Header("Door")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float closedAngle = 0f;
    [SerializeField] private float speed = 8f;

    [Header("Seat")]
    [SerializeField] private Transform exitpoint;
    [SerializeField] private Collider seatCollider;

    [Header("Handles")]
    [SerializeField] private DoorInteractable mainController;

    private VRSeat vrs;

    private float _currentAngle;
    private float _targetAngle;
    private bool _isOpen;
    private Vector3 _initialForward;

    public bool IsOpen => _isOpen;

    protected override void Awake()
    {
        base.Awake();

        if (mainController == null)
        {
            _initialForward = doorPivot.forward;
            _currentAngle = closedAngle;
            _targetAngle = closedAngle;

            if (seatCollider != null)
            {
                vrs = seatCollider.GetComponent<VRSeat>();

                if (vrs == null)
                    vrs = seatCollider.GetComponentInParent<VRSeat>();

                seatCollider.enabled = false;
            }
        }
    }

    public void Toggle()
    {
        _isOpen = !_isOpen;
        _targetAngle = _isOpen ? openAngle : closedAngle;

        if (_isOpen &&
            vrs != null &&
            vrs.IsSeated &&
            exitpoint != null)
        {
            vrs.StandUp(exitpoint);
        }

        UpdateSeatColliderState();
    }

    public void ApplySeatColliderState()
    {
        UpdateSeatColliderState();
    }

    private void UpdateSeatColliderState()
    {
        if (seatCollider == null)
            return;

        // Hinsetzen nur wenn Tür offen und niemand sitzt
        seatCollider.enabled =
            _isOpen &&
            (vrs == null || !vrs.IsSeated);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (mainController != null)
            mainController.Toggle();
        else
            Toggle();
    }

    private void Update()
    {
        if (mainController != null)
            return;

        _currentAngle = Mathf.Lerp(
            _currentAngle,
            _targetAngle,
            Time.deltaTime * speed);

        doorPivot.rotation =
            Quaternion.Euler(0f, _currentAngle, 0f) *
            Quaternion.LookRotation(_initialForward);
    }
}