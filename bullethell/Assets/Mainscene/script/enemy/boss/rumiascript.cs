using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rumiascript : MonoBehaviour
{
    private Rigidbody rigid;  //리지드 바디

    public int Hp = 500;
    private enemyhp enemy_hp;
    public GameObject shootexp;
    
    


   

    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = new enemyhp(Hp);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_hp.getHp() <= 0) //보스 사망 조건
        {
            Destroy(this.gameObject);
            scoremgr.scorevalue += 20000;
        }
        // 463~89 move
        //보스의 움직임 조건 및 전체 움직임, 패턴
        var possx = this.gameObject.transform.localPosition;
        possx.x -= 5f;
        this.gameObject.transform.localPosition = possx;
        if (possx.x < 1021)
        {
            var possf = this.gameObject.transform.localPosition;
            possf.x = 1021;
            this.gameObject.transform.localPosition = possf;

            if (enemy_hp.getHp() <= 450)
            {
                possf.y -= 6f;
                this.gameObject.transform.localPosition = possf;
                if (possf.y <= 89)
                {
                    var posss = this.gameObject.transform.localPosition;
                    posss.y = 89;
                    this.gameObject.transform.localPosition = posss;

                }
               
            }

            if (enemy_hp.getHp() <= 400)
            {
                var posst = this.gameObject.transform.localPosition;
                posst.y += 6f;
                this.gameObject.transform.localPosition = posst;
                if (posst.y <= 463)
                {
                    var posstt = this.gameObject.transform.localPosition;
                    posstt.y = 463;
                    this.gameObject.transform.localPosition = posstt;

                }

            }






        }
        //패턴 시작
       

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
