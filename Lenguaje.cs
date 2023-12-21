using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Generador
{
    public class Lenguaje : Sintaxis
    {
        private int contadorEpsilon = 0;
        private int contador0r = 0;
        public Lenguaje()
        {

        }
        public Lenguaje(string nombre) : base(nombre)
        {
        }

        public void generaLenguaje()
        {

            generado.WriteLine("using System;");
            generado.WriteLine("using System.Collections.Generic;");
            generado.WriteLine("using System.Linq;");
            generado.WriteLine("using System.Reflection.PortableExecutable;");
            generado.WriteLine("using System.Threading.Tasks;");
            generado.WriteLine();
            generado.WriteLine("namespace Generado");
            generado.WriteLine("{");
            generado.WriteLine("    public class Lenguaje : Sintaxis");
            generado.WriteLine("    {");
            generado.WriteLine("        public Lenguaje()");
            generado.WriteLine("        {");

            generado.WriteLine("        }");
            generado.WriteLine("        public Lenguaje(string nombre) : base(nombre)");
            generado.WriteLine("        {");
            generado.WriteLine("        }");

            Producciones();

            generado.WriteLine("    }");
            generado.WriteLine("}");
        }
        private void Producciones()
        {
            contadorEpsilon = 0;
            contador0r = 0;
            generado.WriteLine("        public void " + getContenido() + "()");
            generado.WriteLine("        {");
            match(Tipos.SNT);
            match(Tipos.Flechita);
            listaSimbolos();
            match(Tipos.FinProduccion);
            generado.WriteLine("        }");
            if (getClasificacion() == Tipos.SNT)
            {
                Producciones();
            }
        }
        private void listaSimbolos()
        {
            if (contadorEpsilon > 0)
            {
                if (esPalabraReservada(getContenido()))
                {
                    generado.WriteLine("                match(Tipos." + getContenido() + ");");
                    match(Tipos.SNT);
                }
                else if (getClasificacion() == Tipos.ST)
                {
                    generado.WriteLine("                match(\"" + getContenido() + "\");");
                    match(Tipos.ST);
                }
                else if (getClasificacion() == Tipos.SNT)
                {
                    generado.WriteLine("                " + getContenido() + "();");
                    match(Tipos.SNT);
                }
                if(getClasificacion() != Tipos.PDer){
                    listaSimbolos();
                }
                else if(getClasificacion() == Tipos.PDer){
                    match(Tipos.PDer);
                    generado.WriteLine("            }");
                    contadorEpsilon = 0;
                }
            }
            if (contador0r > 0)
            {
                string simbolo = getContenido();
                generado.Write("            else ");
                if (esPalabraReservada(getContenido()))
                {
                    match(Tipos.SNT);
                    if(getClasificacion() != Tipos.PDer)
                    {
                        generado.Write("if (getClasificacion() == Tipos." + simbolo + ")");
                    }
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(Tipos." + simbolo + ");");
                    generado.WriteLine("            }");
                    
                }
                else if (getClasificacion() == Tipos.ST)
                {
                    match(Tipos.ST);
                    if(getClasificacion() != Tipos.PDer)
                    {
                        generado.WriteLine("if (getContenido() == \"" + simbolo + "\")");
                    }
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(\"" + simbolo + "\");");
                    generado.WriteLine("            }");
                    
                }
                else if (getClasificacion() == Tipos.SNT)
                {
                    
                    match(Tipos.SNT);
                    generado.WriteLine("");
                    generado.WriteLine("            {");
                    generado.WriteLine("                " + simbolo + "();");
                    generado.WriteLine("            }");
                    if(getClasificacion() != Tipos.PDer){
                        throw new Error("No se puede esperar otra condicion debido a que ya hay un else", log, linea, columna);
                    }
                }
                if(getClasificacion() != Tipos.PDer){
                    listaSimbolos();
                }
                else if(getClasificacion() == Tipos.PDer){
                    match(Tipos.PDer);
                    contador0r = 0;
                }
            }
            else
            {
                if (esPalabraReservada(getContenido()))
                {
                    generado.WriteLine("            match(Tipos." + getContenido() + ");");
                    match(Tipos.SNT);
                }
                else if (getClasificacion() == Tipos.ST)
                {
                    generado.WriteLine("            match(\"" + getContenido() + "\");");
                    match(Tipos.ST);
                }
                else if (getClasificacion() == Tipos.SNT)
                {
                    generado.WriteLine("            " + getContenido() + "();");
                    match(Tipos.SNT);
                }
            }

            if (getClasificacion() == Tipos.Epsilon)
            {
                match(Tipos.Epsilon);
                match(Tipos.PIzq);
                string simbolo = getContenido();
                if (esPalabraReservada(simbolo))
                {
                    match(Tipos.SNT);
                    generado.WriteLine("            if (getClasificacion() == Tipos." + simbolo + ")");
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(Tipos." + simbolo + ");");
                }
                else if(getClasificacion() == Tipos.ST)
                {
                    match(Tipos.ST);
                    generado.WriteLine("            if (getContenido() == \"" + simbolo + "\")");
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(\"" + simbolo + "\");");
                }
                else {
                    throw new Error("No se puede iniciar un Epsilon con un SNT", log, linea, columna);
                }
                if (getClasificacion() != Tipos.PDer)
                {
                    contadorEpsilon++;
                    listaSimbolos();
                }
            }
            else if (getClasificacion() == Tipos.Or)
            {
                match(Tipos.Or);
                match(Tipos.PIzq);
                string simbolo = getContenido();
                if (esPalabraReservada(simbolo))
                {
                    match(Tipos.SNT);
                    generado.WriteLine("            if (getClasificacion() == Tipos." + simbolo + ")");
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(Tipos." + simbolo + ");");
                }
                else if(getClasificacion() == Tipos.ST)
                {
                    generado.WriteLine("            if (getContenido() == \"" + simbolo + "\")");
                    match(Tipos.ST);
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(\"" + simbolo + "\");");
                }
                else {
                    throw new Error("No se puede iniciar un Epsilon con un SNT", log, linea, columna);
                }
                generado.WriteLine("            }");
                if (getClasificacion() != Tipos.PDer)
                {
                    contador0r++;
                    listaSimbolos();
                }
                
            }
            if (getClasificacion() != Tipos.FinProduccion)
            {
                listaSimbolos();
            }
        }
        private bool esPalabraReservada(string palabra)
        {
            switch (palabra)
            {
                case "Identificador":
                case "Numero":
                case "Asignacion":
                case "Inicializacion":
                case "OperadorRelacional":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "Cadena":
                case "Ternario":
                case "FinSentencia":
                case "OperadorLogico":
                case "Inicio":
                case "Fin":
                case "Caracter":
                case "TipoDato":
                case "Zona":
                case "Condicion":
                case "Ciclo":
                    return true;
            }
            return false;
        }
        /*
            
        */
    }
}