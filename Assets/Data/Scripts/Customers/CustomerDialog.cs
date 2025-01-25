using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomerDialog : MonoBehaviour
{

    [SerializeField] private RectTransform _bubble; //imagen con el bocadillo
    [SerializeField] private TextMeshProUGUI _message; //texto


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _bubble.gameObject.SetActive(false);
        _message.enabled = false;

        _message.DOFade(0, 0);
        _bubble.GetComponentInChildren<Image>()
            .DOFade(0, 0f);
    }




    public void SetBubbleMessage(String message)
    {
        //set the message
        _message.text = message;
        _message.ForceMeshUpdate(true, true);
        //resize the bubble


        _bubble.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(_message.renderedWidth * 1.25f, _message.renderedHeight * 1.25f);

        //enable the things
        _bubble.gameObject.SetActive(true);
        _message.enabled = true;
        //anime the thing
        _message.DOFade(1, 2);
        _bubble.GetComponentInChildren<Image>()
            .DOFade(1, 1.5f);
        //yeah that's all about it
    }

    public void HideBubbleMessage()
    {
        _message.DOFade(0, 1)
            .OnComplete(() =>
            {
                _message.enabled = false;
            });
        _bubble.GetComponentInChildren<Image>()
            .DOFade(0, 0.75f).OnComplete(() =>
            {
                _bubble.gameObject.SetActive(false);
            });



    }
}
