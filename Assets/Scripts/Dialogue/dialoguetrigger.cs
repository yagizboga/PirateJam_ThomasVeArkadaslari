using UnityEngine;

public class dialoguetrigger : MonoBehaviour
{
    bool canDialogue = false;
    bool isDialogue = false;
    GameObject dialogueSystem;

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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
        if(canDialogue && isDialogue){
            dialogueSystem.SetActive(true);
        }
    }
    
}
