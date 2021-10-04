using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] hiddenObjects;
    [SerializeField] private int currencyAmount;
    [SerializeField] private int currencyMin;

    private void Start() {
        if(hiddenObjects.Length > 0){
            for(int i = 0; i < hiddenObjects.Length; i++){
                hiddenObjects[i].SetActive(false);
            }
        }
    }

    public void OnDeath(){
        if(hiddenObjects.Length > 0){
            for(int i = 0; i < hiddenObjects.Length; i++){
                hiddenObjects[i].SetActive(true);
            }
        }
        //implement currency giving when money system implemented;
    }
}
