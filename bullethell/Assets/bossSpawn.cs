using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawn : MonoBehaviour
{
    public int e_deathcount=0;
    public GameObject boss;

    private enemyspawn e1_spawn;
    private enemymovespawn e2_spawn;



    void spawnboss()
    {
        float randomx = Random.Range(670f, 1110f);
        float randomy = Random.Range(70f, 470f); //적이 나타날 X좌표를 랜덤으로 생성
        GameObject enemy = (GameObject)Instantiate(boss, new Vector3(randomx, randomy, -1.0f), Quaternion.identity);
    }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(e_deathcount>=10)
        {
            e1_spawn.spawnstop();
            e2_spawn.spawnstop();
            spawnboss();
        }
    }
}
