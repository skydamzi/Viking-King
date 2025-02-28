using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GM : MonoBehaviour
{
    #region 싱글톤
    static public GM instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public enum Progress { 퀘스트받기전, 퀘스트받음_수행X, 퀘스트받음_수행O, 퀘스트완료 };
    public Progress eProgress = Progress.퀘스트받기전;
    public TextMeshProUGUI display;
    public GameObject canSNL;
    public GameObject canSound;
    public Transform playerTR;
    public AudioSource audio_camera;
    public Slider slider_bg;
    public Slider slider_effect;
    public AudioMixer mixer;
    public GameObject SonGay;
    public GameObject NaGay;

    void Update()
    {
        display.text = eProgress.ToString();    
        if(Input.GetButtonDown("Cancel"))
        {
            canSNL.SetActive(!canSNL.activeSelf);
        }
    }

    public void SetCanSound_on()
    {
        canSound.SetActive(true);
    }
    public void SetCanSound_off()
    {
        canSound.SetActive(false);
    }

    public void Controll_bg()
    {
        audio_camera.volume = slider_bg.value;
    }
    public void Controll_bg2()
    {
        if(slider_bg.value == -40)
        {
            mixer.SetFloat("Para_bg", -80);
        }
        else
        {
            mixer.SetFloat("Para_bg", slider_effect.value);
        }
    }
    public void Controll_effect()
    {
        if(slider_effect.value == -40)
        {
            mixer.SetFloat("Para_effect", -80);
        }
        else
        {
            mixer.SetFloat("Para_effect", slider_bg.value);
        }
    }
    public IEnumerator CameraVol_down()
    {
        while(audio_camera.volume > 0.05f)
        {
            audio_camera.volume -= 0.01f;
            yield return null;
        }
        audio_camera.volume = 0;
    }
    public IEnumerator CameraVol_up()
    {
        while (audio_camera.volume < 0.4f)
        {
            audio_camera.volume += 0.01f;
            yield return null;
        }
        audio_camera.volume = 0.4f;
    }



    public void Button_continu()
    {
        canSNL.SetActive(false);
    }
    public void Button_save()
    {
        //퀘스트진행도
        PlayerPrefs.SetInt("questPro", (int)eProgress);
        //캐릭터의 위치
        PlayerPrefs.SetFloat("posX" , playerTR.position.x);
        PlayerPrefs.SetFloat("posY", playerTR.position.y);
        PlayerPrefs.SetFloat("posZ", playerTR.position.z);

        Debug.Log($"저장완료. 퀘스트진행도: {PlayerPrefs.GetFloat("questPro")}");
    }
    public void Button_Load()
    {
        //저장한 값을 불러오기
        int intPro = PlayerPrefs.GetInt("questPro");
        float playerX =  PlayerPrefs.GetFloat("posX");
        float playerY = PlayerPrefs.GetFloat("posY");
        float playerZ = PlayerPrefs.GetFloat("posZ");

        //불러온 값을 대입
        eProgress = (Progress)intPro;
        playerTR.position = new Vector3(playerX, playerY, playerZ);

        Debug.Log("불러오기 완료");
    }
    public void Button_Exit()
    {
        //(이)전처리기
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
