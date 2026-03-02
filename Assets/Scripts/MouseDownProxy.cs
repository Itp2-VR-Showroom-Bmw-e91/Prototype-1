using UnityEngine;

public class MouseDownProxy : MonoBehaviour 
{
    public static float maxDistance = 4f;

    public static void UpdateRaycast(Camera cam)
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            hit.collider.SendMessage(
                "OnMouseDown",
                SendMessageOptions.DontRequireReceiver
            );
        }
    }
}

