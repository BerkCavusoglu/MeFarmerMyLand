using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockBakeryUnitController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI binaryText;
    [SerializeField] private int maxStoreProductCount;
    [SerializeField] private ProductType productType;
    private int storeProductCount;
    [SerializeField] private int UseProductInSeconds = 10;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private GameObject coinGO;
    private float time;
    [SerializeField] private ParticleSystem smokeParticle;

    void Start()
    {

    }

    void Update()
    {
        if (storeProductCount > 0)
        {
            time += Time.deltaTime;

            if (time >= UseProductInSeconds)
            {
                time = 0.0f;
                UseProduct();
            }
        }
    }

    private void DisplayProductCount()
    {
        binaryText.text = storeProductCount.ToString() + "/" + maxStoreProductCount.ToString();
        ControlSmokeEffect();
    }

    public ProductType GetNeededProductType()
    {
        return productType;
    }

    public bool StoreProduct()
    {
        if (maxStoreProductCount == storeProductCount)
        {
            return false;
        }
        storeProductCount++;
        DisplayProductCount();
        return true;
    }

    private void UseProduct()
    {
        storeProductCount--;
        DisplayProductCount();
        CreateCoin();
    }

    private void CreateCoin()
    {
        Vector3 position = Random.insideUnitSphere * 1f;
        Vector3 instantiatePos = coinTransform.position + position;

        Instantiate(coinGO, instantiatePos, Quaternion.identity);
    }
    private void ControlSmokeEffect()
    {
        if (storeProductCount > 0)
        {
            if (smokeParticle.isPlaying)
            {
                smokeParticle.Stop();

            }

        }
        else
        {
            if (smokeParticle.isStopped)
            {
               smokeParticle.Play();    
            }
        }
    }
}
