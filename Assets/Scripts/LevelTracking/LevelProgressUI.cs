using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace LevelTracking
{
    public class LevelProgressUI : MonoBehaviour
    {
        public RectTransform fillBG;
        public Image fillBar;
        public RectTransform nodeParent;
        public LevelNodeUI nodePrefab;
        public float nodeSpacing = 100f;
        public LevelProgressDataSO data;

        public RectTransform scrollContent; // ← Add this line

        private int currentLevel;
        private List<LevelNodeUI> levelNodes;

        void Start()
        {
            currentLevel = data.Data.currentLevel;
            Init();
        }

        public void Init()
        {
            ResizeFillBar();
            levelNodes = new List<LevelNodeUI>();
            for (int i = 0; i < data.Data.totalLevels; i++)
            {
                LevelNodeUI node = Instantiate(nodePrefab, nodeParent);
                node.Setup(i + 1, i <= data.Data.currentLevel);
                levelNodes.Add(node);
            }

            float progress = data.Data.currentLevel / (float)(data.Data.totalLevels - 1);
            fillBar.fillAmount = Mathf.Clamp01(progress);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentLevel < data.Data.totalLevels)
                {
                    currentLevel++;
                    UpdateProgress(currentLevel);
                }
            }
        }

        void ResizeFillBar()
        {
            float width = (data.Data.totalLevels - 1) * nodeSpacing;

            // Resize all horizontally
            scrollContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            fillBG.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            fillBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            nodeParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        public void UpdateProgress(int newLevel)
        {
            currentLevel = newLevel;

            // ✅ Đoạn bạn cần chèn ở đây:
            RectTransform firstNode = levelNodes[0].GetComponent<RectTransform>();
            RectTransform targetNode = levelNodes[currentLevel - 1].GetComponent<RectTransform>();

            float startX = firstNode.anchoredPosition.x;
            float endX = targetNode.anchoredPosition.x + targetNode.rect.width / 2f;

            float totalWidth = scrollContent.rect.width;
            float fillAmount = Mathf.Clamp01((endX - startX) / totalWidth);

            fillBar.fillAmount = fillAmount;

            // Update node states như trước
            for (int i = 0; i < levelNodes.Count; i++)
            {
                var node = levelNodes[i];
                if (i + 1 < currentLevel)
                    node.SetState(State.Completed);
                else if (i + 1 == currentLevel)
                    node.SetState(State.Current);
                else
                    node.SetState(State.Locked);
            }
        }

    }
}

