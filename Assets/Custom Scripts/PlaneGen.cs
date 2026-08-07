using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class PlaneGen : MonoBehaviour
{
    public int res = 150;
    public float size = 2f;

    void Awake()
    {
        var m = new Mesh();
        m.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        int n = res + 1;
        var verts = new Vector3[n * n];
        var uvs = new Vector2[n * n];
        for (int z = 0; z < n; z++)
        {
            for (int x = 0; x < n; x++)
            {
                float u = (float)x / res;
                float v = (float)z / res;
                verts[z * n + x] = new Vector3((u - 0.5f) * size, 0, (v - 0.5f) * size);
                uvs[z * n + x] = new Vector2(u, v);
            }
        }
        var tris = new int[res * res * 6];
        int t = 0;
        for (int z = 0; z < res; z++)
        {
            for (int x = 0; x < res; x++)
            {
                int i = z * n + x;
                tris[t++] = i;
                tris[t++] = i + n;
                tris[t++] = i + n + 1;
                tris[t++] = i;
                tris[t++] = i + n + 1;
                tris[t++] = i + 1;
            }
        }
        m.vertices = verts;
        m.uv = uvs;
        m.triangles = tris;
        m.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = m;
        GetComponent<MeshCollider>().sharedMesh = m;
    }
}