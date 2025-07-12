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
            Debug.Log("[Scene] AudioSource を取得しました");
        }
    }

    public void OnClick()
    {
        Debug.Log("[Scene] OnClick 呼び出し - 音再生 & InGameシーン遷移待機開始");
        PlaySound();
        StartCoroutine(ChangeInGameScene());
    }
    
    private IEnumerator ChangeInGameScene()
    {
        Debug.Log($"[Scene] {delaySeconds} 秒後に InGame シーンに遷移します");
        yield return new WaitForSeconds(delaySeconds);
        Debug.Log("[Scene] InGame シーンに遷移します");
        SceneManager.LoadScene("InGame");
    }
    
    public void ChangeResultScene()
    {
        Debug.Log("[Scene] Result シーンに遷移します");
        SceneManager.LoadScene("Result");
    }

    public void ChangeStartScene()
    {
        Debug.Log("[Scene] Title シーンに遷移します");
        SceneManager.LoadScene("Title");
    }
    
    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioClip); // AudioClipを再生
            Debug.Log("[Scene] 効果音を再生しました");
        }
        else
        {
            Debug.LogWarning("[Scene] 効果音の再生に失敗：audioSourceまたはaudioClipが設定されていません");
        }
    }
}
