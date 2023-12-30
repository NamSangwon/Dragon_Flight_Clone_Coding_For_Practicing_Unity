using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine"); // Enemy 생성하는 루틴 시작 (coroutine으로 루틴 시작)
    }

    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }

    // enemy 생성 루틴 함수
    IEnumerator EnemyRoutine(){
        yield return new WaitForSeconds(3f); // while문을 3초 뒤에 수행

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while(true){
            // arrPosX의 5개의 위치에 적 모두 생성
            foreach (float posX in arrPosX){
                // int index = Random.Range(0, enemies.Length); // enemy1 ~ enemy7 랜덤 선택
                SpawnEnemy(posX, enemyIndex, moveSpeed); 
            }

            spawnCount++;
            
            if(spawnCount % 10 == 0){ // 난이도 증가
                enemyIndex++; // enemy 타입 변경
                moveSpeed += 2; // 속도 증가
            }

            if (enemyIndex >= enemies.Length){
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }
            
            yield return new WaitForSeconds(spawnInterval); // spawnInterval 간격으로 enemy 생성
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z); // 적 위치 지정

        if (Random.Range(0, 5) == 0) {
            index += 1; // 20%의 확률로 한 단계 강한 enemy 생성 
        }
        if (index >= enemies.Length) {
            index = enemies.Length - 1; // 오류 방지
        }
        

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity); // 적 생성 
        Enemy enemy = enemyObject.GetComponent<Enemy>(); // Enemy component 얻기
        enemy.setMoveSpeed(moveSpeed); // 적 속도 지정
    }

    void SpawnBoss(){
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
