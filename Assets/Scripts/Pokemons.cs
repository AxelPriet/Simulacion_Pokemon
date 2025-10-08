using UnityEngine;

public class Pokemons : MonoBehaviour
{
    public PokemonsState currentState = PokemonsState.Explorando;

    public float speed = 2f;
    public float changeDirTime = 2f;

    private Vector3 destination;
    private float timer;
    private HierbaAlta zonaAsignada;
    private Rigidbody2D rb;
    private bool enCombate = false;

    void Start()
    {
        // Buscar la zona de hierba más cercana
        HierbaAlta[] zonas = FindObjectsByType<HierbaAlta>(FindObjectsSortMode.None);
        float minDist = Mathf.Infinity;

        foreach (var z in zonas)
        {
            float dist = Vector2.Distance(transform.position, z.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                zonaAsignada = z;
            }
        }

        if (zonaAsignada == null)
        {
            Debug.LogWarning($"{name} no encontró una zona de hierba cercana.");
        }

        SelectNewDestination();

        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void Simulate(float deltaTime)
    {
        if (enCombate) return; // No se mueve mientras está peleando

        timer += deltaTime;
        if (timer >= changeDirTime)
        {
            SelectNewDestination();
            timer = 0f;
        }

        MoveSmooth(deltaTime);
        UpdateColor();
    }

    void MoveSmooth(float deltaTime)
    {
        if (zonaAsignada == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            destination,
            speed * deltaTime
        );

        float distanciaCentro = Vector3.Distance(transform.position, zonaAsignada.transform.position);
        if (Vector3.Distance(transform.position, destination) < 0.1f || distanciaCentro > zonaAsignada.radioZona)
        {
            SelectNewDestination();
        }
    }

    void SelectNewDestination()
    {
        if (zonaAsignada == null) return;

        Vector2 offset = Random.insideUnitCircle * (zonaAsignada.radioZona * 0.9f);
        destination = zonaAsignada.transform.position + new Vector3(offset.x, offset.y, 0);
    }

    void UpdateColor()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        switch (currentState)
        {
            case PokemonsState.Explorando: sr.color = Color.green; break;
            case PokemonsState.Peleando: sr.color = Color.red; break;
            case PokemonsState.Huyendo: sr.color = Color.yellow; break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entrenador entrenador = collision.gameObject.GetComponent<Entrenador>();
        if (entrenador != null && !enCombate)
        {
            // 🔥 Solo cuando colisiona se anuncia el encuentro
            Debug.Log($"🌿 ¡Un Pokémon salvaje ha aparecido en la hierba alta! ({name})");

            enCombate = true;
            currentState = PokemonsState.Peleando;
            entrenador.EntrarCombate(this);
        }
    }

    public void ResultadoCombate(bool fueCapturado, bool derrotoEntrenador, bool huyo)
    {
        enCombate = false;

        if (fueCapturado)
        {
            Debug.Log($"✅ ¡Has capturado al Pokémon {name}!");
            Destroy(gameObject);
        }
        else if (derrotoEntrenador)
        {
            Debug.Log($"🔥 El Pokémon {name} ha derrotado al entrenador.");
            currentState = PokemonsState.Explorando;
        }
        else if (huyo)
        {
            Debug.Log($"💨 El Pokémon {name} ha escapado.");
            currentState = PokemonsState.Explorando;
        }
    }
}


