using UnityEngine;

public class Pokemons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PokemonsState currentState = PokemonsState.Escondido;

    private Vector3 destination;

    public void Simulate(float deltaTime)
    {
        switch (currentState)
        {
            case PokemonsState.Escondido:
                Aparecer();
                break;

            case PokemonsState.Explorando:
                Explorar();
                break;

            case PokemonsState.Peleando:
                Pelear();
                break;

            case PokemonsState.Huyendo:
                Huir();
                break;
        }
    }

    void Aparecer()
    {
        // Ocasionalmente aparece en la hierba
        if (Random.value < 0.02f)
        {
            currentState = PokemonsState.Explorando;
        }
    }

    void Explorar()
    {
        // Pequeño movimiento aleatorio o cambio de dirección
        if (Random.value < 0.01f)
        {
            currentState = PokemonsState.Escondido;
        }

        // Si encuentra un entrenador (colisión ficticia)
        if (Random.value < 0.05f)
        {
            currentState = PokemonsState.Peleando;
        }
    }

    void Pelear()
    {
        // Resultado aleatorio del encuentro
        float resultado = Random.value;

        if (resultado < 0.4f)
            currentState = PokemonsState.Huyendo;
        else
            currentState = PokemonsState.Escondido;
    }

    void Huir()
    {
        // Después de huir vuelve a esconderse
        currentState = PokemonsState.Escondido;
    }
}
