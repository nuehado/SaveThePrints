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
    [SerializeField] private int damageDelt = 40;

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
            spriteRenderer.color = new Color(1f, 1f, 1f, alphaLevel);
        }

        if (effectTimer >= effectDuration)
        {
            GetComponent<BoxCollider>().enabled = false;
            alphaLevel = 1f;
            glueShmear.SetActive(false);
            effectTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollision>() != null)
        {
            isNew = false;
            StartCoroutine(EnemySlow(other.gameObject));
            other.GetComponent<EnemyCollision>().ManualDamage(damageDelt);
        }
    }

    private IEnumerator EnemySlow(GameObject enemy)
    {
        initialEnemyMovementSpeed = enemySpawner.moveSpeed;
        enemySpawnSpeed = enemySpawner.secondsBetweenSpawns * 1.2f; // remove multiplier to stop overlap
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
        glueShmear.SetActive(true);
        transform.position = initialPosition;
        isNew = true;
        effectTimer = 0f;
    }
}
