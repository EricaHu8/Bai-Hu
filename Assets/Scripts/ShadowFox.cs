using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFox : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, 3);
    }
}

//itemSlot.transform.position = new Vector3(cameraPos.x + 750, cameraPos.y + 500, 0.42f);