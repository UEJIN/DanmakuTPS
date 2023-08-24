using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Rigidbody2D コンポーネントを格納する変数
    private Rigidbody2D rb;
    // 自機の移動速度を格納する変数（初期値 5）
    public float speed = 5;
    public float slowSpeed = 2;
    private Vector2 velocity = Vector2.zero; //初期値ゼロ

    private VariableJoystick variableJoystick;
    private ButtonState buttonState;

    // ゲームのスタート時の処理
    void Start()
    {
        // Rigidbody2D コンポーネントを取得して変数 rb に格納
        rb = GetComponent<Rigidbody2D>();

        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        buttonState = GameObject.Find("SlowButton").GetComponent<ButtonState>();
        


    }

    // ゲーム実行中の繰り返し処理
    void Update()
    {
        // 右・左のデジタル入力値を x に渡す
        float x = Input.GetAxisRaw("Horizontal")+ variableJoystick.Horizontal;
        // 上・下のデジタル入力値 y に渡す
        float y = Input.GetAxisRaw("Vertical")+ variableJoystick.Vertical;
        // 移動する向きを求める
        // x と y の入力値を正規化して direction に渡す
        Vector2 direction = new Vector2(x, y).normalized;
        // 移動する向きとスピードを代入する
        // Rigidbody2D コンポーネントの velocity に方向と移動速度を掛けた値を渡す
        //rb.velocity = direction * speed;
        velocity = direction * speed;

        if (buttonState.IsPressed() || Input.GetKey(KeyCode.LeftShift))
        {
            speed = slowSpeed;
        }
        else
        {
            speed = 5;
        }

    }

    private void FixedUpdate()
    {
        if (velocity != Vector2.zero) //velocityに入力値があれば
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); //MovePositionメソッドを呼び、移動させる値を渡す
        }
    }
}
