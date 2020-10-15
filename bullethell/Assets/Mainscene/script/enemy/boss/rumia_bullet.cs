using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rumia_bullet : MonoBehaviour
{
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.Self);
    }
}
