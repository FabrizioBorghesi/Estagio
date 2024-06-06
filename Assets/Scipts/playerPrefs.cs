using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerPrefsViewer : MonoBehaviour
{
    public Text recordsText; // Referência ao objeto de texto UI na cena

    void Start()
    {
        // Exemplo: Recuperando e exibindo a pontuação do jogador
        int score = PlayerPrefs.GetInt("NomePaciente_Pontuacao", 0);
        recordsText.text += "Pontuação do Paciente: " + score + "\n";

        // Exemplo: Recuperando e exibindo o número de sequências do jogador
        int sequences = PlayerPrefs.GetInt("NomePaciente_Sequencias", 0);
        recordsText.text += "Número de Sequências: " + sequences + "\n";

        // Você pode adicionar mais registros conforme necessário
    }

    public void voltar(){ 
        SceneManager.LoadScene("telaInicial");

    }
}
