using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;
    [SerializeField] private int LockedUnitID;
    bool isPowerUpUsed;
    string powerUpStatusKey = "powerUpStatusKey";
    // Start is called before the first frame update
    void Start()
    {
        isPowerUpUsed = GetPowerUpStatus(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerUpData.powerUpType == PowerUpType.bagBooster && !isPowerUpUsed)
            {
                isPowerUpUsed = true;
                BagController bagController = other.GetComponent<BagController>();
                bagController.BoostBagCapacity(powerUpData.boostCount);
                AudioManager.instance.PlayAudio(AudioClipType.grabClip);
                PlayerPrefs.SetString(powerUpStatusKey, "used");
            }
        }
    }
    private bool GetPowerUpStatus()
    {
        string status = PlayerPrefs.GetString(powerUpStatusKey, "ready");
        if (status.Equals("ready"))
        {
            return false;
        }
        return true;
    }
}
