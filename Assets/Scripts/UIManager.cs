using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI popUpMessage;
    [SerializeField] private float animationDuration;
    private Vector3 targetScale = new Vector3(1, 1, 1);
    private Vector3 TextInitialScale = new Vector3(0, 0, 0);
    
    public GameObject coffeePanel;
    public DiceInstance currentlyUsedCoffeeToken;
    public int currentlyUsedCoffeeTokenIndex;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    public void PopMessage(string message)
    {
        popUpMessage.gameObject.SetActive(true);
        popUpMessage.text = message;
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

  

    public void OnClickCancelButton()
    {
        coffeePanel.SetActive(false);
        DiceManager.Instance.DiceChangeSlot.GetComponent<Image>().sprite = null;
        currentlyUsedCoffeeToken = null;
        currentlyUsedCoffeeTokenIndex  = -1;
    }

    //public void OnClickCoffeeButton(int index)
    //{
    //    coffeePanel.SetActive(true);
    //    index = Math.Min(index, DiceManager.Instance.CoffeeButtonLists.Count-1);
       
    //    currentlyUsedCoffeeToken = DiceManager.Instance.CoffeeSlotLists[index].GetComponentInChildren<DiceInstance>();
    //    currentlyUsedCoffeeTokenIndex = index;
    //}

   
}
