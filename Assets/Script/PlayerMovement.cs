using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public static PlayerMovement playerMovementInstance;
    [SerializeField] float movementSpeed = 0;
    [SerializeField] float runningSpeed = 0;
    [SerializeField] Transform[] attackBox;
     Rigidbody2D rb;
    Aim aim;
    int bulletNum = 7;
    float sprintTimer;
    float lastPositionX;
    float lastPositionY;
    bool shooting = false;
    bool running;
    bool allLightsOff;
    bool isMoving = false;
    bool isAttacked = false;

    Transform attackPoint;
    Rigidbody2D rb2D;
    public Animator animator;
    public int bulletCount = 7;
    public Vector2 movement = new Vector2();
    string lastMoveX = "lastMoveX", lastMoveY = "lastMoveY", isWalking = "isWalking",
                        xDir = "xDir", yDir = "yDir", attackVertical = "attackVertical",
                        attackHorizontal = "attackHorizontal", isShooting= "isShooting", isRunning = "isRunning",
                        isAttackReady = "isAttackReady";


    public Light2D[] lightBoxes;
    public Light2D characterLight;
    //public Light2D flashLight;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        aim = FindObjectOfType<Aim>();
    }
    void Start()
    {
        if (playerMovementInstance != null && playerMovementInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            playerMovementInstance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReloadSystem();
    }

    private void FixedUpdate()
    {
        UpdateMovingAnimation();
        runAnimation();
        setLightPosition();
        reduceStamina();
        recoverStamina();
        lightTimer();
        print(running);
    }


    public void moveCharacter(bool canWalk)
    {
        if (canWalk && !animator.GetBool("isShooting"))
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();

            if (!running)
            {
                rb2D.velocity = movement * movementSpeed;
            }
            else
            {
                rb2D.velocity = movement * runningSpeed;
            }
        }
        else
        {
            rb2D.velocity = Vector2.zero;
        }
    }

    void UpdateMovingAnimation()
    {
        //if horizontal is 0 and vertical is 0
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            //set player animation to not move
            animator.SetBool(isWalking, false);
            isMoving = false;
        }
        //if(horizontal is greater than 0, and vertical is greater than 0
        else
        {
            //set player animation to move
            if (!animator.GetBool(isShooting))
            {
                animator.SetBool(isWalking, true);
                isMoving = true;
            }
        }
        
            animator.SetFloat(xDir, movement.x);
            animator.SetFloat(yDir, movement.y);

        //shootAnimation();
            
            if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1
                || movement.x < -0.1 && movement.y < -0.1 || movement.x < -0.1 && movement.y > 0.1
                || movement.x > 0.1 && movement.y < -0.1 || movement.x > 0.1 && movement.y > 0.1)
            {
                lastPositionX = movement.x;
                lastPositionY = movement.y;
                animator.SetFloat(lastMoveX, movement.x);
                animator.SetFloat(lastMoveY, movement.y);
            }
    }
    private void setLightPosition()
    {
        if (!running || !allLightsOff)
        {
            //south
                if (animator.GetFloat(yDir) == -1)
                {
                    lightBoxes[0].gameObject.SetActive(false);
                    lightBoxes[1].gameObject.SetActive(false);
                    lightBoxes[2].gameObject.SetActive(true);
                    lightBoxes[3].gameObject.SetActive(false);
                    lightBoxes[4].gameObject.SetActive(false);
                    lightBoxes[5].gameObject.SetActive(false);
                    lightBoxes[6].gameObject.SetActive(false);
                    lightBoxes[7].gameObject.SetActive(false);
                    attackPoint = attackBox[2];

                }
                //north
                else if (animator.GetFloat(yDir) == 1)
                {
                    lightBoxes[0].gameObject.SetActive(true);
                    lightBoxes[1].gameObject.SetActive(false);
                    lightBoxes[2].gameObject.SetActive(false);
                    lightBoxes[3].gameObject.SetActive(false);
                    lightBoxes[4].gameObject.SetActive(false);
                    lightBoxes[5].gameObject.SetActive(false);
                    lightBoxes[6].gameObject.SetActive(false);
                    lightBoxes[7].gameObject.SetActive(false);
                    attackPoint = attackBox[0];

                }
                //east
                else if (animator.GetFloat(xDir) == 1)
                {
                    lightBoxes[0].gameObject.SetActive(false);
                    lightBoxes[1].gameObject.SetActive(false);
                    lightBoxes[2].gameObject.SetActive(false);
                    lightBoxes[3].gameObject.SetActive(true);
                    lightBoxes[4].gameObject.SetActive(false);
                    lightBoxes[5].gameObject.SetActive(false);
                    lightBoxes[6].gameObject.SetActive(false);
                    lightBoxes[7].gameObject.SetActive(false);
                    attackPoint = attackBox[3];
                }
                //west
                else if (animator.GetFloat(xDir) == -1)
                {
                    lightBoxes[0].gameObject.SetActive(false);
                    lightBoxes[1].gameObject.SetActive(true);
                    lightBoxes[2].gameObject.SetActive(false);
                    lightBoxes[3].gameObject.SetActive(false);
                    lightBoxes[4].gameObject.SetActive(false);
                    lightBoxes[5].gameObject.SetActive(false);
                    lightBoxes[6].gameObject.SetActive(false);
                    lightBoxes[7].gameObject.SetActive(false);
                    attackPoint = attackBox[1];

                }
                //southWest
                if (animator.GetFloat(xDir) < -0.1 && animator.GetFloat(yDir) < -0.1)
                {
                    lightBoxes[0].gameObject.SetActive(false);
                    lightBoxes[1].gameObject.SetActive(false);
                    lightBoxes[2].gameObject.SetActive(false);
                    lightBoxes[3].gameObject.SetActive(false);
                    lightBoxes[4].gameObject.SetActive(false);
                    lightBoxes[5].gameObject.SetActive(false);
                    lightBoxes[6].gameObject.SetActive(false);
                    lightBoxes[7].gameObject.SetActive(true);
                    attackPoint = attackBox[7];
                }
                //northWest
                if (animator.GetFloat(xDir) < -0.1 && animator.GetFloat(yDir) > 0.1)
                {
                    lightBoxes[0].gameObject.SetActive(false);
                    lightBoxes[1].gameObject.SetActive(false);
                    lightBoxes[2].gameObject.SetActive(false);
                    lightBoxes[3].gameObject.SetActive(false);
                    lightBoxes[4].gameObject.SetActive(true);
                    lightBoxes[5].gameObject.SetActive(false);
                    lightBoxes[6].gameObject.SetActive(false);
                    lightBoxes[7].gameObject.SetActive(false);
                    attackPoint = attackBox[4];

                }
                //SouthEast
                if (animator.GetFloat(xDir) > 0.1 && animator.GetFloat(yDir) < -0.1)
                {
                    lightBoxes[0].gameObject.SetActive(false);
                    lightBoxes[1].gameObject.SetActive(false);
                    lightBoxes[2].gameObject.SetActive(false);
                    lightBoxes[3].gameObject.SetActive(false);
                    lightBoxes[4].gameObject.SetActive(false);
                    lightBoxes[5].gameObject.SetActive(false);
                    lightBoxes[6].gameObject.SetActive(true);
                    lightBoxes[7].gameObject.SetActive(false);
                    attackPoint = attackBox[6];

                }
                //NorthEast
                if (animator.GetFloat(xDir) > 0.1 && animator.GetFloat(yDir) > 0.1)
                {
                    lightBoxes[0].gameObject.SetActive(false);
                    lightBoxes[1].gameObject.SetActive(false);
                    lightBoxes[2].gameObject.SetActive(false);
                    lightBoxes[3].gameObject.SetActive(false);
                    lightBoxes[4].gameObject.SetActive(false);
                    lightBoxes[5].gameObject.SetActive(true);
                    lightBoxes[6].gameObject.SetActive(false);
                    lightBoxes[7].gameObject.SetActive(false);
                    attackPoint = attackBox[5];
                }

        }
    }

    /*
    void damageEnemy()
    {
        //animator.SetBool(isAttacking, true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.gameObject.GetComponent<Enemy>().DamageTaken(Player.playerInstance.attackPower);
        }
        nextAttackTime = Time.time + 1f / attackRate;
    }
    */
    void runAnimation()
    {
        if (Player.playerInstance.currentStamPoints == 100 || Player.playerInstance.currentStamPoints > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && isMoving)
            {
                running = true;
                animator.SetBool(isRunning, true);
                turnFlashLightOff(true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                running = false;
                animator.SetBool(isRunning, false);
                
            }
            if (!isMoving)
            {
                animator.SetBool(isRunning, false);
                turnFlashLightOff(false);
                running = false;
            }
        }
        else
        {
            animator.SetBool(isRunning, false);
            turnFlashLightOff(false);
            running = false;
        }

    }

    public void shootAnimation(bool canShoot)
    {
        if (canShoot && isMoving == false && isAttacked == false && Time.time >= nextAttackTime)
        {
            if (bulletCount > 0 && bulletCount <= 7)
            {
                if (Input.GetButtonUp("Fire1"))
                {
                    turnFlashLightOff(true);
                    shooting = true;
                    aim.mouseLocation(true);
                    playGunSound(true);
                    nextAttackTime = Time.time + 1f / attackRate;
                    setBulletsOff(bulletCount -= 1, true);
                }
                else
                {
                    shooting = false;
                    aim.mouseLocation(false);
                    animator.SetBool(isWalking, true);
                }
            }
            else
            {
                aim.mouseLocation(false);
            }
        }
    }
    /*
    public void attack(bool canAttack)
    {
        if(canAttack && !animator.GetBool(isWalking))
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    rb2D.velocity = Vector2.zero;
                    animator.SetBool(isAttacking, true);
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.gameObject.GetComponent<Enemy>().DamageTaken(Player.playerInstance.attackPower);
                    }
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                else{animator.SetBool(isAttacking, false);}
            }
            else{animator.SetBool(isAttacking, false);}
        }
    }
    */

    public void turnFlashLightOff(bool onOff)
    {
        if (onOff)
        {
            for (int i = 0; i < lightBoxes.Length; i++)
            {
                lightBoxes[i].gameObject.SetActive(false);
                //flashLight.gameObject.SetActive(false);
            }
        }
        else
        {

        }
    }

    public void playGunSound(bool condition)
    {
        if(!isMoving && condition)
        {
            SoundManager.instance.gunShotSound(true);
        }
        else
        {
            SoundManager.instance.gunShotSound(false);
        }
    }

    void playFootSteps()
    {
        SoundManager.instance.footSteps();
    }

    void playRunningSteps()
    {
        SoundManager.instance.runningSteps();
    }

    public void isHitAnimation(bool state)
    {
        if (state)
        {
            animator.SetBool("isHit", true);
            isAttacked = true;
        }
        else
        {
            animator.SetBool("isHit", false);
            isAttacked = false;
        }
    }

    public void playTakingDamage()
    {
        SoundManager.instance.takingDamageSound();
    }

    public void playBreathingRun()
    {
        SoundManager.instance.BreathingRun();
    }

    public void setBulletsOff(int bulletNum, bool condition)
    {
        if (condition)
        {
            Healthbar.instance.bulletImage[bulletNum].gameObject.SetActive(false);
        }
    }

    void ReloadSystem()
    {
        int bulletDifference = bulletCount;
        int y;
        int bulletToReload = 0;
        if (bulletCount < 7 && bulletCount >= 0)
        {
            bulletToReload = bulletNum - bulletDifference;
            if (Inventory.instance.slots[2].buttonValue > 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {

                    if(Inventory.instance.slots[2].buttonValue >= bulletToReload)
                    {
                        bulletCount = bulletCount + bulletToReload;
                        Inventory.instance.slots[2].buttonValue = Inventory.instance.slots[2].buttonValue - bulletToReload;
                        Inventory.instance.slots[2].qtyText.text = Inventory.instance.slots[2].buttonValue.ToString();
                        print("Bullet in Gun If: " + bulletCount);
                    }
                    else
                    {
                        bulletCount = bulletCount + Inventory.instance.slots[2].buttonValue;
                        print("Bullet in Gun Else: " + bulletCount);
                        y = (Inventory.instance.slots[2].buttonValue - bulletToReload) *0;
                        Inventory.instance.slots[2].qtyText.text = y.ToString();
                        
                    }
                    for (int i = 0; i < Healthbar.instance.bulletImage.Length; i++)
                    {
                        Healthbar.instance.bulletImage[i].gameObject.SetActive(false);
                        if (!Healthbar.instance.bulletImage[i].gameObject.activeInHierarchy)
                        {
                            for (int x = 0; x < bulletCount; x++)
                            {
                                Healthbar.instance.bulletImage[x].gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }

    void reduceStamina()
    {
        if (Player.playerInstance.currentStamPoints < 0)
        {
            Player.playerInstance.currentStamPoints = Player.playerInstance.currentStamPoints * -1;
        }
        if (running)
        {
            if(Player.playerInstance.currentStamPoints <= 100 && Player.playerInstance.currentStamPoints > 0)
            Player.playerInstance.currentStamPoints -= Time.deltaTime * 5;

        }
    }
    

    void recoverStamina()
    {
        if(!running)
        {
            if (Player.playerInstance.currentStamPoints < 100 && Player.playerInstance.currentStamPoints >= 0)
                Player.playerInstance.currentStamPoints += Time.deltaTime * 0.5f;
        }
    }


    void lightTimer()
    {

        for (int i = 0; i < lightBoxes.Length; i++)
        {
            if (lightBoxes[i].gameObject.activeInHierarchy)
            {
                if(Player.playerInstance.lightTimer <= 600 || Player.playerInstance.lightTimer > 0)
                Player.playerInstance.lightTimer -= Time.deltaTime;
            }   
        }

        if (Player.playerInstance.lightTimer <= 0)
        {
            turnFlashLightOff(true);
            allLightsOff = true;
        }

        if(Player.playerInstance.lightTimer < 0)
        {
            Player.playerInstance.lightTimer = 0;
        }
    }

}
