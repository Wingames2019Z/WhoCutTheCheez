using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject ShopSheet;
    [SerializeField] Image ShopCover;

    [SerializeField] GameObject RestoreButton;
    [SerializeField] GameObject NoAdsButton;
    ShopDataModel ShopDataModel;
    Sequence sequence;
    float AnimeDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        SetActiveSheet(false);
        DisableRestorePurchaseButton();
        ShopSheet.transform.localScale = Vector3.zero;
        ShopDataModel = GameDataSystem.ShopDataLoad();
        if (ShopDataModel.NoAd)
        {
            NoAdsButton.SetActive(false);
        }
    }

    public void OnPurchaseComplete(Product product)
    {
        switch (product.definition.id)
        {
            case "com.wingames.whocutthecheez.noads":
                SetNoAd();
                break;
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of " + product.definition.id + " failed due to " + reason);
    }

    private void DisableRestorePurchaseButton()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            RestoreButton.SetActive(false);
        }
    }
    void SetNoAd()
    {
        ShopDataModel.NoAd = true;
        GameDataSystem.ShopDataSave(ShopDataModel);
        StoreClose();
    }

    public void StoreOpen()
    {
        if (ShopDataModel.NoAd)
        {
            NoAdsButton.SetActive(false);
        }
        SetActiveSheet(true);
        sequence = DOTween.Sequence();
        sequence.Append(ShopSheet.transform.DOScale(Vector3.one, AnimeDuration).SetEase(Ease.OutBack))
                .Join(ShopCover.DOFade(1f, AnimeDuration));


    }
    public void StoreClose()
    {
        sequence = DOTween.Sequence();
        sequence.Append(ShopSheet.transform.DOScale(Vector3.zero, AnimeDuration).SetEase(Ease.InBack))
                .Join(ShopCover.DOFade(0f, AnimeDuration))
                .OnComplete(()=> { SetActiveSheet(false); });
    }
    void SetActiveSheet(bool state)
    {
        ShopSheet.SetActive(state);
        ShopCover.gameObject.SetActive(state);
    }
}
