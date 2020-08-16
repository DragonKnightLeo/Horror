using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class CreepyNPC : MonoBehaviour
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
    [SerializeField] Transform secondPos;
    [SerializeField] private float movementSpeed;
    Vector3 endPosition;
    Vector3 newPosition = new Vector3();
    #endregion

    #region Public Variables
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
    }



    void MoveEnemy(Rigidbody2D rb2D, float moveSpeed)
    {
        #region Calculate Difference between distances
        //get the difference between target position and enemy position
        float targetPos = Vector3.Distance(targetTransform.position, transform.position);
        //get the difference between homePosition and enemy position
        #endregion

        if (targetPos <= maxRange && Mathf.Abs(targetPos) >= minRange && canMove)
        {
            newPosition = Vector3.MoveTowards(rb2D.position, targetTransform.position, moveSpeed * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            animateEnemy(targetTransform, newPosition, true);
        }
        else if (targetPos >= maxRange)
        {
            animateEnemy(targetTransform, newPosition, false);
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
            animator.SetBool("Idling", true);
            animator.SetFloat("dirX", (targetTransform.position.x - newPosition.x));
            animator.SetFloat("dirY", (targetTransform.position.y - newPosition.y));
        }
        else
        {
            animator.SetBool("Idling", false);
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

    public void attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (!animator.GetBool("Idling"))
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
}
