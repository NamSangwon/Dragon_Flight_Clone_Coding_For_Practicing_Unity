using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // Sprite Component에서 조절 가능하도록 함
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons; // 발사체 prefabs
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform; // 공격이 나가는 Transform

    [SerializeField]
    private float shootInterval = 0.05f; // 공격 간격 (= 공격 속도)

    private float lastShotTime = 0f; // 공격 간격을 계산하기 위한 시간 변수

    // Update is called once per frame
    void Update()
    {
        // ############## Using Keyboard ###################
        // ############## Using GetAxisRaw() ###############
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;
    
        // ############## Using GetKey() ###############
        // Vector3 moveToHorizontal = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // Vector3 moveToVertical = new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        // if (Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveToHorizontal;
        // }
        // else if (Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveToHorizontal;
        // }
        // else if (Input.GetKey(KeyCode.UpArrow)){
        //     transform.position += moveToVertical;
        // }
        // else if (Input.GetKey(KeyCode.DownArrow)){
        //     transform.position -= moveToVertical;
        // }

        
        // ############## Using Mouse ###################
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 좌표계를 유니티 좌표계로 변환 ( (0,0) ~ (1080, 1920) => (-5, -5) ~ (5, 5) )

        // transform.position = mousePos; // x, y, z 모두 적용
        // transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z); // only x 좌표만 적용
        // transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z); // only x, y 좌표만 적용

        // using mouse add wall
        float toX = Mathf.Clamp(mousePos.x, -2.5f, 2.5f); // parameter = value, min, max => (value < min -> value = min & value> max -> value = max)
        float toY = Mathf.Clamp(mousePos.y, -4.5f, 4.5f);
        transform.position = new Vector3(toX, toY, transform.position.z);
    
        if (GameManager.instance.isGameOver == false){ // shooting
            Shoot();
        }
    }

    void Shoot(){
        if (Time.time - lastShotTime > shootInterval){
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity); // parameters = (Object, position, rotation)
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Coin"){
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade(){
        weaponIndex++;
        if (weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1;
        }
    }
}