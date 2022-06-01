using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Defines.Scenes SceneType { get; protected set; } = Defines.Scenes.Unknown;
    public string SceneName { get; protected set; } = "Unknown";

    private void Awake()
    {
        Init();
        SceneSetting();
        ObjectInit();
    }

    protected virtual void Init()
    {
        // ���ӿ� �ʼ������� �ʿ��� EventSystem�� ��� Scene���� ���� ó��
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("Common/EventSystem").name = "@EventSystem";
    }

    // �� Child Scene�鿡 �ʿ��� �������� �ż��� �߻�ȭ
    protected abstract void SceneSetting();
    protected abstract void ObjectInit();
    public abstract void Clear();
}
