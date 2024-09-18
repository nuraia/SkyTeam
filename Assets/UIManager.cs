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
    public GameObject blueSlotPlusOne;
    public GameObject blueSlotMinusOne;
    public GameObject DoneMinusOne;
    public GameObject DonePlusOne;
    public GameObject coffeePanel;
    public List<GameObject> CoffeebuttonLists = new List<GameObject>();
    public List<GameObject> CoffeeSlotLists = new List<GameObject>();
    public int coffee = 0;
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

    public void OnClickPlusOne()
    {
        coffee = 1;
        blueSlotPlusOne.SetActive(true);
        
    }
    public void OnClickMinusOne()
    {
        coffee = -1;
        blueSlotMinusOne.SetActive(true);
        
    }

    public void OnClickCancelButton()
    {
        coffeePanel.SetActive(false);
    }

    public void OnClickCoffeeButton()
    {
        coffeePanel.SetActive(true);
        
    }

    public void OnClickDoneButton()
    {
        if(CoffeebuttonLists.Count > 0 && CoffeeSlotLists.Count > 0)
        {
            CoffeebuttonLists[0].gameObject.SetActive(false);
            CoffeebuttonLists.RemoveAt(0);
            Destroy(CoffeeSlotLists[0].transform.GetChild(0).gameObject);
            CoffeeSlotLists.RemoveAt(0);
            blueSlotPlusOne.gameObject.SetActive(false);
            blueSlotMinusOne.gameObject.SetActive(false);
        }
    }

}
