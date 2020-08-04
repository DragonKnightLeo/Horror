using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenu : MonoBehaviour
{
    public Animator animator;

    public void PlayGame()
    {
        animator.SetTrigger("FadeOut");
    }

    
}
