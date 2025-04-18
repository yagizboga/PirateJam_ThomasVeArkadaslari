using UnityEngine;
using UnityEngine.UIElements;

public class dialoguetrigger : MonoBehaviour
{
    bool canDialogue = false;
    bool isDialogue = false;
    GameObject dialogueSystem;
    [SerializeField] TextAsset dialogue;

    void Start(){
        dialogueSystem = GameObject.FindGameObjectWithTag("dialogue");
        dialogueSystem.SetActive(false);

        if(dialogueSystem == null){
        Debug.LogError("Dialogue system not found! Make sure there is a GameObject with tag 'dialogue' in the scene.");
    }
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("DriverPlayer")){
            canDialogue = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.CompareTag("DriverPlayer")){
            canDialogue = false;
        }
    }

    void FixedUpdate(){

        if(canDialogue && Input.GetKey("e")){
            isDialogue = true;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
        if(canDialogue && isDialogue && dialogueSystem.GetComponent<DialogueSystem>().isondialogue){
            dialogueSystem.SetActive(true);
            dialogueSystem.GetComponent<DialogueSystem>().LoadDialogue(dialogue);
            dialogueSystem.GetComponent<DialogueSystem>().ShowDialogue(0);
        }
        if(dialogueSystem.GetComponent<DialogueSystem>().dialogue_ended){
            gameObject.SetActive(false);
        }
    }
    
}
