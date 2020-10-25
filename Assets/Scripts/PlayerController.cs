using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    private Rigidbody2D playerRB2D;
    private Animator playerAnimator;
    private Vector2 movementDir;
    private Vector2 mousePos;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        playerRB2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
}
