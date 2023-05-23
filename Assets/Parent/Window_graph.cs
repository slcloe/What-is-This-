using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSet
{
	public float r, g, b;
	public float a;
	public ColorSet(float r, float g, float b, float a)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
	}
}

public class Window_graph : MonoBehaviour
{
	[SerializeField] private Sprite circleSprite;
	private RectTransform graphContainer;
	private RectTransform labelTemplateX;
	private RectTransform labelTemplateY;

	private void Start()
	{
		//Debug.Log("graphdirector");
		graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
		labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
		labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

		List<float> valueList = new List<float>() { 5, 100, 56, 45, 30, 22, 33, 100 };
		List<float> valueList1 = new List<float>() { 10, 20, 30, 40, 50, 60, 70, 80 };
		List<float> valueList2 = new List<float>() { 7, 22, 40, 65, 55, 70, 80, 90 };

		//ColorSet color1 = new ColorSet(0,1,0,1);
		//ColorSet color2 = new ColorSet(1,0,0,1);
		ColorSet color1 = new ColorSet(130f / 255f, 169f / 255f, 102f / 255f, 1);
		ColorSet color2 = new ColorSet(255f / 255f, 192f / 255f, 0f / 255f, 1);
		ColorSet color3 = new ColorSet(244f / 255f, 149f / 255f, 101f / 255f, 1);

		ShowGraph(valueList,color1);
		ShowGraph(valueList1,color2);
		ShowGraph(valueList2, color3);
		ShowGraph(ParentDirector.successRate1, color1);
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

	private void ShowGraph(List<float> valueList, ColorSet color)
	{
		float graphHeight = graphContainer.sizeDelta.y;
		float yMaximum = 100f;
		float xSize = 90f;

		GameObject lastCircleGameObject = null;
		for (int i = 0; i < valueList.Count; i++)
		{
			float xPosition = (xSize + i * xSize);
			float yPosition = (valueList[i] / yMaximum) * graphHeight;
			GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
			if (lastCircleGameObject != null)
			{
				CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, color);

			}
			lastCircleGameObject = circleGameObject;

			RectTransform labelX = Instantiate(labelTemplateX);
			labelX.SetParent(graphContainer);
			labelX.gameObject.SetActive(true);
			labelX.anchoredPosition = new Vector2(xPosition, -20f);
			labelX.GetComponent<Text>().text=(i + 1).ToString() + "ÁÖÂ÷";
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

	private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, ColorSet color)
	{
		GameObject gameObject = new GameObject("dotConnection", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, color.a);
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
