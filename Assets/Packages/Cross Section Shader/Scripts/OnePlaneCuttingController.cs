using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class OnePlaneCuttingController : MonoBehaviour {

    [SerializeField] private GameObject plane;
    Material mat;
    private Vector3 normal;
    private Vector3 position;
    private Renderer rend;

    private Vector3 finishedSpawningHeight;
    private float spawningTimer = 0;
    private float enemyHeight = 7.3f;
    private EnemySpawner enemySpawner;
    private EnemyMovement enemyMover;


    // Use this for initialization
    void Start () {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemyMover = FindObjectOfType<EnemyMovement>();
        rend = GetComponent<Renderer>();
        normal = plane.transform.TransformVector(new Vector3(0,0,-1));
        position = plane.transform.position;
        finishedSpawningHeight = new Vector3(plane.transform.position.x, plane.transform.position.y + enemyHeight, plane.transform.position.z);
    }
    void Update()
    {
        if (spawningTimer <= enemySpawner.secondsBetweenSpawns * 0.833f)
        {
            spawningTimer += Time.deltaTime;
            plane.transform.position = Vector3.MoveTowards(plane.transform.position, finishedSpawningHeight, enemyHeight / enemySpawner.secondsBetweenSpawns * Time.deltaTime);
            UpdateShaderProperties();
        }
        else
        {
            plane.transform.position = finishedSpawningHeight;
            UpdateShaderProperties();
            enemyMover.isMoving = true;
        }
    }

    private void UpdateShaderProperties()
    {        
        normal = plane.transform.TransformVector(new Vector3(0, 0, -1));
        position = plane.transform.position;
        rend.material.SetVector("_PlaneNormal", normal);
        rend.material.SetVector("_PlanePosition", position);
    }
}
