﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FieldManagement_e : MonoBehaviour
{

    float timer;
    float satteliteSpeed = 5.0f; //衛星放出スピード

    public static int flag = 0;
    /*flag==0
     *flag==1 ボタンを押した時(連打防止)
     *flag==2 Playerの移動を止める
     *flag==3 tutorialのボタン用
    */

    //H2A
    public GameObject player; //H2A全体
    //public GameObject sattelite;
    //public GameObject fire;
    //stage
    public GameObject moon;
    public GameObject heart_generater;
    public GameObject enemy_generater1; //whitecloud
    public GameObject enemy_generater2; //blackcloud
    public GameObject enemy_generater3; //stone
    public GameObject enemy_generater4; //ufo
    //canvas
    public Text conversation;
    public Text telop;
    public GameObject introduction;
    public GameObject key;
    public GameObject gameover;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject batsu;
    //audio
    /*AudioSource audioSource;
    public AudioClip bgm;
    public AudioClip countdown;*/
    //others
    //Animator H2Aanimator;
    GameObject luncher;
    Rigidbody2D luncher_rb;
    public GameObject maincamera;
    Camera cam; //背景の色用


    //TODO
    //スライダ

    enum Phase
    {
        SETUP,
        LAUNCH,
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
    }
    Phase phase;


    void Start()
    {
        /*audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(bgm);
        H2Aanimator = player.GetComponent<Animator>();*/
        timer = 0;
        luncher = GameObject.Find("lunchers");
        luncher_rb = luncher.GetComponent<Rigidbody2D>();
        cam = maincamera.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.19f, 0.48f, 0.82f);

        luncher_rb.simulated = false;
        introduction.SetActive(false);
        //fire.SetActive(false);
        moon.SetActive(false);
        heart_generater.SetActive(false);
        enemy_generater1.SetActive(false);
        enemy_generater2.SetActive(false);
        enemy_generater3.SetActive(false);
        enemy_generater4.SetActive(false);
        player.SetActive(true);
        gameover.SetActive(false);
        target1.SetActive(false);
        target2.SetActive(false);
        target3.SetActive(false);
        batsu.SetActive(false);
        key.SetActive(false);
        /*H2A_SRB_R.SetActive(true);
        H2A_SRB_L.SetActive(true);
        H2A_Fairing_R.SetActive(true);
        H2A_Fairing_L.SetActive(true);
        H2A_main1.SetActive(true);
        H2A_main2.SetActive(true);*/

        flag = 2; //player固定
        phase = Phase.SETUP;
    }

    void Update()
    {
        Debug.Log(phase);
        switch (phase)
        {
            case Phase.SETUP:
                SETUPPhase();
                break;
            case Phase.LAUNCH:
                LAUNCHPhase();
                break;
            case Phase.STAGE1:
                STAGE1Phase();
                break;
            case Phase.STAGE2:
                STAGE2Phase();
                break;
            case Phase.STAGE3:
                STAGE3Phase();
                break;
            case Phase.STAGE4:
                STAGE4Phase();
                break;
        }
        if (PlayerScript.playerHP == 0)
        {
            GameOver();
        }
    }

    public void SETUPPhase()
    {
        timer += Time.deltaTime;
        if (timer >= 0 && flag == 2)
        {
            conversation.text = "こちら総合司令塔　２０２１年８月２日";
        }
        if (timer >= 3 && flag == 2)
        {
            conversation.text = "天気は晴れ。打ち上げ最終判断はＧＯです。";
        }
        if (timer >= 6 && flag == 2)
        {
            introduction.SetActive(true);
        }
        if (flag == 3)
        {
            if (timer > 1)
            {
                conversation.text = "【Ｘ-１０】全システム 準備完了";
            }
            if (timer > 3)
            {
                conversation.text = "【Ｘ-５】メインエンジン 点火";
                telop.text = "５";
                //audioSource.PlayOneShot(countdown);
            }
            if (timer > 4)
            {
                telop.text = "４";
            }
            if (timer > 5)
            {
                telop.text = "３";
            }
            if (timer > 6)
            {
                telop.text = "２";
            }
            if (timer > 7)
            {
                telop.text = "１";
            }
            if (timer > 8)
            {
                conversation.text = "【Ｘ-０】SRB-A点火、リフトオフ！";
                telop.text = "START!";
                timer = 0;
                flag = 0;
                phase = Phase.LAUNCH;
            }
        }
    }

    public void LAUNCHPhase()
    {
        timer += Time.deltaTime;
        luncher_rb.simulated = true;
        //fire.SetActive(true);
        if (timer >= 3)
        {
            telop.text = "";
            conversation.text = "積乱雲を避けてくれ！ロケットは雷に弱いんだ";
            target1.SetActive(true);
            batsu.SetActive(true);
            key.SetActive(true);
            luncher_rb.simulated = true;
            enemy_generater1.SetActive(true);
            enemy_generater2.SetActive(true);
        }
        if (timer >= 13)
        {
            target1.SetActive(false);
            enemy_generater1.SetActive(false);
            enemy_generater2.SetActive(false);
        }
        if (timer >= 16)
        {
            phase = Phase.STAGE1;
            timer = 0;
            flag = 0;
        }
    }

    public void STAGE1Phase()
    {
        if (flag == 0)
        {
            conversation.text = "積乱雲を抜けたぞ！SRBを分離してくれ！";
            telop.text = "スペースキーを押せ！";
        }
        if (Input.GetKey(KeyCode.Space) && flag == 0)
        {
            //TODO SRBの分離
            flag = 1;
        }
        if (flag == 1)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                conversation.text = "よくやった！分離成功だ！！";
                telop.text = "Success";
                //H2A_SRB_R.SetActive(false);
                //H2A_SRB_L.SetActive(false);
                cam.backgroundColor = new Color(0.17f, 0.37f, 0.56f);
            }
            if (timer >= 8)
            {
                enemy_generater3.SetActive(true);
                heart_generater.SetActive(true);
                target2.SetActive(true);
                phase = Phase.STAGE2;
                timer = 0;
                flag = 0;
                conversation.text = "次は隕石を避けてくれ！ハートでHPを回復できるぞ！";
                telop.text = "";
            }
        }
    }

    public void STAGE2Phase()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            enemy_generater3.SetActive(false);
            heart_generater.SetActive(false);
            target2.SetActive(false);
        }
        if (timer >= 13)
        {
            conversation.text = "よし！フェアリングの分離だ！頼んだぞ！";
            telop.text = "Fキーを押して切りはなそう！";
            if (Input.GetKey(KeyCode.F) && flag == 0)
            {
                //TODO フェアリングの分離
                flag = 1;
                timer = 0;
            }
        }
        if (timer >= 5 && flag == 1)
        {
            conversation.text = "分離成功だ！すごいじゃないか！";
            telop.text = "Success";
            //H2A_Fairing_R.SetActive(false);
            //H2A_Fairing_L.SetActive(false);
            cam.backgroundColor = new Color(0.2f, 0.29f, 0.37f);
        }
        if (timer >= 8 && flag == 1)
        {
            target3.SetActive(true);
            enemy_generater4.SetActive(true);
            conversation.text = "UFOだ！よけろ！よけてくれ！！";
            telop.text = "";
            phase = Phase.STAGE3;
            timer = 0;
            flag = 0;
        }
    }

    public void STAGE3Phase()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            target3.SetActive(false);
            enemy_generater4.SetActive(false);
            conversation.text = "なんとか無事だな、よかったよ";
        }
        if (timer >= 13)
        {
            conversation.text = "【ＭＥＣＯ】メインエンジンカットオフ！よろしく頼む！";
            telop.text = "Mキーを押してきりはなそう！";
            if (Input.GetKey(KeyCode.M) && flag == 0)
            {
                //TODO メインエンジン分離
                //H2Aanimator.SetTrigger("main1");
                flag = 1;
                timer = 0;
            }
        }
        if (timer >= 8 && flag == 1)
        {
            conversation.text = "カットオフ成功だ！いよいよ衛星を切り離すぞ！";
            telop.text = "Success";
            //H2A_main1.SetActive(false);
        }
        if (timer >= 11 && flag == 1)
        {
            phase = Phase.STAGE4;
            moon.SetActive(true);
            timer = 0;
            flag = 0;
            if (flag == 5)
            {
                GameOver();
            }
        }
    }

    public void STAGE4Phase()
    {
        timer += Time.deltaTime;
        conversation.text = "月が見えたぞ！君の力で月に衛星を届けてくれ！";
        telop.text = "タイミングを合わせてSキーを押せ！";
        if (Input.GetKey(KeyCode.S) && flag == 0)
        {
            //Todo 衛星放出
            //H2Aanimator.SetTrigger("main2");
            flag = 2;
        }
        if (flag == 2)
        {
            //sattelite.transform.position += transform.up * Time.deltaTime * satteliteSpeed;
        }
    }

    public void GameOver()
    {
        player.SetActive(false);
        gameover.SetActive(true);
        conversation.text = "おいおいまじかよ！なんてこった！";
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("title");
        }
    }

    public void StartButton()
    {
        flag = 3;
        timer = 0;
        introduction.SetActive(false);
    }


}