using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photonサーバーの情報を使用するため
using Photon.Realtime;

public class MovementController : MonoBehaviourPunCallbacks
{
    // Rigidbody2D コンポーネントを格納する変数
    private Rigidbody2D rb;
    // 自機の移動速度を格納する変数（初期値 5）
    public float speed = 5;
    public float slowSpeed = 2;
    private Vector2 velocity = Vector2.zero; //初期値ゼロ

    private VariableJoystick variableJoystick;
    private ButtonState buttonState;

    public Animator animator;
    private Vector2 direction;

    // ゲームのスタート時の処理
    void Start()
    {
        // Rigidbody2D コンポーネントを取得して変数 rb に格納
        rb = GetComponent<Rigidbody2D>();

        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        buttonState = GameObject.Find("SlowButton").GetComponent<ButtonState>();

        //アニメーションの初期値
        animator.SetFloat("x", 0);
        animator.SetFloat("y", -1);

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
        direction = new Vector2(x, y).normalized;
        // 移動する向きとスピードを代入する
        // Rigidbody2D コンポーネントの velocity に方向と移動速度を掛けた値を渡す
        //rb.velocity = direction * speed;
        velocity = direction * speed;

        //左シフトか低速ボタンが押されていたら
        if (buttonState.IsPressed() || Input.GetKey(KeyCode.LeftShift))
        {
            speed = slowSpeed;
        }
        else　//そうでなければ
        {
            speed = 5;
        }

    }

    private void FixedUpdate()
    {
        //自プレイヤーなら
        if (photonView.IsMine && this.gameObject.tag == "Player")
        {
            if (velocity != Vector2.zero) //velocityに入力値があれば
            {
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); //MovePositionメソッドを呼び、移動させる値を渡す
            }

            if (direction != Vector2.zero)
            {
                // 入力されている場合はアニメーターに方向を設定
                animator.SetFloat("x", direction.x);
                animator.SetFloat("y", direction.y);
            }
        }

        //NPCなら
        if (this.CompareTag("Enemy"))
        {
            float distance = 0.01f;
            //一番近いプレイヤーに寄ってくる
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, FetchNearObjectWithTag("Player").position, distance);
        }

    }

    // １番近いオブジェクトを取得する
    // (プレイヤーのMonoBehaviourにアタッチされている前提)
    private Transform FetchNearObjectWithTag(string tagName)
    {
        // 該当タグが1つしか無い場合はそれを返す
        var targets = GameObject.FindGameObjectsWithTag(tagName);
        if (targets.Length == 1) return targets[0].transform;

        GameObject result = null;
        var minTargetDistance = float.MaxValue;
        foreach (var target in targets)
        {
            // 前回計測したオブジェクトよりも近くにあれば記録
            var targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (!(targetDistance < minTargetDistance)) continue;
            minTargetDistance = targetDistance;
            result = target.transform.gameObject;
        }

        // 最後に記録されたオブジェクトを返す
        return result?.transform;
    }
}
