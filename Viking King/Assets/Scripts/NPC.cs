using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject bubble;
    public TextMeshProUGUI bubbleTMP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player scriptPlayer = collision.GetComponent<Player>();
            StartCoroutine(GM.instance.CameraVol_down());

            //대사지정
            switch (GM.instance.eProgress)
            {
                case GM.Progress.퀘스트받기전:
                    bubbleTMP.text = "체리를 두번째로 먹어";
                    GM.instance.eProgress = GM.Progress.퀘스트받음_수행X;
                    break;
                case GM.Progress.퀘스트받음_수행X:
                    bubbleTMP.text = "어서 먹고 와";
                    break;
                case GM.Progress.퀘스트받음_수행O:
                    //NPC에서 플레이어의.itemNames 접근방법
                    //1. 변수로 선언
                    //2. 싱글톤
                    //3. collision활용
                    if(scriptPlayer.itemNames[1] == "cherry")
                    {
                        //퀘스트를 제대로O
                        bubbleTMP.text = "잘했어.";
                        GM.instance.eProgress = GM.Progress.퀘스트완료;
                    }
                    else
                    {
                        //퀘스트를 제대로X
                        bubbleTMP.text = "잊지마," + "\n" + "체리는 2번째야";
                        GM.instance.eProgress = GM.Progress.퀘스트받음_수행X;
                        scriptPlayer.itemNames.Clear();
                    }
                    break;
                case GM.Progress.퀘스트완료:
                    bubbleTMP.text = "수고했어";
                    break;
            }

            bubble.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(GM.instance.CameraVol_up());
            bubble.SetActive(false);
        }
    }
}
