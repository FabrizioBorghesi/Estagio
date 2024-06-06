using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Newtonsoft.Json;

public class GeniusGame : MonoBehaviour
{
    public Button[] corBotao;
    public Button botaoReiniciar;
    public Text textoSequencias;
    public Text campoSequencias; // Campo de texto para exibir todas as pontuações
    public AudioClip[] botaoCores; // Certifique-se de que os sons estão configurados no inspetor
    public Button botaoLimpar; // Botão para limpar as pontuações

    private List<int> jogoSequencia = new List<int>();
    private int sequenciaAtual;
    private int passoJogador;
    private bool vezJogador = false;
    private int sequencias = 0;
    private Dictionary<string, List<int>> todasSequencias = new Dictionary<string, List<int>>();
    private AudioSource audioSource;
    private string patientName;

    private const string ScoresKey = "GeniusGameScores";

    public void voltar()
    {
        SceneManager.LoadScene("telaSelecao");
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        botaoReiniciar.onClick.RemoveAllListeners(); // Remove todos os ouvintes de eventos
        botaoReiniciar.onClick.AddListener(() => StartCoroutine(RestartGameWithDelay()));
        botaoLimpar.onClick.RemoveAllListeners(); // Remove todos os ouvintes de eventos
        botaoLimpar.onClick.AddListener(ClearScores);

        foreach (Button button in corBotao)
        {
            button.onClick.RemoveAllListeners(); // Remove todos os ouvintes de eventos
            button.onClick.AddListener(() => OnColorButtonClick(button));
        }

        patientName = PlayerPrefs.GetString("NomePaciente");
        LoadScores();
        StartGame();
    }

    void StartGame()
    {
        sequencias = 0;
        textoSequencias.text = "Sequências: " + sequencias;
        jogoSequencia.Clear();
        NextRound();
    }

    IEnumerator RestartGameWithDelay()
    {
        SaveScore();
        yield return new WaitForSeconds(1.5f);
        StartGame();
    }

    void NextRound()
    {
        jogoSequencia.Add(UnityEngine.Random.Range(0, corBotao.Length));
        sequenciaAtual = 0;
        passoJogador = 0;
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        vezJogador = false;
        for (int i = 0; i < jogoSequencia.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            corBotao[jogoSequencia[i]].GetComponent<Image>().color = Color.gray;
            audioSource.PlayOneShot(botaoCores[jogoSequencia[i]]);
            yield return new WaitForSeconds(0.5f);
            ResetButtonColors();
        }
        vezJogador = true;
    }

    void ResetButtonColors()
    {
        for (int i = 0; i < corBotao.Length; i++)
        {
            switch (i)
            {
                case 0:
                    corBotao[i].GetComponent<Image>().color = Color.red;
                    break;
                case 1:
                    corBotao[i].GetComponent<Image>().color = Color.green;
                    break;
                case 2:
                    corBotao[i].GetComponent<Image>().color = Color.blue;
                    break;
                case 3:
                    corBotao[i].GetComponent<Image>().color = Color.yellow;
                    break;
            }
        }
    }

    void OnColorButtonClick(Button clickedButton)
    {
        if (!vezJogador) return;

        int clickedIndex = System.Array.IndexOf(corBotao, clickedButton);
        audioSource.PlayOneShot(botaoCores[clickedIndex]);

        if (clickedIndex == jogoSequencia[passoJogador])
        {
            passoJogador++;
            if (passoJogador >= jogoSequencia.Count)
            {
                sequencias++;
                textoSequencias.text = "Sequências: " + sequencias; 
                NextRound();
            }
        }
        else
        {
            StartCoroutine(RestartGameWithDelay());
        }
    }

    void SaveScore()
    {
        if (!todasSequencias.ContainsKey(patientName))
        {
            todasSequencias[patientName] = new List<int>();
        }
        todasSequencias[patientName].Add(sequencias);
        string jsonScores = JsonConvert.SerializeObject(todasSequencias);
        PlayerPrefs.SetString(ScoresKey, jsonScores);
        PlayerPrefs.Save();
        UpdateAllScoresText();
    }

    void LoadScores()
    {
        string jsonScores = PlayerPrefs.GetString(ScoresKey, "{}");
        todasSequencias = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(jsonScores);
        UpdateAllScoresText();
    }

    void UpdateAllScoresText()
    {
        campoSequencias.text = "Todas as Pontuações:\n";
        foreach (var entry in todasSequencias)
        {
            campoSequencias.text += entry.Key + ": " + string.Join(", ", entry.Value) + "\n";
        }
    }

    void ClearScores()
    {
        PlayerPrefs.DeleteKey(ScoresKey);
        todasSequencias.Clear();
        UpdateAllScoresText();
    }
}
