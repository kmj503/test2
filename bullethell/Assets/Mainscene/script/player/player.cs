using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody rigid;
    SpriteRenderer rend;
    public int testval = 0;
    public Vector3 m_dir;
    public float m_power;
    public bool yswitch;
    Animator ani;
    public GameObject bullet;
    public GameObject shootexp ;
    private shake shake;

    private playerhp player_hp;
    public int Hp;

    
    





    // Start is called before the first frame update
    void Start()
    {

        shake = GameObject.FindGameObjectWithTag("screenshake").GetComponent<shake>();
        player_hp = new playerhp(Hp);
        this.gameObject.transform.localPosition = new Vector4(29, 280, -1);
        
        rigid = GetComponent<Rigidbody>();
        rend = GetComponentInChildren<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        movecontrol();


        if (player_hp.getHp() <= 0)
        {
            Destroy(this.gameObject);
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {

            var pos = this.gameObject.transform.localPosition;
            pos.x += 8f;
            this.gameObject.transform.localPosition = pos;
            
            if(Input.GetKey(KeyCode.LeftShift))
            {

                var poss = this.gameObject.transform.localPosition;
                poss.x -= 4f;
                this.gameObject.transform.localPosition = poss;
            }

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
        
            var pos = this.gameObject.transform.localPosition;
            pos.x -= 8f;
            this.gameObject.transform.localPosition = pos;
            if (Input.GetKey(KeyCode.LeftShift))
            {

                var poss = this.gameObject.transform.localPosition;
                poss.x += 4f;
                this.gameObject.transform.localPosition = poss;
            }

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {

            var pos = this.gameObject.transform.localPosition;
            pos.y += 8f;
            this.gameObject.transform.localPosition = pos;
            if (Input.GetKey(KeyCode.LeftShift))
            {

                var poss = this.gameObject.transform.localPosition;
                poss.y -= 4f;
                this.gameObject.transform.localPosition = poss;
            }

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            var pos = this.gameObject.transform.localPosition;
            pos.y -= 8f;
            this.gameObject.transform.localPosition = pos;
            if (Input.GetKey(KeyCode.LeftShift))
            {

                var poss = this.gameObject.transform.localPosition;
                poss.y += 4f;
                this.gameObject.transform.localPosition = poss;
            }

        }
        if(Input.GetKey(KeyCode.Z))
        {
            GameObject instance = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Destroy(instance, 2.0f);
        }

       

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("enemy"))  //적 충돌시 이펙트 나오며 사망
        {

            shake.camshake();
            Instantiate(shootexp, this.transform.position, Quaternion.identity);
            player_hp.decreaseHp(1);
            Destroy(other.gameObject);


        }
    }

    

    void movecontrol() // 카메라 내부에서 제한된 움직임을 위한 함수
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환
        transform.position = worldPos; //좌표를 적용
    }

}
