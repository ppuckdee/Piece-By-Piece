using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public GameObject cameraObj;
    public Vector2 playerPlay;
    public float camScrollSpeed;
    public float[] bounds;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        if(!cameraObj)
        {
            cam = Camera.main;
            cameraObj = cam.gameObject;
        }
        else
        {
            cam = cameraObj.GetComponent<Camera>();
        }
        cameraObj.transform.position = new Vector3(transform.position.x, transform.position.y, cameraObj.transform.position.z);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        
        cam = Camera.main;
        cameraObj = cam.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = transform.position;
        Vector2 cameraPosition = cameraObj.transform.position;

        Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDistFromCam = mouseWorldPos - cameraPosition;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(mouseDistFromCam.x > playerPlay.x)
            {
                cameraObj.transform.position += Vector3.right * camScrollSpeed * Time.deltaTime;
            }
            else if(mouseDistFromCam.x < -playerPlay.x)
            {
                cameraObj.transform.position -= Vector3.right * camScrollSpeed * Time.deltaTime;
            }

            if(mouseDistFromCam.y > playerPlay.y)
            {
                cameraObj.transform.position += Vector3.up * camScrollSpeed * Time.deltaTime;
            }
            else if(mouseDistFromCam.y < -playerPlay.y)
            {
                cameraObj.transform.position -= Vector3.up * camScrollSpeed * Time.deltaTime;
            }
        }

        cameraPosition = cameraObj.transform.position;
        Vector2 playerDistFromCam = playerPosition - cameraPosition;
        if(playerDistFromCam.x > playerPlay.x)
        {
            cameraObj.transform.position = new Vector3(playerPosition.x - playerPlay.x, cameraObj.transform.position.y, cameraObj.transform.position.z);
        }
        else if(playerDistFromCam.x < -playerPlay.x)
        {
            cameraObj.transform.position = new Vector3(playerPosition.x + playerPlay.x, cameraObj.transform.position.y, cameraObj.transform.position.z);
        }
        
        if(playerDistFromCam.y > playerPlay.y)
        {
            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, playerPosition.y - playerPlay.y, cameraObj.transform.position.z);
        }
        else if(playerDistFromCam.y < -playerPlay.y)
        {
            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, playerPosition.y + playerPlay.y, cameraObj.transform.position.z);
        }
    }
}
