namespace EspacioRanking
{
    public class Ranking
    {
        public string Nombre { get; set; }
        public int CantidadDeAciertos { get; set; }

        public Ranking(){}
        public Ranking(string nombre, int cantidadDeAciertos)
        {
            Nombre = nombre;
            CantidadDeAciertos = cantidadDeAciertos;
        }
    }

}