using Match3.Subscripts;
using UnityEngine;

namespace Match3.Scripts
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] Board board;
        [SerializeField] AnimateLayer animLayer;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            board.UpdateBoard();
        }

        public void Init()
        {
            Match3Sub.LoadCellTileData();
            board.Init(animLayer);
        }
    }
}

