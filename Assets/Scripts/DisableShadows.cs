using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class DisableShadows : MonoBehaviour
{
    // Klick im Inspector (Component-Gear) -> "Disable Shadows on Children"
    [ContextMenu("Disable Shadows on Children")]
    public void Apply()
    {
        Renderer[] rends = GetComponentsInChildren<Renderer>(true);
        int changed = 0;

        foreach (Renderer r in rends)
        {
            if (r == null) continue;
            r.shadowCastingMode = ShadowCastingMode.Off;
            r.receiveShadows = false;
            changed++;
        }

        Debug.Log($"DisableShadows: {changed} renderer(s) geändert auf '{name}'");
    }
}