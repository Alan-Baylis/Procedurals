using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PlaneMaker : MonoBehaviour {

    public float cellSize = 1f;
    public int width = 24;
    public int height = 24;

	void Update ()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var meshBuilder = new MeshBuilder(6);

        //points for the plane
        Vector3[,] points = new Vector3[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                points[x, y] = new Vector3(cellSize * x, 0, cellSize * y);
            }
        }

        var submesh = 0;

        for (var x = 0; x < width - 1; x++)
        {
            for (var y = 0; y < height - 1; y++)
            {
                Vector3 bottomRight = points[x, y];
                Vector3 bottomLeft = points[x + 1, y];
                Vector3 topRight = points[x, y + 1];
                Vector3 topLeft = points[x + 1, y + 1];

                meshBuilder.BuildTriangle(bottomLeft, topRight, topLeft, submesh % 6);
                meshBuilder.BuildTriangle(bottomLeft, bottomRight, topRight, submesh % 6);
            }

            submesh++;
        }

        meshFilter.mesh = meshBuilder.CreateMesh();
	}
}
