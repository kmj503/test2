using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int Spawncount = 0;
    public int Bosscount = 0;
    public bool enableSpawn = false;
    public bool enableMSpawn = false;
    public bool enableBSpawn = false;
    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject Boss;

    void SpawnEnemy()
    {
        float randomx = Random.Range(670f, 1110f);
        float randomy = Random.Range(70f, 470f); //적이 나타날 X좌표를 랜덤으로 생성
        if (enableSpawn)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomx, randomy, -1.0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성
        }
        Spawncount += 1;
    }

    void SpawnMEnemy()
    {

        if (enableSpawn)
        {
            float randomx = 1240f;
            float randomy = Random.Range(50f, 500f); //적이 나타날  좌표를 랜덤으로 생성
            GameObject enemy = (GameObject)Instantiate(Enemy2, new Vector3(randomx, randomy, -1.0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성
            GameObject enemy1 = (GameObject)Instantiate(Enemy2, new Vector3(randomx, randomy, -1.0f), Quaternion.identity);
            GameObject enemy2 = (GameObject)Instantiate(Enemy2, new Vector3(randomx, randomy, -1.0f), Quaternion.identity);
            randomx--;
            Spawncount += 1;
        }
    }

   void SpawnBEnemy()
    {
        if(enableBSpawn)
        {
            GameObject boss = (GameObject)Instantiate(Boss, new Vector3(1330f, 261f, -1f), Quaternion.identity);
            Bosscount += 1;
        }
    }

    public void spawnstop()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("SpawnMEnemy");
        
    }
    public void Bossspawnstop()
    {
        CancelInvoke("SpawnBEnemy");

    }

    // Start is called before the first frame update
    void Start()
    {  
        InvokeRepeating("SpawnEnemy", 3, 1);
        InvokeRepeating("SpawnMEnemy", 3, 1);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawncount >= 50)
        {
            spawnstop();
            InvokeRepeating("SpawnBEnemy", 3, 1);
        }
        if(Bosscount == 1)
        {
            Bossspawnstop();
        }

        
    }
}
