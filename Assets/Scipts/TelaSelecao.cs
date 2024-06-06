 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaSelecao : MonoBehaviour
{
    // Start is called before the first frame update
    public void jogarGenius(){ 
        SceneManager.LoadScene("telaNomeGenius");

    }

    public void voltar(){ 
        SceneManager.LoadScene("telaInicial");

    }

    

    // Update is called once per frame
    
}
