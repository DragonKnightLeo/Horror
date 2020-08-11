using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoNextScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
