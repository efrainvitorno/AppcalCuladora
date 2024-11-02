using System;
using System.Text.RegularExpressions;

namespace AppCalculadora
{
    internal class CCalculadora
    {
        private string aExpresion; // Almacena la expresión infija

        public CCalculadora(string expresion)
        {
            aExpresion = expresion;
        }

        // Método para verificar si la expresión contiene solo números y operadores
        public bool ContieneSoloNumeros()
        {
            return !Regex.IsMatch(aExpresion, @"[a-zA-Z]");
        }

        // Convierte la expresión infija a notación posfija
        public string ConvertirAPosFijo()
        {
            CPila pila = new CPila();
            string expresionPosfijo = "";

            // **Retacear**: eliminamos espacios en blanco de la expresión para procesar cada carácter como un token independiente
            foreach (char token in aExpresion.Replace(" ", ""))
            {
                if (char.IsDigit(token) || char.IsLetter(token))
                {
                    expresionPosfijo += token + " ";
                }
                else if (token == '(')
                {
                    pila.Apilar(token);
                }
                else if (token == ')')
                {
                    while (!pila.EsVacia() && (char)pila.Cima() != '(')
                    {
                        expresionPosfijo += pila.Cima() + " ";
                        pila.Desapilar();
                    }
                    pila.Desapilar(); // Quitar '('
                }
                else if (EsOperador(token))
                {
                    while (!pila.EsVacia() && Precedencia(token) <= Precedencia((char)pila.Cima()))
                    {
                        expresionPosfijo += pila.Cima() + " ";
                        pila.Desapilar();
                    }
                    pila.Apilar(token);
                }
            }

            // Extraer todos los operadores restantes de la pila
            while (!pila.EsVacia())
            {
                expresionPosfijo += pila.Cima() + " ";
                pila.Desapilar();
            }

            return expresionPosfijo.Trim();
        }

        // Evalúa la expresión infija en formato posfijo
        public float Evaluar()
        {
            // Convierte la expresión a posfijo y luego la evalúa
            string expresionPosfijo = ConvertirAPosFijo();
            CPila pila = new CPila();
            string[] tokens = expresionPosfijo.Split(' '); // **Tokenizar**: dividir en tokens

            foreach (string token in tokens)
            {
                if (float.TryParse(token, out float num))
                {
                    pila.Apilar(num);
                }
                else if (EsOperador(token))
                {
                    // **Evaluar en posfijo**: procesar los operadores en el orden en que aparecen
                    float operandoDer = (float)pila.Cima();
                    pila.Desapilar();
                    float operandoIzq = (float)pila.Cima();
                    pila.Desapilar();
                    float resultado = EjecutarOperacion(token, operandoIzq, operandoDer);
                    pila.Apilar(resultado);
                }
            }

            return (float)pila.Cima();
        }

        // Método para verificar si un carácter es un operador
        private bool EsOperador(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }

        private bool EsOperador(char token)
        {
            return token == '+' || token == '-' || token == '*' || token == '/' || token == '^';
        }

        // Método que asigna la precedencia a cada operador
        private int Precedencia(char operador)
        {
            switch (operador)
            {
                case '+':
                case '-': return 1;
                case '*':
                case '/': return 2;
                case '^': return 3;
                default: return 0;
            }
        }

        // Ejecuta la operación correspondiente entre dos operandos
        private float EjecutarOperacion(string operador, float izq, float der)
        {
            switch (operador)
            {
                case "+": return izq + der;
                case "-": return izq - der;
                case "*": return izq * der;
                case "/": return izq / der;
                case "^": return (float)Math.Pow(izq, der);
                default: throw new ArgumentException("Operador desconocido");
            }
        }
    }
}
