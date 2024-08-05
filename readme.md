# Quizz

## Descripción del Proyecto

**Quizz** es un juego de trivia desarrollado en C# para la terminal. Permite a los jugadores competir entre sí o contra la inteligencia artificial ChatGPT. El juego desafía a los jugadores con preguntas de diferentes niveles de dificultad y ofrece opciones especiales como pistas y saltos para ayudar en el duelo.

## Características

- **Modos de Juego**:
  - Jugador 1 vs ChatGPT
  - Jugador 1 vs Jugador 2

- **Opciones Especiales**:
  - Pistas: Cada jugador empieza con 1 pista, que se puede obtener adicionalmente al responder correctamente 3 preguntas.
  - Saltos: Cada jugador empieza con 1 salto, que se puede obtener adicionalmente al responder correctamente 5 preguntas.

- **Ataques**:
  - Ataque Ligero: Pregunta fácil.
  - Ataque Medio: Pregunta de dificultad media.
  - Ataque Pesado: Pregunta difícil.

## Instalación

Para ejecutar el juego, necesitas tener [Visual Studio](https://visualstudio.microsoft.com/) o cualquier otro IDE compatible con [.NET](https://dotnet.microsoft.com/es-es/) instalado en tu máquina. 

1. Clona este repositorio en tu máquina local:

   ```bash
   git clone https://github.com/TallerDeLenguajes1/tl1-proyectofinal2024-teoMunoz99/tree/main
## Configuración de la API de ChatGPT
Para que el juego funcione correctamente, necesitas configurar la API de ChatGPT. Para ello, debes proporcionar tu clave API en un archivo de entorno.
Para obetener tu clave api debes ingresar al sitio de [OpenIA](https://openai.com/api/pricing/) y cargar saldo a tu cuenta.

### Paso 1: Crear el Archivo `.env`

1. En el directorio raíz de tu proyecto, crea un archivo llamado `.env`.

2. Dentro de este archivo, agrega la siguiente línea con tu clave API de ChatGPT:
    ```bash
    OPENAI_API_KEY="Pega aqui tu apiKey"
