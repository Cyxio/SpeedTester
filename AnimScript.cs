using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimScript : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Play", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextScene(int num) //button press
    {
        ButtonCommands.nextScene = num;
        anim.SetBool("Play", true);
    }

    public void NewScene() // anim finish
    {
        anim.SetBool("Play", false);
        int temp = ButtonCommands.nextScene;
        ButtonCommands.nextScene = 0;
        SceneManager.LoadScene(temp);
    }
}
