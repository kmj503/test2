using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyUfo1 : MonoBehaviour
{
    public const float movespeed = 1.3f;  //움직이는 속도

    private Rigidbody rigid;  //리지드 바디
    public GameObject bullet;  // 적 총알
    float Firerate;
    float Nextfire;

    public int Hp;
    private enemyhp enemy_hp;
    public GameObject shootexp;
    private bossSpawn boss_s;




    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = new enemyhp(Hp);
        


        Firerate = 1f;
        Nextfire = Time.time;

        
      
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_hp.getHp() <=0)
        {
            Destroy(this.gameObject);
           
            scoremgr.scorevalue += 100;

            boss_s.e_deathcount++;
            
        }
        CheckFire();
       
    }

    //void appearControl()  // 적 랜덤 생성 //이미 스폰 스크립트 적용되어 주석처리
    //{
    //    float distanceY = movespeed * Time.deltaTime;
        
    //    this.gameObject.transform.Translate(0, -1 * distanceY, -1);
        
    //}
    void CheckFire()
    {
        if(Time.time>Nextfire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            Nextfire = Time.time + Firerate;
        }
    }
    void OnBecameInvisible() //화면밖으로 나가 보이지 않게 되면 호출
    {
        Destroy(this.gameObject); //객체를 삭제
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Instantiate(shootexp, this.transform.position, Quaternion.identity);
            enemy_hp.decreaseHp(1);
            Destroy(collision.gameObject);

        }
    }

}
