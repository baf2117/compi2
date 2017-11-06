using Irony.Parsing;

namespace C3D
{
    class Gramaticao : Grammar
    {


        public Gramaticao() : base(caseSensitive: true) {

            #region ER
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            RegexBasedTerminal deci = new RegexBasedTerminal("decimal", "[0-9]+[.][0-9]+");
            IdentifierTerminal id = new IdentifierTerminal("id");
            CommentTerminal str = new CommentTerminal("cadena", "\"", "\"");


            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "/-", "-/");
            StringLiteral cha = TerminalFactory.CreateCSharpChar("caracter");



            #endregion

            #region Terminales

            var url = ToTerm("http");
            var heredar = ToTerm("hereda_de");
            var sla = ToTerm("~");
            var pt = ToTerm(";");
            var dosp = ToTerm(":");
            var par1 = ToTerm("(");
            var par2 = ToTerm(")");
            var principal = ToTerm("principal");
            var intt = ToTerm("entero");
            var stringg = ToTerm("cadena");
            var doublee = ToTerm("decimal");
            var charr = ToTerm("caracter");
            var booll = ToTerm("booleano");
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var por = ToTerm("*");
            var division = ToTerm("/");
            var poten = ToTerm("pow");
            var publico = ToTerm("publico");
            var protegido = ToTerm("protegido");
            var privado = ToTerm("privado");
            var corch1 = ToTerm("[");
            var corch2 = ToTerm("]");
            var llave1 = ToTerm("{");
            var llave2 = ToTerm("}");
            var truee = ToTerm("true");
            var falsee = ToTerm("false");
            var and = ToTerm("and");
            var or = ToTerm("or");
            var not = ToTerm("not");
            var xor = ToTerm("xor");
            var mayor = ToTerm(">");
            var menor = ToTerm("<");
            var mayori = ToTerm(">=");
            var menori = ToTerm("<=");
            var iguali = ToTerm("==");
            var distinto = ToTerm("!=");
            var imprimir = ToTerm("imprimir");
            var metodo = ToTerm("metodo");
            var funcion = ToTerm("funcion");
            var retornar = ToTerm("retorno");
            var clase = ToTerm("clase");
            var nuevo = ToTerm("nuevo");
            var si = ToTerm("Si");
            var sino = ToTerm("Sino");
            var caso = ToTerm("caso");
            var defecto = ToTerm("defecto");
            var mientras = ToTerm("Mientras");
            var hacer = ToTerm("hacer");
            var salir = ToTerm("salir");
            var continuar = ToTerm("continuar");
            var repetir = ToTerm("Repetir");
            var hasta = ToTerm("until");
            var x = ToTerm("X");
            var para = ToTerm("Para");
            var masmas = ToTerm("++");
            var menmen = ToTerm("--");
            var igual = ToTerm("=");
            var superr = ToTerm("super");
            var importar = ToTerm("importar");
            var self = ToTerm("este");
            var elegir = ToTerm("elegir");
            var llamar = ToTerm("llamar");

            #endregion

            #region No Terminales
            NonTerminal S = new NonTerminal("S"),
            E = new NonTerminal("E"),
            DECGF = new NonTerminal("DECGF"),
            BLOQUE = new NonTerminal("BLOQUE"),
            SENTENCIA = new NonTerminal("SENTENCIA"),
            MOSTRAR = new NonTerminal("MOSTRAR"),
            LID = new NonTerminal("LID"),
            LVEC = new NonTerminal("LVEC"),
            LVEC1 = new NonTerminal("LVEC1"),
            TIPO = new NonTerminal("TIPO"),
            VALOR = new NonTerminal("VALOR"),
            CLASE = new NonTerminal("CLASE"),
            FUNCIONES = new NonTerminal("FUNCIONES"),
            CFUN = new NonTerminal("CFUN"),
            LLAMAR = new NonTerminal("LLAMAR"),
            ASG = new NonTerminal("ASIGNAR"),
            ENTERES = new NonTerminal("ENTERES"),
            PARAMETROS = new NonTerminal("PARAMETROS"),
            CFUNCIONES = new NonTerminal("CFUNCIONES"),
            DEC = new NonTerminal("DEC"),
            DEC2 = new NonTerminal("DEC2"),
            DECV = new NonTerminal("DECV"),
            DECV1 = new NonTerminal("DECV1"),
            PRIV = new NonTerminal("PRIVACIADA"),
            IMPRESION = new NonTerminal("IMPRESION"),
            IFC = new NonTerminal("IFC"),
            LLAMADA = new NonTerminal("LLAMADA"),

            IF = new NonTerminal("IF"),
            FOR = new NonTerminal("FOR"),
            ASIGNACION = new NonTerminal("ASIGNACION"),
            METODOS = new NonTerminal("METODOS"),
            PRINCIPAL = new NonTerminal("PRINCIPAL"),
            LELIF = new NonTerminal("LELIF"),
            ELIF = new NonTerminal("ELIF"),
            ELSE = new NonTerminal("ELSE"),
            COND = new NonTerminal("COND"),
            ELEGIR = new NonTerminal("ELEGIR"),
            CASO = new NonTerminal("CASO"),
            CASO2 = new NonTerminal("CASO2"),
            DEF = new NonTerminal("DEF"),
            RET = new NonTerminal("RET"),
            CICLOS = new NonTerminal("CICLOS"),
            SAL = new NonTerminal("SAL"),
            WHILE = new NonTerminal("WHILE"),
            DOWHILE = new NonTerminal("DO"),
            INCREMENTO = new NonTerminal("INCRE"),
            CONDIFOR = new NonTerminal("CONDI"),
            ATRIBUTOS = new NonTerminal("ATRIBUTOS"),
            ATRIBUTO1 = new NonTerminal("ATRIBUTO1"),
            ATRIO = new NonTerminal("ATRIO"),
            OPEREL = new NonTerminal("OPEREL"),
            TABU1 = new NonTerminal("TABU"),
            DECLARACION = new NonTerminal("DECLARACION"),
            LSENTENCIAS = new NonTerminal("LSENTENCIAS"),
            TIPO2 = new NonTerminal("TIPO2"),
            PARA = new NonTerminal("PARA"),
            PARAMETROS1 = new NonTerminal("PARAMETROS1"),
            CUERPO = new NonTerminal("CUERPO"),
            CUERPO2 = new NonTerminal("CUERPO2"),
            LCUERPO = new NonTerminal("LCUERPO"),
            DECG = new NonTerminal("DECG"),
            CONSTRUCTOR = new NonTerminal("CONSTRUCTOR"),
            INSTANCIA = new NonTerminal("INSTANCIA"),
            SUPER = new NonTerminal("SUPER"),
            SELF = new NonTerminal("SELF"),
            ATRIBUTO = new NonTerminal("ATRIBUTO"),
            HERENCIA = new NonTerminal("HERENCIA"),
            MIENTRAS = new NonTerminal("MIENTRAS"),
            HACER = new NonTerminal("HACER"),
            REPETIR = new NonTerminal("REPETIR"),
            LOOP = new NonTerminal("X"),
            MM = new NonTerminal("MM"),
            TABU = new NonTerminal("TABULACIONES"),
            IMPORT = new NonTerminal("IMPORT"),
            IMPORT2 = new NonTerminal("IMPORT2"),
            PATH = new NonTerminal("PATH"),
            PATH2 = new NonTerminal("PATH2"),
            URL = new NonTerminal("URL"),
            TIPOA = new NonTerminal("TIPOA"),
            PARAF = new NonTerminal("FOR"),
            S1 = new NonTerminal("S1"),
            EA = new NonTerminal("EA"),
            VALA = new NonTerminal("VALA"),
            VALA2 = new NonTerminal("VALA2"),
            LE = new NonTerminal("LE"),
            DEC3 = new NonTerminal("DEC3"),
            DEC4 = new NonTerminal("DEC"),
            DEC5 = new NonTerminal("DEC5"),
            EC = new NonTerminal("E"),
            NATIVAS = new NonTerminal("NATIVAS");

            #endregion

            #region Gramatica
            S.Rule = IMPORT2 + S1;

            S1.Rule = MakePlusRule(S1, CLASE);
            
            IMPORT2.Rule = MakePlusRule(IMPORT2, IMPORT)|Empty;

            IMPORT.Rule = importar + par1 + str + par2+pt | llamar + par1 + str + par2 + pt;      

            TIPOA.Rule = ToTerm("olc") | ToTerm("tree");

            PATH.Rule = MakePlusRule(PATH, sla, id);

            CLASE.Rule = clase + id + HERENCIA + llave1 + LCUERPO + llave2;

            HERENCIA.Rule = heredar + id | Empty;

            LCUERPO.Rule = MakePlusRule(LCUERPO, CUERPO);

            PRINCIPAL.Rule = principal + par1 + par2 + llave1 + BLOQUE + llave2;

            CUERPO.Rule = METODOS | DECGF | CONSTRUCTOR | PRINCIPAL;

            PRIV.Rule = protegido | privado | publico|Empty;

        
            PARA.Rule = TIPO2 + id
                       | TIPO2 + id + LVEC;

            PARAMETROS.Rule = MakePlusRule(PARAMETROS, ToTerm(","), PARA)|Empty;

            CONSTRUCTOR.Rule = id + par1 + PARAMETROS + par2 +llave1+ BLOQUE + llave2;

            FUNCIONES.Rule = par1 + PARAMETROS + par2 + llave1 + BLOQUE+llave2;

            METODOS.Rule = PRIV + metodo + id + par1 + PARAMETROS + par2 + llave1 + BLOQUE +llave2;

            BLOQUE.Rule = MakeStarRule(BLOQUE, SENTENCIA);

            SENTENCIA.Rule = DEC |ATRIBUTO + pt|LLAMADA + pt| ASIGNACION | SELF | CICLOS |MM + pt |salir + pt | continuar + pt | RET | IMPRESION;

            LLAMADA.Rule = id + par1+LE+par2;    

            RET.Rule = retornar + E + pt;

            INSTANCIA.Rule =  nuevo + id + par1 + LE + par2 ;

            IMPRESION.Rule = imprimir + par1 + E + par2 + pt;


            ASIGNACION.Rule = ATRIBUTO + ASG + pt;

            SELF.Rule = self +ToTerm(".")+ ATRIBUTO +ASG +pt;


            ATRIBUTO.Rule = MakePlusRule(ATRIBUTO, ToTerm("."), ATRIBUTOS);

            ATRIBUTOS.Rule = id | id + par1 + EA + par2 | id + LVEC;

            EA.Rule =  LE | Empty;

            ASG.Rule = igual + E| igual + nuevo + id + par1 + EA + par2;
                     

            CICLOS.Rule = IF | PARAF | MIENTRAS | HACER | REPETIR | LOOP;

            IF.Rule = si + par1 + COND + par2 + llave1 + BLOQUE + llave2 + LELIF;

            LELIF.Rule = MakePlusRule(LELIF, ELIF)|Empty;

            ELIF.Rule = sino + si  + par1 + COND + par2 + llave1 + BLOQUE + llave2| sino + llave1 + BLOQUE + llave2;
 

            ELEGIR.Rule = elegir + caso + corch1 + E + corch2 + dosp+ CASO + DEF;

            CASO2.Rule = E + dosp  + BLOQUE;

            DEF.Rule = defecto + dosp  + BLOQUE;

            CASO.Rule = MakePlusRule(CASO, CASO2);

            MIENTRAS.Rule = mientras + par1 + COND + par2 + llave1 + BLOQUE+ llave2;

            HACER.Rule = hacer + llave1 + BLOQUE +llave2+ mientras + par1+ COND + par2 + pt;

            REPETIR.Rule = repetir + llave1 + BLOQUE+ llave2 + hasta + par1 + COND + par2 + pt;

            PARAF.Rule = para + par1 + intt + id + igual + E + pt + COND + pt + MM +par2 + llave1 + BLOQUE+llave2 ;

            MM.Rule = ATRIBUTO + masmas | ATRIBUTO + menmen;

            LOOP.Rule = x + par1 + COND+ToTerm(",")+COND+par2 +llave1+ BLOQUE+ llave2;


            DECGF.Rule = PRIV + TIPO2 + id + (DECG | FUNCIONES) ;

            DEC3.Rule = ToTerm(",") + LID | LVEC |Empty;

            DEC5.Rule = E | VALA | INSTANCIA;

            DEC4.Rule = igual + DEC5| Empty; 

            DECG.Rule = DEC3 + DEC4 + pt | LVEC + DEC4 + pt;

            LE.Rule = MakePlusRule(LE, ToTerm(","), E) | Empty;

            VALA.Rule = MakePlusRule(VALA, ToTerm(","), VALA2);

            VALA2.Rule = llave1 + LE + llave2 | llave1 + VALA + llave2;

            DEC.Rule = TIPO2 + DEC2 + DEC4 + pt;

            DEC2.Rule = LID | id + LVEC;

            LVEC1.Rule = corch1 + E + corch2;

            LVEC.Rule = MakePlusRule(LVEC, LVEC1);

            LID.Rule = MakePlusRule(LID, ToTerm(","), id);


            OPEREL.Rule = iguali
                         | mayor
                         | menor
                         | mayori
                         | menori
                         | distinto;

            E.Rule = 
                      E + OPEREL+E
                    | E + mas + E
                    | E + menos+E
                    | E +  por + E
                    | E + division + E
                    | E + poten + E
                    | E + masmas
                    | E + menmen
                    | par1 + E + par2
                    | VALOR;

            COND.Rule = COND + or + COND
                    | COND + and + COND
                    | COND + xor + COND
                    | not + COND
                    | EC + OPEREL + EC
                    | VALOR;

            EC.Rule = EC + mas + EC
                     | EC + menos + EC
                     | EC + por + EC
                     | EC + division + EC
                     | EC + poten + EC
                     | VALOR;

            VALOR.Rule = numero
                        | deci
                        | str
                        | truee
                        | falsee
                        | cha
                        | LLAMADA
                        | self + ToTerm(".") + ATRIBUTO
                        | ATRIBUTO;

            TIPO.Rule = intt
                       | stringg
                       | charr
                       | booll
                       | doublee;
            TIPO2.Rule = TIPO
                        | id;

            #endregion

            #region Preferencias
            this.Root = S;
            this.NonGrammarTerminals.Add(comentarioLinea);
            this.NonGrammarTerminals.Add(comentarioBloque);
            this.MarkTransient(VALOR, PRIV, TIPO, TIPO2, SENTENCIA, CUERPO2, BLOQUE, CICLOS, CUERPO, PARAMETROS1,EA);
            this.RegisterOperators(2, Associativity.Left, mas, menos);
            this.RegisterOperators(3, Associativity.Left, por, division);
            this.RegisterOperators(4, Associativity.Left, poten);
            this.RegisterOperators(6, Associativity.Left, and);
            this.RegisterOperators(7, Associativity.Left, or);
            this.RegisterOperators(8, Associativity.Left, xor);
            this.RegisterOperators(9, Associativity.Left, not);
            this.RegisterOperators(10, Associativity.Left, iguali, distinto, mayor, menor, mayori, menori);
            this.MarkPunctuation(":",".","llamar","hereda_de","=","Para","X","Si","Sino","Mientras","Repetir","until","este","metodo","principal","imprimir",";",",","[","]","(",")", "~","{","}" ,"void", "funcion","clase", "si", "si_no_si", "si_no", "elegir", "caso", "defecto", "mientras", "para", "hacer", "repetir", "hasta", "loop");
            #endregion

        }

    }
}
