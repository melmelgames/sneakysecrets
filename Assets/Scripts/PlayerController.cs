using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Transform gunPoint;

    private Rigidbody2D playerRB2D;
    private Animator playerAnimator;
    private ObjectPooler objectPooler;
    private Vector2 movementDir;
    private Vector2 mousePos;
    private bool isNearComputer;
    private GameObject computer;

    [SerializeField] private int score;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float bulletForce;

    private void Awake()
    {
        playerRB2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        score = 0;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Shoot();
        StealDocuments();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // STEAL DOCUMENTS WHEN NEAR A COMPUTER WITH RIGHT MOUSE BUTTON
        if(collision.gameObject.tag == "computer")
        {
            isNearComputer = true;
            computer = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "computer")
        {
            isNearComputer = false;
            computer = null;
        }
    }

    private void GetInput()
    {
        movementDir.x = Input.GetAxisRaw("Horizontal");
        movementDir.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MovePlayer()
    {

        playerRB2D.MovePosition(playerRB2D.position + movementDir.normalized * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - playerRB2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRB2D.rotation = angle;

        playerAnimator.SetFloat("movementInput", movementDir.magnitude);

    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet =  objectPooler.SpawnFromPool("playerBullet", gunPoint.position, gunPoint.rotation);
            Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
            rb2D.AddForce(gunPoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    private void StealDocuments()
    {
        if (isNearComputer)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                GameObject documents = computer.GetComponent<Computer>().StealDocuments();
                AddScore(documents.GetComponent<Document>().GetPoints());
            }
        }
        
    }

    private void AddScore(int points)
    {
        score += points;
    }

}
