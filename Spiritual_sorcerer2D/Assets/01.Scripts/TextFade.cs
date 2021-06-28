using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class TextFade : MonoBehaviour
{
    [SerializeField]
    private Text startTxt = null;
    // Start is called before the first frame update
    void Start()
    {
        startTxt.DOColor(new Color(0f, 0f, 0f, 0f), 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void GameStart() 
    {
        SceneController.LoadScene("InGame");
        UIManager.Instance.CheckSkill();
    }


}
