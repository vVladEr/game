using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMenu : MonoBehaviour
{
    public Text textUI;
    [SerializeField] private Animator transitionAnimator;
    private string text = "Рауди - едва ли не единственный мастер кузнечного дела своего города. Каждое утро он брал с Гефестовых Гор железо как дар кузнечных Божеств, а затем днями и ночами он ковал всё для своих жителей. \nНо вот одним утром Рауди не вернулся с этих Гор. Его связала местная прислуга - негоже было воровать местные богатства. Он очнулся в одной из темниц, с собой у него был только коротенький нож за пазухой. Оставаться здесь было нельзя - нужно было срочно спасаться. Он слышал этот удар Наковальни свыше, знал, что только двигаясь в такт с этим звуком, он останется незамеченным и сможет вернуться домой.";

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

