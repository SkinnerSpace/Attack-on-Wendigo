using UnityEngine;

public class ArchitectVisualizer : MonoBehaviour
{
    public Architect architect;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (architect == null || !architect.debugMode) return;

        //FindCenter();

        float forestFullSize = (architect.explication.forestSize * architect.explication.scale);
        Vector3 forestCubeSize = new Vector3(forestFullSize, 20f, forestFullSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(architect.center, forestCubeSize);

        float townFullSize = (architect.explication.townSize * architect.explication.scale);
        Vector3 townCubeSize = new Vector3(townFullSize, 20f, townFullSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(architect.center, townCubeSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(architect.center, (forestFullSize / 2) * 0.8f);

        float mapFullSize = (architect.explication.size * architect.explication.scale);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(architect.center, mapFullSize / 2);
    }
#endif
}
