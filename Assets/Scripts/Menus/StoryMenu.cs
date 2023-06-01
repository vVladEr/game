using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMenu : MonoBehaviour
{
    public Text textUI;
    [SerializeField] private Animator transitionAnimator;
    private string text = "����� - ���� �� �� ������������ ������ ���������� ���� ������ ������. ������ ���� �� ���� � ���������� ��� ������ ��� ��� ��������� �������, � ����� ����� � ������ �� ����� �� ��� ����� �������. \n�� ��� ����� ����� ����� �� �������� � ���� ���. ��� ������� ������� �������� - ������ ���� �������� ������� ���������. �� ������� � ����� �� ������, � ����� � ���� ��� ������ ����������� ��� �� �������. ���������� ����� ���� ������ - ����� ���� ������ ���������. �� ������ ���� ���� ���������� �����, ����, ��� ������ �������� � ���� � ���� ������, �� ��������� ������������ � ������ ��������� �����.";

    void Start()
    {
        StartCoroutine("showText", text);
    }

    IEnumerator showText(string text)
    {
        int i = 0;
        while (i <= text.Length)
        {
            textUI.text = text.Substring(0, i);
            i++;

            yield return new WaitForSeconds(0.05f);
        }
        transitionAnimator.Play("TransitionExit");
    }
}

