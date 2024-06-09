using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestTrigger : MonoBehaviour
{
    public Canvas uiCanvas; // UI Canvas
    public GameObject[] objectsToDestroy;
    public TextMeshProUGUI HeadText; // �޽����� ǥ���� TMP �ؽ�Ʈ
    public TextMeshProUGUI messageText;
    public TMP_InputField userInput; // ������� �Է��� ���� TMP InputField
    public int scoreIncrement = 50;
    public Animator playerAnimator;
    public GameObject StarObj;

    private bool isTriggered = false;
    private string[] messages; // �ؽ�Ʈ�� ������ �迭
    private string[] answers;
    private string Quiz;
    private bool isStarted = false;
    private int currentScore;
    private int newScore;
    private string Category;
    private int chance;
    private int Star;
    private int newStar;

    void Start()
    {

        chance = 1;
        currentScore = PlayerPrefs.GetInt("Score");
        Star = PlayerPrefs.GetInt("Star");
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
            Star = PlayerPrefs.GetInt("Star");
            uiCanvas.gameObject.SetActive(true); // UI ǥ��
            int randomIndex = Random.Range(0, messages.Length);
            Quiz = messages[randomIndex];
            if (gameObject.CompareTag("Chest")) // Ʈ������ ������ ���� �ٸ� �޽��� ǥ��
            {
                HeadText.text = "���� ���� �߰�!!";
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
                playerAnimator.SetTrigger("GetStar");
                foreach (GameObject obj in objectsToDestroy) { Destroy(obj); }
                uiCanvas.gameObject.SetActive(false);
                userInput.text = "";
                newScore = currentScore + scoreIncrement;
                newStar = Star + 1;
                isTriggered = false;
            }
            else
            {
                if (chance >= 1)
                {
                    messageText.text = $"{scoreIncrement}�� ����! ���� ��ȸ {chance} �� \n" + Quiz;
                    userInput.text = "";
                    userInput.ActivateInputField();
                    newScore = currentScore - scoreIncrement;
                    chance -= 1;
                }
                else
                {
                    messageText.text = $"{scoreIncrement}�� ����!";
                    uiCanvas.gameObject.SetActive(false);
                    userInput.text = "";
                    isTriggered = false;
                }
            }
            PlayerPrefs.SetInt("Score", newScore);
            currentScore = newScore;
            PlayerPrefs.SetInt("Star", newStar);
            Star = newStar;
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
}
