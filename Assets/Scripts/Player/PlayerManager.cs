using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rb;
    float x;
    float z;
    public float moveSpeed = 2;
    Animator animator;
    // Update関数の前に実行される：設定
    void Start()
    {
        // リジットボディを取得
        rb = GetComponent<Rigidbody>();
        // アニメーターを取得
        animator = GetComponent<Animator>();
    }

    // 約0.02秒に１回実行される：実行
    void Update()
    {
        // キーボード入力で移動させたい
        x = Input.GetAxisRaw("Horizontal");// 横方向
        z = Input.GetAxisRaw("Vertical");// 縦方向

        // 攻撃実装
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
        
    }

    private void FixedUpdate()
    {
        // 方向転換
        Vector3 direction = transform.position + new Vector3(x, 0, z) * moveSpeed;
        transform.LookAt(direction);
        // 速度設定
        rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {

        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            // ダメージを与えるものにぶつかったら
            animator.SetTrigger("Hurt");
        }

    }
}
