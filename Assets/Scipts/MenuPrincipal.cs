using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour


{
    public void jogar(){ 
        SceneManager.LoadScene("telaSelecao");

    }

    public void sairJogo(){
        Debug.Log("SairDoJogo");
        Application.Quit();

    }
}
