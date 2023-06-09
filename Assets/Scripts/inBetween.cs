using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inBetween : MonoBehaviour
{
    PlayerAbilities.mutationType[] abilities;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in FindObjectOfType<PlayerMovement>().gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        abilities = FindObjectOfType<PlayerAbilities>().getAbilities();
        string text = "";
        if(abilities.Length == 1)
        {
            switch(abilities[0])
            {
                case PlayerAbilities.mutationType.ROBOT:
                text = "i hope these *beep* abilities can help *boop* me get out of here...";
                break;
                case PlayerAbilities.mutationType.PLANT:
                text = "i hope these... umm... these abilities can help me.....";
                break;
                case PlayerAbilities.mutationType.ANIMAL:
                text = "*hrrnnggsshh* These abilities better let me escape this place!! *rgharrr*";
                break;
            }
        }
        else
        {
            switch(abilities[0])
            {
                case PlayerAbilities.mutationType.ROBOT:
                switch(abilities[1])
                {
                    case PlayerAbilities.mutationType.ROBOT:
                    text = "01010011 01100101 01101110 01110011 01101111 01110010 01110011 00100000 01101001 01101110 01100100 01101001 01100011 01100001 01110100 01100101 00100000 01100101 01111000 01101001 01110100 00100000 01101001 01110011 00100000 01101110 01100101 01100001 01110010 00101110";
                    break;
                    case PlayerAbilities.mutationType.PLANT:
                    text = "*beep* sunlight needed *boop*, exit should be up ahead";
                    break;
                    case PlayerAbilities.mutationType.ANIMAL:
                    text = "Sensors.. *rgharrr* indicate exit is near";
                    break;
                }
                break;
                case PlayerAbilities.mutationType.PLANT:
                switch(abilities[1])
                {
                    case PlayerAbilities.mutationType.ROBOT:
                    text = "*beep* sunlight needed *boop*, exit should be up ahead";
                    break;
                    case PlayerAbilities.mutationType.PLANT:
                    text = "...";
                    break;
                    case PlayerAbilities.mutationType.ANIMAL:
                    text = "*hrrnnggsshh* ... *photosynthesis* ... *rgharrr*";
                    break;
                }
                break;
                case PlayerAbilities.mutationType.ANIMAL:
                switch(abilities[1])
                {
                    case PlayerAbilities.mutationType.ROBOT:
                    text = "Sensors.. *rgharrr* indicate exit is near";
                    break;
                    case PlayerAbilities.mutationType.PLANT:
                    text = "*hrrnnggsshh* ... *photosynthesis* ... *rgharrr*";
                    break;
                    case PlayerAbilities.mutationType.ANIMAL:
                    text = "rrghhrrshhnrrr\nArrhhhgghhh";
                    break;
                }
                break;
            }
        }
        GetComponent<Text>().text = text;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > 3)
        {
            SceneManager.LoadScene(FindObjectOfType<PlayerMovement>().nextScene, LoadSceneMode.Single);
        }
    }
}
