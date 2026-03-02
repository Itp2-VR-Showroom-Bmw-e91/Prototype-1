using UnityEngine;

public class MouseDownProxyRunner : MonoBehaviour
{
 
    void Update()
    {
        MouseDownProxy.UpdateRaycast(Camera.main);
    }
}
