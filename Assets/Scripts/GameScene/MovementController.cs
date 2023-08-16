using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Rigidbody2D �R���|�[�l���g���i�[����ϐ�
    private Rigidbody2D rb;
    // ���@�̈ړ����x���i�[����ϐ��i�����l 5�j
    public float speed = 5;

    private Vector2 velocity = Vector2.zero; //�����l�[��

    private VariableJoystick variableJoystick;

    // �Q�[���̃X�^�[�g���̏���
    void Start()
    {
        // Rigidbody2D �R���|�[�l���g���擾���ĕϐ� rb �Ɋi�[
        rb = GetComponent<Rigidbody2D>();

        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();

    }

    // �Q�[�����s���̌J��Ԃ�����
    void Update()
    {
        // �E�E���̃f�W�^�����͒l�� x �ɓn��
        float x = Input.GetAxisRaw("Horizontal")+ variableJoystick.Horizontal;
        // ��E���̃f�W�^�����͒l y �ɓn��
        float y = Input.GetAxisRaw("Vertical")+ variableJoystick.Vertical;
        // �ړ�������������߂�
        // x �� y �̓��͒l�𐳋K������ direction �ɓn��
        Vector2 direction = new Vector2(x, y).normalized;
        // �ړ���������ƃX�s�[�h��������
        // Rigidbody2D �R���|�[�l���g�� velocity �ɕ����ƈړ����x���|�����l��n��
        //rb.velocity = direction * speed;
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        if (velocity != Vector2.zero) //velocity�ɓ��͒l�������
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); //MovePosition���\�b�h���ĂсA�ړ�������l��n��
        }
    }
}
