using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private Vector2 targetPos;
    private Rigidbody2D rb2D;
    private Animator animator;
    private GameObject player;
    private EnemyController enemyController;

    [SerializeField] private Transform startingPos;
    [SerializeField] private Transform endPos;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float delta;
    [SerializeField] private bool playerOnSight;
    [SerializeField] private bool shootingCoroutineStarted;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        shootingCoroutineStarted = false;

        startingPos = pointA;
        endPos = pointB;
        gameObject.transform.position = startingPos.position;
        targetPos = endPos.position;
    }

    private void Start()
    {
        enemyController = gameObject.GetComponent<EnemyController>();
    }
    private void Update()
    {
        if (ReachedTarget())
        {
            Transform endPosHolder = endPos;
            Transform startPosHolder = startingPos;
            endPos = startPosHolder;
            startingPos = endPosHolder;
            targetPos = endPos.position;
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
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            playerOnSight = true;
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

    private bool ReachedTarget()
    {

        if (Vector2.Distance(targetPos, rb2D.position) < delta)
        {
            return true;
        }
        return false;
    }
}
