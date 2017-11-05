
using Irony.Parsing;
namespace C3D
{
    class Gramatica: Grammar
    {

        public override void CreateTokenFilters(LanguageData language, TokenFilterList filters)
        {
            var outlineFilter = new CodeOutlineFilter(language.GrammarData, OutlineOptions.ProduceIndents | OutlineOptions.CheckBraces, ToTerm(@"\"));
            filters.Add(outlineFilter);
        }
        public Gramatica() : base(caseSensitive: true) {

            #region ER
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            RegexBasedTerminal deci = new RegexBasedTerminal("decimal", "[0-9]+[.][0-9]+");
            IdentifierTerminal id = new IdentifierTerminal("id");
            CommentTerminal str = new CommentTerminal("cadena", "\"", "\"");


            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "##", "\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "{--", "--}");
            StringLiteral cha = TerminalFactory.CreateCSharpChar("caracter");



            #endregion

            #region Terminales
            
            var url = ToTerm("http");
            var sla = ToTerm("~");
            var pt = ToTerm("&");          
            var dosp = ToTerm(":");
            var par1 = ToTerm("(");
            var par2 = ToTerm(")");
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
            var retornar = ToTerm("return");
            var clase = ToTerm("clase");
            var constructor = ToTerm("__constructor");
            var nuevo = ToTerm("nuevo");
            var si = ToTerm("si");
            var sino1 = ToTerm("si_no");
            var sinosi = ToTerm("si_no_si");
            var caso = ToTerm("caso");
            var defecto = ToTerm("defecto");
            var mientras = ToTerm("mientras");
            var hacer = ToTerm("hacer");
            var salir = ToTerm("salir");
            var continuar = ToTerm("continuar");
            var repetir = ToTerm("repetir");
            var hasta = ToTerm("hasta");
            var loop = ToTerm("loop");
            var para = ToTerm("para");
            var masmas = ToTerm("++");
            var menmen = ToTerm("--");
            var punto = ToTerm(".");
            var igual = ToTerm("=>");
            var superr = ToTerm("super");
            var importar = ToTerm("importar");
            var self = ToTerm("self");
            var elegir = ToTerm("elegir");
            var outs = ToTerm("out_string");
            var parseint = ToTerm("parseint");
            var parsedo = ToTerm("parsedouble");
            var doustr = ToTerm("doubletostr");
            var douint = ToTerm("doubletoint");



            #endregion

            #region No Terminales
            NonTerminal S = new NonTerminal("S"),
            E = new NonTerminal("E"),
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
            DEC3 = new NonTerminal("DEC3"),
            PRIV = new NonTerminal("PRIVACIADA"),
            IMPRESION = new NonTerminal("IMPRESION"),
            IFC = new NonTerminal("IFC"),
            SALIR = new NonTerminal("SALIR"),
            IF = new NonTerminal("IF"),
            FOR = new NonTerminal("FOR"),
            ASIGNACION = new NonTerminal("ASIGNACION"),
            METODOS = new NonTerminal("METODOS"),
            EA = new NonTerminal("EA"),
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
            LOOP = new NonTerminal("LOOP"),
            MM = new NonTerminal("MM"),
            CONTINUAR = new NonTerminal("CONTINUAR"),
            TABU = new NonTerminal("TABULACIONES"),
            IMPORT = new NonTerminal("IMPORT"),
            IMPORT2 = new NonTerminal("IMPORT2"),
            PATH = new NonTerminal("PATH"),
            PATH2 = new NonTerminal("PATH2"),
            URL = new NonTerminal("URL"),
            TIPOA = new NonTerminal("TIPOA"),
            PARAF = new NonTerminal("FOR"),
            S1 = new NonTerminal("S1"),
            LE = new NonTerminal("LE"),
            EC = new NonTerminal("E"),
            NATIVAS = new NonTerminal("NATIVAS");
            



            #endregion

            #region Gramatica
            S.Rule = IMPORT + S1;

            S1.Rule = MakePlusRule(S1,CLASE);

            IMPORT.Rule = importar+ IMPORT2 + Eos | Empty;

            IMPORT2.Rule = MakePlusRule(IMPORT2, ToTerm(","), PATH2);

            PATH2.Rule = url + dosp + division + division + ToTerm("mynube") + division + id + punto + TIPOA | id + punto + TIPOA | ToTerm("c")+dosp+sla+ PATH + punto + TIPOA;

            TIPOA.Rule = ToTerm("olc") | ToTerm("tree");

            PATH.Rule = MakePlusRule(PATH, sla, id);

            CLASE.Rule = clase + id + corch1 +HERENCIA+ corch2 + dosp + Eos + CUERPO2;

            HERENCIA.Rule = id | Empty;

            CUERPO2.Rule = Indent + LCUERPO + Dedent;

            LCUERPO.Rule = MakePlusRule(LCUERPO, CUERPO);

            CUERPO.Rule = METODOS | FUNCIONES |DECG | CONSTRUCTOR;  

            PRIV.Rule = protegido | privado | publico|Empty;

            PARAMETROS1.Rule = PARAMETROS | Empty;


            PARA.Rule = TIPO2 + id
                       |TIPO2 + id + corch1 + corch2;


            PARAMETROS.Rule = MakePlusRule(PARAMETROS, ToTerm(",") ,PARA);

            CONSTRUCTOR.Rule = constructor + corch1 + PARAMETROS1 + corch2 + dosp + Eos + BLOQUE;

            FUNCIONES.Rule = PRIV + funcion + TIPO2 + id + corch1 + PARAMETROS1+corch2 + dosp + Eos + BLOQUE;

            METODOS.Rule = PRIV + metodo + id + corch1 +PARAMETROS1+ corch2 + dosp + Eos + BLOQUE;

            BLOQUE.Rule = Indent + LSENTENCIAS + Dedent|Empty;

            LSENTENCIAS.Rule = MakeStarRule(LSENTENCIAS,SENTENCIA);

            SENTENCIA.Rule = DEC | ATRIBUTO + Eos | ASIGNACION | INSTANCIA|SELF|CICLOS|SALIR|CONTINUAR| NATIVAS + Eos|RET |MM + Eos;

            SALIR.Rule = salir + Eos;

            CONTINUAR.Rule = continuar + Eos;

            LE.Rule = MakePlusRule(LE, ToTerm(","), E) | Empty;

            LLAMAR.Rule = id + corch1 + LE + corch2 + Eos;

            RET.Rule = retornar + E + Eos;

            INSTANCIA.Rule = id + id + igual + nuevo + id + corch1 + PARAMETROS1 + corch2 + Eos|id + id + Eos;

            ASIGNACION.Rule = ATRIBUTO + ASG + Eos ;

            SELF.Rule = self +punto+ ATRIBUTO+ ASG + Eos;

            ATRIBUTO.Rule = MakePlusRule(ATRIBUTO, ToTerm("."), ATRIBUTOS);

            ATRIBUTOS.Rule = id | id + corch1 + EA + corch2 | id + LVEC;

            EA.Rule = E | Empty;

            ASG.Rule = igual + E | igual + nuevo + id + corch1 + PARAMETROS1 + corch2;
                     
            CICLOS.Rule = IF|ELEGIR|PARAF | MIENTRAS | HACER |REPETIR | LOOP;

            IF.Rule = si + corch1 + COND + corch2 + dosp + Eos + BLOQUE + LELIF+ELSE;

            LELIF.Rule = MakePlusRule(LELIF, ELIF) | Empty;

            ELIF.Rule = sinosi + corch1 + COND + corch2 + dosp + Eos + BLOQUE|Empty;

            ELSE.Rule = sino1 + dosp + Eos + BLOQUE | Empty;

            ELEGIR.Rule = elegir + caso +corch1+ E +corch2+ dosp + Eos + Indent+CASO+ DEF;

            CASO2.Rule = E + dosp + Eos + BLOQUE|Empty;

            DEF.Rule = defecto + dosp + Eos + BLOQUE + Dedent | Dedent;

            CASO.Rule = MakePlusRule(CASO, CASO2);

            MIENTRAS.Rule = mientras + corch1 + COND + corch2 + dosp + Eos + BLOQUE;

            HACER.Rule = hacer + dosp + Eos + BLOQUE + mientras + corch1 + COND + corch2+ Eos;

            REPETIR.Rule = repetir + dosp + Eos + BLOQUE + hasta + corch1 + COND + corch2+ Eos;

            PARAF.Rule = para + corch1 + intt + id + igual + E + dosp  + COND  + dosp + MM +corch2 + dosp + Eos + BLOQUE;

            MM.Rule = ATRIBUTO + masmas | ATRIBUTO + menmen;

            LOOP.Rule = loop + dosp + Eos + BLOQUE;

            NATIVAS.Rule = outs + corch1 + E + corch2 
                          | parseint + corch1 + E + corch2
                          | parsedo + corch1 + E + corch2
                          | doustr + corch1 + E + corch2 
                          | douint + corch1 + E + corch2;

            DECG.Rule = PRIV + DEC | DEC;

            DEC.Rule =  TIPO2 + DEC2 + DEC3 +Eos;

            DEC2.Rule = LID | id + LVEC;

            DEC3.Rule = ASG | Empty;

            LVEC1.Rule = corch1 + E + corch2;

            LVEC.Rule = MakePlusRule(LVEC, LVEC1);

            LID.Rule = MakePlusRule(LID, ToTerm(","), id);


            OPEREL.Rule = iguali
                         | mayor
                         | menor
                         | mayori
                         | menori
                         | distinto;

            E.Rule =  E + E + OPEREL
                    | E + E + mas
                    | E + E + menos
                    | E + E + por
                    | E + E + division
                    | E + E + poten
                    | E + masmas
                    | E + menmen
                    | par1 + E + par2
                    | VALOR;

            COND.Rule = COND + COND + or
                   | COND + COND + and 
                   | COND + COND + xor
                   | not + COND
                   | EC + EC + OPEREL
                   | VALOR;

            EC.Rule = EC + EC + mas
                     | EC + EC + menos
                     | EC + EC + por
                     | EC + EC + division
                     | EC + EC + poten
                     | VALOR;

            VALOR.Rule =  numero
                        | deci
                        | str
                        | truee
                        | falsee
                        | ATRIBUTO
                        | NATIVAS
                        | self + punto + ATRIBUTO
                        | cha;

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
            this.MarkTransient(VALOR,PRIV,TIPO,TIPO2,SENTENCIA,CUERPO2,EA,BLOQUE,CICLOS,CUERPO,PARAMETROS1);
            this.RegisterOperators(2, Associativity.Left, mas, menos);
            this.RegisterOperators(3, Associativity.Left, por, division);
            this.RegisterOperators(4, Associativity.Left, poten);
            this.RegisterOperators(6, Associativity.Left, and);
            this.RegisterOperators(7, Associativity.Left, or);
            this.RegisterOperators(8, Associativity.Left, xor);
            this.RegisterOperators(9, Associativity.Left, not);
            this.RegisterOperators(10, Associativity.Left, iguali,distinto,mayor,menor,mayori,menori);
            this.MarkPunctuation(":","__constructor","=>","self",".","~","metodo","funcion","importar","clase","si","si_no_si","si_no","elegir","caso","defecto","mientras","para","hacer","repetir","hasta","loop");
            #endregion

        }

    }
}
