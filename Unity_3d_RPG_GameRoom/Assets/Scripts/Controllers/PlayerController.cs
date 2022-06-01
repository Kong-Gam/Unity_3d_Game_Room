#define MOBILE_JOYSTICK_BASE_CONTROLLER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ActorController
{
    private Transform _camPivot;

    public void SetCamPivot(Transform campivot) { _camPivot = campivot; }
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
        //_stat = gameObject.GetComponent<Stat>();
        // Move Stat [Json] Parsing ����
    }
    private void Moving()
    {
        // InputManager�� ���Ͽ� ���ŵ� �̵����� Input��(���̽�ƽ Input�� H,V) Getter
        Vector2 conDir = Managers.Input.MoveInput;

        /***** �ִϸ��̼� ���� *****/
        _anim.SetFloat("MoveX", conDir.x);
        _anim.SetFloat("MoveY", conDir.y);

        if (conDir == Vector2.zero) return;

        /***** �̵� ���� �ڵ� *****/

        // Input�� ���⺤�Ϳ� y�� ������ ����(theta)�� ���ϰ� _camPivot.rotation.y ���� ���Ͽ� �÷��̾��� ȸ������ ����. => ī�޶� �������� ĳ������ rotation���� �����ϱ� ���Ͽ�
        // theta = Acos(conDir.y(y��)/conDir.magnitude(Input���⺤���� ũ��))
        // thetaEuler = theta * (180/PI)
        // X���� ������ �� ��� y�� �����ϱ� ������ theta�� ������� ���´� => �������� ���� ���� ���� �߻� => 'x�� ��ȣ'�� theta���� ���Ѵ� => Sign(conDir.x)�� ���������ν� �ذ��� �� �ִ�.
        float thetaEuler = Mathf.Acos(conDir.y / conDir.magnitude) * (180 / Mathf.PI) * Mathf.Sign(conDir.x);
        Vector3 moveAngle = Vector3.up * (_camPivot.transform.rotation.eulerAngles.y + thetaEuler);
        transform.rotation = Quaternion.Euler(moveAngle);
        
        // Player�� Speed, Input���⺤���� ũ��, ������ Smooth Move ���� ������ ���Ͽ� ���� speed�� ����
        // ���� Player�� �����������, Speed, �߷°��� �����Ͽ� ���� velocity���� ���� �̵���Ų��.
        float speed = _moveSpeed * Managers.Input.MoveInput.magnitude;
        speed = Mathf.SmoothDamp(_currentSpeed, speed, ref _speedSmoothVelocity, _speedSmoothTime);

        _currentVelocityY += Time.deltaTime * Physics.gravity.y; // �߷� ��

        Vector3 moveDir = transform.forward.normalized; // ���� ��������
        Vector3 velocity = moveDir * speed + Vector3.up * _currentVelocityY;

        _characterController.Move(velocity * Time.deltaTime); // �̵��Ÿ� = �ӵ� * �ð�
    }

}
