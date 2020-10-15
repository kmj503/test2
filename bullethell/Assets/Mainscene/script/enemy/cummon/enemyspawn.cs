using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab을 받을 public 변수
    void SpawnEnemy()
    {
        float randomx = Random.Range(670f, 1110f);
        float randomy = Random.Range(70f, 470f); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomx, randomy, -1.0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성
        }
    }

    public void spawnstop()
    {
        CancelInvoke("SpawnEnemy");
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5, 1); //3초후 부터, SpawnEnemy함수를 1초마다 반복해서 실행
    }
    void Update()
    {

    }
}

