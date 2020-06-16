using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    private Vector3 initialPosition;
    private EnemySpawner enemySpawner;
    private float initialEnemyMovementSpeed;
    private float enemySpawnSpeed;
    private float effectTimer = 0f;
    [SerializeField] private float effectDuration = 3f;
    [SerializeField] private GameObject glueShmear;
    public bool isNew = true;
    private SpriteRenderer spriteRenderer;
    private float alphaLevel = Mathf.Clamp(1f, 0f, 1f);

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        initialPosition = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isNew == false)
        {
            effectTimer += Time.deltaTime;
            alphaLevel = 1f - effectTimer/ effectDuration;
            //alphaLevel = alphaLevel - perSecondShmearFade;
            spriteRenderer.color = new Color(1f, 1f, 1f, alphaLevel);
        }

        if (effectTimer >= effectDuration)
        {
            GetComponent<BoxCollider>().enabled = false;
            alphaLevel = 1f;
            glueShmear.SetActive(false);
            //GetComponent<MeshRenderer>().enabled = false;
            effectTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollision>() != null)
        {
            isNew = false;
            StartCoroutine(EnemySlow(other.gameObject));
            /*currentDefenseSupportHealth -= 1;
            if (currentDefenseSupportHealth <= 0)
            {
                gameObject.GetComponent<DefenseSupportMover>().ResetSupportToStart();
                currentDefenseSupportHealth = initialSupportHealth;
            }*/
        }
    }

    private IEnumerator EnemySlow(GameObject enemy)
    {
        //private float slowStartTime = //needs a way to track time per benchy
        initialEnemyMovementSpeed = enemySpawner.moveSpeed;
        enemySpawnSpeed = enemySpawner.secondsBetweenSpawns;
        enemy.GetComponent<EnemyMovement>().movementSpeed = initialEnemyMovementSpeed * 0.5f;
        yield return new WaitForSeconds(enemySpawnSpeed);
        if (enemy != null)
        {
            enemy.GetComponent<EnemyMovement>().movementSpeed = initialEnemyMovementSpeed;
        }
    }

    public void ResetSlowEffect()
    {
        GetComponent<BoxCollider>().enabled = true;
        //GetComponent<MeshRenderer>().enabled = true;
        glueShmear.SetActive(true);
        transform.position = initialPosition;
        isNew = true;
        effectTimer = 0f;
    }
}
