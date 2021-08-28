using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_TEST_DEV.Models
{
    public class Tb_PersonasFisicas
    {        
        public int IdPersonaFisica { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public Nullable<int> UsuarioAgrega { get; set; }
        public Nullable<bool> Activo { get; set; }
    }
}