using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorInteractable : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float closedAngle = 0f;
    [SerializeField] private float speed = 8f;

    // Beim inneren Griff hier den äußeren Griff reinziehen, beim äußeren leer lassen
    [SerializeField] private DoorInteractable mainController;

    private float _currentAngle = 0f;
    private float _targetAngle = 0f;
    private bool _isOpen = false;
    private Vector3 _initialForward;

    protected override void Awake()
    {
        base.Awake();
        if (mainController == null) // nur der Haupt-Griff initialisiert
        {
            _initialForward = doorPivot.forward;
            _currentAngle = closedAngle;
            _targetAngle = closedAngle;
        }
    }

    public void Toggle()
    {
        _isOpen = !_isOpen;
        _targetAngle = _isOpen ? openAngle : closedAngle;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (mainController != null)
            mainController.Toggle(); // innerer Griff ruft äußeren auf
        else
            Toggle(); // äußerer Griff steuert selbst
    }

    private void Update()
    {
        if (mainController != null) return; // innerer Griff macht kein Update

        _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime * speed);
        doorPivot.rotation = Quaternion.Euler(0f, _currentAngle, 0f) * Quaternion.LookRotation(_initialForward);
    }
}