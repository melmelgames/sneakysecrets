using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform gunPoint;

    private ObjectPooler objectPooler;

    [SerializeField] private float bulletForce;
    [SerializeField] private float fireRate;
    [SerializeField] private int health;

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }
    private IEnumerator ShootAndWait(float timeBetweenShots)
    {
        while (true)
        {
            Debug.Log("shooting!");
            GameObject enemyBullet = objectPooler.SpawnFromPool("enemyBullet", gunPoint.position, gunPoint.rotation);
            Rigidbody2D rb2D = enemyBullet.GetComponent<Rigidbody2D>();
            rb2D.AddForce(gunPoint.up * bulletForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(timeBetweenShots);
        }
        
    }

    public void Shoot()
    {
        StartCoroutine(ShootAndWait(1/ fireRate));
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

}
