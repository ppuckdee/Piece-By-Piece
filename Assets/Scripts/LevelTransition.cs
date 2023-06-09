using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextScene;
    public bool dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(dialogue)
            {
                other.gameObject.GetComponent<PlayerMovement>().nextScene = nextScene;
                SceneManager.LoadScene("InbetweenDialogue", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            }
        }
    }
}
