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

Para ejecutar el juego, necesitas tener [Visual Studio](https://visualstudio.microsoft.com/) o cualquier otro IDE compatible con [.NET](https://dotnet.microsoft.com/es-es/) instalado en tu máquina. Para un correcto funcionamiento es necesario tener acceso a internet para consumir la API de ChatGPT.

1. Clona este repositorio en tu máquina local:

   ```bash
   git clone https://github.com/TallerDeLenguajes1/tl1-proyectofinal2024-teoMunoz99/tree/main
   ```

## Configuración de la API de ChatGPT

Para que el juego funcione correctamente, necesitas configurar la API de ChatGPT. Para ello, debes proporcionar tu clave API en un archivo de entorno.
Para obetener tu clave api debes ingresar al sitio de [OpenIA](https://openai.com/api/pricing/) y cargar saldo a tu cuenta.

### Paso 1: Crear el Archivo `.env`

1. En el directorio raíz de tu proyecto, crea un archivo llamado `.env`.

2. Dentro de este archivo, agrega la siguiente línea con tu clave API de ChatGPT:
   ```bash
   OPENAI_API_KEY="Pega aqui tu apiKey"
   ```

### Paso 2: Abre el proyecto en Visual Studio o en tu IDE

3. Restaura los paquetes necesarios ejecutando:
   ```bash
   dotnet restore
   ```
4. Compila y ejecuta el proyecto:
   ```bash
   dotnet run
   ```

## Instrucciones de Uso

- **Menú Principal**:
  - Jugar: Comienza una nueva partida de trivia.
  - Ver Ranking: Consulta el ranking de jugadores.
  - Instrucciones: Lee las reglas e instrucciones del juego.
  - Salir: Sal del juego.
- **Comenzar una Partida**:
  - Elegir Modo de Juego: Selecciona entre Jugador 1 vs ChatGPT o Jugador 1 vs Jugador 2.
  - Ingresar Nombres: Ingresa los nombres de los jugadores.
  - Elegir Tema de Preguntas: Selecciona el tema de las preguntas de trivia.
- **Mecánica del Juego**:
  - Inicio del Duelo: Ambos jugadores comienzan con 100 puntos de vida, 1 pista y 1 salto.
  - Turnos: El jugador que inicia el turno es elegido al azar.
  - Ataque: El jugador del turno elige entre ataque ligero, medio o pesado, respondiendo la pregunta correspondiente.
  - Defensa del Rival: El rival puede reducir el daño respondiendo correctamente una pregunta de la misma dificultad.
- **Opciones Especiales**:
  - Pista: Gana una pista adicional al responder 3 preguntas correctamente.
  - Salto: Gana un salto adicional al responder 5 preguntas correctamente.
- **Final del Juego**:
  - El juego termina cuando la vida de un jugador llega a 0 o se agotan las preguntas disponibles.

## Contribuciones
Si quieres contribuir al desarrollo de **Quizz**, por favor sigue estos pasos:

- Haz un fork del repositorio.
- Crea una nueva rama (git checkout -b feature/NombreDeTuRama).
- Realiza tus cambios y haz commit (git commit -m 'Agrega una nueva característica').
- Envía tus cambios (git push origin feature/NombreDeTuRama).
- Abre un Pull Request.

## Contacto
Si tienes preguntas o sugerencias, puedes contactarme a través de tadeomunoz09@hotmail.com.