using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightCollider : MonoBehaviour
{
    Light2DBlendStyle blendStyle;
    [SerializeField] Light2D light2D;

    // Start is called before the first frame update

    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            light2D.gameObject.SetActive(false);
        }
    }

}
