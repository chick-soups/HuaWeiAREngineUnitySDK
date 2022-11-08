namespace Common
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Toast:MonoBehaviour
    {

        GameObject m_Text;
        GameObject m_Image;

        void Start()
        {
            m_Image = transform.Find("Image").gameObject;
            m_Text = transform.Find("Text").gameObject;
            _SetActive(false);
        }

        public void ShowToast(string message)
        {
            CancelInvoke("_Hide");
            _SetActive(true);
            var texCom = m_Text.GetComponent<Text>();
            texCom.text = message;
            Invoke("_Hide", 1.5f);
        }

        private void _Hide()
        {
            _SetActive(false);
        }
        private void _SetActive(bool active)
        {
            gameObject.SetActive(active);
            m_Image.SetActive(active);
            m_Text.SetActive(active);
        }
    }
}