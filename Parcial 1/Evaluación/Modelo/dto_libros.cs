    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Evaluación.Modelo
    {
        public class dto_libros
        {
            public int? LibroID { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public string Genero { get; set; }
            public int? AnioPublicacion { get; set; }
            public bool Disponible { get; set; }
        }
    }
