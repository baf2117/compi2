using Irony.Parsing;

namespace C3D
{
    class _3DG : Grammar
    {
        public _3DG() : base(caseSensitive: true) {

            #region ER
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            RegexBasedTerminal deci = new RegexBasedTerminal("decimal", "[0-9]+[.][0-9]+");
            RegexBasedTerminal temporal = new RegexBasedTerminal("temporal", "t[0-9]+");
            IdentifierTerminal label = new IdentifierTerminal("label");
            // RegexBasedTerminal label = new RegexBasedTerminal("label", "[A-Za-z]([A-Za-z]|[0-9])*(,[A-Za-z]([A-Za-z]|[0-9])*)*");
            //CommentTerminal str = new CommentTerminal("cadena", "\"", "\"");
            // StringLiteral cha = TerminalFactory.CreateCSharpChar("caracter");

            //RegexBasedTerminal label = new RegexBasedTerminal("label", "t[0-9]+");


            #endregion

            #region Terminales

            var sp = ToTerm("sp");
            var pt = ToTerm(";");
            var dosp = ToTerm(":");
            var par1 = ToTerm("(");
            var par2 = ToTerm(")");
            var main = ToTerm("main");
            var gotoo = ToTerm("goto");
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var por = ToTerm("*");
            var division = ToTerm("/");
            var poten = ToTerm("^");
            var corch1 = ToTerm("[");
            var corch2 = ToTerm("]");
            var llave1 = ToTerm("{");
            var llave2 = ToTerm("}");
            var stack = ToTerm("stack");
            var selfp = ToTerm("selfp");
            var selfp2 = ToTerm("selfp2");
            var DS = ToTerm("DS");
            var igual = ToTerm("=");
            var mayor = ToTerm(">");
            var menor = ToTerm("<");
            var mayori = ToTerm(">=");
            var menori = ToTerm("<=");
            var iguali = ToTerm("==");
            var distinto = ToTerm("!=");
            var imprimir = ToTerm("print");
            var retorno = ToTerm("return");
            var heap = ToTerm("heap");
            var hp = ToTerm("hp");
            
            var si = ToTerm("if");
            var sif = ToTerm("iffalse");
            var entonces = ToTerm("then");
            var fincadena = ToTerm("\\0");
            
            #endregion

            #region No Terminales
            NonTerminal S = new NonTerminal("S"),
            E = new NonTerminal("E"),
            ASIGNASP = new NonTerminal("ASIGNASP"),
            ASIGNASEL1 = new NonTerminal("ASIGNASEL1"),
            ASIGNASEL2 = new NonTerminal("ASIGNASEL2"),
            ASIGNAHP = new NonTerminal("ASIGNAHP"),
            ASIGNASTACK = new NonTerminal("ASIGNASTACK"),
            ASIGNAHEAP = new NonTerminal("ASIGNAHEAP"),
            ASIGNADS = new NonTerminal("ASIGNADS"),
            IF = new NonTerminal("IF"),
            IFFALSE = new NonTerminal ("IFFALSE"),
            LLAMADA = new NonTerminal("LLAMADA"),
            METODO = new NonTerminal("METODO"),
            OPEREL = new NonTerminal("OPEREL"),
            GOTO = new NonTerminal("GOTO"),
            BLOQUE = new NonTerminal("BLOQUE"),
            BLOQUE2 = new NonTerminal("BLOQUE"),
            ASIGNACIONT = new NonTerminal("ASIGNACIONT"),
            IMPRIMIR = new NonTerminal("IMPRIMIR"),
            ETIQUETA = new NonTerminal("ETIQUETA"),
            ETIQUETAS = new NonTerminal("ETIQUETAS"),
            RETORNO = new NonTerminal ("RETORNO"),
            VALOR = new NonTerminal("VALOR");

            
            




            #endregion

            #region Gramatica

            S.Rule = MakePlusRule(S,BLOQUE);

            BLOQUE.Rule = GOTO | ASIGNAHP |IFFALSE|ASIGNASEL1 | ASIGNASP|ASIGNASEL2|ASIGNASTACK|ASIGNAHEAP|ASIGNADS|RETORNO|METODO|ASIGNACIONT|LLAMADA | IMPRIMIR|ETIQUETA | IF ;

            RETORNO.Rule = retorno + pt;

            IMPRIMIR.Rule = imprimir + par1 +ToTerm("\"%d\",")+ VALOR + par2 + pt;

            ASIGNAHP.Rule = hp + igual + E + pt;

            ASIGNASP.Rule = sp + igual + E + pt;

            ASIGNASEL1.Rule = selfp +igual + E + pt;

            ASIGNASEL2.Rule = selfp2 + igual + E + pt;

            ASIGNASTACK.Rule = stack + corch1 + VALOR + corch2 + igual + VALOR +pt;

            ASIGNAHEAP.Rule = heap + corch1 + VALOR + corch2 + igual + VALOR + pt;

            ASIGNADS.Rule = DS + corch1 + VALOR + corch2 + igual + VALOR + pt;

            ASIGNACIONT.Rule = temporal + igual + E + pt;

            ETIQUETA.Rule = ETIQUETAS + dosp;

            ETIQUETAS.Rule = MakePlusRule(ETIQUETAS, ToTerm(","), label);

            BLOQUE2.Rule = MakePlusRule(BLOQUE2, BLOQUE);

            METODO.Rule = label + par1 + par2 + llave1 + BLOQUE2 + llave2;

            GOTO.Rule = gotoo + label + pt;

            LLAMADA.Rule = label + par1 + par2 + pt;

            IF.Rule = si + VALOR + OPEREL + VALOR  + gotoo + label + pt;

            IFFALSE.Rule = sif + VALOR + OPEREL + VALOR + gotoo + label + pt;

            OPEREL.Rule = igual
                         | mayor
                         | menor
                         | mayori
                         | menori
                         | distinto;

            E.Rule = VALOR + mas + VALOR
                    | VALOR + menos + VALOR
                    | VALOR + por + VALOR
                    | VALOR + division + VALOR
                    | VALOR + poten + VALOR
                    | VALOR;



            VALOR.Rule = numero
                        | deci
                        | temporal
                        | sp
                        | hp
                        | selfp
                        | stack + corch1 + VALOR + corch2
                        | heap + corch1 + VALOR + corch2
                        | DS + corch1 + VALOR + corch2
                        | selfp2
                        | fincadena;
                        


            #endregion

            #region Preferencias
            this.Root = S;
            this.MarkTransient(BLOQUE);
            this.RegisterOperators(10, Associativity.Left, iguali, distinto, mayor, menor, mayori, menori);
            
            #endregion

        }


    }
}
