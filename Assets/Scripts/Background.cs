using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime; // Time.deltaTime (using for different fps each others / ex. 60fps & 30fps)
        if (transform.position.y <= -10){
            transform.position += new Vector3(0, 20f, 0); // y의 값을 -10 -> 10으로 변경 (즉, -10 + 20 = 10)
        }
    }
}
