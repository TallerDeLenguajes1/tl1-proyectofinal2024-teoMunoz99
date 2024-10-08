using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using EspacioAtaque;
using EspacioPersonaje;
using EspacioPregunta;
using EspacioEntradas;
using EspacioMenu;
using EspacioRanking;
using System.Runtime.InteropServices;
using EspacioIA;
using System.Reflection;

namespace EspacioJuego
{
    public class Juego
    {
        //Atributos------------------------------
        private List<Pregunta> Preguntas { get; set; }
        private Personaje Jugador1 { get; set; }
        private Personaje Jugador2 { get; set; }
        private int TurnoActual { get; set; }
        private string ModeloIA { get; set; }
        private string[] ModelosDisponibles = ["gpt-3.5-turbo", "gpt-4o"];
        private string DificultadElegida { get; set; }
        //---------------------------------------

        //Constructor----------------------------
        public Juego()
        {
            MenuBuclePrincipal();
        }
        //---------------------------------------

        //Metodos--------------------------------
        private void IniciarJuego()
        {
            InicializarJugadores();
            ElegirTema();
            TurnoActual = new Random().Next(1, 3);
            IniciarDuelo().Wait();
            FinDelJuego();
        }
        private void FinDelJuego()
        {
            Personaje ganador = null;
            if (!QuedanPreguntas() || !Jugador1.EstaVivo() || !Jugador2.EstaVivo())
            {
                ganador = Jugador1.Vida > Jugador2.Vida ? Jugador1 : Jugador2.Vida > Jugador1.Vida ? Jugador2 : null;
                if (ganador != null)
                {
                    GuardarGanador(ganador, "./Ranking/Ranking.json", "../../../Ranking/Ranking.json");
                    Menu.MostrarMensaje($"GANADOR: {ganador.Nombre}");
                }
                else
                {
                    Menu.MostrarMensaje("EMPATE");
                }
            }
            Menu.MostrarMensaje(">>>>>>>>>>> Fin del Duelo <<<<<<<<<<<<");
        }
        private List<Pregunta> CargarPreguntasDesdeJson(string _ruta, string _rutaAlternativa)
        {
            try
            {
                // Intento cargar las preguntas desde la ruta principal
                if (File.Exists(_ruta))
                {
                    string preguntas = File.ReadAllText(_ruta);
                    return JsonSerializer.Deserialize<List<Pregunta>>(preguntas);
                }
                // Si no existe el archivo en la ruta principal, intento cargarlo desde la ruta alternativa
                else if (File.Exists(_rutaAlternativa))
                {
                    string preguntas = File.ReadAllText(_rutaAlternativa);
                    return JsonSerializer.Deserialize<List<Pregunta>>(preguntas);
                }
                else
                {
                    throw new FileNotFoundException("No se encontraron preguntas en ninguna de las rutas especificadas.");
                }
            }
            catch (System.Exception e)
            {
                Menu.MostrarMensaje($"Error al recuperar las preguntas: {e.Message}");
                return new List<Pregunta>();
            }
        }
        private List<Pregunta> RetornarDosPreguntas(Dificultad _dificultadBuscada)
        {
            List<Pregunta> preguntasFiltradas = Preguntas.Where(o => o.Dificultad == _dificultadBuscada).ToList();
            if (preguntasFiltradas.Count >= 2)
            {
                Random random = new Random();
                List<Pregunta> preguntasRetornar = preguntasFiltradas.OrderBy(x => random.Next()).Take(2).ToList();
                foreach (var pregunta in preguntasRetornar)
                {
                    Preguntas.Remove(pregunta);
                }
                return preguntasRetornar;
            }
            else
            {
                Menu.MostrarMensaje("No quedan preguntas con la dificultad elegida");
                return null;
            }
        }
        private void InicializarJugadores()
        {
            int _cantidadDeJugadores = 1 + Menu.MostrarMenu("Elija la cantidad de jugadores", ["Jugador 1 vs CHAT GPT", "Jugador 1 vs Jugador 2", "\n<< Volver atras"]);
            if (_cantidadDeJugadores == 3) VolverAlMenuPrincipal();
            Jugador1 = new Personaje(Ingresar.NombreJugador("Ingrese el nombre del Jugador 1"));
            if (_cantidadDeJugadores == 2)
            {
                Jugador2 = new Personaje(Ingresar.NombreJugador("Ingrese el nombre del Jugador 2"));
            }
            else
            {
                int opcion = Menu.MostrarMenu("Elija el modelo de IA", ModelosDisponibles);
                ModeloIA = ModelosDisponibles[opcion];
                DificultadElegida = DefinirDificultad();
                Jugador2 = new Personaje(ModeloIA, true);
            }
            Menu.MostrarMensaje($"Duelo: {Jugador1.Nombre} VS {Jugador2.Nombre}");
        }
        private bool QuedanPreguntas()
        {
            return Preguntas.GroupBy(p => p.Dificultad).Any(g => g.Count() >= 2);
        }
        private void GuardarGanador(Personaje ganador, string rutaJson, string rutaAlt)
        {
            string rutaFinal;
            // Verifico si alguna de las rutas existe
            if (File.Exists(rutaJson))
            {
                rutaFinal = rutaJson;
            }
            else if (File.Exists(rutaAlt))
            {
                rutaFinal = rutaAlt;
            }
            else
            {
                // Si ninguna existe, uso la ruta principal
                rutaFinal = rutaJson;
                string directoryPath = Path.GetDirectoryName(rutaFinal);
                // Creo el directorio de la ruta principal si no existe
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
            // Cargo la lista de ranking existente o creo una nueva si no existe el archivo
            List<Ranking> rankingList = new List<Ranking>();
            if (File.Exists(rutaFinal))
            {
                string jsonString = File.ReadAllText(rutaFinal);
                rankingList = JsonSerializer.Deserialize<List<Ranking>>(jsonString) ?? new List<Ranking>();
            }
            // Agregar el nuevo ganador y ordenar la lista
            rankingList.Add(new Ranking(ganador.Nombre, ganador.CantidadDeAciertos));
            rankingList = rankingList.OrderByDescending(r => r.CantidadDeAciertos).ToList();
            // Guardar la lista actualizada en el archivo
            string nuevoJsonString = JsonSerializer.Serialize(rankingList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaFinal, nuevoJsonString);
        }
        private List<Ranking> CargarRankingDesdeJson(string rutaJson, string rutaAlt)
        {
            string rutaFinal;
            try
            {
                if (File.Exists(rutaJson))
                {
                    rutaFinal = rutaJson;
                }
                else if (File.Exists(rutaAlt))
                {
                    rutaFinal = rutaAlt;
                }
                else
                {
                    return new List<Ranking>();
                }
                string ranking = File.ReadAllText(rutaFinal);
                return JsonSerializer.Deserialize<List<Ranking>>(ranking);
            }
            catch (System.Exception e)
            {
                Menu.MostrarMensaje($"Error al recuperar el ranking: {e.Message}");
                return new List<Ranking>();
            }
        }
        private void ElegirTema()
        {
            int tema = Menu.MostrarMenu("Elije el tema", ["Random", "Internet/Memes/Youtubers...", "Argentina", "Futbol", "Era Medieval", "Programacion", "Test"]);
            switch (tema)
            {
                case 0:
                    string rutaPreguntasRandom = "../../../Preguntas/Random.json";
                    string rutaPreguntasRandomAlternativa = "./Preguntas/Random.json";
                    Preguntas = CargarPreguntasDesdeJson(rutaPreguntasRandom, rutaPreguntasRandomAlternativa);
                    break;
                case 1:
                    string rutaPreguntasInternet = "../../../Preguntas/PopularInternet.json";
                    string rutaPreguntasInternetAlternativa = "./Preguntas/PopularInternet.json";
                    Preguntas = CargarPreguntasDesdeJson(rutaPreguntasInternet, rutaPreguntasInternetAlternativa);
                    break;
                case 2:
                    string rutaPreguntasArg = "../../../Preguntas/Argentina.json";
                    string rutaPreguntasArgAlternativa = "./Preguntas/Argentina.json";
                    Preguntas = CargarPreguntasDesdeJson(rutaPreguntasArg, rutaPreguntasArgAlternativa);
                    break;
                case 3:
                    string rutaPreguntasFutbol = "../../../Preguntas/Futbol.json";
                    string rutaPreguntasFutbolAlternativa = "./Preguntas/Futbol.json";
                    Preguntas = CargarPreguntasDesdeJson(rutaPreguntasFutbol, rutaPreguntasFutbolAlternativa);
                    break;
                case 4:
                    string rutaPreguntasEraMedieval = "../../../Preguntas/Medieval.json";
                    string rutaPreguntasEraMedievalAlt = "./Preguntas/Medieval.json";
                    Preguntas = CargarPreguntasDesdeJson(rutaPreguntasEraMedieval, rutaPreguntasEraMedievalAlt);
                    break;
                case 5:
                    string rutaPregProg = "../../../Preguntas/Programacion.json";
                    string rutaPregProgAlt = "./Preguntas/Programacion.json";
                    Preguntas = CargarPreguntasDesdeJson(rutaPregProg, rutaPregProgAlt);
                    break;
                case 6:
                    string rutaTest = "../../../Preguntas/Test.json";
                    string rutaTestAlt = "./Preguntas/Test.json";
                    Preguntas = CargarPreguntasDesdeJson(rutaTest, rutaTestAlt);
                    break;
                default:
                    Menu.MostrarMensaje("Opcion incorrecta");
                    break;
            }
        }
        private void MenuBuclePrincipal()
        {
            int opcion = 0;
            do
            {
                opcion = Menu.MostrarMenu(Menu.tituloPrincipal, ["[01].  JUGAR", "[02].  VER RANKING", "[03].  INSTRUCCIONES", "[04].  SALIR"]);
                switch (opcion)
                {
                    case 0:
                        IniciarJuego();
                        break;
                    case 1:
                        MostrarRanking();
                        break;
                    case 2:
                        Menu.MostrarInstrucciones();
                        break;
                    case 3:
                        Menu.MostrarMensaje("SALIR");
                        Environment.Exit(0);
                        break;
                    default:
                        Menu.MostrarMensaje("Opcion incorrecta, intente nuevamente");
                        break;
                }
            } while (opcion != 3);
        }
        private void MostrarRanking()
        {
            Console.Clear();
            Console.WriteLine(Menu.tituloPrincipal);
            Console.WriteLine(">>>MEJORES PUNTUACIONES\n");
            List<Ranking> lista = CargarRankingDesdeJson("../../../Ranking/Ranking.json", "./Ranking/Ranking.json");
            if (lista.Count == 0)
            {
                Console.WriteLine("El Ranking esta vacio\n");
            }
            else
            {
                foreach (var item in lista)
                {
                    Console.WriteLine($"{item.Nombre} - {item.CantidadDeAciertos}");
                }
            }
            Console.WriteLine("Press key");
            Console.ReadKey();
        }
        private async Task IniciarDuelo()
        {
            while (Jugador1.EstaVivo() && Jugador2.EstaVivo() && QuedanPreguntas())
            {
                if (TurnoActual == 1)
                {
                    await TurnoJugador(Jugador1, Jugador2);
                    TurnoActual = 2;
                }
                else
                {
                    await TurnoJugador(Jugador2, Jugador1);
                    TurnoActual = 1;
                }
            }
        }
        private async Task<bool> RespuestaIA(Pregunta _pregunta)
        {
            var chatGPT = new ChatGPT(ModeloIA, DificultadElegida);
            Menu.MostrarMensaje($"Responde {Jugador2.Nombre}\n\nPregunta: {_pregunta.Texto}\n\n" + string.Join("\n", _pregunta.OpcionesRespuestas) + "\n");
            Task<string> respuestaTask = chatGPT.ObtenerRespuesta(_pregunta);
            while (!respuestaTask.IsCompleted)
            {
                Console.Clear();
                Console.Write("Esperando respuesta");
                for (int i = 0; i < 3; i++)
                {
                    await Task.Delay(500); // Espero medio segundoo
                    Console.Write(".");
                }
                Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop);
            }
            try
            {
                string respuesta = await respuestaTask;
                Menu.MostrarMensaje($"Respuesta de ChatGPT: {respuesta}");
                return respuesta.Equals(_pregunta.RespuestaCorrecta, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Menu.MostrarMensaje($"Error: {ex.Message}");
                return false;
            }
        }
        private List<Pregunta> RetornarDosPreguntasAleatorias()
        {
            List<Pregunta> preguntasFiltradas = new List<Pregunta>();
            List<Pregunta> preguntasRetornar = null;
            do
            {
                Dificultad _dificultadBuscada = RetornarDificultadAleatoria();
                preguntasFiltradas = Preguntas.Where(o => o.Dificultad == _dificultadBuscada).ToList();
                if (preguntasFiltradas.Count >= 2)
                {
                    Random random = new Random();
                    preguntasRetornar = preguntasFiltradas.OrderBy(x => random.Next()).Take(2).ToList();
                    foreach (var pregunta in preguntasRetornar)
                    {
                        Preguntas.Remove(pregunta);
                    }
                }
            } while (preguntasRetornar == null);
            return preguntasRetornar;
        }
        private Dificultad RetornarDificultadAleatoria()
        {
            Array dificultades = Enum.GetValues(typeof(Dificultad));
            Random random = new Random();
            int indiceAleatorio = random.Next(dificultades.Length);
            Dificultad dificultadAleatoria = (Dificultad)dificultades.GetValue(indiceAleatorio);
            return dificultadAleatoria;
        }
        private async Task TurnoJugador(Personaje _jugador, Personaje _enemigo)
        {
            Ataque ataqueElegido = null;
            List<Pregunta> preguntasTurno = null;
            bool respuestaJugador;
            bool respuestaEnemigo;

            if (_jugador.EsIA)
            {
                Menu.MostarMensajeEnDuelo("Ataca", _jugador, _enemigo);
                preguntasTurno = RetornarDosPreguntasAleatorias();
                ataqueElegido = new Ataque(preguntasTurno[0].Dificultad);
            }
            else
            {
                do
                {
                    Menu.MostarMensajeEnDuelo("Ataca", _jugador, _enemigo);
                    ataqueElegido = _jugador.ElegirAtaque();
                    preguntasTurno = RetornarDosPreguntas(ataqueElegido.Dificultad);
                    if (preguntasTurno == null || ataqueElegido == null)
                    {
                        Menu.MostrarMensaje("Intenta eligiendo otro ataque");
                    }
                } while (preguntasTurno == null || ataqueElegido == null);
            }

            if (_jugador.EsIA)
            {
                respuestaJugador = await RespuestaIA(preguntasTurno[0]);
            }
            else
            {
                respuestaJugador = preguntasTurno[0].VerificarRespuesta(_jugador);
            }

            if (respuestaJugador)
            {
                Menu.MostrarMensaje("Respuesta correcta");
                _jugador.AumentarHabilidad(true);
                Menu.MostarMensajeEnDuelo("Defiende", _enemigo, _jugador);
                if (_enemigo.EsIA)
                {
                    respuestaEnemigo = await RespuestaIA(preguntasTurno[1]);
                }
                else
                {
                    respuestaEnemigo = preguntasTurno[1].VerificarRespuesta(_enemigo);
                }
                _enemigo.Defender(ataqueElegido, respuestaEnemigo);
                _enemigo.AumentarHabilidad(respuestaEnemigo);
            }
            else
            {
                Menu.MostrarMensaje($"Respuesta incorrecta, {_jugador.Nombre} no ataca");
            }
        }
        private string DefinirDificultad()
        {
            int indiceDificultad = Menu.MostrarMenu($"Elije la dificultad de {ModeloIA}", ["Facil", "Media", "Dificil"]);
            string dificultad;
            switch (indiceDificultad)
            {
                case 0:
                    dificultad = "Tu nivel de inteligencia sera la de un estudiante de secundaria.";
                    break;
                case 1:
                    dificultad = "Tu nivel de inteligencia sera la de un estudiante universitario avanzado.";
                    break;
                case 2:
                    dificultad = "Eres una experta en todos los temas. Responde con precisión eligiendo la opción más correcta.";
                    break;
                default:
                    dificultad = "Responde lo que consideres correcto";
                    break;
            }
            return dificultad;
        }
        private void VolverAlMenuPrincipal()
        {
            MenuBuclePrincipal();
        }
        //---------------------------------------
    }
}
