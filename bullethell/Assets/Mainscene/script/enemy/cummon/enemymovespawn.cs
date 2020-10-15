using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymovespawn : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab을 받을 public 변수
  
    void SpawnEnemy()
    {
        
        if (enableSpawn)
        {
            float randomx = 1240f;
            float randomy = Random.Range(50f, 500f); //적이 나타날  좌표를 랜덤으로 생성
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomx, randomy, -1.0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성
            GameObject enemy1 = (GameObject)Instantiate(Enemy, new Vector3(randomx, randomy, -1.0f), Quaternion.identity);
            GameObject enemy2 = (GameObject)Instantiate(Enemy, new Vector3(randomx, randomy, -1.0f), Quaternion.identity);
            randomx--;
        }
    }

    public void spawnstop()
    {
        CancelInvoke("SpawnEnemy");
    }

    void Start()
    {
        
        InvokeRepeating("SpawnEnemy", 3, 1); //3초후 부터, SpawnEnemy함수를 1초마다 반복해서 실행
    }
    void Update()
    {
           

    }
}
