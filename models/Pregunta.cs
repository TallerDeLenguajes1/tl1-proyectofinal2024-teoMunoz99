using EspacioAtaque;
using EspacioMenu;
using EspacioPersonaje;

namespace EspacioPregunta
{
    public class Pregunta
    {
        //Atributos------------------------------
        public string Texto { get; set; }
        public string RespuestaCorrecta { get; set; }
        public Dificultad Dificultad { get; set; }
        public string Pista { get; set; }
        public string[] OpcionesRespuestas { get; set; }
        //---------------------------------------
        //Constructor----------------------------
        public Pregunta() { }
        public Pregunta(string _texto, string _respuestaCorrecta, Dificultad _dificultad, string _pista, string[] _opcionesRespuestas)
        {
            Texto = _texto;
            RespuestaCorrecta = _respuestaCorrecta;
            Dificultad = _dificultad;
            Pista = _pista;
            OpcionesRespuestas = _opcionesRespuestas;
        }
        //---------------------------------------
        //Metodos--------------------------------
        public bool VerificarRespuesta(Personaje _jugador)
        {
            string respuestaElegida = Menu.MostrarOpcionesRespuestas(Texto, OpcionesRespuestas, _jugador);
            if (respuestaElegida.Equals($">>>Pedir una pista ({_jugador.PistasDisponibles})", StringComparison.OrdinalIgnoreCase))
            {
                _jugador.PistasDisponibles--;
                Menu.MostrarMensaje($"Pista: {Pista}");
                int indiceResputesta = Menu.MostrarMenu(Texto, OpcionesRespuestas);
                return OpcionesRespuestas[indiceResputesta].Equals(RespuestaCorrecta, StringComparison.OrdinalIgnoreCase);
            }else if (respuestaElegida.Equals($">>>Saltar pregunta ({_jugador.SaltosDisponibles})", StringComparison.OrdinalIgnoreCase)){
                _jugador.SaltosDisponibles--;
                Menu.MostrarMensaje($"Saltaste la pregunta");
                return true;
            }else
            {
                return respuestaElegida.Equals(RespuestaCorrecta, StringComparison.OrdinalIgnoreCase);
            }
        }
        //---------------------------------------
    }
}