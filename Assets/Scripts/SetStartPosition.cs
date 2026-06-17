using UnityEngine;
using Unity.XR.CoreUtils;

public class SetStartPosition : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        // Teleportiert den XR Origin zum SpawnPoint
        GetComponent<XROrigin>().MoveCameraToWorldLocation(spawnPoint.position);
    }
}