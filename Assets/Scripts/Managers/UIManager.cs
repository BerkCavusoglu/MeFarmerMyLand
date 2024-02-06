using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI coinCountText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }
    public void ShowCoinCountOnScreen(int coins)
    {
        coinCountText.text = coins.ToString();
    }
}
