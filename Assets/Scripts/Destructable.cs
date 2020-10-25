using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    private ObjectPooler objectPooler;

    [SerializeField] private int health;

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    private void Update()
    {
        if(health <= 0)
        {
            GameObject destructableExplosion = objectPooler.SpawnFromPool("destructableExplosion", gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bullet" || collision.gameObject.tag == "enemyBullet")
        {
            TakeDamage();
        }
    }
    private void TakeDamage()
    {
        health--;
    }
}
