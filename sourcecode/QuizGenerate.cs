using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizGenerate : MonoBehaviour
{
    private int correctAnswer;
    private int incorrectAnswer;
    public Transform leftWayBlock;
    public Transform rightWayBlock;
    public GameObject correctWayPrefeb;
    public GameObject incorrectWayPrefeb;

   
    private UISlideTrigger uiSlideTrigger;

    private QuizUIManager quizUIManager;
    void Start()
    {
        quizUIManager = FindObjectOfType<QuizUIManager>();
        
        
        if (quizUIManager == null)
        {
            Debug.LogError("QuizUIManager not found in the scene!");
        }
        uiSlideTrigger = FindObjectOfType<UISlideTrigger>();
        if (uiSlideTrigger == null)
        {
            Debug.LogError("UISlideTrigger component not found !");   
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GenerateQuestion();
            uiSlideTrigger.HitGenerateBlock();
            Destroy(gameObject);
        }
    }

    public void GenerateQuestion()
    {
        int num1 = Random.Range(31, 101); // 1-100
        int num2 = Random.Range(31, 101);
        string operatorSymbol ; 
        int randomOperator = Random.Range(1, 4); //1-3
        


        if (randomOperator == 1)
        {
            operatorSymbol = "+";
            num1 = Random.Range(500, 1000); 
            num2 = Random.Range(500, 1000);
        }
        else if (randomOperator == 2)
        {
            operatorSymbol = "-";
            num1 = Random.Range(500, 1000);
            num2 = Random.Range(500, 1000);
        }
        else 
        {
            operatorSymbol = "*";
        }

        // Formulate the question
        string question = $"{num1} {operatorSymbol} {num2} = ?";
        quizUIManager.quizText.text = question.ToString();

        // Calculate the correct answer
        if (operatorSymbol == "+") {
            correctAnswer = num1 + num2;
            incorrectAnswer = correctAnswer + Random.Range(-2, 3); 
        }
        else if (operatorSymbol == "-")
        {
            correctAnswer = num1 - num2;
            incorrectAnswer = correctAnswer + Random.Range(-2, 3); 
        }
        else if (operatorSymbol == "*")
        {
            correctAnswer = num1 * num2;
            int randomOp = Random.Range(0, 2);
            if (randomOp == 0)
            {
                incorrectAnswer = correctAnswer + 10;
            }
            else 
            {
                incorrectAnswer = correctAnswer - 10;
            }
            
        }

        // Ensure the incorrect answer is not the same as the correct answer
        while (incorrectAnswer == correctAnswer)
        {
            incorrectAnswer = correctAnswer + Random.Range(-2, 3);
        }


        // Set way
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            quizUIManager.choice1Text.text = correctAnswer.ToString();
            quizUIManager.choice2Text.text = incorrectAnswer.ToString();

            // Create correct way at left (leftWayBlock)
            GameObject correctWay = Instantiate(correctWayPrefeb, leftWayBlock.position, leftWayBlock.rotation);
            correctWay.transform.localScale = leftWayBlock.localScale;

            // Create incorrect way at right (rightWayBlock)
            GameObject incorrectWay = Instantiate(incorrectWayPrefeb, rightWayBlock.position, rightWayBlock.rotation);
            incorrectWay.transform.localScale = rightWayBlock.localScale;
        }
        else
        {
            quizUIManager.choice1Text.text = incorrectAnswer.ToString();
            quizUIManager.choice2Text.text = correctAnswer.ToString();

            // Create incorrect way at left (leftWayBlock)
            GameObject incorrectWay = Instantiate(incorrectWayPrefeb, leftWayBlock.position, leftWayBlock.rotation);
            incorrectWay.transform.localScale = leftWayBlock.localScale;

            // Create correct way at right (rightWayBlock)
            GameObject correctWay = Instantiate(correctWayPrefeb, rightWayBlock.position, rightWayBlock.rotation);
            correctWay.transform.localScale = rightWayBlock.localScale;
        }
        // ShowUI
        quizUIManager.ShowUI();
        quizUIManager.ShowSmallTime();
    }

    

 }
