using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using EspacioPregunta;
using DotNetEnv;

namespace EspacioIA
{
    class ChatGPT
    {
        //Atributos------------------------------------------
        private string ApiKey;
        private string Url = "https://api.openai.com/v1/chat/completions";
        private string Comportamiento = "Responde la pregunta devolviendo una de las opciones, la que consideres correcta";
        private string ModeloUsado;// gpt-4o o gpt-3.5-turbo
        //---------------------------------------------------
        //Constructor----------------------------------------
        public ChatGPT(string _modelo)
        {
            // Cargo la variable con la key desde archivo .env
            Env.Load();
            // Leo la key desde el archivo .env
            ApiKey = Env.GetString("OPENAI_API_KEY");
            ModeloUsado = _modelo; // guardo el modelo a usar
        }
        //---------------------------------------------------
        //Metodos--------------------------------------------
        private Peticion CrearPeticion(Pregunta _pregunta)
        {
            string preguntaConOpciones = $"Pregunta: {_pregunta.Texto} - Opciones: " + string.Join(", ", _pregunta.OpcionesRespuestas);
            Peticion peticionEnviar = new Peticion();
            peticionEnviar.model = ModeloUsado;
            peticionEnviar.messages = new List<Mensaje>(){
                new Mensaje {role = "system", content = Comportamiento},
                new Mensaje {role = "user", content = preguntaConOpciones}
            };
            return peticionEnviar;
        }
        public async Task<string> ObtenerRespuesta(Pregunta _pregunta)
        {
            var peticionEnviar = CrearPeticion(_pregunta);

            //usando la estrucura using instancio el objeto httpclient
            using (var client = new HttpClient())
            {
                //agrego mi clave api al encabezado de la solicitud
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                var jsonContent = JsonSerializer.Serialize(peticionEnviar);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var respuesta = JsonSerializer.Deserialize<Respuesta>(responseContent);
                    return respuesta.choices[0].message.content.Trim();
                }
                else
                {
                    throw new Exception($"Error en la llamada a la API: {response.StatusCode}");
                }
            }
        }
    }

    class Mensaje
    {
        public string role { get; set; }//rol que tendra la ia
        public string content { get; set; }//preg con las opciones
    }

    class Peticion
    {
        public string model { get; set; }
        public List<Mensaje> messages { get; set; }
    }
    class Respuesta
    {
        public List<Choice> choices { get; set; }
    }

    class Choice
    {
        public Mensaje message { get; set; }
    }
}