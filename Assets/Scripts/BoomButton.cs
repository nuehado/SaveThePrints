using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomButton : MonoBehaviour
{
    private bool boomPressed = false;
    private bool isBooming = false;
    private float secondsBetweenBooms = 0.1f;
    private AudioSource breakage;
    private EnemyMovement[] enemies;

    private void Awake()
    {
        breakage = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boomPressed && isBooming == false)
        {
            isBooming = true;
            StartCoroutine(BoomEnemies());
            boomPressed = false;
        }
        else if (boomPressed && isBooming == true)
        {
            isBooming = false;
            StopCoroutine(BoomEnemies());
            boomPressed = false;
            breakage.Stop();
        }
    }

    public void PressButton()
    {
        boomPressed = !boomPressed;
    }

    private IEnumerator BoomEnemies()
    {
        if(isBooming)
        {
            enemies = FindObjectsOfType<EnemyMovement>();
            if(enemies.Length > 0)
            {
                breakage.Play();
            }
            else 
            { 
                breakage.Stop(); 
            }
            foreach (EnemyMovement enemy in enemies)
            {
                var boomExplode = FragmentPool.Instance.Get(); //Instantiate(fracturedBenchy, enemy.transform.position, enemy.transform.rotation);
                boomExplode.transform.position = enemy.transform.position;
                boomExplode.transform.rotation = enemy.transform.rotation;
                boomExplode.SetActive(true);
            }

            yield return new WaitForSeconds(secondsBetweenBooms);

            StartCoroutine(BoomEnemies());
        }
        else
        {
            yield return new WaitForSeconds(0f);
        }
        
    }
}
