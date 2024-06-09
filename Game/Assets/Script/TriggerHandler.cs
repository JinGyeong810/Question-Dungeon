using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerHandler : MonoBehaviour
{
    public GameObject[] objectsToDestroy; // 삭제할 오브젝트들
    public Canvas uiCanvas; // UI Canvas
    public TextMeshProUGUI HeadText; // 메시지를 표시할 TMP 텍스트
    public TextMeshProUGUI messageText;
    public TMP_InputField userInput; // 사용자의 입력을 받을 TMP InputField
    public int scoreIncrement = 10;
    public Animator playerAnimator;

    private bool isTriggered = false;
    private string[] messages; // 텍스트를 저장할 배열
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
        uiCanvas.gameObject.SetActive(false); // 초기에는 UI를 숨김
        userInput.onEndEdit.AddListener(CheckAnswer); // 사용자가 입력을 마치면 CheckAnswer 메서드 호출

        TextAsset csvFile = Resources.Load<TextAsset>(Category); // Resources 폴더에 있는 CSV 파일 로드
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n'); // 줄 바꿈을 기준으로 텍스트를 나눔
            messages = new string[lines.Length];
            answers = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(','); // 쉼표를 기준으로 열을 나눔
                messages[i] = values[0].Trim(); // 첫 번째 열의 텍스트를 메시지로 저장
                answers[i] = values[1].Trim(); // 두 번째 열의 텍스트를 정답으로 저장
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
        if (other.CompareTag("Player")) // 플레이어가 트리거에 닿았을 때
        {
            isTriggered = true;
            currentScore = PlayerPrefs.GetInt("Score");
            uiCanvas.gameObject.SetActive(true); // UI 표시
            int randomIndex = Random.Range(0, messages.Length);
            Quiz = messages[randomIndex];
            if (gameObject.CompareTag("ArrowTrap")) // 트리거의 종류에 따라 다른 메시지 표시
            {
                HeadText.text = "화살 함정 발견!";// 메시지 설정
            }
            else if (gameObject.CompareTag("FireTrap"))
            {
                HeadText.text = "불꽃 함정 발견!"; // 메시지 설정
            }
            else if (gameObject.CompareTag("TrunkTrap"))
            {
                HeadText.text = "통나무 함정 발견!"; // 메시지 설정
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
                    messageText.text = $"{scoreIncrement}점 감점! 남은 기회 {chance} 번 \n" + Quiz;
                    userInput.text = "";
                    userInput.ActivateInputField();
                    newScore = currentScore - scoreIncrement;
                    chance -= 1;
                }
                else
                {
                    playerAnimator.SetTrigger("WrongAnswer");
                    messageText.text = $"{scoreIncrement}점 감점!";
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
            return null; // 일치하는 메시지가 없는 경우
        }
    private IEnumerator WaitAndDestroyObjects(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // waitTime만큼 대기
        foreach (GameObject obj in objectsToDestroy) { Destroy(obj); }
    }
}
