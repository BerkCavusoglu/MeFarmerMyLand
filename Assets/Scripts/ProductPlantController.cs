using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    private bool isReadyToPick;
    private Vector3 originalScale;
    [SerializeField] private ProductData productData;
    private BagController bagController;

    void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Your update logic here
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToPick)
        {
            bagController = other.GetComponent<BagController>();
            if (bagController.IsEmptySpace())
            {
                AudioManager.instance.PlayAudio(AudioClipType.grabClip);
                bagController.AddProductToBag(productData);
                isReadyToPick = false;
                StartCoroutine(ProductsPicked());
            }
            
        }
    }

    IEnumerator ProductsPicked()
    {
        Vector3 targetScale = originalScale / 3;
        transform.gameObject.LeanScale(targetScale, 1f);
        yield return new WaitForSeconds(10f);
        transform.gameObject.LeanScale(originalScale, 1f).setEase(LeanTweenType.easeOutBack);
        isReadyToPick = true;
    }
}
