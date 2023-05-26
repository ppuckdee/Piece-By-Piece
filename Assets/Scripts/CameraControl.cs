using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject cameraObj;
    public Vector2 playerPlay;
    public float[] bounds;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = cameraObj.GetComponent<Camera>();
        cameraObj.transform.position = new Vector3(transform.position.x, transform.position.y, cameraObj.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = transform.position;
        Vector2 cameraPosition = cameraObj.transform.position;
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
        

        Debug.Log("Dist From Camera: " + playerDistFromCam);
    }
}
