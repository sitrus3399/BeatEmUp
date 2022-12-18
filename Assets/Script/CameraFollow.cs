using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private BaseCharacter playerCharacter;  

    void Start()
    {
        playerCharacter = player.GetComponent<BaseCharacter>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!playerCharacter.isDead)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, -10);
        }
    }
}
