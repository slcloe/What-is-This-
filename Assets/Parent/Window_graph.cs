using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_graph : MonoBehaviour
{
	[SerializeField] private Sprite circleSprite;
	private RectTransform graphContainer;
	private RectTransform labelTemplateX;
	private RectTransform labelTemplateY;

	private void Awake()
	{
		graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
		labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
		labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

		List<float> valueList = new List<float>() { 5, 100, 56, 45, 30, 22, 33, 100 };
		List<float> valueList1 = new List<float>() { 10, 20, 30, 40, 50, 60, 70, 80 };
		ShowGraph(valueList);
		ShowGraph(valueList1);
	}

	private GameObject CreateCircle(Vector2 anchoredPosition)
	{
		GameObject gameObject = new GameObject("circle", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().sprite = circleSprite;
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = anchoredPosition;
		rectTransform.sizeDelta = new Vector2(11, 11);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		return gameObject;
	}

	private void ShowGraph(List<float> valueList)
	{
		float graphHeight = graphContainer.sizeDelta.y;
		float yMaximum = 100f;
		float xSize = 70f;

		GameObject lastCircleGameObject = null;
		for (int i = 0; i < valueList.Count; i++)
		{
			float xPosition = (xSize + i * xSize);
			float yPosition = (valueList[i] / yMaximum) * graphHeight;
			GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
			if (lastCircleGameObject != null)
			{
				CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);

			}
			lastCircleGameObject = circleGameObject;

			RectTransform labelX = Instantiate(labelTemplateX);
			labelX.SetParent(graphContainer);
			labelX.gameObject.SetActive(true);
			labelX.anchoredPosition = new Vector2(xPosition, -20f);
			labelX.GetComponent<Text>().text=i.ToString();
		}
		int separatorCount = 10;
		for (int i = 0; i <= separatorCount; i++)
		{
			RectTransform labelY = Instantiate(labelTemplateY);
			labelY.SetParent(graphContainer, false);
			labelY.gameObject.SetActive(true);
			float normalizedValue = i * 1f / separatorCount;
			labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
			labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();
		}
	}

	private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
	{
		GameObject gameObject = new GameObject("dotConnection", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		Vector2 dir = (dotPositionB - dotPositionA).normalized;
		float distance = Vector2.Distance(dotPositionA, dotPositionB);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		rectTransform.sizeDelta = new Vector2(distance, 3f);
		rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		rectTransform.localEulerAngles = new Vector3(0, 0, angle);
	}
}
