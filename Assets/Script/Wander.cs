using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;
    public float directionChangeInterval;
    // We can reuse this script to make other creatures in the game wander about without chasing the player.
    // Use this flag to turn off the player-chasing behavior.
    public bool followPlayer;
    Coroutine moveCoroutine;
    CircleCollider2D circleCollider;
    Rigidbody2D rb2d;
    Animator animator;
    // only used when we have a target to pursue
    Transform targetTransform = null;
    Transform attackPoint;
    public LayerMask characterLayers;
    [SerializeField] Transform[] attackBoxes;
    [SerializeField]float attackRange;
    Vector3 endPosition;
    float currentAngle = 0;
    string dirX = "dirX", dirY = "dirY", lastMoveX = "lastMoveX", lastMoveY = "lastMoveY", isAttacking = "isAttacking";
    public float attackRate = 2f;
    public float nextAttackTime = 0f;


    public float lastPositionX;

    public float lastPositionY;


    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;

        circleCollider = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        //StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        // target line

        Debug.DrawLine(rb2d.position, endPosition, Color.red);
    }

    /*
    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));

            // wait directionChangeInterval seconds then change direction
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0, 360); // degrees

        // if currentAngle is greater than 360, loop so it starts at 0 again, keeping the value between 0 and 360
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
        print("End Position: " + endPosition);

    }
    */


    public IEnumerator ChasePlayer(Rigidbody2D rigidBodyToMove, float speed)
    {

        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            // When in pursuit, the targetTransform won't be null.
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }
            if (rigidBodyToMove != null)
            {

                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);

                animator.SetBool("isWalking", true);

                animator.SetFloat(dirX, targetTransform.position.x - newPosition.x);
                animator.SetFloat(dirY, targetTransform.position.y - newPosition.y);

                if (newPosition.x > 1 || newPosition.x > -1 || newPosition.y > 1 || newPosition.y < -1)
                {
                    lastPositionX = targetTransform.position.x - newPosition.x;
                    lastPositionY = targetTransform.position.y - newPosition.y;
                    animator.SetFloat(lastMoveX, targetTransform.position.x - newPosition.x);
                    animator.SetFloat(lastMoveY, targetTransform.position.y - newPosition.y);
                }


                for (int i = 0; i < attackBoxes.Length; i++)
                {

                    attackBoxes[i].gameObject.SetActive(false);


                    if (lastPositionY == -1)
                    {
                        attackPoint = attackBoxes[0];
                    }
                    else if (lastPositionY == 1)
                    {
                        attackPoint = attackBoxes[3];
                    }
                    else if (lastPositionX == 1)
                    {
                        attackPoint = attackBoxes[1];
                    }
                    else if (lastPositionX == -1)
                    {
                        attackPoint = attackBoxes[2];
                    }
                    else
                    {
                        attackPoint = attackBoxes[0];
                    }
                }

                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }

        // enemy has reached endPosition and waiting for new direction selection
        animator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;
            // Set this variable so the Move coroutine can use it to follow the player.
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // At this point, endPosition is now player object's transform, ie: will now move towards the player
            moveCoroutine = StartCoroutine(ChasePlayer(rb2d, currentSpeed));
            AttackPlayer();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            targetTransform = null; 
        }
    }


    void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {   
                animator.SetBool("isAttacking", true);

            Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, characterLayers);
            if (hitCharacters != null)
            {
                rb2d.velocity = Vector2.zero;
                animator.SetBool(isAttacking, true);
                foreach (Collider2D player in hitCharacters)
                {
                }
            }
            else
            {
                animator.SetBool(isAttacking, false);
            }
                nextAttackTime = Time.time + 1f / attackRate;
        }
        else{ animator.SetBool(isAttacking, false); }
    }

    void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    private void setAttackBoxes()
    {
        for (int i = 0; i < attackBoxes.Length; i++)
        {
            if (!attackBoxes[i].gameObject.activeInHierarchy)
            {
                attackBoxes[i].gameObject.SetActive(false);
            }

            if (lastPositionY == -1)
            {
                attackPoint = attackBoxes[0];
            }
            else if (lastPositionY == 1)
            {
                attackPoint = attackBoxes[3];
            }
            else if (lastPositionX == 1)
            {
                attackPoint = attackBoxes[1];
            }
            else if (lastPositionX == -1)
            {
                attackPoint = attackBoxes[2];
            }
            else
            {
                attackPoint = attackBoxes[0];
            }
        }
    }
}