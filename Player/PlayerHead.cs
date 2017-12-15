using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerHead : MonoBehaviour {

        private Camera cam;
        Reticle reticle;
        public GameObject focused;
        private GameObject last_focused;
        public Color color;
        // Use this for initialization
        void Start () {
            originalRotation = gameObject.transform.localRotation;
            cam = gameObject.GetComponentInChildren<Camera>();
            reticle = new Reticle(2, 8);
            reticle.SetColor(color);
            SetMin();
        }

        public GameObject GetFocused()
        {
            return focused;
        }

        public void FixedUpdate()
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            last_focused = focused;
            focused = null;
            if (Physics.Raycast(ray, out hit, 8f))
            {
                focused = hit.collider.gameObject.GetComponent<IInteractable>() != null ? 
                    hit.collider.gameObject : 
                    null;
            }
            if (last_focused == focused) return;
            
            if (focused == null) {
                SetMin();
            }
            else
            {
                SetMax();
            }
        }
        /// <summary>
        /// Used for rotations. 
        /// </summary>
        public float rotationY = 0;
        public Quaternion originalRotation;
        public void Move(float delta)
        {
            rotationY += delta;
            rotationY = ClampAngle(rotationY, -90, 90);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
        /// <summary>
        /// Used for rotations. 
        /// </summary>
        float ClampAngle(float angle,float min, float max){
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
            return Mathf.Clamp(angle, min, max);
        }

        /*   CROSSHAIR    */

        void OnGUI()
        {
            int max = reticle.max;
            GUI.DrawTexture(new Rect(Screen.width / 2 - max, Screen.height / 2 - max, max * 2, max * 2), (Texture)reticle.GetReticle(),ScaleMode.ScaleToFit);
        }

        public void SetMax()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", reticle.currentSize, "to", reticle.max, "time", 0.2f, "onupdate","tweenValue","easetype",iTween.EaseType.easeOutQuad));
        }

        public void SetMin()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", reticle.currentSize, "to", reticle.min, "time", 0.2f, "onupdate", "tweenValue", "easetype", iTween.EaseType.easeInQuad));
        }

        private void tweenValue(float f)
        {
            reticle.currentSize = f;
        }

        private class Reticle
        {
            public int min, max;
            private bool isCircle;
            private Color color;
            public float currentSize;

            public Reticle(int min, int max, bool isCircle=true)
            {
                this.min = min;
                this.max = max;
                this.isCircle = isCircle;
                color = new Color(0.9f,0.9f,0.9f,0.5f);
                currentSize = this.min;
            }

            public void SetColor(Color color)
            {
                this.color = color;
            }

            public Texture2D GetReticle()
            {
                Texture2D reticle = new Texture2D(max * 2, max * 2);
                Vector2 middle = new Vector2(max, max);
                if (isCircle)
                {
                    for(int x = 0; x < max*2; x++)
                    {
                        for (int y = 0; y < max*2; y++)
                        {
                            Vector2 newVec = new Vector2(x, y);
                            float distance = Vector2.Distance(middle, newVec);
                            if (distance < currentSize)
                            {
                                Color c = color;
                                c.a = (distance / currentSize)*(distance / currentSize) * (distance / currentSize);
                                reticle.SetPixel(x, y, c);
                            } else
                            {
                                reticle.SetPixel(x, y, Color.clear);
                            }
                        }
                    }
                }
                reticle.Apply();
                reticle.filterMode = FilterMode.Trilinear;
                reticle.Apply();
                return reticle;
            }

        }

    }
}
