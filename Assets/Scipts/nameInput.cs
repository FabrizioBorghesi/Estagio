using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.UI;

public class nameInput : MonoBehaviour
{
    public TMP_InputField nomeInput; 
    public Button botaoIniciar;
    public TMP_Text erro;

    public void voltar()
    {
        SceneManager.LoadScene("telaSelecao");
    }

    void Start()
    {
        botaoIniciar.onClick.AddListener(OnStartGameButtonClicked);
    }

    void OnStartGameButtonClicked()
    {
        string nomePaciente = nomeInput.text;
        if (!string.IsNullOrEmpty(nomePaciente))
        {
            PlayerPrefs.SetString("NomePaciente", nomePaciente);
            PlayerPrefs.Save();
            SceneManager.LoadScene("telaGenius");
        }
        else
        {
            erro.text = "Por favor, insira o nome do paciente.";
        }
    }
}
