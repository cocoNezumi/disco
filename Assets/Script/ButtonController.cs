using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public string moveName; // �������� ��������

    public string keyToPress;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(Input.GetButtonDown(keyToPress))
        {
            theSR.sprite = pressedImage;
            GameManager.instance.dancer.Play(moveName); // ����������� �������� � �������������� ���������
        }

        if(Input.GetButtonUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }
    }
}
