using EspacioAtaque;
using EspacioPersonaje;
using System;
namespace EspacioMenu
{
    public static class Menu
    {
        public static string tituloPrincipal = @"
 ██████╗ ██╗   ██╗██╗███████╗███████╗
██╔═══██╗██║   ██║██║╚══███╔╝╚══███╔╝
██║   ██║██║   ██║██║  ███╔╝   ███╔╝ 
██║▄▄ ██║██║   ██║██║ ███╔╝   ███╔╝             ─▄▀─▄▀                                         
╚██████╔╝╚██████╔╝██║███████╗███████╗           ──▀──▀                        
 ╚══▀▀═╝  ╚═════╝ ╚═╝╚══════╝╚══════╝           █▀▀▀▀▀█▄                                                        
                                      v1.0      █░░░░░█─█                                      
                                      by: Teo   ▀▄▄▄▄▄▀▀
 ";

        //Recibo el un sting con las opciones del menu, lo muestro y retorno el indice de la opcion elegida
        public static int MostrarMenu(string titulo, string[] opciones)
        {
            int indice = 0;//Acá guardare el indice, lo arranco en la primer opcion
            ConsoleKey teclaPresionada;//En esta variable guardare la tecla que se precione
            do
            {
                Console.Clear();
                Console.WriteLine(titulo + "\n");
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == indice)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine(opciones[i]);
                }
                Console.ResetColor();
                ConsoleKeyInfo keyInfo = Console.ReadKey();//capturo la info de la tecla presionada
                teclaPresionada = keyInfo.Key;
                if (teclaPresionada == ConsoleKey.UpArrow)
                {
                    indice--;
                    if (indice < 0)
                    {
                        indice = opciones.Length - 1;
                    }
                }
                else if (teclaPresionada == ConsoleKey.DownArrow)
                {
                    indice++;
                    if (indice >= opciones.Length)
                    {
                        indice = 0;
                    }
                }
            } while (teclaPresionada != ConsoleKey.Enter);
            return indice;
        }
        public static string MostrarOpcionesRespuestas(string titulo, string[] _listaOpciones, Personaje _jugador)
        {
            List<string> opciones = new List<string>();
            foreach (var item in _listaOpciones)
            {
                opciones.Add(item);
            }
            if (_jugador.PistasDisponibles >= 1)
            {
                opciones.Add($">>>Pedir una pista ({_jugador.PistasDisponibles})");
            }
            if (_jugador.SaltosDisponibles >= 1)
            {
                opciones.Add($">>>Saltar pregunta ({_jugador.SaltosDisponibles})");
            }
            int indice = 0;//Acá guardare el indice, lo arranco en la primer opcion
            ConsoleKey teclaPresionada;//En esta variable guardare la tecla que se precione
            do
            {
                Console.Clear();
                Console.WriteLine(titulo + "\n");
                for (int i = 0; i < opciones.Count; i++)
                {
                    if (i == indice)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine(opciones[i]);
                }
                Console.ResetColor();
                ConsoleKeyInfo keyInfo = Console.ReadKey();//capturo la info de la tecla presionada
                teclaPresionada = keyInfo.Key;
                if (teclaPresionada == ConsoleKey.UpArrow)
                {
                    indice--;
                    if (indice < 0)
                    {
                        indice = opciones.Count - 1;
                    }
                }
                else if (teclaPresionada == ConsoleKey.DownArrow)
                {
                    indice++;
                    if (indice >= opciones.Count)
                    {
                        indice = 0;
                    }
                }
            } while (teclaPresionada != ConsoleKey.Enter);
            return opciones[indice];
        }
        public static Ataque MostrarAtaques(string titulo, List<Ataque> _ataques)
        {
            int indice = 0;//Acá guardare el indice, lo arranco en la primer opcion
            ConsoleKey teclaPresionada;//En esta variable guardare la tecla que se precione
            do
            {
                Console.Clear();
                Console.WriteLine(titulo + "\n");
                for (int i = 0; i < _ataques.Count; i++)
                {
                    if (i == indice)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine($"{_ataques[i].Nombre} - Daño: {_ataques[i].Danio}, Dificultad: {_ataques[i].Dificultad}");
                }
                Console.ResetColor();
                ConsoleKeyInfo keyInfo = Console.ReadKey();//capturo la info de la tecla presionada
                teclaPresionada = keyInfo.Key;
                if (teclaPresionada == ConsoleKey.UpArrow)
                {
                    indice--;
                    if (indice < 0)
                    {
                        indice = _ataques.Count - 1;
                    }
                }
                else if (teclaPresionada == ConsoleKey.DownArrow)
                {
                    indice++;
                    if (indice >= _ataques.Count)
                    {
                        indice = 0;
                    }
                }
            } while (teclaPresionada != ConsoleKey.Enter);
            return _ataques[indice];
        }
        public static void MostrarMensaje(string _mensaje)
        {
            Console.Clear();
            Console.WriteLine(_mensaje);
            Console.WriteLine("\nPress Key");
            Console.ReadKey();
        }

        public static void MostrarInstrucciones()
        {
            Console.Clear();
            Console.WriteLine(tituloPrincipal);
            Console.WriteLine(Instrucciones);
            Console.WriteLine("\nPress key");
            Console.ReadKey();
        }

        public static void MostarMensajeEnDuelo(string _mensaje, Personaje _jugador, Personaje _enemigo)
        {
            Console.Clear();
            Console.WriteLine($"Salud =>> {_jugador.Nombre}: {_jugador.Vida} - {_enemigo.Nombre}: {_enemigo.Vida}");
            Console.WriteLine($"\n{_jugador.Nombre} {_mensaje}");
            Console.WriteLine("\nPress key");
            Console.ReadKey();
        }
        public static string Instrucciones = @"
>>>> Instrucciones de Quizz

¡Bienvenido a Quizz, el juego de trivia definitivo! Aquí te explicamos cómo jugar:

>> Menú Principal ------------------------------------------------------------
Al iniciar el juego, verás el menú principal con las siguientes opciones:

Jugar: Comienza una nueva partida de trivia.
Ver Ranking: Consulta el ranking de jugadores.
Instrucciones: Lee las reglas e instrucciones del juego.
Salir: Sal del juego.
------------------------------------------------------------------------------
>> Comenzar una Partida ------------------------------------------------------
Elegir Modo de Juego:

Jugador1 vs ChatGPT-4: Juega contra la IA de ChatGPT-4.
Jugador1 vs Jugador2: Juega contra otro jugador.

>> Ingresar Nombres 
Ingresa los nombres de los jugadores según el modo de juego seleccionado.

>> Elegir Tema de Preguntas

Selecciona el tema de las preguntas de trivia.
------------------------------------------------------------------------------
>> Mecánica del Juego --------------------------------------------------------
Inicio del Duelo:

Cada jugador comienza con 100 puntos de vida.
Cada jugador tiene 1 pista disponible y 1 salto disponible.

>> Turnos:

El jugador que inicia el turno es elegido al azar.
El jugador del turno actual debe elegir su ataque: ligero, medio o pesado.

Ataque Ligero: Pregunta fácil.
Ataque Medio: Pregunta de dificultad media.
Ataque Pesado: Pregunta difícil.

>> Responder Preguntas:

Si el jugador responde correctamente, ataca a su rival.
Si el jugador falla, no realiza el ataque.

>> Defensa del Rival:

El rival tiene la oportunidad de defenderse respondiendo una pregunta de la misma dificultad.
Si el rival responde correctamente, reduce el daño recibido en un 75%.
Si el rival falla, recibe todo el daño del ataque.

>> Opciones Especiales
Pista:
Cada jugador comienza con 1 pista disponible.
Al responder correctamente 3 preguntas, se obtiene una pista adicional.

Salto:
Cada jugador comienza con 1 salto disponible.
Al responder correctamente 5 preguntas, se obtiene un salto adicional.
------------------------------------------------------------------------------
>> Final del Juego------------------------------------------------------------
El duelo continúa hasta que:

La vida de un jugador llega a 0.
Se agotan las preguntas disponibles.
------------------------------------------------------------------------------
¡Buena suerte y diviértete jugando a Quizz!
------------------------------------------------------------------------------
";
    }
}
