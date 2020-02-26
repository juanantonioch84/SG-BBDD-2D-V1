using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rt;
    private Animator _animator;

    public void Start()
    {
        GameObject inventory = transform.Find("InventoryPanel").gameObject;
        _rt = inventory.GetComponent<RectTransform>();
        _animator = inventory.GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool("opened", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("opened", false);
    }
}
