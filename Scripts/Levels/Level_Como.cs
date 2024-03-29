using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Como : MonoBehaviour
{
    public GameObject Goobie;
    public GameObject Shoob;
    public GameObject Ship;

    private Animation Goobie_Speak_Anim;
    private Animation Shoob_Speak_Anim;
    private Animation Ship_Anim;

    private bool Goobie_Speak = false;
    private bool Shoob_Speak = false;

    public Image Goobie_VisBubble;
    public Image Shoob_VisBubble;

    public TextMeshProUGUI Goobie_Bubble;
    public TextMeshProUGUI Shoob_Bubble;
    public TextMeshProUGUI AfterLabel;
    public TextMeshProUGUI TravelDataEmission;
    public TextMeshProUGUI TravelDataCost;
    public TextMeshProUGUI ArrivedLabel;
    public TextMeshProUGUI LocationLabel;

    public Image Fade;
    public Image FadeExplosion;

    public AudioSource Music;
    public AudioSource Explosion;
    public AudioSource SpaceshipFlyBy;

    public Image QuestionBubble;
    public Image Car_Button;
    public Image Plane_Button;
    public Image Train_Button;
    public Image Boat_Button;

    public Image VehicleIcon;
    public Image BoatIcon;
    public Image PlaneIcon;
    public Image Trainicon;

    private string G_DIALOG_1 = "Oh no! Our fuel basin ran out of go-go slime!!";
    private string G_DIALOG_2 = "According to my location-o-meter, looks like we are in Columbia, Missouri..";
    private string G_DIALOG_3 = "I am getting kind of hungry and have always wanted to see a bright earthling city...";
    private string G_DIALOG_4 = "How should we get there?";

    private string S_DIALOG_1 = "Where in the galaxy did we crash land??";
    private string S_DIALOG_2 = "I love it here already!";
    private string S_DIALOG_3 = "Then let�s take a bite of their Big Apple, and travel to New York City.";

    private string CAR_COST = "- $158.20 SPENT FROM OUR PIGGY BANK";
    private string CAR_TIME = "AFTER 16 HOURS AND 17 MINUTES ...";
    private string CAR_EMISSIONS = "- 165.45 KG OF CARBON DIOXIDE EMISSIONS";

    private string PLANE_COST = "- $273.00 SPENT FROM OUR PIGGY BANK";
    private string PLANE_TIME = "AFTER 2 HOURS AND 28 MINUTES ...";
    private string PLANE_EMISSIONS = "- 439.47 KG OF CARBON DIOXIDE EMISSIONS";

    private string TRAIN_COST = "- $167 SPENT FROM OUR PIGGY BANK";
    private string TRAIN_TIME = "AFTER 1 DAY AND 3 HOURS ...";
    private string TRAIN_EMISSIONS = "- 70.66 KG OF CARBON DIOXIDE EMISSIONS";

    private string BOAT_COST = "N/A";
    private string BOAT_TIME = "N/A";
    private string BOAT_EMISSIONS = "N/A";

    private string ARRIVAL_MESSAGE = "WE ARRIVED TO NEW YORK CITY!";

    private int CAR = 1;
    private int PLANE = 2;
    private int TRAIN = 3;
    private int BOAT = 4;

    public static bool CAN_HOVER = true;
    public static bool CAN_SELECT = false;

    // Start is called before the first frame update

    void Start()
    {
        Goobie_Speak_Anim = Goobie.GetComponent<Animation>();
        Shoob_Speak_Anim = Shoob.GetComponent<Animation>();
        Ship_Anim = Ship.GetComponent<Animation>();
        StartCoroutine(LevelSequence());
    }

    public void EnableBubble(Image Bubble)
    {

        Vector2 setScale = new Vector3(1f, 1f, 1f);
        Vector2 oldScale = Bubble.rectTransform.sizeDelta;
        LeanTween.scale(Bubble.GetComponent<RectTransform>(), setScale,0.75f).setEaseOutBounce();
    }

    public void DisableButton(Image Bubble)
    {
        Vector2 setScale = new Vector3(0f, 0f, 0f);
        LeanTween.scale(Bubble.GetComponent<RectTransform>(), setScale, 0.75f).setEaseInBounce();
    }

    public void FadeScene(bool typeOfFade)
    {
        if (typeOfFade == true)
        {
            //FADE IN
            LeanTween.alpha(Fade.rectTransform, 0f, 0.5f).setEaseInOutQuad();
        }
        else
        {
            //FADE OUT
            LeanTween.alpha(Fade.rectTransform, 1f, 0.5f).setEaseInOutQuad();
        }
    }

    public void FadeExplosionScene(bool typeOfFade)
    {
        if (typeOfFade == true)
        {
            //FADE IN
            LeanTween.alpha(FadeExplosion.rectTransform, 0f, 0.3f).setEaseInOutQuad();
        }
        else
        {
            //FADE OUT
            LeanTween.alpha(FadeExplosion.rectTransform, 1f, 0.3f).setEaseInOutQuad();
        }
    }

    void updateValueExampleCallback(Color val, TextMeshProUGUI label)
    {
        label.color = val;
    }

    //SCENE FUNCTIONS
    void DisableAll()
    {
        DisableButton(Goobie_VisBubble);
        DisableButton(Shoob_VisBubble);
        DisableButton(Car_Button);
        DisableButton(Plane_Button);
        DisableButton(Train_Button);
        DisableButton(Boat_Button);
        DisableButton(QuestionBubble);
    }

    //BUTTON FUNCTIONS --------------------------------------------------------------------------------
    public void CarSelected()
    {
        CAN_HOVER = false;
        CAN_SELECT = false;

        DisableAll();
        FadeScene(false);
        StartCoroutine(TransitionScreen(1));
    }

    public void PlaneSelected()
    {
        CAN_HOVER = false;
        CAN_SELECT = false;

        DisableAll();
        FadeScene(false);
        StartCoroutine(TransitionScreen(2));
    }

    public void TrainSelected()
    {
        CAN_HOVER = false;
        CAN_SELECT = false;

        DisableAll();
        FadeScene(false);
        StartCoroutine(TransitionScreen(3));
    }
    
    public void BoatSelected()
    {
        CAN_HOVER = false;
        CAN_SELECT = false;

        DisableAll();
        FadeScene(false);
        StartCoroutine(TransitionScreen(4));
    }

    IEnumerator TransitionScreen(int vehicleChoice)
    {
        float waitTime = 2f;

        LeanTween.value(gameObject, Music.volume, 0f, 2f).setOnUpdate((float volume) => { Music.volume = volume; }).setOnComplete(() => { Debug.Log("Done"); });

        yield return new WaitForSeconds(waitTime);

        
       
        switch (vehicleChoice)
        {
            case 1:
                {
                    //CAR WAS CHOSEN
                    EnableBubble(VehicleIcon);
                    Dialog.SetDialog(AfterLabel, CAR_TIME, this);
                    yield return new WaitForSeconds(waitTime);

                    Dialog.SetDialog(TravelDataCost, CAR_COST, this);
                    Dialog.SetDialog(TravelDataEmission, CAR_EMISSIONS, this);

                    break;
                }
            case 2:
                {
                    //PLANE WAS CHOSEN
                    EnableBubble(PlaneIcon);
                    Dialog.SetDialog(AfterLabel, PLANE_TIME, this);
                    yield return new WaitForSeconds(waitTime);

                    Dialog.SetDialog(TravelDataCost, PLANE_COST, this);
                    Dialog.SetDialog(TravelDataEmission, PLANE_EMISSIONS, this);

                    break;
                }
            case 3:
                {
                    //TRAIN WAS CHOSEN
                    EnableBubble(Trainicon);
                    Dialog.SetDialog(AfterLabel, TRAIN_TIME, this);
                    yield return new WaitForSeconds(waitTime);

                    Dialog.SetDialog(TravelDataCost, TRAIN_COST, this);
                    Dialog.SetDialog(TravelDataEmission, TRAIN_EMISSIONS, this);

                    break;
                }
            case 4:
                {
                    //BOAT WAS CHOSEN
                    //not possible....

                    break;
                }
            default:
                {
                    break;
                }
        }

        Dialog.SetDialog(ArrivedLabel, ARRIVAL_MESSAGE, this);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("02_NewYorkCity");
    }
    
    IEnumerator LevelSequence()
    {
        DisableAll();

        yield return new WaitForSeconds(0.75f);
        FadeScene(true);

        yield return new WaitForSeconds(1.2f);

        SpaceshipFlyBy.Play();
        Ship_Anim.Play();

        yield return new WaitForSeconds(0.9f);
        Explosion.Play(); 
        yield return new WaitForSeconds(0.2f);
        SpaceshipFlyBy.Stop();
        yield return new WaitForSeconds(0.2f);

        FadeExplosionScene(false);

        yield return new WaitForSeconds(0.7f);
        LocationLabel.enabled = true;
        Goobie.GetComponent<SpriteRenderer>().enabled = true;
        Shoob.GetComponent<SpriteRenderer>().enabled = true;

        FadeExplosionScene(true);

        yield return new WaitForSeconds(0.7f);

        Music.Play();

        EnableBubble(Goobie_VisBubble);
        yield return new WaitForSeconds(0.75f);

        Goobie_Speak_Anim.Play("GoobieSpeak");
        Dialog.SetDialog(Goobie_Bubble, G_DIALOG_1, this);
        yield return new WaitForSeconds(2);
        Goobie_Speak_Anim.Stop("GoobieSpeak");

        
        EnableBubble(Shoob_VisBubble);
        yield return new WaitForSeconds(0.75f);
        
        Shoob_Speak_Anim.Play("Shoob_Speak");
        Dialog.SetDialog(Shoob_Bubble, S_DIALOG_1, this);
        yield return new WaitForSeconds(3f);
        Shoob_Speak_Anim.Stop("Shoob_Speak");

        Goobie_Speak_Anim.Play("GoobieSpeak");
        Dialog.SetDialog(Goobie_Bubble, G_DIALOG_2, this);
        yield return new WaitForSeconds(5f);
        Goobie_Speak_Anim.Stop("GoobieSpeak");

        Shoob_Speak_Anim.Play("Shoob_Speak");
        Dialog.SetDialog(Shoob_Bubble, S_DIALOG_2, this);
        yield return new WaitForSeconds(2f);
        Shoob_Speak_Anim.Stop("Shoob_Speak");

        Goobie_Speak_Anim.Play("GoobieSpeak");
        Dialog.SetDialog(Goobie_Bubble, G_DIALOG_3, this);
        yield return new WaitForSeconds(6f);
        Goobie_Speak_Anim.Stop("GoobieSpeak");

        Shoob_Speak_Anim.Play("Shoob_Speak");
        Dialog.SetDialog(Shoob_Bubble, S_DIALOG_3, this);
        yield return new WaitForSeconds(3f);
        Shoob_Speak_Anim.Stop("Shoob_Speak");

        Goobie_Speak_Anim.Play("GoobieSpeak");
        Dialog.SetDialog(Goobie_Bubble, G_DIALOG_4, this);
        yield return new WaitForSeconds(3f);
        Goobie_Speak_Anim.Stop("GoobieSpeak");

        DisableButton(Goobie_VisBubble);
        DisableButton(Shoob_VisBubble);

        EnableBubble(QuestionBubble);
        yield return new WaitForSeconds(1f);

        EnableBubble(Car_Button);
        yield return new WaitForSeconds(0.1f);
        EnableBubble(Plane_Button);
        yield return new WaitForSeconds(0.1f);
        EnableBubble(Train_Button);
        yield return new WaitForSeconds(0.1f);
        EnableBubble(Boat_Button);
    }
}
