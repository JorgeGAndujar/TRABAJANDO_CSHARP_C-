﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.ProyectoConsultaHospital
{
    public class Consulta
    {
        public string NumeroConsulta { get; set; }
        public string Fecha { get; set; }
        public string NombreMedico { get; set; }
        public string Deinpr { get; set; }
        public string Procedencia {  get; set; }
        public Consulta(string numeroConsulta, string fecha, string nombreMedico, string deinpr, string procedencia)
        {
            NumeroConsulta = numeroConsulta;
            Fecha = fecha;
            NombreMedico = nombreMedico;
            Deinpr = deinpr;
            Procedencia = procedencia;
        }  
    }
}
