using UnityEngine;

namespace TestCode.Scripts
{
    public class TestAnim : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] float castTime;

        void Update()
        {
            // Base Layer (chạy, đi bộ...)
            float move = Input.GetAxis("Vertical");
            animator.SetFloat("Speed", Mathf.Abs(move));

            // Upper Body Layer (bắn súng)
            if (Input.GetMouseButtonDown(0)) // Chuột trái
            {
                animator.SetTrigger("UnderBody");
            }

            // Ví dụ chỉnh weight của layer 1 (UpperBody)
            // Layer 0 = Base Layer, Layer 1 = UpperBody
            float weight = Input.GetMouseButton(1) ? 1f : 0f; // giữ chuột phải thì enable layer
            animator.SetLayerWeight(1, weight);
        }

        public void OnChangeStateAnim()
        {
            float totalSlice = 0;
            for(int i = 0; i < totalSlice; i++)
            {
                totalSlice += castTime;
                totalSlice = Mathf.Clamp(totalSlice, 0f, 1f);
                float weight = Input.GetMouseButton(1) ? Mathf.Abs(totalSlice) : 0f;
                animator.SetLayerWeight(1, weight);

            }
        }
    }
}
