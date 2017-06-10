using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class LandscapeMaker : MonoBehaviour
{
    public float cellSize = 1f;
    public int width = 24;
    public int height = 24;
    public float bumpyness = 5;
    public float bumpHeight = 5;

    void Update()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var meshBuilder = new MeshBuilder(6);

        //points for the plane
        Vector3[,] points = new Vector3[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                points[x, y] = new Vector3(
                    cellSize * x, 
                    Mathf.PerlinNoise(
                        (x + Time.time + transform.position.x) * bumpyness * 0.1f, 
                        (y + Time.time + transform.position.z) * bumpyness * 0.1f
                    ) * bumpHeight, 
                    cellSize * y);
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

                meshBuilder.BuildTriangle(bottomLeft, topRight, topLeft, submesh % 3);
                meshBuilder.BuildTriangle(bottomLeft, bottomRight, topRight, submesh % 3);
            }

            submesh++;
        }

        meshFilter.mesh = meshBuilder.CreateMesh();
    }
}
