using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class point : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private List<GameObject> markers = new List<GameObject>();
    public GameObject markerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        GameObject PreviousMarker = null;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            if (IsPositionValid(worldPos))
            {
                GameObject marker = Instantiate(markerPrefab, worldPos, Quaternion.identity);
                marker.name = $"Vertex_{i}";
                marker.transform.SetParent(transform);
                markers.Add(marker);
                if (PreviousMarker != null && i != vertices.Length - 1)
                {
                    marker.GetComponent<HingeJoint2D>().connectedBody = PreviousMarker.GetComponent<Rigidbody2D>();
                    marker.GetComponent<HingeJoint2D>().useLimits = true;
                    JointAngleLimits2D limits = new JointAngleLimits2D();
                    limits.min = 0;
                    limits.max = 15;
                    marker.GetComponent<HingeJoint2D>().limits = limits;
                }
                PreviousMarker = marker;
            }
        }
        markers[0].GetComponent<HingeJoint2D>().connectedBody = markers[markers.Count - 2].GetComponent<Rigidbody2D>();
        markers[0].GetComponent<HingeJoint2D>().useLimits = true;
        JointAngleLimits2D lastLimits = new JointAngleLimits2D();
        lastLimits.min = 0;
        lastLimits.max = 15;
        markers[0].GetComponent<HingeJoint2D>().limits = lastLimits;
        
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject marker in markers)
        {
            if (Vector3.Distance(marker.transform.position, position) < 0.2f)
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        updateVerices();
        RebuildMesh();
        
    }

    void updateVerices()
    {
        if (markers.Count != mesh.vertexCount) return;

        Vector3[] newVertices = new Vector3[mesh.vertexCount];

        for (int i = 0; i < markers.Count; i++)
        {
            newVertices[i] = transform.InverseTransformPoint(markers[i].transform.position);
        }

        mesh.vertices = newVertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    
    void RebuildMesh()
    {
        if (markers.Count < 3) return; // ✅ Prevents invalid meshes

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        // ✅ Create a new mesh instance
        Mesh newMesh = new Mesh();
        Vector3[] newVertices = new Vector3[markers.Count];

        for (int i = 0; i < markers.Count; i++)
        {
            newVertices[i] = transform.InverseTransformPoint(markers[i].transform.position);
        }

        // ✅ Dynamically generate triangles (assuming a simple shape)
        int[] newTriangles = GenerateTriangles(newVertices.Length);

        newMesh.vertices = newVertices;
        newMesh.triangles = newTriangles;
        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();

        // ✅ Assign the new mesh to the MeshFilter
        meshFilter.mesh = newMesh;

        // ✅ Ensure MeshCollider exists before setting it
        if (!TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        meshCollider.sharedMesh = newMesh;

        // ✅ Destroy the old mesh to prevent memory leaks
        Destroy(mesh);
        mesh = newMesh;
    }

    int[] GenerateTriangles(int vertexCount)
    {
        // 🔥 Generates triangles for a convex polygon (works if markers are arranged in order)
        List<int> triangles = new List<int>();

        for (int i = 1; i < vertexCount - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        return triangles.ToArray();
    }



}
