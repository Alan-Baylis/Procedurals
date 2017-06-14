using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PathMaker : MonoBehaviour
{
    public Transform[] path;
    public PathShape pathShape;

    void Update ()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var meshBuilder = new MeshBuilder(6);

        var previousShape = TranslateShape(
            path[path.Length - 1].transform.position,
            (path[0].transform.position - path[path.Length - 1].transform.position).normalized,
            pathShape
        );

        for (var i = 0; i < path.Length; i++)
        {
            var nextShape = TranslateShape(
                path[i].transform.position,
                (path[(i + 1) % path.Length].transform.position - path[i].transform.position).normalized,
                pathShape
            );

            for (var j = 0; j < nextShape.Length - 1; j++)
            {
                meshBuilder.BuildTriangle(previousShape[j], nextShape[j], nextShape[j + 1], 0);
                meshBuilder.BuildTriangle(previousShape[j + 1], previousShape[j], nextShape[j + 1], 0);
            }

            previousShape = nextShape;
        }

        meshFilter.mesh = meshBuilder.CreateMesh();
	}

    private Vector3[] TranslateShape(Vector3 point, Vector3 forward, PathShape shape)
    {
        var translatedShape = new Vector3[pathShape.shape.Length];

        // create rotation using forward
        var forwardRotation = Quaternion.LookRotation(forward);

        for (var i = 0; i < pathShape.shape.Length; i++)
        {
            translatedShape[i] = (forwardRotation * pathShape.shape[i]) + point;
        }

        return translatedShape;
    }
}

[Serializable]
public class PathShape
{
    public Vector3[] shape = new Vector3[] { -Vector3.up, Vector3.up, -Vector3.up };
}
