using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text label;
    public GameObject panelToggle;
    public event Action<bool> ONActiveEvent;
    private bool _isActive = false;

    private void Start()
    {
        if(panelToggle) panelToggle.SetActive(false);
        if(label) label.gameObject.SetActive(true);
    }

    public void SetData(int index)
    {
        if(label) label.text = "Элемент " + index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ActiveToggle(!_isActive);
    }

    private void ActiveToggle(bool state)
    {
        _isActive = state;
        ONActiveEvent?.Invoke(state);
        if(panelToggle) panelToggle.SetActive(state);
    }
}