using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{

    private const float moveSpeed = 800.0f;
    public GameObject playerexp;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
    }

    void OnBecameInvisible() //화면밖으로 나가 보이지 않게 되면 호출
    {
        Destroy(this.gameObject); //객체를 삭제
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (collision.CompareTag("Player"))
        //{
        //    Debug.Log("충돌 확인");

        //    Instantiate(playerexp, this.transform.position, Quaternion.identity);
        //    Destroy(collision.gameObject);
        //    Destroy(this.gameObject);
            
        //}



    }
}
