using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;
    
    [SerializeField]
    private float hp = 1f;

    public void setMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < minY) {
            Destroy(gameObject);
        }
    }

    // isTrigger 설정
    private void OnTriggerEnter2D(Collider2D other) { // enemy와 weapon 충돌 시
        if (other.gameObject.tag == "Weapon"){
            Weapon weapon = other.gameObject.GetComponent<Weapon>(); // Weapon 객체 받아 오기
            hp -= weapon.damage; // enemy hp 감소
            if (hp <= 0) { // enemy 제거
                if (gameObject.tag == "Boss"){
                    GameManager.instance.SetGameOver();
                }
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity); // 적 제거 시 코인 생성
            } 
            Destroy(other.gameObject); // weapon 제거 (관통 X)
        }
    }

    // if isTrigger is not checked
    // private void OnCollisionEnter2D(Collision2D other) {
        
    // }
}
