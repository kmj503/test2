using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private const float moveSpeed = 1000.0f;
    public GameObject shootexp;
    private enemyhp EH;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (collision.CompareTag("enemy"))
        //{
        //    EH.decreaseHp(2);
        //    if (EH.getHp() <= 0)
        //    {
        //        Instantiate(shootexp, this.transform.position, Quaternion.identity);
        //        Destroy(collision.gameObject);
        //        Destroy(this.gameObject);
        //    }

        //}



    }
}
