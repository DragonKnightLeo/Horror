using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
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
    [SerializeField]Transform targetTransform = null;
    [SerializeField] float nextAttackTime;
    [SerializeField] Transform homePosition;
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
    public  float maxRange;
    public  float minRange;
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
<<<<<<< Updated upstream
=======
        canMove = true;
>>>>>>> Stashed changes
        walking = true;
    }

    void Start()
    {
        targetTransform = FindObjectOfType<Player>().transform;
        enemyAttackPower = FindObjectOfType<Enemy>().attackPower;
        walking = true;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
    
=======
        IfAttacked();
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
            if (targetPos <= maxRange && Mathf.Abs(targetPos) >= minRange && canMove)
=======
        if (targetPos <= idleRange && targetPos > maxRange && Mathf.Abs(targetPos) >= minRange && canMove)
        {
            newPosition = Vector3.MoveTowards(rb2D.position, targetTransform.position, moveSpeed * Time.deltaTime);
            animateEnemy(targetTransform, newPosition, false);
        }
        else if (targetPos <= maxRange && Mathf.Abs(targetPos) >= minRange && canMove)
>>>>>>> Stashed changes
            {
                newPosition = Vector3.MoveTowards(rb2D.position, targetTransform.position, moveSpeed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                animateEnemy(targetTransform, newPosition, true);    
            }
            else if (targetPos >= maxRange)
            {

                newPosition = Vector3.MoveTowards(rb2D.position, homePosition.position, moveSpeed * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                animateEnemy(homePosition, newPosition, true);
                if (homePos <= 1)
                {
<<<<<<< Updated upstream
                    maxRange = 12;
=======
                    maxRange = 5;
>>>>>>> Stashed changes
                    animateEnemy(homePosition, transform.position, false);
                }
            }
            else
            {
                attack();
                animateEnemy(targetTransform, newPosition, false);
                
            }        
    }
    void animateEnemy(Transform targetTransform, Vector3 newPosition, bool canWalk)
    {
        moveX = (targetTransform.position.x - newPosition.x);
        moveY = (targetTransform.position.y - newPosition.y);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().DamageTaken(enemyAttackPower);
        }
    }

<<<<<<< Updated upstream
=======
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMovement.playerMovementInstance.isHitAnimation(false);
        }
    }

>>>>>>> Stashed changes
    public void attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if(!animator.GetBool("isWalking"))
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
        if(animator.GetBool("isAttacking"))
        {
            PlayerMovement.playerMovementInstance.isHitAnimation(true);
        }
        else
        {
            PlayerMovement.playerMovementInstance.isHitAnimation(false);
        }
    }
<<<<<<< Updated upstream
=======
    void IfAttacked()
    {
        if (GetComponent<Enemy>().isAttacked == true)
        {
            maxRange = 20;
        }
    }

>>>>>>> Stashed changes
}