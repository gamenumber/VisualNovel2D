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
        // InputField�� ��Ŀ���� �־�� ���� Ű �Է��� ������ �� �ֽ��ϴ�.
        nameInputField.ActivateInputField();
    }

    public void OnNameInputEndEdit(string input)
    {
        // ���� Ű�� ������ �� �̺�Ʈ ȣ��
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            onEnterKey.Invoke();
        }
    }
}
