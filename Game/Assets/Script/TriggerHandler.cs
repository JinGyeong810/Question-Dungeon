using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerHandler : MonoBehaviour
{
    public GameObject[] objectsToDestroy; // ������ ������Ʈ��
    public Canvas uiCanvas; // UI Canvas
    public TextMeshProUGUI HeadText; // �޽����� ǥ���� TMP �ؽ�Ʈ
    public TextMeshProUGUI messageText;
    public TMP_InputField userInput; // ������� �Է��� ���� TMP InputField
    public int scoreIncrement = 10;
    public Animator playerAnimator;

    private bool isTriggered = false;
    private string[] messages; // �ؽ�Ʈ�� ������ �迭
    private string[] answers;
    private string Quiz;
    private bool isStarted = false;
    private int currentScore;
    private int newScore;
    private string Category;
    private int chance;

    void Start()
    {
        chance = 3;
        currentScore = PlayerPrefs.GetInt("Score");
        Category = PlayerPrefs.GetString("Category");
        uiCanvas.gameObject.SetActive(false); // �ʱ⿡�� UI�� ����
        userInput.onEndEdit.AddListener(CheckAnswer); // ����ڰ� �Է��� ��ġ�� CheckAnswer �޼��� ȣ��

        TextAsset csvFile = Resources.Load<TextAsset>(Category); // Resources ������ �ִ� CSV ���� �ε�
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n'); // �� �ٲ��� �������� �ؽ�Ʈ�� ����
            messages = new string[lines.Length];
            answers = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(','); // ��ǥ�� �������� ���� ����
                messages[i] = values[0].Trim(); // ù ��° ���� �ؽ�Ʈ�� �޽����� ����
                answers[i] = values[1].Trim(); // �� ��° ���� �ؽ�Ʈ�� �������� ����
            }
        }
    }

    void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                userInput.ActivateInputField();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ Ʈ���ſ� ����� ��
        {
            isTriggered = true;
            currentScore = PlayerPrefs.GetInt("Score");
            uiCanvas.gameObject.SetActive(true); // UI ǥ��
            int randomIndex = Random.Range(0, messages.Length);
            Quiz = messages[randomIndex];
            if (gameObject.CompareTag("ArrowTrap")) // Ʈ������ ������ ���� �ٸ� �޽��� ǥ��
            {
                HeadText.text = "ȭ�� ���� �߰�!";// �޽��� ����
            }
            else if (gameObject.CompareTag("FireTrap"))
            {
                HeadText.text = "�Ҳ� ���� �߰�!"; // �޽��� ����
            }
            else if (gameObject.CompareTag("TrunkTrap"))
            {
                HeadText.text = "�볪�� ���� �߰�!"; // �޽��� ����
            }
            messageText.text = Quiz;
        }
    }

    void CheckAnswer(string answer)
    {
        if (isTriggered)
        {
            if (answer == GetAnswer(Quiz))
            {
                playerAnimator.SetTrigger("CorrectAnswer");
                StartCoroutine(WaitAndDestroyObjects(1.2f));
                uiCanvas.gameObject.SetActive(false);
                userInput.text = "";
                newScore = currentScore + scoreIncrement;
                isTriggered = false;
            }
            else
            {
                if (chance >= 1)
                {
                    playerAnimator.SetTrigger("WrongAnswer");
                    messageText.text = $"{scoreIncrement}�� ����! ���� ��ȸ {chance} �� \n" + Quiz;
                    userInput.text = "";
                    userInput.ActivateInputField();
                    newScore = currentScore - scoreIncrement;
                    chance -= 1;
                }
                else
                {
                    playerAnimator.SetTrigger("WrongAnswer");
                    messageText.text = $"{scoreIncrement}�� ����!";
                    uiCanvas.gameObject.SetActive(false);
                    userInput.text = "";
                    isTriggered = false;
                }
            }
            PlayerPrefs.SetInt("Score", newScore);
            currentScore = newScore;
            }
        }
        string GetAnswer(string message)
        {
            for (int i = 0; i < messages.Length; i++)
            {
                if (messages[i] == message)
                {
                    return answers[i];
                }
            }
            return null; // ��ġ�ϴ� �޽����� ���� ���
        }
    private IEnumerator WaitAndDestroyObjects(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // waitTime��ŭ ���
        foreach (GameObject obj in objectsToDestroy) { Destroy(obj); }
    }
}
