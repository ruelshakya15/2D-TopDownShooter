using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//for scene management

public class SceneTransitions : MonoBehaviour
{
    private Animator transitionAnim;//for scene animations
    // Start is called before the first frame update
    void Start()
    {
        transitionAnim = GetComponent<Animator>();//getting animator component at the start
    }

    public void LoadScene(string sceneName)//loading another scene
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)//coroutine for animation transistion
    {
        transitionAnim.SetTrigger("end");//end animation
        yield return new WaitForSeconds(1);//wait so animation completes and only after we load another scene
        SceneManager.LoadScene(sceneName);//loading the scene acc to parameter
    }
}
