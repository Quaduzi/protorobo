using UnityEngine;

namespace UI
{
    public class Background : MonoBehaviour
    {
        public float speed;
        public float startPos;
        public float endPos;
        void Update()
        {
            transform.Translate(Vector2.down * (speed * Time.deltaTime));
            if (transform.position.y <= endPos)
            {
                var pos = new Vector3(transform.position.x, startPos, transform.position.z);
                transform.position = pos;
            }
        }
    }
}
