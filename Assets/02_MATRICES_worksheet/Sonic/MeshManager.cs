using UnityEngine;

public class MeshManager : MonoBehaviour
{
    private MeshFilter meshFilter;

    [HideInInspector]
    public Mesh originalMesh, clonedMesh;

    public Vector3[] vertices { get; private set; }
    public int[] triangles { get; private set; }

    //save the values of shared mesh to the properties to a copy of the mesh
    //this is so that if the mesh of is overwrite, it could be reset easily using the script
    //shared mesh: a mesh used by more than one object + preferably used for reading data as it's difficult to undo changes
    //no need for a deepcopy of the original as the values of the clone properties will only be updated once when the script is loaded
    void Awake()
    {
        //assign the meshfilter component to the meshFilter var
        meshFilter = GetComponent<MeshFilter>();
        //original mesh is referencing the sharedMesh 
        originalMesh = meshFilter.sharedMesh;
        //new mesh instance created to copy original
        clonedMesh = new Mesh();

        //clone will copy all the original values
        clonedMesh.name = "clone";
        clonedMesh.vertices = originalMesh.vertices;
        clonedMesh.triangles = originalMesh.triangles;
        clonedMesh.normals = originalMesh.normals;
        clonedMesh.uv = originalMesh.uv;

        //assign cloned mesh(shared mesh) to the mesh of the object 
        meshFilter.mesh = clonedMesh;
        //store the cloned mesh vertices and triangles 
        vertices = clonedMesh.vertices;
        triangles = clonedMesh.triangles;
    }
}
