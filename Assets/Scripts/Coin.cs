using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -7f;

    // Start is called before the first frame update
    void Start()
    {
        Jump(); // coin 생성 시 위로 튀기기
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY){
            Destroy(gameObject);
        }
    }

    void Jump(){ // 코인 생성 시 위로 튀어 오르기
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        // 위로 튀어 오르기 + 좌 or 우로 이동
        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f); 

        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }
}
