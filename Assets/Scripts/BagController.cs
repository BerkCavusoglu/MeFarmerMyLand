using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    public List<ProductData> productDataList;
    private Vector3 productSize;
    [SerializeField] TextMeshPro maxText;
    int maxBagCapacity;
    private string bagCapacityKey = "bagCapacityKey";
    void Start()
    {
        maxBagCapacity = LoadBagCapacity();
    }

    
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            PlayShopSound();
            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                SellProductsToShop(productDataList[i]);
                Destroy(bag.transform.GetChild(i).gameObject);
                productDataList.RemoveAt(i);
            }
            ControlBagCapacity();
        }
        if (other.CompareTag("UnlockBakeryUnit"))
        {
            UnlockBakeryUnitController bakeryUnity = other.GetComponent<UnlockBakeryUnitController>();

            ProductType neededType = bakeryUnity.GetNeededProductType();

            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                if (productDataList[i].productType == neededType)
                {
                    if (bakeryUnity.StoreProduct() == true)
                    {
                        Destroy(bag.transform.GetChild(i).gameObject);
                        productDataList.RemoveAt(i);
                    }
                }

            }
            StartCoroutine(PutProductInOrder());           
            ControlBagCapacity();
        }
    }
    private void SellProductsToShop(ProductData productData)
    {
        CashManager.instance.ExchangeProduct(productData);
    }
    public void AddProductToBag(ProductData productData)
    {
      
        GameObject boxProduct = Instantiate(productData.productPrefab, Vector3.zero, Quaternion.identity);
        boxProduct.transform.SetParent(bag, true);
        CalculateObjectSize(boxProduct);
        float yPosition = CalculateNewYPositionOfBox();
        boxProduct.transform.localRotation = Quaternion.identity;
        boxProduct.transform.localPosition = Vector3.zero;
        boxProduct.transform.localPosition = new Vector3(0, yPosition, 0);
        productDataList.Add(productData);
        ControlBagCapacity();
    }
    private float CalculateNewYPositionOfBox()
    {
        float newYPos = productSize.y * productDataList.Count;
        return newYPos;
    }
    private void CalculateObjectSize(GameObject gameObject)
    {
        if (productSize == Vector3.zero)
        {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            productSize = renderer.bounds.size;
        }

    }
    private void ControlBagCapacity()
    {
        if (productDataList.Count == maxBagCapacity)
        {
            SetMaxTextOn();
        }
        else
        {
            SetMaxTextOff();
        }

    }
    private void SetMaxTextOn()
    {
        if (!maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(true);
        }

    }
    private void SetMaxTextOff()
    {
        if (maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(false);
        }

    }
    public bool IsEmptySpace()
    {
        if (productDataList.Count < maxBagCapacity)
        {
            return true;
        }
        return false;
    }
    private IEnumerator PutProductInOrder()
    {
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < bag.childCount; i++)
        {
            float newYPos = productSize.y * i;
            bag.GetChild(i).transform.localPosition = new Vector3(0, newYPos, 0);
        }
    }
    private void PlayShopSound()
    {
        if (productDataList.Count > 0)
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
            //AudioManager.instance.StopBackgroundMusic();
        }
    }
    public void BoostBagCapacity(int boostCount)
    {
        maxBagCapacity += boostCount;
        PlayerPrefs.SetInt(bagCapacityKey, maxBagCapacity);
        ControlBagCapacity();
    }
    private int LoadBagCapacity()
    {
        int maxBag = PlayerPrefs.GetInt(bagCapacityKey, 5);
        return maxBag;
    }
}
