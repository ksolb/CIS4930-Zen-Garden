using UnityEngine;

public class SandDeform : MonoBehaviour
{
    public Transform[] tines;
    public float radius = 0.025f;
    public float pushRate = 0.4f;
    public float maxDepth = 0.04f;
    public float surfaceTol = 0.005f;
    public int colliderRefresh = 8;

    Mesh mesh;
    MeshCollider mc;
    Vector3[] verts;
    int frame;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mc = GetComponent<MeshCollider>();
        verts = mesh.vertices;
    }

    void FixedUpdate()
    {
        bool dirty = false;
        float r2 = radius * radius;
        foreach (var t in tines)
        {
            Vector3 lp = transform.InverseTransformPoint(t.position);
            if (lp.y > surfaceTol) continue;
            for (int i = 0; i < verts.Length; i++)
            {
                float dx = verts[i].x - lp.x;
                float dz = verts[i].z - lp.z;
                float d2 = dx * dx + dz * dz;
                if (d2 < r2)
                {
                    float f = 1f - Mathf.Sqrt(d2) / radius;
                    verts[i].y -= pushRate * f * Time.fixedDeltaTime;
                    if (verts[i].y < -maxDepth) verts[i].y = -maxDepth;
                    dirty = true;
                }
            }
        }
        if (dirty)
        {
            mesh.vertices = verts;
            mesh.RecalculateNormals();
            if (++frame >= colliderRefresh)
            {
                mc.sharedMesh = null;
                mc.sharedMesh = mesh;
                frame = 0;
            }
        }
    }
}