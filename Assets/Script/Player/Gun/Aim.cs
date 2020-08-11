using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Aim : MonoBehaviour
{
    public int poolSize;
    Animator animator;
    public Ammo ammoPrefab;
    public GameObject playerPos;
    List<Ammo> ammoPool;
    Ammo[] ammoArray = new Ammo[7];

    public float weaponVelocity;
    bool isFiring;

    private void OnEnable()
    {
        
    }   


    private void OnDisable()
    {
        
    }


    void Awake()
    {
        // object pool
        if (ammoPool == null)
        {
            ammoPool = new List<Ammo>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            Ammo ammoObject = Instantiate(ammoPrefab);
            ammoObject.gameObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void mouseLocation(bool canShoot)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 posToChange = (mousePosition - playerPos.transform.position);

        posToChange.Normalize();

        if (canShoot)
        {
            animator.SetBool("isShooting", true);
            animator.SetFloat("lastMoveX", posToChange.x);
            animator.SetFloat("lastMoveY", posToChange.y);
            FireAmmo();
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
        
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (Ammo ammo in ammoPool)
        {
            if (ammo.gameObject.activeSelf == false)
            {
                ammo.gameObject.SetActive(true);
                ammo.transform.position = location;
                return ammo.gameObject;
            }
        }
        return null;
    }

    void FireAmmo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);

        if (ammo != null)
        {
            ArcScript arcScript = ammo.GetComponent<ArcScript>();
            float travelDuration = 1.0f / weaponVelocity;
            StartCoroutine(arcScript.Travel(mousePosition, travelDuration));
        }
    }

    void OnDestroy()
    {
        ammoPool = null;
    }
}
