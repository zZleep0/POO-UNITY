using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyMe());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, bulletSpeed * Time.deltaTime);
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
