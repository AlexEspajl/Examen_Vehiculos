﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen_Vehiculos.Modelo
{
    //public class Encabezado
    //{
    //    public List<reponse> CatalogoJsonString { get; set; }
    //}
    public class Estado
    {
        public int iIdEstado { get; set; }
        public object Pais { get; set; }
        public int iEstadoPais { get; set; }
        public int iClaveEstadoCepomex { get; set; }
        public string sEstado { get; set; }
    }

    public class Municipio
    {
        public int iIdMunicipio { get; set; }
        public Estado Estado { get; set; }
        public int iMunicipioEstado { get; set; }
        public int iClaveMunicipioCepomex { get; set; }
        public string sMunicipio { get; set; }
    }

    public class Root
    {
        public Municipio Municipio { get; set; }
        public List<Ubicacion> Ubicacion { get; set; }
    }

    public class Ubicacion
    {
        public int iIdUbicacion { get; set; }
        public object CodigoPostal { get; set; }
        public int iUbicacionCodigoPostal { get; set; }
        public object TipoUbicacion { get; set; }
        public int iClaveUbicacionCepomex { get; set; }
        public object Ciudad { get; set; }
        public string sUbicacion { get; set; }
    }

    public class postPeticion
    {
        public string decripcionId { get; set; }
    }
}
