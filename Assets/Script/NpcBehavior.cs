using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    #region Private Variables
    const int attackBoxNum = 4;
    bool walking;
    private float moveX;
    private float moveY;
    private bool canMove;
    private float lastPositionX;
    private float lastPositionY;
    private float npcAttackPower;
    public Animator animator;
    Coroutine moveCoroutine;
    Vector2 targetPosition;
    private Transform attackPoint;
    [SerializeField]private Rigidbody2D rb2D;
    [SerializeField] Transform targetTransform = null;
    [SerializeField] float nextAttackTime;
    [SerializeField] Transform homePosition;
    [SerializeField] private float movementSpeed;
    [SerializeField] GameObject francine;
    Vector3 endPosition;
    Vector3 newPosition = new Vector3();
    #endregion

    #region Public Variables
    public bool reachedHome;
    public float maxRange;
    public float minRange;
    public float followInterval;
    public float attackRange;
    public float attackRate = 2f;
    public LayerMask characterLayers;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        npcAttackPower = FindObjectOfType<NonCombatNpc>().attackPower;
        targetTransform = homePosition;
        walking = true;
        canMove = false;
        reachedHome = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<DialogActivator>().conversationEnded)
        {
            canMove = true;
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy(rb2D, movementSpeed);
    }



    void MoveEnemy(Rigidbody2D rb2D, float moveSpeed)
    {
        #region Calculate Difference between distances
        //get the difference between target position and enemy position
        float targetPos = Vector3.Distance(targetTransform.position, transform.position);
        //get the difference between homePosition and enemy position
        float homePos = Vector3.Distance(homePosition.position, transform.position);
        #endregion
        if (canMove)
        {
            if (targetPos != 0)
            {
                newPosition = Vector3.MoveTowards(rb2D.position, targetTransform.position, moveSpeed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                animateNPC(targetTransform, newPosition, true);
            }
            else
            {
                Destroy(francine.gameObject);
                reachedHome = true;
            }
        }
    }
    void animateNPC(Transform targetTransform, Vector3 newPosition, bool canWalk)
    {
        moveX = (targetTransform.position.x - newPosition.x);
        moveY = (targetTransform.position.y - newPosition.y);

        if (canWalk)
        {
            animator.SetBool("isRunning", true);
            animator.SetFloat("dirX", (targetTransform.position.x - newPosition.x));
            animator.SetFloat("dirY", (targetTransform.position.y - newPosition.y));
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1
        || moveX < -0.1 && moveY < -0.1 || moveX < -0.1 && moveY > 0.1
        || moveX > 0.1 && moveY < -0.1 || moveX > 0.1 && moveY > 0.1)
        {
            lastPositionX = moveX;
            lastPositionY = moveY;
            animator.SetFloat("lastMoveX", moveX);
            animator.SetFloat("lastMoveY", moveY);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    */

    public void attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (!animator.GetBool("isWalking"))
            {
                canMove = false;
                rb2D.velocity = Vector2.zero;
                animator.SetBool("isAttacking", true);
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else
            {
                canMove = true;
                animator.SetBool("isAttacking", false);
            }
        }
        else
        {
            canMove = true;
            animator.SetBool("isAttacking", false);
        }
    }
}
