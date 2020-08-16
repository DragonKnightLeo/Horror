using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class InsEnemyController : MonoBehaviour
{
    #region Private Variables
    const int attackBoxNum = 4;
    bool walking;
    private float moveX;
    private float moveY;
    private bool canMove;
    private float lastPositionX;
    private float lastPositionY;
    private float enemyAttackPower;
    public Animator animator;
    Coroutine moveCoroutine;
    Vector2 targetPosition;
    private Transform attackPoint;
    private Rigidbody2D rb2D;
    [SerializeField] Transform targetTransform = null;
    [SerializeField] float nextAttackTime;
    [SerializeField] HomePosition homePosition;
    [SerializeField] Transform secondPos;
    [SerializeField] private float movementSpeed;
    Vector3 endPosition;
    Vector3 newPosition = new Vector3();
    #endregion

    #region Public Variables
<<<<<<< Updated upstream
=======
    public float idleRange;
>>>>>>> Stashed changes
    public float maxRange;
    public float minRange;
    public float followInterval;
    public float attackRange;
    public float attackRate = 2f;
    public LayerMask characterLayers;
    public Transform attackBoxes;
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        walking = true;
    }

    void Start()
    {
<<<<<<< Updated upstream
=======
        canMove = true;
>>>>>>> Stashed changes
        homePosition = FindObjectOfType<HomePosition>();
        targetTransform = FindObjectOfType<Player>().transform;
        enemyAttackPower = FindObjectOfType<Enemy>().attackPower;
        walking = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        MoveEnemy(rb2D, movementSpeed);
        IfAttacked();
    }



    void MoveEnemy(Rigidbody2D rb2D, float moveSpeed)
    {
        #region Calculate Difference between distances
        //get the difference between target position and enemy position
        float targetPos = Vector3.Distance(targetTransform.position, transform.position);
        float homePos = 0;
        //get the difference between homePosition and enemy position
        if (homePosition != null)
        {
            homePos = Vector3.Distance(homePosition.getPosition(), transform.position);
        }
        #endregion
<<<<<<< Updated upstream

        if (targetPos <= maxRange && Mathf.Abs(targetPos) >= minRange && canMove)
=======
        if (targetPos <= idleRange && targetPos > maxRange && Mathf.Abs(targetPos) >= minRange && canMove)
        {
            newPosition = Vector3.MoveTowards(rb2D.position, targetTransform.position, moveSpeed * Time.deltaTime);
            animateEnemy(targetTransform, newPosition, false);
        }
        if (targetPos <= maxRange  && Mathf.Abs(targetPos) >= minRange && canMove)
>>>>>>> Stashed changes
        {
            newPosition = Vector3.MoveTowards(rb2D.position, targetTransform.position, moveSpeed * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            animateEnemy(targetTransform, newPosition, true);
        }
        else if (targetPos >= maxRange)
        {
            if (homePosition != null)
            {
                newPosition = Vector3.MoveTowards(rb2D.position, homePosition.getPosition(), moveSpeed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                animateEnemy(homePosition.position, newPosition, true);
<<<<<<< Updated upstream
                if (homePos <= 2 && homePos < 0)
=======
                if (homePos <= 2)
>>>>>>> Stashed changes
                {
                    animateEnemy(homePosition.position, transform.position, false);
                }
            }
        }
        else
        {
            attack();
            animateEnemy(targetTransform, newPosition, false);
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        }
    }
    void animateEnemy(Transform targetTransform, Vector3 newPosition, bool canWalk)
    {
        moveX = (targetTransform.position.x - newPosition.x);
        moveY = (targetTransform.position.y - newPosition.y);

<<<<<<< Updated upstream
        if (canWalk)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("dirX", (targetTransform.position.x - newPosition.x));
            animator.SetFloat("dirY", (targetTransform.position.y - newPosition.y));
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
=======
            if (canWalk)
            {
                animator.SetBool("isWalking", true);
                animator.SetFloat("dirX", (targetTransform.position.x - newPosition.x));
                animator.SetFloat("dirY", (targetTransform.position.y - newPosition.y));
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
       
>>>>>>> Stashed changes
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().DamageTaken(enemyAttackPower);
        }
        if(other.gameObject.CompareTag("MonsterHomePos"))
        {
            homePosition = other.gameObject.GetComponent<HomePosition>();
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
        }
    }

    public void attack()
    {
        if (Time.time >= nextAttackTime && attackRange <= minRange)
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
    /*
    private void setAttackBoxes(Vector3 targetPos)
    {
        
        print("X axis" + moveX);
        print("Y axis" + moveY);
        for (int i = 0; i < attackBoxes.Length; i++)
        {
            attackBoxes[i].gameObject.SetActive(false);

            if (lastPositionY < 0)
            {
                attackPoint = attackBoxes[0];
            }
            else if (lastPositionY > 0)
            {
                attackPoint = attackBoxes[3];
            }
            else if (lastPositionX < 0)
            {
                attackPoint = attackBoxes[1];
            }
            else if (lastPositionX > 0)
            {
                attackPoint = attackBoxes[2];
            }
        }
    }
    */
    void hit()
    {
        if (animator.GetBool("isAttacking"))
        {
            PlayerMovement.playerMovementInstance.isHitAnimation(true);
        }
        else
        {
            PlayerMovement.playerMovementInstance.isHitAnimation(false);
        }
    }

    void IfAttacked()
    {
        if (GetComponent<Enemy>().isAttacked == true)
        {
            maxRange = 10;
        }
    }
}
