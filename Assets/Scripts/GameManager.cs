using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;

    [HideInInspector] // Unity에서 안 보이도록 함
    public bool isGameOver = false;

    // Start() 보다 먼저 사용됨
    void Awake() {
        if (instance == null){ // 해당 GameManager 객체를 이 게임의 GameManager로 지정
            instance = this;
        }
    }

    public void IncreaseCoin(){
        coin++;
        text.SetText(coin.ToString());

        if (coin % 20 == 0){ // 코인 개수 = 20, 40, 60, .... => 무기 레벨 증가
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver(){
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();

        if (enemySpawner != null){
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 1f); // 1초 뒤 ShowGameOverPanel() 실행
    }   

    void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain(){ // 게임 재실행 
        SceneManager.LoadScene("SampleScene"); // Scene을 다시 로드하여 게임 재실행 
    }
}
