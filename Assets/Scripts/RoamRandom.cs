using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamRandom : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float minRoamX;
    [SerializeField] private float maxRoamX;
    [SerializeField] private float minRoamY;
    [SerializeField] private float maxRoamY;
    private Rigidbody2D rb2D;
    private Animator animator;
    private GameObject player;
    private EnemyController enemyController;
    private bool playerOnSight;

    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float delta;
    [SerializeField] private bool shootingCoroutineStarted;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        targetPos = GenerateRandomTargetPosition();
        shootingCoroutineStarted = false;
    }

    private void Update()
    {
        if (ReachedTarget())
        {
            targetPos = GenerateRandomTargetPosition();
        }

        if (playerOnSight && !shootingCoroutineStarted)
        {
            enemyController.Shoot();
            shootingCoroutineStarted = true;
        }
        if (!playerOnSight && shootingCoroutineStarted)
        {
            enemyController.StopShooting();
            shootingCoroutineStarted = false;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(targetPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            playerOnSight = true;
        }

        if (collision.gameObject.tag == "wall")
        {
            targetPos = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            playerOnSight = false;
        }
    }

    private IEnumerator WaitBeforeChangingTargetPos()
    {
        yield return new WaitForSeconds(1f);
        targetPos = GenerateRandomTargetPosition();
    }

    private void MoveCharacter(Vector2 targetPos)
    {
        Vector2 moveDir;
        float angle;
        if (playerOnSight)
        {
            moveDir = targetPos - rb2D.position;
            animator.SetFloat("moveSpeed", moveDir.sqrMagnitude);
            rb2D.MovePosition(rb2D.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
            Vector2 lookDir = player.transform.position - gameObject.transform.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb2D.rotation = angle;
        }
        if (!playerOnSight)
        {
            moveDir = targetPos - rb2D.position;
            animator.SetFloat("moveSpeed", moveDir.sqrMagnitude);
            rb2D.MovePosition(rb2D.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
            angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            rb2D.rotation = angle;
        }
    }

    private Vector2 GenerateRandomTargetPosition()
    {
        float randomX = Random.Range(minRoamX, maxRoamX);
        float randomY = Random.Range(minRoamY, maxRoamY);
        Vector2 randomTargetPos;
        randomTargetPos.x = rb2D.position.x + randomX;
        randomTargetPos.y = rb2D.position.y + randomY;

        return randomTargetPos;
    }

    private bool ReachedTarget()
    {

        if (Vector2.Distance(targetPos, rb2D.position) < delta)
        {
            return true;
        }
        return false;
    }
}
