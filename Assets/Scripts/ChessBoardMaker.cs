using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChessBoardMaker : MonoBehaviour
{
    public float cellSize = 1f;
    public int width = 24;
    public int height = 24;

    void Update()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var meshBuilder = new MeshBuilder(6);

        CreateCheckBoard(meshBuilder);

        meshFilter.mesh = meshBuilder.CreateMesh();
    }

    private void CreateCheckBoard(MeshBuilder meshBuilder)
    {
        //points for the plane
        Vector3[,] points = new Vector3[width, height];

        var halfCellSize = cellSize / 2;
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        for (var x = -halfWidth; x < halfWidth; x++)
        {
            for (var y = -halfHeight; y < halfHeight; y++)
            {
                points[x + halfWidth, y + halfHeight] = new Vector3(
                    cellSize * x + halfCellSize, 
                    0, 
                    cellSize * y + halfCellSize
                );
            }
        }

        var submesh = 0;

        for (var x = 0; x < width - 1; x++)
        {
            for (var y = 0; y < height - 1; y++)
            {
                submesh++;

                Vector3 bottomRight = points[x, y];
                Vector3 bottomLeft = points[x + 1, y];
                Vector3 topRight = points[x, y + 1];
                Vector3 topLeft = points[x + 1, y + 1];

                meshBuilder.BuildTriangle(bottomLeft, topRight, topLeft, submesh % 2);
                meshBuilder.BuildTriangle(bottomLeft, bottomRight, topRight, submesh % 2);
            }
        }
    }
}
