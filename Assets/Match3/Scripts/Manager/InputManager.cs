using Match3.SubScripts;
using UnityEngine;

namespace Match3.Manager
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] Grid grid;
        [SerializeField] protected Vector3 mouseWorldPos;
        [SerializeField] protected bool isLeftMouseButtonDown;
        [SerializeField] protected bool isRightMouseButtonDown;
        [SerializeField] protected bool arrowLeft;
        [SerializeField] protected bool arrowRight;
        [SerializeField] protected bool arrowUp;
        [SerializeField] protected bool arrowDown;
        [SerializeField] protected bool isMouseButtonUp;

        public bool mouseHold;
        protected bool squareSelected;
        protected bool xSelected;
        private Vector3 lastMousePosition;
        private bool isDragging;


        public bool IsLeftMouseButtonDown => isLeftMouseButtonDown;
        public bool IsRightMouseButtonDown => isRightMouseButtonDown;
        public bool SquareSelected => squareSelected;
        public bool XSelected => xSelected;

        public bool IsMouseButtonUp => isMouseButtonUp;
        public bool IsDragging => isDragging;

        public bool Disable { get; set; }

        public void UpdateInput()
        {
            GetMousePos();
            GetMouseDown();
        }

        public void GetMousePos()
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
        }

        private void GetMouseDown()
        {
            isLeftMouseButtonDown = Input.GetMouseButtonDown(0);
            isRightMouseButtonDown = Input.GetMouseButtonDown(1);

            if (isLeftMouseButtonDown)
            {
                mouseHold = true;
                lastMousePosition = mouseWorldPos;
            }
            if (isMouseButtonUp = Input.GetMouseButtonUp(0))
            {
                mouseHold = false;
                isDragging = false;
            }

            if (mouseHold && (mouseWorldPos != lastMousePosition))
            {
                isDragging = true;
            }
        }
       

        public Vector3Int MousePos() => grid.WorldToCell(mouseWorldPos);
        
    }
}

