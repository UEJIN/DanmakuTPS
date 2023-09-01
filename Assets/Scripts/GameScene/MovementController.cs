using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photon�T�[�o�[�̏����g�p���邽��
using Photon.Realtime;

public class MovementController : MonoBehaviourPunCallbacks
{
    // Rigidbody2D �R���|�[�l���g���i�[����ϐ�
    private Rigidbody2D rb;
    // ���@�̈ړ����x���i�[����ϐ��i�����l 5�j
    public float speed = 5;
    public float slowSpeed = 2;
    private Vector2 velocity = Vector2.zero; //�����l�[��

    private VariableJoystick variableJoystick;
    private ButtonState buttonState;

    public Animator animator;
    private Vector2 direction;

    // �Q�[���̃X�^�[�g���̏���
    void Start()
    {
        // Rigidbody2D �R���|�[�l���g���擾���ĕϐ� rb �Ɋi�[
        rb = GetComponent<Rigidbody2D>();

        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        buttonState = GameObject.Find("SlowButton").GetComponent<ButtonState>();

        //�A�j���[�V�����̏����l
        animator.SetFloat("x", 0);
        animator.SetFloat("y", -1);

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
        direction = new Vector2(x, y).normalized;
        // �ړ���������ƃX�s�[�h��������
        // Rigidbody2D �R���|�[�l���g�� velocity �ɕ����ƈړ����x���|�����l��n��
        //rb.velocity = direction * speed;
        velocity = direction * speed;

        //���V�t�g���ᑬ�{�^����������Ă�����
        if (buttonState.IsPressed() || Input.GetKey(KeyCode.LeftShift))
        {
            speed = slowSpeed;
        }
        else�@//�����łȂ����
        {
            speed = 5;
        }

    }

    private void FixedUpdate()
    {
        //���v���C���[�Ȃ�
        if (photonView.IsMine && this.gameObject.tag == "Player")
        {
            if (velocity != Vector2.zero) //velocity�ɓ��͒l�������
            {
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); //MovePosition���\�b�h���ĂсA�ړ�������l��n��
            }

            if (direction != Vector2.zero)
            {
                // ���͂���Ă���ꍇ�̓A�j���[�^�[�ɕ�����ݒ�
                animator.SetFloat("x", direction.x);
                animator.SetFloat("y", direction.y);
            }
        }

        //NPC�Ȃ�
        if (this.CompareTag("Enemy"))
        {
            float distance = 0.01f;
            //��ԋ߂��v���C���[�Ɋ���Ă���
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, FetchNearObjectWithTag("Player").position, distance);
        }

    }

    // �P�ԋ߂��I�u�W�F�N�g���擾����
    // (�v���C���[��MonoBehaviour�ɃA�^�b�`����Ă���O��)
    private Transform FetchNearObjectWithTag(string tagName)
    {
        // �Y���^�O��1���������ꍇ�͂����Ԃ�
        var targets = GameObject.FindGameObjectsWithTag(tagName);
        if (targets.Length == 1) return targets[0].transform;

        GameObject result = null;
        var minTargetDistance = float.MaxValue;
        foreach (var target in targets)
        {
            // �O��v�������I�u�W�F�N�g�����߂��ɂ���΋L�^
            var targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (!(targetDistance < minTargetDistance)) continue;
            minTargetDistance = targetDistance;
            result = target.transform.gameObject;
        }

        // �Ō�ɋL�^���ꂽ�I�u�W�F�N�g��Ԃ�
        return result?.transform;
    }
}
