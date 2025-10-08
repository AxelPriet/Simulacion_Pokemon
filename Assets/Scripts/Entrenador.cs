using UnityEngine;

public class Entrenador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Estado del Entrenador")]
    public EntrenadorState currentState = EntrenadorState.Explorando;

    public void Simulate(float deltaTime)
    {
        switch (currentState)
        {
            case EntrenadorState.Explorando:
                Explorar();
                break;

            case EntrenadorState.BuscarPokemon:
                BuscarPokemon();
                break;

            case EntrenadorState.Capturando:
                Capturar();
                break;

            case EntrenadorState.Derrotado:
                Descansar();
                break;
        }
    }

    void Explorar()
    {
        // Chance de encontrar hierba o Pok�mon
        if (Random.value < 0.05f)
        {
            currentState = EntrenadorState.BuscarPokemon;
        }
    }

    void BuscarPokemon()
    {
        // Simula b�squeda de Pok�mon
        if (Random.value < 0.4f)
        {
            currentState = EntrenadorState.Capturando;
        }
        else
        {
            currentState = EntrenadorState.Explorando;
        }
    }

    void Capturar()
    {
        // �xito o derrota
        float resultado = Random.value;

        if (resultado < 0.6f)
        {
            Debug.Log($"{name} captur� un Pok�mon!");
            currentState = EntrenadorState.Explorando;
        }
        else
        {
            currentState = EntrenadorState.Derrotado;
        }
    }

    void Descansar()
    {
        // Tras descansar, vuelve a explorar
        currentState = EntrenadorState.Explorando;
    }
}
