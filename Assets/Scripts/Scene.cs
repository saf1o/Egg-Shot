using UnityEngine;
using UnityEngine.SceneManagement;

//　シーンの遷移を行う
public class Scene : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 20f;// 遷移までの待機時間
    [SerializeField] private string NextSceneName = "Result";// 遷移先のシーン名
    public void ChangeInGameScene()
    {
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
}
