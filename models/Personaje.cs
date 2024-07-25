using System.Runtime.InteropServices;
using EspacioAtaque;
using EspacioPregunta;
using EspacioMenu;
using EspacioJuego;


namespace EspacioPersonaje
{
    public class Personaje
    {
        //Atributos------------------------------
        public string Nombre { get; set; }
        public int Vida { get; set; }
        public int CantidadDeAciertos { get; set; }
        public int PistasDisponibles { get; set; }
        public int SaltosDisponibles { get; set; }
        public List<Ataque> Ataques { get; set; }
        public bool EsIA {get;set;}
        //---------------------------------------

        //Constructor----------------------------
        public Personaje(string _nombre, bool _esIA = false)
        {
            Nombre = _nombre;
            Vida = 100;
            CantidadDeAciertos = 0;
            PistasDisponibles = 1;
            SaltosDisponibles = 1;
            Ataques = new List<Ataque>(){
                new Ataque("Ataque Ligero", 10, Dificultad.Facil),
                new Ataque("Ataque Medio", 20, Dificultad.Media),
                new Ataque("Ataque Pesado", 30, Dificultad.Dificil)
            };
            EsIA = _esIA;
        }
        //---------------------------------------

        //Metodos--------------------------------
        public void Defender(Ataque _ataqueRecibido, bool _respuesta)
        {
            if (_respuesta)
            {
                Menu.MostrarMensaje($"Respuesta correcta, {Nombre} no recibe todo el daño");
                int danioReducido = (_ataqueRecibido.Danio * 25) / 100;
                RecibirDanio(danioReducido);
            }
            else
            {
                Menu.MostrarMensaje($"Respuesta incorrecta, {Nombre} recibe todo el daño");
                RecibirDanio(_ataqueRecibido.Danio);
            }
        }
        public void RecibirDanio(int danio)
        {
            Vida -= danio;
            if (Vida <= 0)
            {
                Vida = 0;
            }
        }
        public bool EstaVivo()
        {
            if (Vida == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Ataque ElegirAtaque()
        {
            return Menu.MostrarAtaques($"{Nombre} elige tu ataque", Ataques);
        }
        public void AumentarHabilidad(bool _resultadoRespuesta)
        {
            if (_resultadoRespuesta)
            {
                CantidadDeAciertos++;
                if (CantidadDeAciertos % 3 == 0)
                {
                    PistasDisponibles++;
                    Menu.MostrarMensaje($"{Nombre} aumentas tus habilidades\nPistas disponibles: {PistasDisponibles}");
                }
                if (CantidadDeAciertos % 5 == 0)
                {
                    SaltosDisponibles++;
                    Menu.MostrarMensaje($"{Nombre} aumentas tus habilidades\nSaltos disponibles: {SaltosDisponibles}");
                }
            }
        }
        //---------------------------------------
    }
}