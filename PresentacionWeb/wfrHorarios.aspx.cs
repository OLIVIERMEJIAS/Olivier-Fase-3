using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrHorarios : System.Web.UI.Page
    {
        LNDetalleHorario lnDH = new LNDetalleHorario(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            
            List<byte> horariosId = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            char[] diasGrupInf = new char[5] { 'L', 'K', 'M', 'J', 'V' };
            char[] diasGrupSup = new char[5] { 'M', 'J', 'V', 'L', 'K' };
            char[] dias;
            byte[] materiasInf = new byte[10] { 1, 2, 3, 4, 5, 6, 8, 9, 10, 11 };
            byte[] materiasSup = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] materias;
            byte materia = 0;
            byte horId = 0;
            byte dia = 0;
            char diaSem = ' ';
            int profeId = 0;
            byte limInfAula = 1;
            byte limSupAula = 2;
            bool haySegundProf = true;
            Random ran = new Random(0);
            int ranNum = 0;
            string[] horasInicio = new string[5] { "07:20", "09:00", "10:40", "13:00", "14:40" };
            string[] horasFin = new string[5] { "08:40", "10:20", "12:00", "14:20", "16:00" };
            string horI = "";
            string horF = "";
            LNDetalleHorario lnDH = new LNDetalleHorario(Config.getCadConec);
            LNProfesor lnP = new LNProfesor(Config.getCadConec);

            //valoración si ya existen registros en la entidad DetallesHorario
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
                ranNum = ran.Next(0, horariosId.Count - 1);
                horId = horariosId[ranNum];


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

                dia = 0;
                //ciclo que controla los días de la semana
                while (dia < 5)
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
                    materia = 0;
                    profeId = 0;
                    
                    while(materia != materias.Count())
                    {
                        if(materias[materia] == 6)
                        {
                            limInfAula = 11;
                            limSupAula = 12;
                        }
                        else if(materias[materia] == 7)
                        {
                            limInfAula = 9;
                            limSupAula = 10;
                        }
                        else
                        {
                            limInfAula = 1;
                            limSupAula = 8;
                        }

                        if(materias[materia] <= 7)
                        {
                            haySegundProf = true;
                            profeId = materias[materia] + materia;
                            if (lnP.accederAProfesor(materias[materia]) != -1)
                            {
                                profeId = lnP.accederAProfesor(materias[materia]);
                            }
                        }
                        else
                        {
                            haySegundProf = false;
                            if(lnP.accederAProfesor(materias[materia]) != -1)
                            {
                                profeId = lnP.accederAProfesor(materias[materia]);
                            }
                        }
                        while(limInfAula <= limSupAula)
                        {

                            limInfAula ++;
                            if(lnP.numLecciones(profeId) > 40)
                            {

                            }
                            else
                            {

                            }
                        }
                    }
                    //instrucciones
                    dia++;
                }
            }


            
               
                    
        }
    }
}