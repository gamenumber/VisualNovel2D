using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputFieldController : MonoBehaviour
{
    public InputField nameInputField;
    public UnityEvent onEnterKey;


    private void Start()
    {
        // InputField에 포커스를 주어야 엔터 키 입력을 감지할 수 있습니다.
        nameInputField.ActivateInputField();
    }

    public void OnNameInputEndEdit(string input)
    {
        // 엔터 키가 눌렸을 때 이벤트 호출
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            onEnterKey.Invoke();
        }
    }
}
