using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform position;
    void Start()
    {
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    public Vector3 getPosition()
    {
        return gameObject.transform.position;
    }    
}
