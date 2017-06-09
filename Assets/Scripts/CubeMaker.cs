using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CubeMaker : MonoBehaviour {

    public Vector3 size = Vector3.one;
	
	void Update ()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var meshBuilder = new MeshBuilder(6);

        var cubeSize = size * 0.5f;

        // Top
        var t0 = new Vector3(cubeSize.x, cubeSize.y, -cubeSize.z);
        var t1 = new Vector3(-cubeSize.x, cubeSize.y, -cubeSize.z);
        var t2 = new Vector3(-cubeSize.x, cubeSize.y, cubeSize.z);
        var t3 = new Vector3(cubeSize.x, cubeSize.y, cubeSize.z);

        // Bottom
        var b0 = new Vector3(cubeSize.x, -cubeSize.y, -cubeSize.z);
        var b1 = new Vector3(-cubeSize.x, -cubeSize.y, - cubeSize.z);
        var b2 = new Vector3(-cubeSize.x, -cubeSize.y, cubeSize.z);
        var b3 = new Vector3(cubeSize.x, -cubeSize.y, cubeSize.z);

        // Top square
        meshBuilder.BuildTriangle(t0, t1, t2, 0);
        meshBuilder.BuildTriangle(t0, t2, t3, 0);

        // Bottom square
        meshBuilder.BuildTriangle(b2, b1, b0, 1);
        meshBuilder.BuildTriangle(b3, b2, b0, 1);

        // Faces
        meshBuilder.BuildTriangle(b0, t1, t0, 2);
        meshBuilder.BuildTriangle(b0, b1, t1, 2);

        meshBuilder.BuildTriangle(b1, t2, t1, 3);
        meshBuilder.BuildTriangle(b1, b2, t2, 3);

        meshBuilder.BuildTriangle(b2, t3, t2, 4);
        meshBuilder.BuildTriangle(b2, b3, t3, 4);

        meshBuilder.BuildTriangle(b3, t0, t3, 5);
        meshBuilder.BuildTriangle(b3, b0, t0, 5);

        meshFilter.mesh = meshBuilder.CreateMesh();
    }
}
