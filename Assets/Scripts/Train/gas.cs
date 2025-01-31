using UnityEngine;

public class gas : MonoBehaviour
{
    [SerializeField] GameObject gas0;
    [SerializeField] GameObject gas50;
    [SerializeField] GameObject gas100;

    public void ChangeGas(){
        if(gas0.gameObject.activeInHierarchy){
            gas0.SetActive(false);
            gas50.SetActive(true);
        }
        else if(gas50.gameObject.activeInHierarchy){
            gas50.SetActive(false);
            gas100.SetActive(true);
        }
        else if(gas100.gameObject.activeInHierarchy){
            gas100.SetActive(false);
            gas0.SetActive(true);
        }
    }

    public int getactivegas(){
        if(gas0.activeInHierarchy){
            return 0;
        }
        else if(gas50.activeInHierarchy){
            return 50;
        }
        else if(gas100.activeInHierarchy){
            return 100;
        }
        else{
            return 0;
        }
    }
    public void setgas0(){
        gas0.SetActive(true);
        gas50.SetActive(false);
        gas100.SetActive(false);
    }
}
