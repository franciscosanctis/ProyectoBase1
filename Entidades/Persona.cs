using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBase1.Entidades
{
    public class Persona
    {
        private string Documento;
        private string Apellido;
        private string Nombre;
        private DateTime FechaNacimiento;
        private int IdSexo;
        private int IdTipoDocumento;
        private string Calle;
        private string NroCasa;
        private bool Soltero;
        private bool Casado;
        private bool Hijos;
        private int CantidadHijos;
        private int IdCarrera;

        public Persona(string documento, string apellido, string nombre)
        {
            Documento = documento;
            Apellido = apellido;
            Nombre = nombre;
        }

        public Persona()
        {

        }

        public int CantidadHijosPersona
        {
            get => CantidadHijos;
            set => CantidadHijos = value;
        }

        public int CarreraPersona
        {
            get => IdCarrera;
            set => IdCarrera = value;
        }

        public bool HijosPersona
        {
            get => Hijos;
            set => Hijos = value;
        }

        public bool CasadoPersona
        {
            get => Casado;
            set => Casado = value;
        }

        public bool SolteroPersona
        {
            get => Soltero;
            set => Soltero = value;
        }

        public string NroCasaPersona
        {
            get => NroCasa;
            set => NroCasa = value;
        }

        public string CallePersona
        {
            get => Calle;
            set => Calle = value;
        }

        public string DocumentoPersona
        {
            get => Documento;
            set => Documento = value;
        }

        public int TipoDocumentoPersona
        {
            get => IdTipoDocumento;
            set => IdTipoDocumento = value;
        }

        public int SexoPersona
        {
            get => IdSexo;
            set => IdSexo = value;
        }

        public DateTime FechaNacimientoPersona
        {
            get => FechaNacimiento;
            set => FechaNacimiento = value;
        }

        public string ApellidoPersona
        {
            get => Apellido;
            set => Apellido = value;
        }
        public string NombrePersona
        {
            get => Nombre;
            set => Nombre = value;
        }
    }
}
