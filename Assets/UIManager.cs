using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI popUpMessage;
    [SerializeField] private float animationDuration;
    private Vector3 targetScale = new Vector3(1, 1, 1);
    private Vector3 TextInitialScale = new Vector3(0, 0, 0);

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    public void PopMessage()
    {
        popUpMessage.gameObject.SetActive(true);
        popUpMessage.text = "Game Starts";
        popUpMessage.transform.DOScale(targetScale, animationDuration)
            .OnComplete(() =>
            {
                StartCoroutine(ScaleDown());
            });

    }
    IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds(1f);
        popUpMessage.transform.DOScale(TextInitialScale, animationDuration);
        GameManager.Instance.gameObject.SetActive(false);
    }
}
