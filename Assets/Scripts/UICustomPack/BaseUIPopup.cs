using UnityEngine;

namespace UICustomPack
{
    public abstract class BaseUIPopup : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvas;
        public abstract string PopupID { get; }

        public virtual void Show()
        {
            canvas.alpha = 1.0f;
            canvas.blocksRaycasts = true;
        }

        public virtual void Hide()
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
        }
    }

}

