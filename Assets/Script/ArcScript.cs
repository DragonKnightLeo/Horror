using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcScript : MonoBehaviour
{
    public IEnumerator Travel(Vector2 destination, float duration)
    {
        var startPosition = transform.position;

        var percentCompelete = 0.0f;

        while(percentCompelete < 1.0f)
        {
            percentCompelete += Time.deltaTime / duration;

            transform.position = Vector2.Lerp(startPosition, destination, percentCompelete);

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
