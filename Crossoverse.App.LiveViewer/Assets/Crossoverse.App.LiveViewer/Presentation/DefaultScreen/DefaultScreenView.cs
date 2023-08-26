using UnityEngine;
using UnityEngine.UI;

namespace Crossoverse.App.LiveViewer.Presentation.DefaultScreen
{
    public class DefaultScreenView : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void SetText(string message)
        {
            text.text = message;
        }
    }
}
