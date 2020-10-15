using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyfairy: MonoBehaviour
{
    public const float movespeed = 500f;  //움직이는 속도

    private Rigidbody rigid;  //리지드 바디
    public int Hp;
    private enemyhp enemy_hp;
    public GameObject shootexp;

    private bossSpawn boss_s;

    // radius projectile

    //[SerializeField]
    //int numberOfProjectiles = 0;


    //public GameObject projectile;

    //public GameObject playerexp;



    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = new enemyhp(Hp);
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_hp.getHp() <= 0)
        {
            Destroy(this.gameObject);
            scoremgr.scorevalue += 300;
            boss_s.e_deathcount++;
        }

        //startPoint = this.gameObject.transform.localPosition;
        //SpawnProjectiles(numberOfProjectiles);


        var possx = this.gameObject.transform.localPosition;
            possx.x -= 5f;
            this.gameObject.transform.localPosition = possx;
            if(possx.x<629 && possx.y > 285)
            {
                var possf = this.gameObject.transform.localPosition;
                possf.y += 5f;
                this.gameObject.transform.localPosition = possf;
                
            }
            if(possx.x<629 && possx.y <285    )
            {
               var possf = this.gameObject.transform.localPosition;
               possf.y -= 5f;
               this.gameObject.transform.localPosition = possf;
            }
        

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
    //void shot()
    //{
    //    //360번 반복
    //    for (int i = 0; i < 1; i += 13)
    //    {
    //        //총알 생성
    //        GameObject temp = Instantiate(this.gameObject);

    //        //2초마다 삭제
    //        //Destroy(temp, 2f);

    //        //총알 생성 위치를 (0,0) 좌표로 
    //        temp.transform.position = this.gameObject.transform.localPosition;

    //        //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
    //        temp.transform.rotation = Quaternion.Euler(i, 0, 0);
    //    }
    //}

    //void SpawnProjectiles(int numberOfProjectiles)
    //{
    //    float angleStep = 360f / numberOfProjectiles;
    //    float angle = 0f;

    //    for (int i = 0; i <= numberOfProjectiles - 1; i++)
    //    {

    //        float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
    //        float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

    //        Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
    //        Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

    //        var proj = Instantiate(projectile, startPoint, Quaternion.identity);
    //        proj.GetComponent<Rigidbody2D>().velocity =
    //            new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

    //        angle += angleStep;
    //    }
    //}



    //void appearControl()  // 적 랜덤 생성 //이미 스폰 스크립트 적용되어 주석처리
    //{
    //    float distanceY = movespeed * Time.deltaTime;

    //    this.gameObject.transform.Translate(0, -1 * distanceY, -1);

    //}

    void OnBecameInvisible() //화면밖으로 나가 보이지 않게 되면 호출
    {
        Destroy(this.gameObject); //객체를 삭제
    }

   



}
