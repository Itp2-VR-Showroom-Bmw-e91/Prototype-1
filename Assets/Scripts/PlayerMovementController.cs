using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerMovementController : MonoBehaviour
{
    public DynamicMoveProvider moveProvider;

    private float originalSpeed;

    private void Start()
    {
        originalSpeed = moveProvider.moveSpeed;
        Debug.Log("Original speed: " + originalSpeed);
    }

    public void DisableMovement()
    {
        moveProvider.moveSpeed = 0f;
    }

    public void EnableMovement()
    {
        moveProvider.moveSpeed = originalSpeed;
    }
}