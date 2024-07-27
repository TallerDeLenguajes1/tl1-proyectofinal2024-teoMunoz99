namespace EspacioEntradas
{
    public static class Ingresar
    {

        private static int Entero()
        {
            string num;
            int numeroIngresado;
            bool bandera = false;
            do
            {
                num = Console.ReadLine();
                if (int.TryParse(num, out numeroIngresado))
                {
                    bandera = true;
                }
                else
                {
                    Console.WriteLine("Debe ingresar un numero");
                }
            } while (!bandera);
            return numeroIngresado;
        }
        private static string Cadena()
        {
            string texto;
            do
            {
                texto = Console.ReadLine();
                if (texto == null || texto.Length < 3)
                {
                    Console.WriteLine("Debe ingresar al menos 3 caracteres");
                }
            } while (texto == null || texto.Length < 3);
            return texto;
        }
        public static string NombreJugador(string cadena)
        {
            Console.Clear();
            Console.WriteLine(cadena+"\n");
            string _nombreDelJugador = Cadena();
            Console.Clear();
            return _nombreDelJugador;
        }
        public static int CantidadDeJugadores(int _min, int _max){
            int cant;
            do
            {
              cant = Entero();  
            } while (cant < _min || cant > _max);
            return cant;
        }
    }
}