using System;
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
        LNProfesor lnP = new LNProfesor(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {

            List<int> horariosId = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
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
            string disponibleGrupo = "";
            string disponibleAula = "";
            string disponibleProfe = "";


            //valoración si ya existen registros en la entidad DetallesHorario
            //si los se borran para crear nuevos desde cero
            try
            {
                if (lnDH.hayRegistros())
                {
                    lnDH.eliminarRegistros();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }

            //instrucciones
            while (horariosId.Count != 0)
            {
                ranNum = ran.Next(horariosId.Count);
                horId = horariosId[ranNum];
                horariosId.Remove(ranNum);

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


                //ciclo que controla los días de la semana

                materia = 0;
                

                while (materia != materias.Count())
                {
                    materiaAgregada = false;
                    segundoProfe = false;
                    dia = 0;
                    while (dia < 5 && !materiaAgregada && !segundoProfe)
                    {

                        //asignación del día de la semana
                        switch (dia)
                        {
                            case 0:
                                diaSem = dias[0];
                                break;
                            case 1:
                                diaSem = dias[1];
                                break;
                            case 2:
                                diaSem = dias[2];
                                break;
                            case 3:
                                diaSem = dias[3];
                                break;
                            case 4:
                                diaSem = dias[4];
                                break;
                        }

                        iHoraI = 0;
                        iHoraF = 0;
                        while (iHoraF <= 4 && iHoraI <= 4 && !materiaAgregada && !segundoProfe)
                        {
                            if (materias[materia] == 6 || materias[materia] == 12)
                            {
                                limInfAula = 11;
                                limSupAula = 12;
                            }
                            else if (materias[materia] == 7)
                            {
                                limInfAula = 9;
                                limSupAula = 10;
                            }
                            else
                            {
                                limInfAula = 1;
                                limSupAula = 8;
                            }
                            while (limInfAula <= limSupAula && !materiaAgregada)
                            {
                                if (materias[materia] <= 5 || materias[materia] == 7)
                                {
                                    haySegundProf = true;
                                    profeId = lnP.accederAProfesor(materias[materia]);
                                }
                                else
                                {
                                    haySegundProf = false;
                                    profeId = lnP.accederAProfesor(materias[materia]);
                                }
                                segundoProfe = false;

                                horI = horasInicio[iHoraI];
                                switch (materias[materia])
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


                                if (materias[materia] == 10)
                                    horF = horasFinEduFinan[iHoraF];
                                else
                                    horF = horasFin[iHoraF];
                                disponibleGrupo = lnDH.disponibleHoraI(horI, diaSem, horId);
                                disponibleAula = lnA.disponibleHoraI(horI, diaSem, limInfAula);
                                disponibleProfe = lnP.disponibleHoraI(horI, diaSem, profeId);

                                if (disponibleGrupo == "")
                                {

                                    if (disponibleAula == "")
                                    {

                                        while (!segundoProfe)
                                        {
                                            if (disponibleProfe == "")
                                            {

                                                try
                                                {
                                                    switch (materias[materia])
                                                    {
                                                        case 1:
                                                        case 6:
                                                        case 7:
                                                            if (lnP.numLecciones(profeId) <= 6)
                                                            {

                                                                EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                                lnDH.agregar(det);
                                                                materiaAgregada = true;
                                                                segundoProfe = true;
                                                            }
                                                            else
                                                            {
                                                                if (haySegundProf && !segundoProfe)
                                                                {
                                                                    profeId++;
                                                                    haySegundProf = false;
                                                                    iHoraI = iHoraI + 2;
                                                                    iHoraF = iHoraF + 2;
                                                                }
                                                                else if (!haySegundProf)
                                                                {
                                                                    segundoProfe = true;
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
                                                                segundoProfe = true;
                                                            }
                                                            else
                                                            {
                                                                if (haySegundProf && !segundoProfe)
                                                                {
                                                                    profeId++;
                                                                    segundoProfe = true;
                                                                    iHoraI = iHoraI + 1;
                                                                    iHoraF = iHoraF + 1;
                                                                }
                                                                else if (!haySegundProf)
                                                                {
                                                                    segundoProfe = true;
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
                                                                segundoProfe = true;
                                                            }
                                                            else
                                                            {
                                                                iHoraI = iHoraI + 1;
                                                                iHoraF = iHoraF + 1;
                                                            }
                                                            break;
                                                        case 10:

                                                            if (lnP.numLecciones(profeId) <= 40)
                                                            {
                                                                EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                                lnDH.agregar(det);
                                                                materiaAgregada = true;
                                                                segundoProfe = true;
                                                            }
                                                            else
                                                            {
                                                                iHoraI = iHoraI + 1;
                                                                iHoraF = iHoraF + 1;
                                                            }
                                                            break;
                                                    }

                                                }
                                                catch (Exception ex)
                                                {

                                                    Session["_err"] = ex.Message;
                                                }

                                            }
                                            else
                                            {

                                                if ((materias[materia] == 1 || materias[materia] == 6
                                        || materias[materia] == 7) && iHoraI >= 3)
                                                {
                                                    iHoraF = 0;
                                                    iHoraI = 0;
                                                    limInfAula++;
                                                }
                                                else if (materias[materia] != 1 && materias[materia] != 6
                                                        && materias[materia] != 7 && iHoraI >= 4)
                                                {
                                                    iHoraF = 0;
                                                    iHoraI = 0;
                                                    limInfAula++;
                                                }
                                                else
                                                {
                                                    buscarIndiceHoraFinal(disponibleProfe, horasFin,
                                                    materias[materia], ref iHoraI, ref iHoraF);
                                                }
                                            }


                                        }


                                    }
                                    else
                                    {
                                        if ((materias[materia] == 1 || materias[materia] == 6
                                           || materias[materia] == 7) && iHoraI >= 3)
                                        {
                                            iHoraF = 0;
                                            iHoraI = 0;
                                            limInfAula++;
                                        }
                                        else if (materias[materia] != 1 && materias[materia] != 6
                                                && materias[materia] != 7 && iHoraI >= 4)
                                        {
                                            iHoraF = 0;
                                            iHoraI = 0;
                                            limInfAula++;
                                        }
                                        else
                                        {
                                            buscarIndiceHoraFinal(disponibleAula, horasFin,
                                            materias[materia], ref iHoraI, ref iHoraF);
                                        }
                                    }


                                }
                                else
                                { if ((materias[materia] == 1 || materias[materia] == 6
                                          || materias[materia] == 7) && iHoraI >= 3)
                                    {
                                        iHoraF = 0;
                                        iHoraI = 0;
                                        limInfAula++;
                                    }
                                    else if (iHoraI >= 4)
                                    {
                                        iHoraF = 0;
                                        iHoraI = 0;
                                        limInfAula++;
                                    }
                                    else
                                    {
                                        buscarIndiceHoraFinal(disponibleGrupo, horasFin,
                                        materias[materia], ref iHoraI, ref iHoraF);
                                    }
                                    
                                }


                            }

                            dia++;
                        }
                        //instrucciones
                        materia++;
                    }
                }

            }
        }

        private void buscarIndiceHoraFinal(string horaFinal, string[] horasFin
            , byte materia, ref int iHoraI, ref int iHoraF)
        {
            for (int i = 0; i < horasFin.Length; i++)
            {
                if (horaFinal.Substring(0,5) == horasFin[i])
                {
                    iHoraI = i + 1;
                    if (materia == 1 || materia == 6
                        || materia == 7)
                    {

                        iHoraF = iHoraI + 2;
                    }
                    else
                    {

                        iHoraF = iHoraI + 1;
                    }
                }
            }
        }
    }
}