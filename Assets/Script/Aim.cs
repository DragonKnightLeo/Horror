using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Aim : MonoBehaviour
{
    Animator animator;
    public GameObject ammoPrefab;
    public GameObject playerPos;
    static List<GameObject> ammoPool;
    public int poolSize;

    public float weaponVelocity;
    bool isFiring;


    void Awake()
    {
        // object pool
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
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
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
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
