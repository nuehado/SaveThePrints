using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(10.0f, 20.0f)] private float gridSize;
    private TextMesh coordinateLabel;

    void Update()
    {
        Vector3 snapPosition;
        snapPosition.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPosition.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = new Vector3(snapPosition.x, 0.0f, snapPosition.z);

        coordinateLabel = GetComponentInChildren<TextMesh>();
        coordinateLabel.text = snapPosition.x/gridSize+ "," + snapPosition.z/gridSize;
    }
}
