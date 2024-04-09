using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class sellitem : MonoBehaviour
{
    public Text scoreText;
    private int score = -10;

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าวัตถุที่ชนมาอยู่ใน Layer ที่ต้องการหรือไม่
        if (other.gameObject.layer == LayerMask.NameToLayer("Layer1"))
        {
            Debug.Log("ชน");
            score += 10;
            scoreText.text = "Score : " + score.ToString();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Layer2"))
        {
            Debug.Log("ชน");
            score += 50;
            scoreText.text = "Score : " + score.ToString();
            Destroy(other.gameObject);
        }
    }
}

  