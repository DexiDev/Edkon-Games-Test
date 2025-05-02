using UnityEngine;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate.UI.Specifics
{
    public class UIAlert : MonoBehaviour
    {
        [SerializeField] private Text _textField;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _triggerKey;
        
        public void ShowMessage(string message)
        {
            _textField.text = message;
            _animator.SetTrigger(_triggerKey);
        }
    }
}