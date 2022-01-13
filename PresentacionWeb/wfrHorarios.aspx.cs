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
        LNHorario lnH = new LNHorario(Config.getCadConec);
        LNProfesor lnP = new LNProfesor(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["_director"] != null)
            {
                btnGenerar.Enabled = false;
            }
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

            List<int> horariosId = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13};
            char[] diasGrupInf = new char[5] { 'L', 'K', 'M', 'J', 'V' };
            char[] diasGrupSup = new char[5] { 'M', 'J', 'V', 'L', 'K' };
            char[] dias;
            byte[] materiasInf = new byte[10] { 1, 2, 3, 4, 5, 8, 9, 10, 11, 12 };
            byte[] materiasSup = new byte[8] { 1, 6, 7, 2, 3, 4, 5, 8 };
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
            string horF1Profe = "";
            string horF2Profe = "";
            string horF3Profe = "";
            string horFAula1 = "";
            string horFAula2 = "";
            string horFAula3 = "";
            string horFAula4 = "";
            string horFAula5 = "";
            string horFAula6 = "";
            string horFAula7 = "";
            string horFAula8 = "";
            string horFAula1Otros = "";
            string horFAula2Otros = "";
            
            byte diaAula1 = 0;
            byte diaAula2 = 0;
            byte diaAula3 = 0;
            byte diaAula4 = 0;
            byte diaAula5 = 0;
            byte diaAula6 = 0;
            byte diaAula7 = 0;
            byte diaAula8 = 0;
            byte diaAula1Otros = 0;
            byte diaAula2Otros = 0;
            byte diaProfe1 = 0;
            byte diaProfe2 = 0;
            byte diaProfe3 = 0;
            bool materiaAgregada = false;
            bool segundoProfe = false;
            bool segundoDiaEsp = false;
            bool segundaEsp = false;
            bool asignacionDos = false;
            bool reiniciarAulas = false;
            bool usoDeTercerProfe = false;
            bool primeraRondaProfesores = true;
            bool primeraRondaAulas = true;
            bool aulas2 = false;
            bool aulas8 = false;
            bool inicio = true;
            bool inicioProfes = true;
            bool compEspAgregada = false;
            bool hayTresProfes = false;
            char diaSemAux = ' ';
            string auxHoraI = "";
            string auxHorIProfe = "";
            string auxHorIGrupo = "";
            string disponibleGrupo = "";
            string disponibleAula = "";
            string disponibleProfe = "";
            byte espMaxLecciones = 0;


            //valoración si ya existen registros en la entidad DetallesHorario
            //si los se borran para crear nuevos desde cero
            try
            {
                if (lnDH.hayRegistros())
                {
                    lnDH.eliminarRegistros();
                }

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
                    compEspAgregada = false;
                    
                    while (materia < materias.Count())
                    {
                        materiaAgregada = false;
                        segundoDiaEsp = false;
                        aulas2 = false;
                        aulas8 = false;
                    fijarLimitesAulas(ref limInfAula, ref limSupAula,
                            materias[materia], ref aulas2, ref aulas8);
                        segundoProfe = false;
                        hayTresProfes = false;
                        inicio = true;
                        inicioProfes = true;
                        diaSemAux = diaSem;
                        auxHoraI = "";
                        auxHorIGrupo = "";
                        auxHorIProfe = "";
                        primeraRondaProfesores = true;
                        primeraRondaAulas = true;
                    while (limInfAula <= limSupAula && !materiaAgregada)
                    {

                        if ((materias[materia] == 7 && reiniciarAulas == true) || (materias[materia] == 6 && asignacionDos == true && reiniciarAulas == true))
                        {
                            fijarLimitesAulas(ref limInfAula,
                                ref limSupAula, materias[materia], ref aulas2,
                                ref aulas8);
                            reiniciarAulas = false;
                            if (auxHoraI == "")
                            {
                                auxHoraI = horI;
                                diaSemAux = diaSem;
                            }
                        }
                        if (!segundoProfe)
                            asignaciondeProfe(materias[materia], ref haySegundProf,
                            ref profeId, ref segundoProfe, ref usoDeTercerProfe, ref hayTresProfes);

                            asignarHoras(iHoraI, iHoraF,
                                        ref horI, ref horF,
                                        horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                            if (auxHorIGrupo == "")
                            {
                                auxHorIGrupo = horI;

                            }
                            disponibleGrupo = lnDH.disponibleHoraI(horI, diaSem, horId);
                            if (materias[materia] == 7 && compEspAgregada == true)
                                disponibleGrupo = "";
                            if (disponibleGrupo == "")
                            {
                                if (inicio)
                                {
                                    diaSemAux = diaSem;
                                    auxHoraI = horI;
                                    inicio = false;
                                }
                                disponibleAula = lnA.disponibleHoraI(horI, diaSem, limInfAula);

                                respaldoHorasFinalesAulas(primeraRondaAulas,
                                aulas2, limInfAula, disponibleAula,
                                ref horFAula1Otros, ref horFAula2Otros,
                                ref horFAula1, ref horFAula2, ref horFAula3,
                                ref horFAula4, ref horFAula5, ref horFAula6,
                                ref horFAula7,
                                ref horFAula8, ref diaAula1Otros, ref diaAula2Otros,
                                ref diaAula1, ref diaAula2, ref diaAula3, ref diaAula4,
                                ref diaAula5, ref diaAula6, ref diaAula7, ref diaAula8,
                                dia
                                );

                                if (disponibleAula == "")
                                {
                                    if (inicioProfes)
                                    {
                                        diaSemAux = diaSem;
                                        auxHorIProfe = horI;
                                        inicioProfes = false;
                                    }
                                    disponibleProfe = lnP.disponibleHoraI(horI, diaSem, profeId);

                                    if (haySegundProf && !segundoProfe)
                                    {
                                        horF1Profe = disponibleProfe;
                                        if (primeraRondaProfesores)
                                            diaProfe1 = dia;
                                    }
                                    else if (haySegundProf && segundoProfe && !usoDeTercerProfe)
                                    {
                                        horF2Profe = disponibleProfe;
                                        if(primeraRondaProfesores)
                                            diaProfe2 = dia;

                                }
                                    else if (haySegundProf && segundoProfe && usoDeTercerProfe)
                                    {
                                        horF3Profe = disponibleProfe;
                                        if (primeraRondaProfesores)
                                            diaProfe3 = dia;

                                }

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
                                                    auxHoraI = "";
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
                                                        compEspAgregada = false;
                                                        primeraRondaAulas = true;
                                                        primeraRondaProfesores = true;
                                                }
                                                    else if (materias[materia] != 1 && !segundoDiaEsp && !segundaEsp)
                                                    {
                                                        if (!asignacionDos)
                                                        {

                                                            materia++;
                                                            segundaEsp = true;
                                                            reiniciarAulas = true;
                                                            segundoProfe = false;
                                                            compEspAgregada = true;
                                                            primeraRondaAulas = true;
                                                            primeraRondaProfesores = true;
                                                        }
                                                        else
                                                        {

                                                            materia++;
                                                            segundaEsp = true;
                                                            segundoDiaEsp = true;
                                                            segundoProfe = false;
                                                            reiniciarAulas = true;
                                                            compEspAgregada = true;


                                                        }
                                                    }
                                                    else
                                                    {
                                                        materiaAgregada = true;

                                                    }
                                                }
                                                else if (segundoProfe && materias[materia] != 1)
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
                                                        if (materias[materia] == 1 && !usoDeTercerProfe)
                                                        {
                                                            profeId++;
                                                            usoDeTercerProfe = true;
                                                            segundoProfe = true;
                                                        }
                                                        else
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

                                    }
                                    else
                                    {
                                        if (!haySegundProf)
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
                                                ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                                                ref usoDeTercerProfe, ref reiniciarAulas,
                                                ref primeraRondaProfesores, ref primeraRondaAulas,
                                                ref aulas2, ref aulas8);
                                            }
                                            else
                                                buscarIndiceHoraFinal(disponibleProfe, horasFin,
                                                materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                                ref diaSem, dias, false, true, ref limInfAula,
                                                ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                                                ref usoDeTercerProfe, ref reiniciarAulas,
                                                ref primeraRondaProfesores, ref primeraRondaAulas,
                                                ref aulas2, ref aulas8);
                                                asignarHoras(iHoraI, iHoraF,
                                                ref horI, ref horF,
                                                horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                                siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                                auxHorIProfe, horI, materias[materia], ref materiaAgregada,
                                                ref materia, ref reiniciarAulas, ref segundoProfe,
                                                ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                                ref compEspAgregada, ref dia, dias, ref iHoraI,
                                                ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                                        }
                                        else if (primeraRondaProfesores)

                                        {
                                            buscarIndiceHoraFinal(horI, horasInicio,
                                            materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                            ref diaSem, dias, false, true, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe,
                                            ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                            ref primeraRondaProfesores, ref primeraRondaAulas,
                                            ref aulas2, ref aulas8);
                                        }
                                        else if (!primeraRondaProfesores && !segundoProfe)
                                        {
                                            valorarHorasYAvanzar(horF2Profe, horasFinEduFinan,
                                            horasFin, ref iHoraI, ref iHoraF, ref diaProfe2,
                                            ref diaSem, dias, false, true, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe,
                                            ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                            ref primeraRondaProfesores,
                                            ref primeraRondaAulas, materias[materia],
                                            ref aulas2, ref aulas8);
                                        }
                                        else if (!primeraRondaProfesores && segundoProfe && materias[materia] != 1)
                                        {
                                            valorarHorasYAvanzar(horF1Profe, horasFinEduFinan,
                                            horasFin, ref iHoraI, ref iHoraF, ref diaProfe1,
                                            ref diaSem, dias, true, false, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe,
                                            ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                            ref primeraRondaProfesores,
                                            ref primeraRondaAulas, materias[materia],
                                            ref aulas2, ref aulas8);
                                            asignarHoras(iHoraI, iHoraF,
                                            ref horI, ref horF,
                                            horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                            siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                            auxHorIProfe, horI, materias[materia], ref materiaAgregada,
                                            ref materia, ref reiniciarAulas, ref segundoProfe,
                                            ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                            ref compEspAgregada, ref dia, dias, ref iHoraI,
                                            ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                                        }
                                        else if (!primeraRondaProfesores && segundoProfe && materias[materia] == 1 && !usoDeTercerProfe)
                                        {
                                            valorarHorasYAvanzar(horF3Profe, horasFinEduFinan,
                                            horasFin, ref iHoraI, ref iHoraF, ref diaProfe3,
                                            ref diaSem, dias, true, false, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe,
                                            ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                            ref primeraRondaProfesores,
                                            ref primeraRondaAulas, materias[materia],
                                            ref aulas2, ref aulas8);
                                            asignarHoras(iHoraI, iHoraF,
                                            ref horI, ref horF,
                                            horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                            siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                            auxHorIProfe, horI, materias[materia], ref materiaAgregada,
                                            ref materia, ref reiniciarAulas, ref segundoProfe,
                                            ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                            ref compEspAgregada, ref dia, dias, ref iHoraI,
                                            ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                                        }
                                        else if (!primeraRondaProfesores && segundoProfe && usoDeTercerProfe)
                                        {
                                            valorarHorasYAvanzar(horF1Profe, horasFinEduFinan,
                                            horasFin, ref iHoraI, ref iHoraF, ref diaProfe1,
                                            ref diaSem, dias, true, false, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe,
                                            ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                            ref primeraRondaProfesores,
                                            ref primeraRondaAulas, materias[materia],
                                            ref aulas2, ref aulas8);
                                            asignarHoras(iHoraI, iHoraF,
                                            ref horI, ref horF,
                                            horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                            siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                            auxHorIProfe, horI, materias[materia], ref materiaAgregada,
                                            ref materia, ref reiniciarAulas, ref segundoProfe,
                                            ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                            ref compEspAgregada, ref dia, dias, ref iHoraI,
                                            ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                                        }


                                    }

                                }
                                else
                                {
                                    if (primeraRondaAulas)
                                        buscarIndiceHoraFinal(horI, horasInicio,
                                    materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                    ref diaSem, dias, true, false, ref limInfAula,
                                    ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                                    ref usoDeTercerProfe, ref reiniciarAulas,
                                    ref primeraRondaProfesores, ref primeraRondaAulas,
                                    ref aulas2, ref aulas8);
                                    else
                                    {
                                        if (aulas2)
                                        {
                                            if (limInfAula == 9 || limInfAula == 11)
                                            {
                                                valorarHorasYAvanzar(horFAula2Otros, horasFinEduFinan,
                                                horasFin, ref iHoraI, ref iHoraF, ref diaAula2Otros,
                                                ref diaSem, dias, true, false, ref limInfAula,
                                                ref limSupAula, haySegundProf, ref segundoProfe,
                                                ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                ref primeraRondaProfesores,
                                                ref primeraRondaAulas, materias[materia],
                                                ref aulas2, ref aulas8);
                                                asignarHoras(iHoraI, iHoraF,
                                                ref horI, ref horF,
                                                horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                                siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                                auxHoraI, horI, materias[materia], ref materiaAgregada,
                                                ref materia, ref reiniciarAulas, ref segundoProfe,
                                                ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                                ref compEspAgregada, ref dia, dias, ref iHoraI,
                                                ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                                            }
                                            else
                                                valorarHorasYAvanzar(horFAula1Otros, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula1Otros,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                        }
                                        else
                                        {
                                            switch (limInfAula)
                                            {
                                                case 1:
                                                    valorarHorasYAvanzar(horFAula2, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula2,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                                    break;
                                                case 2:
                                                    valorarHorasYAvanzar(horFAula3, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula3,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                                    break;
                                                case 3:
                                                    valorarHorasYAvanzar(horFAula4, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula4,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                                    break;
                                                case 4:
                                                    valorarHorasYAvanzar(horFAula5, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula5,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                                    break;
                                                case 5:
                                                    valorarHorasYAvanzar(horFAula6, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula6,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                                    break;
                                                case 6:
                                                    valorarHorasYAvanzar(horFAula7, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula7,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                                    break;
                                                case 7:
                                                    valorarHorasYAvanzar(horFAula8, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula8,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);
                                                    asignarHoras(iHoraI, iHoraF,
                                                    ref horI, ref horF,
                                                    horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                                    siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                                    auxHoraI, horI, materias[materia], ref materiaAgregada,
                                                    ref materia, ref reiniciarAulas, ref segundoProfe,
                                                    ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                                    ref compEspAgregada, ref dia, dias, ref iHoraI,
                                                    ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                                                    break;
                                                case 8:
                                                    valorarHorasYAvanzar(horFAula1, horasFinEduFinan,
                                                    horasFin, ref iHoraI, ref iHoraF, ref diaAula1,
                                                    ref diaSem, dias, true, false, ref limInfAula,
                                                    ref limSupAula, haySegundProf, ref segundoProfe,
                                                    ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                                    ref primeraRondaProfesores,
                                                    ref primeraRondaAulas, materias[materia],
                                                    ref aulas2, ref aulas8);

                                                    break;
                                            }
                                        }

                                    }
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
                                    ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                                    ref usoDeTercerProfe, ref reiniciarAulas,
                                    ref primeraRondaProfesores, ref primeraRondaAulas,
                                    ref aulas2, ref aulas8);
                                    asignarHoras(iHoraI, iHoraF,
                                    ref horI, ref horF,
                                    horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                    siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                    auxHorIGrupo, horI, materias[materia], ref materiaAgregada,
                                    ref materia, ref reiniciarAulas, ref segundoProfe,
                                    ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                    ref compEspAgregada, ref dia, dias, ref iHoraI,
                                    ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                                }
                                else
                                    buscarIndiceHoraFinal(disponibleGrupo, horasFin,
                                    materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                    ref diaSem, dias, false, false, ref limInfAula,
                                    ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                                    ref usoDeTercerProfe, ref reiniciarAulas,
                                    ref primeraRondaProfesores, ref primeraRondaAulas,
                                    ref aulas2, ref aulas8);
                                asignarHoras(iHoraI, iHoraF,
                                        ref horI, ref horF,
                                        horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                auxHorIGrupo, horI, materias[materia], ref materiaAgregada,
                                ref materia, ref reiniciarAulas, ref segundoProfe,
                                ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                ref compEspAgregada, ref dia, dias, ref iHoraI,
                                ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores);
                            }
                        }
                        materia++;

                    }
                }
                cargarDatos();
                Session["_exito"] = "Horarios creados con éxito!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }

        private void siNoHayDisponiblilidad(ref char diaSem, char diaSemAux,
            string auxHorI, string horI, byte materia, ref bool materiaAgregada,
            ref byte indiceMateria, ref bool reiniciarAulas, ref bool segundoProfe,
            ref bool asignacionDos, ref bool segundaEsp, ref bool segundoDiaEsp,
            ref bool compEspAgregada, ref byte dia, char[] dias, ref int iHoraI, 
            ref int iHoraF, ref bool primeraRondaAulas, ref bool primeraRondaProfesores)
        {
            if ((diaSem == diaSemAux) && (auxHorI == horI))
            {

                if (materia != 6 && materia != 7)
                {
                    materiaAgregada = true;
                }
                else
                {
                    if (!segundoDiaEsp && segundaEsp)
                    {
                        dia++;
                        if (dia > 4)
                            dia = 0;
                        diaSem = dias[dia];
                        iHoraI = 0;
                        iHoraF = 0;
                        indiceMateria--;
                        segundaEsp = false;
                        asignacionDos = true;
                        reiniciarAulas = true;
                        segundoProfe = false;
                        compEspAgregada = false;
                        primeraRondaAulas = true;
                        primeraRondaProfesores = true;
                    }
                    else if (!segundoDiaEsp && !segundaEsp)
                    {
                        if (!asignacionDos)
                        {

                            indiceMateria++;
                            segundaEsp = true;
                            reiniciarAulas = true;
                            segundoProfe = false;
                            compEspAgregada = true;
                            primeraRondaAulas = true;
                            primeraRondaProfesores = true;
                        }
                        else
                        {
                            indiceMateria++;
                            segundaEsp = true;
                            segundoDiaEsp = true;
                            segundoProfe = false;
                            reiniciarAulas = true;
                            compEspAgregada = true;
                        }
                    }
                    else
                        materiaAgregada = true;
                }
            }
        }

        private void respaldoHorasFinalesAulas(bool primeraRondaAulas,
            bool aulas2, byte limInfAula, string disponible,
            ref string horFAula1Otros, ref string horFAula2Otros, 
            ref string horFAula1, ref string horFAula2, ref string horFAula3,
            ref string horFAula4, ref string horFAula5, ref string horFAula6,
            ref string horFAula7,
            ref string horFAula8,
            ref byte diaAula1Otros, ref byte diaAula2Otros,
            ref byte diaAula1, ref byte diaAula2, ref byte diaAula3,
            ref byte diaAula4, ref byte diaAula5, ref byte diaAula6,
            ref byte diaAula7,
            ref byte diaAula8, byte dia
            )
        {
            if (aulas2)
            {
                if (limInfAula == 9 || limInfAula == 11)
                {
                    horFAula1Otros = disponible;
                    if(primeraRondaAulas)
                        diaAula1Otros = dia;
                }
                else if (limInfAula == 10 || limInfAula == 12)
                {
                    horFAula2Otros = disponible;
                    if(primeraRondaAulas)
                        diaAula2Otros = dia;
                    
                }
            }
            else
                switch (limInfAula)
                {
                    case 1:
                        horFAula1 = disponible;
                        if (primeraRondaAulas)
                            diaAula1 = dia;
                        break;
                    case 2:
                        horFAula2 = disponible;
                        if (primeraRondaAulas)
                            diaAula2 = dia;
                        break;
                    case 3:
                        horFAula3 = disponible;
                        if (primeraRondaAulas)
                            diaAula3 = dia;
                        break;
                    case 4:
                        horFAula4 = disponible;
                        if (primeraRondaAulas)
                            diaAula4 = dia;
                        break;
                    case 5:
                        horFAula5 = disponible;
                        if (primeraRondaAulas)
                            diaAula5 = dia;
                        break;
                    case 6:
                        horFAula6 = disponible;
                        if (primeraRondaAulas)
                            diaAula6 = dia;
                        break;
                    case 7:
                        horFAula7 = disponible;
                        if (primeraRondaAulas)
                            diaAula7 = dia;
                        break;
                    case 8:
                        horFAula8 = disponible;
                        if (primeraRondaAulas)
                            diaAula8 = dia;

                        break;
                }


            
        }

        private void asignaciondeProfe(byte materia, ref bool haySegundProf,
            ref int profeId, ref bool segundoProfe, ref bool usoDeTercerProfe, ref bool
            hayTresProfes)
        {
            if (materia <= 5 || materia == 7 || materia == 6)
            {
                haySegundProf = true;
                segundoProfe = false;
                profeId = lnP.accederAProfesor(materia);
                usoDeTercerProfe = false;
                if(materia == 1){
                    hayTresProfes = true;
                }
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

        private void fijarLimitesAulas(ref byte limInfAula,
            ref byte limSupAula, byte materia, ref bool aulas2, ref bool aulas8)
        {
            if (materia == 6 || materia == 12)
            {
                limInfAula = 11;
                limSupAula = 12;
                aulas2 = true;
            }
            else if (materia == 7)
            {
                limInfAula = 9;
                limSupAula = 10;
                aulas2 = true;
            }
            else
            {
                limInfAula = 1;
                limSupAula = 8;
                aulas8 = true;
            }
        }

        private void valorarHorasYAvanzar(string disponible, string[] horasFinEduFinan,
            string[] horasFin, ref int iHoraI, ref int iHoraF, ref byte dia,
                ref char diaSem, char[] dias, bool evalAulas, bool evalProfe, ref byte limInfAula,
                ref byte limSupAula, bool haySegundProf, ref bool segundoProfe, ref int profeId,
                ref bool usoDeTercerProfe, ref bool reiniciarAulas,
                 ref bool primeraRondaProfesores, ref bool primeraRondaAulas, byte materia, ref bool aulas2, ref bool aulas8)
        {
            if (disponible.Substring(0, 5) == "08:00" ||
                disponible.Substring(0, 5) == "09:40"
                || disponible.Substring(0, 5) == "11:20" ||
                disponible.Substring(0, 5) == "13:40" ||
                disponible.Substring(0, 5) == "15:20")
            {
                buscarIndiceHoraFinal(disponible, horasFinEduFinan,
                materia, ref iHoraI, ref iHoraF, ref dia,
                ref diaSem, dias, evalAulas, evalProfe, ref limInfAula,
                ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                ref usoDeTercerProfe, ref reiniciarAulas,
                 ref primeraRondaProfesores, ref primeraRondaAulas, ref aulas2, ref aulas8);
            }
            else
                buscarIndiceHoraFinal(disponible, horasFin,
                materia, ref iHoraI, ref iHoraF, ref dia,
                ref diaSem, dias, evalAulas, evalProfe, ref limInfAula,
                ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                ref usoDeTercerProfe, ref reiniciarAulas,
                 ref primeraRondaProfesores, ref primeraRondaAulas, ref aulas2, ref aulas8);
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
            , byte materia, ref int iHoraI, ref int iHoraF, ref byte dia, ref char diaSem, char[] dias,
            bool evalAulas, bool evalProfe, ref byte limInfAula, ref byte limSupAula,
            bool haySegundProf, ref bool segundoProfe, ref int profeId, 
            ref bool usoDeTercerProfe, ref bool reiniciarAulas,
            ref bool primeraRondaProfesores, ref bool primeraRondaAulas, ref bool aulas2, ref bool aulas8)
        {

            if (evalAulas && !evalProfe)
            {
                if (limInfAula != limSupAula)
                {
                    limInfAula++;
                    if (!primeraRondaAulas)
                        avanzarHoras(horasFin, materia, ref iHoraI,
                        ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                        
                }

                else
                {
                    primeraRondaAulas = false;
                    fijarLimitesAulas(ref limInfAula, ref limSupAula, materia,
                        ref aulas2, ref aulas8);
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
                    if(!primeraRondaProfesores)
                    {
                        avanzarHoras(horasFin, materia, ref iHoraI,
                        ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                        reiniciarAulas = true;
                    }
                }
                else if(haySegundProf && segundoProfe)
                {
                    if (materia == 1 && !usoDeTercerProfe)
                    {
                        profeId++;
                        usoDeTercerProfe = true;
                        segundoProfe = true;
                        if (!primeraRondaProfesores)
                        {
                            avanzarHoras(horasFin, materia, ref iHoraI,
                            ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                            reiniciarAulas = true;
                        }
                    }
                    else
                    {

                        avanzarHoras(horasFin, materia, ref iHoraI,
                        ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                        reiniciarAulas = true;

                        segundoProfe = false;
                    }
                    
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