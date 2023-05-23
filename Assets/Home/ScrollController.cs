using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtensions
{
    public static void ScrollToFront(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
    public static void ScrollToEnd(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(1, 0);
    }
}

public class ScrollController : MonoBehaviour
{
    ScrollRect scrollRect;
    void Start()
    {
        scrollRect = transform.gameObject.GetComponent<ScrollRect>();
        ScrollRectExtensions.ScrollToEnd(scrollRect);
    }

    void Update()
    {

    }
}