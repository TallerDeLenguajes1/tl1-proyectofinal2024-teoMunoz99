using System.Text.Json.Serialization;

namespace EspacioAtaque
{
    public class Ataque
    {
        //Atributos------------------------------
        public string Nombre { get; set; }
        public int Danio { get; set; }
        public Dificultad Dificultad { get; set; }
        //---------------------------------------

        //Constructor----------------------------
        public Ataque(){}
        public Ataque(string _nombre, int _danio, Dificultad _dificultad){
            Nombre = _nombre;
            Danio = _danio;
            Dificultad = _dificultad;
        }

        public Ataque(Dificultad _dificultad){
            Dificultad = _dificultad;
            switch (_dificultad)
            {
                case Dificultad.Facil:
                Danio = 10;
                break;
                case Dificultad.Media:
                Danio = 20;
                break;
                case Dificultad.Dificil:
                Danio = 30;
                break;
                default:
                break;
            }
        }
        //---------------------------------------
    }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Dificultad
    {
        Facil,
        Media,
        Dificil
    }
}