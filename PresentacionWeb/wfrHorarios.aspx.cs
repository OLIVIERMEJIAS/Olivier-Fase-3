﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidades;

namespace PresentacionWeb
{
    public partial class wfrHorarios : System.Web.UI.Page
    {
        LNDetalleHorario lnDH = new LNDetalleHorario(Config.getCadConec);
        LNAula lnA = new LNAula(Config.getCadConec);
        LNHorario lnH = new LNHorario(Config.getCadConec);
        LNProfesor lnP = new LNProfesor(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
            }
            gdvSecciones.DataSource = lnH.secciones();
            gdvSecciones.DataBind();
        }

        protected void lnkSeleccionarSeccion_Command(object sender, CommandEventArgs e)
        {
            txtSeccion.Text = e.CommandArgument.ToString();
            cargarDatos();
        }

        public void cargarDatos()
        {
            try
            {
                gdvLunes.DataSource = lnH.horarioPorDiaYSeccion(txtSeccion.Text, 'L');
                gdvLunes.DataBind();
                gdvMartes.DataSource = lnH.horarioPorDiaYSeccion(txtSeccion.Text, 'K');
                gdvMartes.DataBind();
                gdvMiercoles.DataSource = lnH.horarioPorDiaYSeccion(txtSeccion.Text, 'M');
                gdvMiercoles.DataBind();
                gdvJueves.DataSource = lnH.horarioPorDiaYSeccion(txtSeccion.Text, 'J');
                gdvJueves.DataBind();
                gdvViernes.DataSource = lnH.horarioPorDiaYSeccion(txtSeccion.Text, 'V');
                gdvViernes.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        protected void btnGenerar_Click(object sender, EventArgs e)
        {

            List<int> horariosId = new List<int> {6,8};
            char[] diasGrupInf = new char[5] { 'L', 'K', 'M', 'J', 'V' };
            char[] diasGrupSup = new char[5] { 'M', 'J', 'V', 'L', 'K' };
            char[] dias;
            byte[] materiasInf = new byte[10] { 1, 2, 3, 4, 5, 8, 9, 10, 11, 12 };
            byte[] materiasSup = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] materias;
            byte materia = 0;
            int horId = 0;
            byte dia = 0;
            char diaSem = ' ';
            int profeId = 0;
            byte limInfAula = 1;
            byte limSupAula = 2;
            bool haySegundProf = true;
            Random ran = new Random();
            int ranNum = 0;
            int iHoraI = 0;
            int iHoraF = 0;
            string[] horasInicio = new string[5] { "07:20", "09:00", "10:40", "13:00", "14:40" };
            string[] horasFin = new string[5] { "08:40", "10:20", "12:00", "14:20", "16:00" };
            string[] horasFinEduFinan = new string[5] { "08:00", "09:40", "11:20", "13:40", "15:20" };
            string horI = "";
            string horF = "";
            bool materiaAgregada = false;
            bool segundoProfe = false;
            bool segundoDiaEsp = false;
            bool segundaEsp = false;
            bool asignacionDos = false;
            bool reiniciarAulas = false;
            char diaSemAux = ' ';
            string auxHoraI = "";
            string disponibleGrupo = "";
            string disponibleAula = "";
            string disponibleProfe = "";
            byte espMaxLecciones = 0;


            //valoración si ya existen registros en la entidad DetallesHorario
            //si los se borran para crear nuevos desde cero
            //try
            //{
            //    if (lnDH.hayRegistros())
            //    {
            //        lnDH.eliminarRegistros();
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Session["_err"] = ex.Message;
            //}

            //instrucciones
            while (horariosId.Count != 0)
            {
                ranNum = ran.Next(horariosId.Count);
                horId = horariosId[ranNum];
                horariosId.Remove(horId);

                //validación para abordar el orden de días de la semana
                //de asignación de horarios según sección
                if (horId <= 9)
                {
                    dias = diasGrupInf;
                    materias = materiasInf;
                }
                else
                {
                    dias = diasGrupSup;
                    materias = materiasSup;
                }
                materia = 0;
                segundoDiaEsp = false;
                asignacionDos = false;
                dia = 0;
                diaSem = dias[dia];
              
                iHoraI = 0;
                iHoraF = 0;
                while (materia < materias.Count())
                {
                    materiaAgregada = false;
                    segundoDiaEsp = false;
                    
                    fijarLimitesAulas(ref limInfAula, ref limSupAula, materias[materia]);
                    segundoProfe = false;
                    auxHoraI = horI;
                    diaSemAux = diaSem;
                    while (limInfAula <= limSupAula && !materiaAgregada)
                    {

                        if ((materias[materia] == 7 && reiniciarAulas == true) || (materias[materia] == 6 && asignacionDos == true && reiniciarAulas == true))
                        {
                            fijarLimitesAulas(ref limInfAula, ref limSupAula, materias[materia]);
                            reiniciarAulas = false;
                            auxHoraI = horI;
                            diaSemAux = diaSem;
                        }
                        if(!segundoProfe)
                            asignaciondeProfe(materias[materia], ref haySegundProf,
                            ref profeId, ref segundoProfe);

                        if (haySegundProf && segundoProfe)
                            segundoProfe = false;

                        asignarHoras(iHoraI, iHoraF,
                                    ref horI, ref horF,
                                    horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                        
                        disponibleGrupo = lnDH.disponibleHoraI(horI, diaSem, horId);
                        if (materias[materia] == 7)
                            disponibleGrupo = "";
                        if (disponibleGrupo == "")
                        {
                            disponibleAula = lnA.disponibleHoraI(horI, diaSem, limInfAula);
                            if (disponibleAula == "")
                            {
                                disponibleProfe = lnP.disponibleHoraI(horI, diaSem, profeId);
                                
                                    if (disponibleProfe == "")
                                    {

                                        //try
                                        //{
                                            switch (materias[materia])
                                            {
                                                case 1:
                                                case 6:
                                                case 7:
                                                    if (materias[materia] == 1)
                                                        espMaxLecciones = 6;
                                                    else
                                                        espMaxLecciones = 4;
                                                    if (lnP.numLecciones(profeId) <= espMaxLecciones)
                                                    {

                                                        EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                        lnDH.agregar(det);
                                                        if (materias[materia] != 1 &&
                                                            !segundoDiaEsp && segundaEsp)
                                                        {
                                                            
                                                            dia++;
                                                            if (dia > 4)
                                                                dia = 0;
                                                            diaSem = dias[dia];
                                                            iHoraI = 0;
                                                            iHoraF = 0;
                                                            materia--;
                                                            segundaEsp = false;
                                                            asignacionDos = true;
                                                            reiniciarAulas = true;
                                                            segundoProfe = false;
                                                        }
                                                        else if (materias[materia] != 1 && !segundoDiaEsp && !segundaEsp)
                                                        {
                                                            if (!asignacionDos)
                                                            {
                                                             
                                                                materia++;
                                                                segundaEsp = true;
                                                                reiniciarAulas = true;
                                                                segundoProfe = false;
                                                            }
                                                            else
                                                            {
                                                                
                                                                materia++;
                                                                segundaEsp = true;
                                                                segundoDiaEsp = true;
                                                                segundoProfe = false;
                                                                
                                                            }
                                                        }
                                                        else
                                                        {
                                                            materiaAgregada = true;
                                                            
                                                        }
                                                    }
                                                    else if (segundoProfe)
                                                    {
                                                        materiaAgregada = true;
                                                    }
                                                    else
                                                    {
                                                        if (haySegundProf && !segundoProfe)
                                                        {
                                                            profeId++;
                                                            segundoProfe = true;
                                                        }
                                                        else if (haySegundProf && segundoProfe)
                                                        {
                                                            materiaAgregada = true;
                                                        }
                                                    }
                                                    break;

                                                case 2:
                                                case 3:
                                                case 4:
                                                case 5:
                                                    if (lnP.numLecciones(profeId) <= 10)
                                                    {
                                                        EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                        lnDH.agregar(det);
                                                        materiaAgregada = true;
                                                    }
                                                    else if (segundoProfe)
                                                    {
                                                        materiaAgregada = true;
                                                    }
                                                    else
                                                    {
                                                        if (haySegundProf && !segundoProfe)
                                                        {
                                                            profeId++;
                                                            segundoProfe = true;
                                                        }
                                                        else if (haySegundProf && segundoProfe)
                                                        {
                                                            materiaAgregada = true;
                                                        }
                                                    }
                                                    break;
                                                case 8:
                                                case 9:
                                                case 11:
                                                case 12:

                                                    if (lnP.numLecciones(profeId) <= 20)
                                                    {
                                                        EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                        lnDH.agregar(det);
                                                        materiaAgregada = true;
                                                       
                                                    }
                                                    else
                                                    {
                                                        materiaAgregada = true;
                                                        
                                                    }
                                                    break;
                                                case 10:

                                                    if (lnP.numLecciones(profeId) <= 40)
                                                    {
                                                        EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                        lnDH.agregar(det);
                                                        materiaAgregada = true;
                                                        
                                                    }
                                                    else
                                                    {
                                                        materiaAgregada = true;
                                                       
                                                    }
                                                    break;
                                            }
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    Session["_err"] = ex.Message;
                                        //}
                                    }
                                    else
                                    {
                                    
                                        if (disponibleProfe.Substring(0, 5) == "08:00" ||
                                            disponibleProfe.Substring(0, 5) == "09:40"
                                            || disponibleProfe.Substring(0, 5) == "11:20" ||
                                            disponibleProfe.Substring(0, 5) == "13:40" ||
                                            disponibleProfe.Substring(0, 5) == "15:20")
                                        {

                                            buscarIndiceHoraFinal(disponibleProfe, horasFinEduFinan,
                                            materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                            ref diaSem, dias, false, true, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe, ref profeId);
                                        }
                                        else
                                            buscarIndiceHoraFinal(disponibleProfe, horasFin,
                                            materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                            ref diaSem, dias, false, true, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe, ref profeId);
                                        
                                    }
                                
                            }
                            else
                            {
                                if (disponibleAula.Substring(0, 5) == "08:00" ||
                                disponibleAula.Substring(0, 5) == "09:40"
                                || disponibleAula.Substring(0, 5) == "11:20" ||
                                disponibleAula.Substring(0, 5) == "13:40" ||
                                disponibleAula.Substring(0, 5) == "15:20")
                                {
                                    buscarIndiceHoraFinal(horI, horasInicio,
                                    materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                    ref diaSem, dias, true, false, ref limInfAula,
                                    ref limSupAula, haySegundProf, ref segundoProfe, ref profeId);
                                }
                                else
                                    buscarIndiceHoraFinal(horI, horasInicio,
                                    materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                    ref diaSem, dias, true, false, ref limInfAula,
                                    ref limSupAula, haySegundProf, ref segundoProfe, ref profeId);
                            }
                        }
                        else
                        {
                            if (disponibleGrupo.Substring(0, 5) == "08:00" ||
                                disponibleGrupo.Substring(0, 5) == "09:40"
                                || disponibleGrupo.Substring(0, 5) == "11:20" ||
                                disponibleGrupo.Substring(0, 5) == "13:40" ||
                                disponibleGrupo.Substring(0, 5) == "15:20")
                            {
                                buscarIndiceHoraFinal(disponibleGrupo, horasFinEduFinan,
                                materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                ref diaSem, dias, false, false, ref limInfAula,
                                ref limSupAula,haySegundProf, ref segundoProfe, ref profeId);
                                asignarHoras(iHoraI, iHoraF,
                                    ref horI, ref horF,
                                    horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                if ((diaSem == diaSemAux) && (auxHoraI == horI))
                                {
                                    
                                    if(materias[materia] != 6 && materias[materia] != 7)
                                    {
                                        materiaAgregada = true;
                                    }
                                    else
                                    {
                                        reiniciarAulas = true;
                                        if (!asignacionDos)
                                            asignacionDos = true;
                                        segundoProfe = false;
                                    }
                                    materia++;

                                }
                            }
                            else
                                buscarIndiceHoraFinal(disponibleGrupo, horasFin,
                                materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                ref diaSem, dias, false, false, ref limInfAula,
                                ref limSupAula, haySegundProf, ref segundoProfe, ref profeId);
                            asignarHoras(iHoraI, iHoraF,
                                    ref horI, ref horF,
                                    horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                            if ((diaSem == diaSemAux) && (auxHoraI == horI))
                            {
                                
                                if (materias[materia] != 6 && materias[materia] != 7)
                                {
                                    materiaAgregada = true;
                                }
                                else
                                {
                                    reiniciarAulas = true;
                                    if (!asignacionDos)
                                        asignacionDos = true;
                                    segundoProfe = false;
                                }
                                materia++;
                            }
                        }
                    }
                    materia++;
                    
                }
            }
        }

        private void asignaciondeProfe(byte materia, ref bool haySegundProf,
            ref int profeId, ref bool segundoProfe)
        {
            if (materia <= 5 || materia == 7 || materia ==6)
            {
                haySegundProf = true;
                segundoProfe = false;
                profeId = lnP.accederAProfesor(materia);
            }
            else
            {
                haySegundProf = false;
                segundoProfe = false;
                profeId = lnP.accederAProfesor(materia);
            }
            
        }

        private void asignarHoras(int iHoraI, int iHoraF, 
            ref string horI, ref string horF,
            string[] horasInicio,
            string[] horasFin, string[] horasFinEduFinan, byte materia)
        {
            horI = horasInicio[iHoraI];
            switch (materia)
            {
                case 1:
                case 6:
                case 7:
                    if (iHoraF == 0)
                        iHoraF = iHoraF + 2;
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    if (iHoraF == 0)
                        iHoraF = iHoraF + 1;
                    break;
            }


            if (materia == 10)
                horF = horasFinEduFinan[iHoraF];
            else
                horF = horasFin[iHoraF];
        }

        private void fijarLimitesAulas(ref byte limInfAula, ref byte limSupAula, byte materia)
        {
            if (materia == 6 || materia == 12)
            {
                limInfAula = 11;
                limSupAula = 12;
            }
            else if (materia == 7)
            {
                limInfAula = 9;
                limSupAula = 10;
            }
            else
            {
                limInfAula = 1;
                limSupAula = 8;
            }
        }
        private void avanzarHoras(string[] horasFin, byte materia, ref int iHoraI,
            ref int iHoraF, string horaFinal, ref byte dia, ref char diaSem, char[] dias)
        {
            for (int i = 0; i < horasFin.Length; i++)
            {
                if (horaFinal.Substring(0, 5) == horasFin[i])
                {
                    iHoraI = i + 1;
                    if (materia == 1 || materia == 6
                        || materia == 7)
                        iHoraF = iHoraI + 2;
                    else if (materia != 8 && materia != 9 && materia != 11 && materia != 12 && materia != 10)
                        iHoraF = iHoraI + 1;
                    else
                        iHoraF = iHoraI;
                    if (iHoraI > 4 || iHoraF > 4)
                    {
                        iHoraI = 0;
                        iHoraF = 0;
                        dia++;
                        if (dia > 4)
                            dia = 0;
                        diaSem = dias[dia];
                    }

                    break;
                }
            }
        }

        private void buscarIndiceHoraFinal(string horaFinal, string[] horasFin
            ,byte materia, ref int iHoraI, ref int iHoraF, ref byte dia, ref char diaSem, char[] dias,
            bool evalAulas, bool evalProfe, ref byte limInfAula, ref byte limSupAula, 
            bool haySegundProf, ref bool segundoProfe, ref int profeId)
        {

            if (evalAulas && !evalProfe)
            {
                if (limInfAula != limSupAula)
                    limInfAula++;
                else
                {
                    fijarLimitesAulas(ref limInfAula, ref limSupAula, materia);
                    avanzarHoras(horasFin, materia, ref iHoraI,
                    ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                }

            }
            else if (!evalAulas && evalProfe)
            {
                if (haySegundProf && !segundoProfe)
                {
                    profeId++;
                    segundoProfe = true;
                }
                else if(haySegundProf && segundoProfe)
                {
                    
                    avanzarHoras(horasFin, materia, ref iHoraI,
                    ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                    
                }
                else
                {
                    
                    avanzarHoras(horasFin, materia, ref iHoraI,
                    ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                }
            }
            else
            {
                avanzarHoras(horasFin, materia, ref iHoraI,
                    ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
            }
        }

    }
}