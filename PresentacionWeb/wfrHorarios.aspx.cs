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
            //se considera que sea el director, este puede ver {
            //los horarios, pero no puede generar
            if(Session["_director"] != null)
            {
                btnGenerar.Enabled = false;
            }
            if (!IsPostBack)
            {
                cargarDatos();
            }
            //se cargan las secciones para ver horarios
            gdvSecciones.DataSource = lnH.secciones();
            gdvSecciones.DataBind();
        }
        /// <summary>
        /// Se modifica el textBox de secciones
        /// al elegir de modal de secciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSeleccionarSeccion_Command(object sender, CommandEventArgs e)
        {
            txtSeccion.Text = e.CommandArgument.ToString();
            cargarDatos();
        }
        /// <summary>
        /// Carga todos los detalles de horario
        /// en un formato atractivo según sección y día específica
        /// </summary>
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
        /// <summary>
        /// Método principal en la generación de horarios
        /// los métodos secundarios se dejaron al final de
        /// este para mantener el orden con respecto al principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            //lista de Id de horarios
            //por ejemplo el 1 es la 7-1, 2 es la 7-2,
            //va en orden la 9 es la 9-2
            //hasta el nuevo son tratados de una manera y después de otra
            List<int> horariosId = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13};
            //para no congestionar mucho y la posiblidad de aulas más libres
            //se decidió modificar los grupos inferiores
            //y superiores con días de inicio de horarios diferentes
            char[] diasGrupInf = new char[5] { 'L', 'K', 'M', 'J', 'V' };
            char[] diasGrupSup = new char[5] { 'M', 'J', 'V', 'L', 'K' };
            //arreglo auxiliar para guardar el orden de días correspondiente
            char[] dias;
            //las materias son distintas en ciertos grupos dependiendo de la sección
            //las materias a impartir son divididas en dos grupos, 
            //igual que lo fueron los días de horarios
            byte[] materiasInf = new byte[10] { 1, 2, 3, 4, 5, 8, 9, 10, 11, 12 };
            byte[] materiasSup = new byte[8] { 1, 6, 7, 2, 3, 4, 5, 8 };
            //arreglo auxiliar de materias según corresponde
            byte[] materias;
            //índice que irá por el arreglo materias
            byte materia = 0;
            //variable auxiliar para el índice 
            //de los Id de horarios
            int horId = 0;
            //índice que irá por el arreglo de días de horario
            byte dia = 0;
            //variable que guardará el char de la inicia del d´´ia
            char diaSem = ' ';
            //variable que guarda el Id dep profesor de la materia correspondiente
            int profeId = 0;
            //se usarán dos límites de aulas, ya que estás fueron añadidas en orden
            byte limInfAula = 1;
            byte limSupAula = 2;
            //variable para indicar que la materia tiene un  segundo profesor
            bool haySegundProf = true;
            //objeto random para inciar la distribución 
            //de horarios por sección, así el orden será distinto cada vez
            Random ran = new Random();
            //aquí se guarda el random
            int ranNum = 0;
            //variables auxiliares de índice en los arreglos de horas de inicio y fin
            int iHoraI = 0;
            int iHoraF = 0;
            string[] horasInicio = new string[5] { "07:20", "09:00", "10:40", "13:00", "14:40" };
            string[] horasFin = new string[5] { "08:40", "10:20", "12:00", "14:20", "16:00" };
            //arreglo auxiliar solo para la materia de
            //educación financiera que maneja solo una lección a la vez
            string[] horasFinEduFinan = new string[5] { "08:00", "09:40", "11:20", "13:40", "15:20" };
            //aquí se guardará el elemento string de los arreglos de horas
            string horI = "";
            string horF = "";
            //se plantea un recorrido por aulas y profesores para consultar
            //disponiblidad de horarios,
            //para ello después de a todos darles una hora de inicio igual,
            //se procede a una segunda ronda de consulta, 
            //pero ahora cada uno irá en un colsulta paralela 
            //con horas y días personalizados, está parte de explica a más detalle 
            //adelante
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
            //variable para manejar cuando una materia fue insertada
            bool materiaAgregada = false;
            //esta variable es para saber cuando un segundo profesor está en uso
            bool segundoProfe = false;
            //variable para saber cuando se está en el segundo día de especialidad
            //se presenta la idea de agregar contabilidad y computo un día a la misma
            //hora, 6 lecciones a la vez, y otro día lo mismo, y se planea hacer
            //en la misma iteración de inserción de una materia
            bool segundoDiaEsp = false;
            //indica cuando la segunda especialidad se insertó
            bool segundaEsp = false;
            //esta variable es paracuando sea la segunda asignación de las especialidades
            //es decir, el segundo día
            bool asignacionDos = false;
            //esta variable es para asignar las aulas correspondiente en las especialidades
            bool reiniciarAulas = false;
            //indica cuando se está haciendo uso de un tercer profesor
            //solo inglés tiene este característica
            bool usoDeTercerProfe = false;
            //controla la primera vez que se consulta disponiblidad de profesores
            bool primeraRondaProfesores = true;
            //controla la primera vez que se consulta disponiblidad de aulas
            bool primeraRondaAulas = true;
            //indica si esta materia solo dispone de dos aulas o laborarios
            bool aulas2 = false;
            //indica si la materia dispone de 8 aulas que serían las genéricas
            bool aulas8 = false;
            //estas variables de incio controlan la asignación
            //de las variable auxiliares sigueintes
            bool inicio = true;
            bool inicioProfes = true;
            //variable que controla cuando se agrega la especialidad 
            //de computación
            bool compEspAgregada = false;
            //variable que indica cuando la materia tiene cuatro profesores
            bool hayTresProfes = false;
            //estás llevan el control de cuando y a qué hora se comenzó a 
            //analizar la disponibilidad de un grupo, aula, o profesor
            //así cuando se vuelve al inicio de nuevo, se sabe que no hubo disponiblidad en la semana
            //para las carácterísticas de ese grupo, aulas o profesores
            char diaSemAux = ' ';
            string auxHoraI = "";
            string auxHorIProfe = "";
            string auxHorIGrupo = "";
            //estas son las variables que se mantienem
            //vacías cuando un grupo, aula o profesor
            //está disponible a cierta hora de incio y día
            //si se procesa y sale llena esta que ese actor, está
            //ocupado y muestrará en su contenido la hora de finalización
            //hasta que hora está ocupadon segúnn esos datos de entrada
            string disponibleGrupo = "";
            string disponibleAula = "";
            string disponibleProfe = "";
            //esta variable lleva el control de las máximas lecciones que pueden tener
            //los profesor de las materias que se dan en 6 lecciones seguidas, 
            //como lo son inglés, contabilidad y computación(especialidad)
            byte espMaxLecciones = 0;


            //valoración si ya existen registros en la entidad DetallesHorario
            //si los se borran para crear nuevos desde cero
            try
            {
                if (lnDH.hayRegistros())
                {
                    lnDH.eliminarRegistros();
                }
                //se analiza que aún existan Id de horarios
                //que procesar
                while (horariosId.Count != 0)
                {
                    //se elige el número de Id de horario
                    //al azar
                    ranNum = ran.Next(horariosId.Count);
                    //se asigna el número
                    horId = horariosId[ranNum];
                    //y el Id de horario es removida del arreglo de 
                    //horarios
                    horariosId.Remove(horId);

                    //validación para saber en que rango de sección se está
                    //trabajando
                    if (horId <= 9)
                    {
                        //asignación de materias y días para secciones inferiores
                        dias = diasGrupInf;
                        materias = materiasInf;
                    }
                    else
                    {
                        //asignación de materias y días para secciones superiores

                        dias = diasGrupSup;
                        materias = materiasSup;
                    }
                    //reinició de variables al iniciar cada horario
                    materia = 0;
                    segundoDiaEsp = false;
                    asignacionDos = false;
                    dia = 0;
                    diaSem = dias[dia];
                    iHoraI = 0;
                    iHoraF = 0;
                    compEspAgregada = false;
                    //se valida que se hayan terminado de asignar todas las materias
                    while (materia < materias.Count())
                    {
                        //reasignación de variables clave para
                        //nueva inserción de materia
                        materiaAgregada = false;
                        segundoDiaEsp = false;
                        aulas2 = false;
                        aulas8 = false;
                        //este método fija los límites de aula menor y mayor
                        //de acuerdo a la materia
                        //en su interior está un switch que controla los límites
                        //como están en orden en la base de datos, se mueve por ella aumentando 1
                    fijarLimitesAulas(ref limInfAula, ref limSupAula,
                            materias[materia], ref aulas2, ref aulas8);
                        //reinico de variables clave para cada nueva inserción de materias
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
                        //ciclo que comprueba que aún una materia no ha sido asignada
                    while (!materiaAgregada)
                    {
                        //cuando se presenta el turno de 
                        //insertar especialidad
                        //la especialidad computación es la materia de Id 6
                        //la especialidad de contabilidad es la materia con Id 7
                        //hay ocasiones como cuando se cambia de especialidad
                        //que amerita que se reinicien las aulas según la especialiad en foco
                        if ((materias[materia] == 7 && reiniciarAulas == true) || (materias[materia] == 6 && asignacionDos == true && reiniciarAulas == true))
                        {
                            fijarLimitesAulas(ref limInfAula,
                                ref limSupAula, materias[materia], ref aulas2,
                                ref aulas8);
                            reiniciarAulas = false;
                                //aquí se valida si en este turno de las especialidad
                                //las variables auxiliares que permiten saber cuando se ha acabado la disponibilidad
                                //para impartir un materia, se debierán llenar para eviat enciclación
                            if (auxHoraI == "")
                            {
                                auxHoraI = horI;
                                diaSemAux = diaSem;
                            }
                        }

                        //cuando esta variable esté negativa puede significar que 
                        //no existe segundo profesor o que no es necesario este método
                        //el cual asigna el profesor que es el primero que imparte una materia
                        //cuando se están recorriendo todos los profesores
                        //para ver disponiblidad no es neceario, se reinicen los profes desde el inicio,
                        //por ejemplo
                        if (!segundoProfe)
                            asignaciondeProfe(materias[materia], ref haySegundProf,
                            ref profeId, ref segundoProfe, ref usoDeTercerProfe, ref hayTresProfes);

                            //durante el método principal de horarios es necesario movernos por las lecciones
                            //para esto se toma en cuenta que materia se está asignando para
                            //definir que horas le convienen a la misma, 
                            //por ejemplo que hora de finalización se usará desde el principio
                            //esto irá afectando los movimientos de horas para buscar dispobilidad 
                            //después, se trabaja con asignación de las 4 lecciones de las materias general,
                            //las 6 de inglés de una vez, junto con 6 por día en las especialidades
                            //y 2 en los demás generales y 1 en educación financiera,
                            //como fue establecido
                            asignarHoras(iHoraI, iHoraF,
                                        ref horI, ref horF,
                                        horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                            //validación para asignar la variable que controla la no disponiblidad
                            //del grupo para evitar enciclaciones
                            if (auxHorIGrupo == "")
                            {
                                auxHorIGrupo = horI;

                            }
                            //mediante esta asignación se valida si un grupo está disponible 
                            //a un hora de inicio correspondiente y un día puntual
                            //se se devuelve vacía esta disponible, de lo contrario
                            //devolverá la hora de finalización de las lecciones
                            //para un proceso de avance de lecciones posterior
                            disponibleGrupo = lnDH.disponibleHoraI(horI, diaSem, horId);
                            //cuando se asignan especialidad es necesario vaciarlo, 
                            //para que se asigne al grupo una segunda especialidad
                            //a la misma hora y día
                            if (materias[materia] == 7 && compEspAgregada == true)
                                disponibleGrupo = "";
                            //se consulta si el grupo está disponible, 
                            // de no estarlo se va a avance de horas de inicio y día en caso necesario
                            //si lo está se busca un aula disponible
                            if (disponibleGrupo == "")
                            {
                                //aquí se valida que las variables auxiliar que cuando no hay disponibilidad
                                //para una materia en la semana, se llenen
                                //evitando ciclos sin fin
                                if (inicio)
                                {
                                    diaSemAux = diaSem;
                                    auxHoraI = horI;
                                    inicio = false;
                                }
                                //mediante esta asignación se valida si una aula está disponible 
                                //a un hora de inicio correspondiente y un día puntual
                                //se se devuelve vacía esta disponible, de lo contrario
                                //devolverá la hora de finalización de las lecciones
                                //para un proceso de avance de lecciones posterior
                                disponibleAula = lnA.disponibleHoraI(horI, diaSem, limInfAula);
                                //este método se encarga de guardar en variables 
                                //los días y horas finales cuando están ocupadas las aulas
                                //para llevar después de la primera ronda de consultas que
                                //es con datos de entra igual en todas las aulas, se pregunte
                                //después personalizadamente, a que horas de inicio y día,
                                //estaría disponible un aula
                                respaldoHorasFinalesAulas(primeraRondaAulas,
                                aulas2, limInfAula, disponibleAula,
                                ref horFAula1Otros, ref horFAula2Otros,
                                ref horFAula1, ref horFAula2, ref horFAula3,
                                ref horFAula4, ref horFAula5, ref horFAula6,
                                ref horFAula7,
                                ref horFAula8, ref diaAula1Otros, ref diaAula2Otros,
                                ref diaAula1, ref diaAula2, ref diaAula3, ref diaAula4,
                                ref diaAula5, ref diaAula6, ref diaAula7, ref diaAula8,
                                ref dia
                                );
                                //se valida que esté disponible el aula,
                                //si lo está se busca profesor disponible
                                //si no lo está se procesa avande de horas de inicio
                                //y día en caso necesario 
                                if (disponibleAula == "")
                                {
                                    //validación de variables que ayudan a verificar que no haya 
                                    //falta de disponibilidad en la semana de profesor, 
                                    //ya es de no hacerlo, puede hacer encicliación
                                    if (inicioProfes)
                                    {
                                        diaSemAux = diaSem;
                                        auxHorIProfe = horI;
                                        inicioProfes = false;
                                    }
                                    //esta validación se encarga de guardar en variables 
                                    //los días y horas finales cuando están ocupados los profesores
                                    //para llevar después de la primera ronda de consultas que
                                    //es con datos de entra igual en todas los profesores, y preguntar
                                    //después personalizadamente, a que horas de inicio y día,
                                    //estaría disponible un profesore
                                    disponibleProfe = lnP.disponibleHoraI(horI, diaSem, profeId);

                                    if (haySegundProf && !segundoProfe)
                                    {
                                        horF1Profe = disponibleProfe;
                                        if (primeraRondaProfesores)
                                            diaProfe1 = dia;
                                        dia = diaProfe1;
                                    }
                                    else if (haySegundProf && segundoProfe && !usoDeTercerProfe)
                                    {
                                        horF2Profe = disponibleProfe;
                                        if(primeraRondaProfesores)
                                            diaProfe2 = dia;
                                        dia = diaProfe2;

                                    }
                                    else if (haySegundProf && segundoProfe && usoDeTercerProfe)
                                    {
                                        horF3Profe = disponibleProfe;
                                        if (primeraRondaProfesores)
                                            diaProfe3 = dia;
                                        dia = diaProfe3;

                                    }
                                    //validación para saber cuando un profesor está disponible, si no está vacía la variable
                                    //se envía a procesamiento para avanzar de horas de incio y de día en caso necesaio
                                    //de estar vacía hay disponiblidad de, habría disponiblidad de los tres factores
                                    //para asignar una materia, en este punto
                                    if (disponibleProfe == "")
                                    {
                                        //se procede a procede a evaluar la materia a asignar
                                        switch (materias[materia])
                                        {
                                            case 1: //inglés
                                            case 6: //computación especialidad
                                            case 7: //contabilidad
                                                //se asigna el número máximo que pueden dar los profesores
                                                // de estas materias
                                                //la disponiblidad de horas se ve como número de registros,
                                                //y que se ha establecido un número fijo de lecciones por registro
                                                //se controlo multiplicando es número de registro por el número de lecciones en cada
                                                //uno
                                                if (materias[materia] == 1)
                                                    //un profesor de inglés para que de completas las clases
                                                    //a las grupos sin pasarse de las
                                                    //40 lecciones se ha establecido quedará solo 36
                                                    espMaxLecciones = 6;
                                                else
                                                    //un profesor de especialidad para que de completas las clases
                                                    //a los grupos sin pasarse de las
                                                    //40 lecciones se ha establecido quedará solo 24
                                                    espMaxLecciones = 4;
                                                if (lnP.numLecciones(profeId) <= espMaxLecciones)
                                                {
                                                    //se crea un objeto EDetalleHorario
                                                    EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                    //se agrega
                                                    lnDH.agregar(det);
                                                    //variable para indicar cuando pudiera ver enciclado se reasigna
                                                    auxHoraI = "";
                                                    //cuando se esta en espacialiad,
                                                    //y el segundo día de especialidad no se ha dado, pero si la segunda especialidad
                                                    //del mismo día entra por aquí
                                                    if (materias[materia] != 1 &&
                                                        !segundoDiaEsp && segundaEsp)
                                                    {
                                                        //el día avanza al siguiente
                                                        //si se terminan los días de la semana se reincian
                                                        dia++;
                                                        if (dia > 4)
                                                            dia = 0;
                                                        //asigna la letra del día con el índice anterior
                                                        diaSem = dias[dia];
                                                        iHoraI = 0;
                                                        iHoraF = 0;
                                                        //se devuelve la materia de contabilidad a computación
                                                        materia--;
                                                        //si ocurriera un error de devolvernos a inglés en la lista de
                                                        //materia, ya que lo que restamos es un índice, se vuelve a computación
                                                        //sumando el índice
                                                        if (materia == 0)
                                                            materia++;
                                                        //variables importantes se reinician para volver a evaluar todo
                                                        //desde cero
                                                        segundaEsp = false;
                                                        asignacionDos = true;
                                                        reiniciarAulas = true;
                                                        segundoProfe = false;
                                                        compEspAgregada = false;
                                                        primeraRondaAulas = true;
                                                        primeraRondaProfesores = true;
                                                        inicio = true;
                                                        inicioProfes = true;
                                                        auxHorIGrupo = "";
                                                }
                                                    //cuando no se ha insertado la segunda especialidad ese día,
                                                    //ni se ha usado el segundo día de especialidad
                                                    else if (materias[materia] != 1 && !segundoDiaEsp && !segundaEsp)
                                                    {//se no es el segundo día, la segunda asignación
                                                        if (!asignacionDos)
                                                        {
                                                            //esto es cuando de computación pasa a contabilidad
                                                            materia++;
                                                            //reincio de variables clave
                                                            segundaEsp = true;
                                                            reiniciarAulas = true;
                                                            segundoProfe = false;
                                                            compEspAgregada = true;
                                                            primeraRondaAulas = true;
                                                            primeraRondaProfesores = true;
                                                            inicio = true;
                                                            inicioProfes = true;
                                                            auxHorIGrupo = "";
                                                        }
                                                        else
                                                        {
                                                            //cuando es la segunda asignación se ha hecho
                                                            //se lleva el índice a la materia siguiente de las de especialidad
                                                            //y con las variable se sale de estas validaciones
                                                            materia++;
                                                            segundaEsp = true;
                                                            segundoDiaEsp = true;
                                                            segundoProfe = false;
                                                            reiniciarAulas = true;
                                                            compEspAgregada = true;


                                                        }
                                                    }
                                                    //cuando se han asif¿gnado los dos días y las dos especialidades
                                                    //en cada uno se da por agregada la materia, 
                                                    //para ir a la siguiente en el arreglo de materias
                                                    //después de las especialidades
                                                    else
                                                    {
                                                        materiaAgregada = true;

                                                    }
                                                }
                                                //si el profesor ya dio todas sus lecciones
                                                //y se está usando el segundo profesor
                                                //y la materia no es inglés que tiene 3 profesores
                                                //la materia se da por hecha, lo que quiere decir que no se agregó,
                                                //porque ningun profsor tenía espacio parqa darla, lo cual sería un error
                                                //pero evitaría ciclos sin fin
                                                else if (segundoProfe && materias[materia] != 1)
                                                {
                                                    materiaAgregada = true;
                                                }
                                                else
                                                {//validaciones para cuando no se cumple lo anterior
                                                    //es decir la materia si es inglés
                                                    //si hay segundo profesor y no se usa,
                                                    //se usa el sigueinte
                                                    if (haySegundProf && !segundoProfe)
                                                    {
                                                        profeId++;
                                                        segundoProfe = true;
                                                    }
                                                    //cuando hay segundo profesor y se usa
                                                    //pero inglés tiene tres así que se usa el siguiente
                                                    else if (haySegundProf && segundoProfe)
                                                    {
                                                        if (materias[materia] == 1 && !usoDeTercerProfe)
                                                        {
                                                            profeId++;
                                                            usoDeTercerProfe = true;
                                                            segundoProfe = true;
                                                        }
                                                        //si ya el tercero está en uso se da por dada la materia,
                                                        //ya que no habría disponiblidad
                                                        //por llegar al máximo de sis lecciones
                                                        else
                                                            materiaAgregada = true;
                                                    }
                                                }
                                                break;

                                            case 2:
                                            case 3:
                                            case 4:
                                            case 5:
                                                //estas son las cuatro materis básicas con 4 lecciones a la semana
                                                //solo se permiten 10 registro al 10 * 4  = 40
                                                if (lnP.numLecciones(profeId) <= 10)
                                                {
                                                    EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                    lnDH.agregar(det);
                                                    materiaAgregada = true;
                                                }
                                                //si ya se usa el segundo profe se da por dada, ya que no hay dispobibilidad de aulas
                                                else if (segundoProfe)
                                                {
                                                    materiaAgregada = true;
                                                }
                                                else
                                                //si no se uso el segundo profesor
                                                {
                                                    if (haySegundProf && !segundoProfe)
                                                    {
                                                        //se usa
                                                        profeId++;
                                                        //esta variable ayuda que no se reincie el profesor,
                                                        //en un método que lo ejecuta apenas es permitido
                                                        segundoProfe = true;
                                                    }

                                                }
                                                break;
                                            case 8:
                                            case 9:
                                            case 11:
                                            case 12:
                                                //estas son las materias que solo se dan 2 lecciones dos a la
                                                //semana, 20 registros , ya que 2 * 20 = 40 lecciones

                                                if (lnP.numLecciones(profeId) <= 20)
                                                {
                                                    EDetalleHorario det = new EDetalleHorario(horId, profeId, limInfAula, diaSem, horI, horF);
                                                    lnDH.agregar(det);
                                                    materiaAgregada = true;

                                                }
                                                //si el profesor ya hubiera dado las lecciones se da por dada,
                                                //ya que en estas solo existe un solo profesor
                                                else
                                                {
                                                    materiaAgregada = true;

                                                }
                                                break;
                                            case 10:
                                                //esta es la materia de educación financiera
                                                //se permite 40 registros ya que son 1 lección a la vez para cada grupo
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
                                    {//cuando un profesor no está disponible
                                        //se analiza la hora de final para avanzar
                                        if (!haySegundProf)
                                            //cuando no hay segundo profesor
                                            
                                        {//está validación es para cuando la hora de fin está
                                         //dentro de las que usa la materia de edicación financiera
                                            if (disponibleProfe.Substring(0, 5) == "08:00" ||
                                            disponibleProfe.Substring(0, 5) == "09:40"
                                            || disponibleProfe.Substring(0, 5) == "11:20" ||
                                            disponibleProfe.Substring(0, 5) == "13:40" ||
                                            disponibleProfe.Substring(0, 5) == "15:20")
                                            {
                                                //este método usa la hora final
                                               //la busca dentro del arreglo de horas finales,
                                               //y se mueve a la siguiente de inicio,
                                               //y la nueva de hora de fin es personalizada según la materia
                                                buscarIndiceHoraFinal(disponibleProfe, horasFinEduFinan,
                                                materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                                ref diaSem, dias, false, true, ref limInfAula,
                                                ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                                                ref usoDeTercerProfe, ref reiniciarAulas,
                                                ref primeraRondaProfesores, ref primeraRondaAulas,
                                                ref aulas2, ref aulas8);
                                                //se asignan las horas nuevas de una vez, para el siguiente método
                                                asignarHoras(iHoraI, iHoraF,
                                               ref horI, ref horF,
                                               horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                                //este revisa según el inicio de consulta de los profesores
                                                //si ya no se busca en toda la semana y no se hayó cupo
                                                siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                                auxHorIProfe, horI, materias[materia], ref materiaAgregada,
                                                ref materia, ref reiniciarAulas, ref segundoProfe,
                                                ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                                ref compEspAgregada, ref dia, dias, ref iHoraI,
                                                ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                                ref inicio, ref inicioProfes, ref auxHorIGrupo);
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
                                                ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                                ref inicio, ref inicioProfes, ref auxHorIGrupo);
                                        }
                                        else if (primeraRondaProfesores)

                                        {
                                            //si hay más de profesor en la materia
                                            //y es primera ronda no se perosnaliza ningún flujo 
                                            //paralelo para ver doisponiblidades
                                            buscarIndiceHoraFinal(horI, horasInicio,
                                            materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                            ref diaSem, dias, false, true, ref limInfAula,
                                            ref limSupAula, haySegundProf, ref segundoProfe,
                                            ref profeId, ref usoDeTercerProfe, ref reiniciarAulas,
                                            ref primeraRondaProfesores, ref primeraRondaAulas,
                                            ref aulas2, ref aulas8);
                                        }
                                        //si ya no es la primera ronda y es el primer profe que estaba ocupado
                                        //las horas y días de ser necesario de mueven,
                                        //pero personalizados para el segundo profesor
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
                                        //si ya no es la primera ronda y es el segundo profe que estaba ocupado
                                        //las horas y días de ser necesario de mueven,
                                        //pero personalizados para el primer profesor,
                                        //si no es inglés es decir si no hay tercero
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
                                            //se evita enciclación ya cuando se ha llegado al segundo profesor
                                            siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                            auxHorIProfe, horI, materias[materia], ref materiaAgregada,
                                            ref materia, ref reiniciarAulas, ref segundoProfe,
                                            ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                            ref compEspAgregada, ref dia, dias, ref iHoraI,
                                            ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                            ref inicio, ref inicioProfes, ref auxHorIGrupo);
                                        }
                                        //cuando no es la primera ronda, es inglés y no se ha usado el tercer profesor
                                        //el segundo profesor al estar ocupado mueve las horas y día de ser necesario
                                        //personalizadamente para el tercer profesor
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
                                            
                                        }
                                        //al ser inglés y haber usado elterce profesor se mueve personalizadamente el tiempo y día
                                        //para el primer profesor para inciar de nuevo
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
                                            //como ya fue el último profe se busca r en posiblidad de futuro enciclación
                                            siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                            auxHorIProfe, horI, materias[materia], ref materiaAgregada,
                                            ref materia, ref reiniciarAulas, ref segundoProfe,
                                            ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                            ref compEspAgregada, ref dia, dias, ref iHoraI,
                                            ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                            ref inicio, ref inicioProfes, ref auxHorIGrupo);
                                        }


                                    }

                                }
                                else
                                {//cuando es la primera vez que se busca por disponibilidad de aulas
                                    //se aplica las mismas horas de inicio y día a todas las aulas
                                    //y se van moviendo
                                    if (primeraRondaAulas)
                                        buscarIndiceHoraFinal(horI, horasInicio,
                                    materias[materia], ref iHoraI, ref iHoraF, ref dia,
                                    ref diaSem, dias, true, false, ref limInfAula,
                                    ref limSupAula, haySegundProf, ref segundoProfe, ref profeId,
                                    ref usoDeTercerProfe, ref reiniciarAulas,
                                    ref primeraRondaProfesores, ref primeraRondaAulas,
                                    ref aulas2, ref aulas8);
                                    else
                                    {//si no es la primera ronda y
                                     //se tiene solo dos aulas se mueven de manera personalizada, uno le mueve a la siguiente
                                     //solo computación especialidad, computación general y contabilidad
                                     //tiene dos aulas o laboratorios
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
                                                ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                                ref inicio, ref inicioProfes, ref auxHorIGrupo);
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
                                        {//cuando no es la primera ronda, pero se tiene 8 aulas a sisposición,
                                         //se mueven personalizadas y en flujos paralelos todas una,
                                            //le da el pase de movimiento a la siguiente
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
                                                    //se asignan las horas nuevas
                                                    //y se busca la posibilidad de enciclación
                                                     asignarHoras(iHoraI, iHoraF,
                                                    ref horI, ref horF,
                                                    horasInicio, horasFin, horasFinEduFinan, materias[materia]);
                                                    siNoHayDisponiblilidad(ref diaSem, diaSemAux,
                                                    auxHoraI, horI, materias[materia], ref materiaAgregada,
                                                    ref materia, ref reiniciarAulas, ref segundoProfe,
                                                    ref asignacionDos, ref segundaEsp, ref segundoDiaEsp,
                                                    ref compEspAgregada, ref dia, dias, ref iHoraI,
                                                    ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                                    ref inicio, ref inicioProfes, ref auxHorIGrupo);
                                                    break;
                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {//cuando el grupo está ocupado se mueven las horas y el día de ser necesario
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
                                    ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                    ref inicio, ref inicioProfes, ref auxHorIGrupo);
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
                                ref iHoraF, ref primeraRondaAulas, ref primeraRondaProfesores,
                                ref inicio, ref inicioProfes, ref auxHorIGrupo);
                            }
                        }
                    //al insertarse una materia está se mueve o el índice de materias se mueve, para trabajar con
                    //la posición del arreglo de materias que está siguiente
                        materia++;

                    }
                }
                cargarDatos();
                //se cargan los datos de los detalles de los horarios en la vista
                //ya después de que los horarios estén creados
                Session["_exito"] = "Horarios creados con éxito!";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        //Este es el método que se encarga de validar si se puede caer en un ciclo
        //si fin debido a que ya no hay disponiblidad en algún rubro
        private void siNoHayDisponiblilidad(ref char diaSem, char diaSemAux,
            string auxHorI, string horI, byte materia, ref bool materiaAgregada,
            ref byte indiceMateria, ref bool reiniciarAulas, ref bool segundoProfe,
            ref bool asignacionDos, ref bool segundaEsp, ref bool segundoDiaEsp,
            ref bool compEspAgregada, ref byte dia, char[] dias, ref int iHoraI, 
            ref int iHoraF, ref bool primeraRondaAulas, ref bool primeraRondaProfesores,
            ref bool inicio, ref bool inicioProfes, ref string auxHorIGrupo)
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
                        if (indiceMateria == 0)
                            indiceMateria++;
                        segundaEsp = false;
                        asignacionDos = true;
                        reiniciarAulas = true;
                        segundoProfe = false;
                        compEspAgregada = false;
                        primeraRondaAulas = true;
                        primeraRondaProfesores = true;
                        inicio = true;
                        inicioProfes = true;
                        auxHorIGrupo = "";

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
                            inicio = true;
                            inicioProfes = true;
                            auxHorIGrupo = "";
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
        //este es el método que respalda las horas y días de las aulas para
        //llevar flujos paralelos en cada disponibilidad de aula
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
            ref byte diaAula8, ref byte dia
            )
        {
            if (aulas2)
            {
                if (limInfAula == 9 || limInfAula == 11)
                {
                    horFAula1Otros = disponible;
                    //solo en la primera ronda se guarda el día general
                    //ya que este es el que se usará en la primera vez de la segunda
                    //ronda
                    if(primeraRondaAulas)
                        diaAula1Otros = dia;
                    //el dia se vuelve a reasignar para cuando se haya dispobilidad en un aula
                    //al pasar a profesores la evaluación el día general que se
                    //usa sean el del aula que estuvo disponible
                    dia = diaAula1Otros;
                }
                else if (limInfAula == 10 || limInfAula == 12)
                {
                    horFAula2Otros = disponible;
                    if(primeraRondaAulas)
                        diaAula2Otros = dia;
                    dia = diaAula2Otros;
                }
            }
            else
                switch (limInfAula)
                {
                    case 1:
                        horFAula1 = disponible;
                        if (primeraRondaAulas)
                            diaAula1 = dia;
                        dia = diaAula1;
                        break;
                    case 2:
                        horFAula2 = disponible;
                        if (primeraRondaAulas)
                            diaAula2 = dia;
                        dia = diaAula2;
                        break;
                    case 3:
                        horFAula3 = disponible;
                        if (primeraRondaAulas)
                            diaAula3 = dia;
                        dia = diaAula3;
                        break;
                    case 4:
                        horFAula4 = disponible;
                        if (primeraRondaAulas)
                            diaAula4 = dia;
                        dia = diaAula4;
                        break;
                    case 5:
                        horFAula5 = disponible;
                        if (primeraRondaAulas)
                            diaAula5 = dia;
                        dia = diaAula5;
                        break;
                    case 6:
                        horFAula6 = disponible;
                        if (primeraRondaAulas)
                            diaAula6 = dia;
                        dia = diaAula6;
                        break;
                    case 7:
                        horFAula7 = disponible;
                        if (primeraRondaAulas)
                            diaAula7 = dia;
                        dia = diaAula7;
                        break;
                    case 8:
                        horFAula8 = disponible;
                        if (primeraRondaAulas)
                            diaAula8 = dia;
                        dia = diaAula8;

                        break;
                }


            
        }
        //Este es el método que asigna o reinciar a los profesores
        //desde el primer profesor que imparte la materia
        //asigna que hay segundo profesor según materia
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
        //aquí se asignan las horas en las variables string
        //según los índices de horas
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
        //Aquí se fijan los límites de las aulas desde el principio,
        //desde la primera aula disponible
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
        ///Este método usa el método buscarIndiceHoraFinal
        //y la valoración de su se trabaja o está involucrada
        //la materia de educación financiera
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
        //aquí se avanzan las horas de inicio y fin
        //segun las materias,
        //se busca la horaFinal que se ingrese y se busca
        //en el arreglo de horas finales y
        //según las lecciones seguidas que se quieran dar
        //se moveran específicamente
        //si lel índice de las horas se sale de los arreglos de horas, se reinican a cero
        //y pasa al siguiente día
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
        //este método se encarga de moverse a través de las aulas y
        //los profesores, de ir avanzando o sumando a los Id,
        //ya que en la base de datos están en orden
        //se va moviendo hacia adelante
        private void buscarIndiceHoraFinal(string horaFinal, string[] horasFin
            , byte materia, ref int iHoraI, ref int iHoraF, ref byte dia, ref char diaSem, char[] dias,
            bool evalAulas, bool evalProfe, ref byte limInfAula, ref byte limSupAula,
            bool haySegundProf, ref bool segundoProfe, ref int profeId, 
            ref bool usoDeTercerProfe, ref bool reiniciarAulas,
            ref bool primeraRondaProfesores, ref bool primeraRondaAulas, ref bool aulas2, ref bool aulas8)
        {
            //esta evalucación se refiere a cuando se trata de aulas
            if (evalAulas && !evalProfe)
            {
                //cuando el límite menos no ha alcanzado el mayor
                //las aulas siguen hacia adelante
                if (limInfAula != limSupAula)
                {
                    limInfAula++;
                    //si no es la primera ronda se avanzan horas
                    //en la primera ronda no es preciso puesto que las mismas
                    //se deben analizar las mismas variables en las aulas
                    if (!primeraRondaAulas)
                        avanzarHoras(horasFin, materia, ref iHoraI,
                        ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                    //la primera ronda de aulas se reinicia
                    //debido auqe las variantes de horas y días deben 
                    //se consultadas a todos los profesores como tal
                    primeraRondaProfesores = true; 


                }

                else
                {//cuando ya se llegó a la última aula se vuelve 
                    //al inicio
                    //se reiniciar las rondas
                    //y se avanzan las horas y el día de ser necesario
                    primeraRondaProfesores = true;
                    primeraRondaAulas = false;
                    fijarLimitesAulas(ref limInfAula, ref limSupAula, materia,
                        ref aulas2, ref aulas8);
                    avanzarHoras(horasFin, materia, ref iHoraI,
                    ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                    
                }

            }
            //cuando es la valoración de profesores
            else if (!evalAulas && evalProfe)
            {
                //cuando hay segundo profesor y no se ha usado
                //se pasa al siguente y se falsea la variable
                //segundoProfe para que no se reinicen
                //los profesores después en el método de
                //arriba de asignar profesores
                if (haySegundProf && !segundoProfe)
                {
                    profeId++;
                    segundoProfe = true;
                    //al cambiar de profesor, se reinica la ronda de las aulas
                    //ya que es nuevo ahorario de profesor
                    //debe poder consultar en todos las aulas por igual
                    primeraRondaAulas = true;
                    //si no es la primera ronda no se vanzan horas, 
                    //ya que se necesita uniformidad en las consultas de disponibilidad

                    if(!primeraRondaProfesores)
                    {
                        avanzarHoras(horasFin, materia, ref iHoraI,
                        ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                        reiniciarAulas = true;
                    }
                }
                //cuando el segundo profe está en uso
                else if(haySegundProf && segundoProfe)
                {
                    //se pregunta si hay un tercer profesor
                    //en uso si es la materia de inglés
                    if (materia == 1 && !usoDeTercerProfe)
                    {
                        //se avanza al tercer profesor
                        //variable importantes se reasignan
                        profeId++;
                        usoDeTercerProfe = true;
                        segundoProfe = true;
                        primeraRondaAulas = true;
                        //si no es primera ronda se avanzar las horas
                        // y de serlo se consulta disponiblidad de forma
                       //uniforme
                        if (!primeraRondaProfesores)
                        {
                            avanzarHoras(horasFin, materia, ref iHoraI,
                            ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                            reiniciarAulas = true;
                        }
                    }
                    else
                    {
                        //cuando no haya tercer profesor
                        //o ya está en uso
                        //se avanzan las horas y el día de ser preciso

                        avanzarHoras(horasFin, materia, ref iHoraI,
                        ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                        reiniciarAulas = true;
                        primeraRondaAulas = true;
                        //se torna false la variable segundoPorfe, para que entre en el método de
                        //asignación o reinicio de profesor al primero
                        segundoProfe = false;
                    }
                    
                }
                else
                {
                    //cuando no hay segundo profe del todo
                    //se avanzan las horas 
                    //y reinicar variables importantes
                    primeraRondaAulas = true;
                    avanzarHoras(horasFin, materia, ref iHoraI,
                    ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
                }
            }
            else
            {
                //aquí se evalúa la disponibilidad del grupo,
                //se avanzan las horas el el día si ne necesita
                //y se reinicia la primera ronda de aulas
                primeraRondaAulas = true;
                avanzarHoras(horasFin, materia, ref iHoraI,
                    ref iHoraF, horaFinal, ref dia, ref diaSem, dias);
            }
        }

    }
}