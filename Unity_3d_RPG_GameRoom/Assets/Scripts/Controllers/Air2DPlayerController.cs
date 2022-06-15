using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air2DPlayerController : ActorController
{
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Moving();
    }

    public override void Init()
    {
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        ActorsType = Defines.Actors.Player;
        _moveSpeed = 15.0f;
    }
    private void Moving()
    {
        // InputManager�� ���Ͽ� ���ŵ� �̵����� Input��(���̽�ƽ Input�� H,V) Getter
        Vector2 conDir = Managers.Input.MoveInput;

        /***** �ִϸ��̼� ���� *****/
        //_anim.SetFloat("MoveX", conDir.x);
        //_anim.SetFloat("MoveY", conDir.y);

        if (conDir == Vector2.zero) return;

        // Player�� Speed, Input���⺤���� ũ��, ������ Smooth Move ���� ������ ���Ͽ� ���� speed�� ����
        // ���� Player�� �����������, Speed, �߷°��� �����Ͽ� ���� velocity���� ���� �̵���Ų��.
        if (conDir.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);

        conDir = conDir.normalized;
        float speed = _moveSpeed * conDir.magnitude;
        speed = Mathf.SmoothDamp(_currentSpeed, speed, ref _speedSmoothVelocity, _speedSmoothTime);


        Vector3 moveDir = new Vector3(conDir.x, conDir.y, 0);  //transform.right.normalized; // ���� ��������
        Vector3 velocity = moveDir * _moveSpeed;

        _characterController.Move(velocity * Time.deltaTime); // �̵��Ÿ� = �ӵ� * �ð�
    }
}
