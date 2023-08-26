using UnityEngine;
using UnityEngine.UI;

namespace Crossoverse.App.LiveViewer.Presentation.DefaultScreen
{
    public class DefaultScreenView : MonoBehaviour
    {
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private Text _uiText;

        public void SetCanvasActiveState(bool isActive)
        {
            _uiCanvas.gameObject.SetActive(isActive);
        }

        public void SetDisplayText(string message)
        {
            _uiText.text = message;
        }
    }
}
