using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    public float damage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f); // params = (object, time)
    }

    // Update is called once per frame
    void Update()
    {
        // weapon이 위로 이동 (Vector3.up = new Vector3(0, 1, 0))
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
