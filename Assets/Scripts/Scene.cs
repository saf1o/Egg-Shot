using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//　シーンの遷移を行う
public class Scene : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 2f;// 遷移までの待機時間
    [SerializeField] public AudioClip audioClip;
    private AudioSource audioSource; // InspectorからAudioSourceを設定

    void Start()
    {
        // もしAudioSourceがInspectorが設定されていなければ、自身から取得
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void OnClick()
    {
        PlaySound();
        StartCoroutine(ChangeInGameScene());
    }
    private IEnumerator ChangeInGameScene()
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("InGame");
    }
    
    public void ChangeResultScene()
    {
        SceneManager.LoadScene("Result");
    }

    public void ChangeStartScene()
    {
        SceneManager.LoadScene("Title");
    }
    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioClip); // AudioClipを再生
        }
    }
}
