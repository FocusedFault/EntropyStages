using UnityEngine;

namespace EntropyStages
{
    [RequireComponent(typeof(MeshFilter))]
    public class ReverseNormals : MonoBehaviour
    {
        private void Start()
        {
            MeshFilter component = this.GetComponent(typeof(MeshFilter)) as MeshFilter;
            if ((Object)component != (Object)null)
            {
                Mesh mesh = component.mesh;
                Vector3[] normals = mesh.normals;
                for (int index = 0; index < normals.Length; ++index)
                    normals[index] = -normals[index];
                mesh.normals = normals;
                for (int submesh = 0; submesh < mesh.subMeshCount; ++submesh)
                {
                    int[] triangles = mesh.GetTriangles(submesh);
                    for (int index = 0; index < triangles.Length; index += 3)
                    {
                        int num = triangles[index];
                        triangles[index] = triangles[index + 1];
                        triangles[index + 1] = num;
                    }
                    mesh.SetTriangles(triangles, submesh);
                }
            }
            this.GetComponent<MeshCollider>().sharedMesh = component.mesh;
        }
    }
}
