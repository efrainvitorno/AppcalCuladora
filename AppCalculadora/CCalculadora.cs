using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class CPila<T>
{
    private Stack<T> stack = new Stack<T>();

    public void Apilar(T elemento)
    {
        stack.Push(elemento);
    }

    public T Desapilar()
    {
        return stack.Pop();
    }

    public T Cima()
    {
        return stack.Peek();
    }

    public bool EsVacia()
    {
        return stack.Count == 0;
    }
}

class CConvertidorAPosFijo
{
    public string Convertir(string expresion)
    {
        CPila<char> pila = new CPila<char>();
        string expresionPosFijo = "";
        foreach (char token in expresion)
        {
            if (char.IsDigit(token) || char.IsLetter(token))
            {
                expresionPosFijo += token + " ";
            }
            else if (token == '(')
            {
                pila.Apilar(token);
            }
            else if (token == ')')
            {
                while (!pila.EsVacia() && pila.Cima() != '(')
                {
                    expresionPosFijo += pila.Desapilar() + " ";
                }
                pila.Desapilar(); // Quitar '(' de la pila
            }
            else if (EsOperador(token))
            {
                while (!pila.EsVacia() && Precedencia(token) <= Precedencia(pila.Cima()))
                {
                    expresionPosFijo += pila.Desapilar() + " ";
                }
                pila.Apilar(token);
            }
        }

        while (!pila.EsVacia())
        {
            expresionPosFijo += pila.Desapilar() + " ";
        }

        return expresionPosFijo.Trim();
    }

    private bool EsOperador(char token)
    {
        return token == '+' || token == '-' || token == '*' || token == '/' || token == '^';
    }

    private int Precedencia(char operador)
    {
        if (operador == '+' || operador == '-') return 1;
        if (operador == '*' || operador == '/') return 2;
        if (operador == '^') return 3;
        return 0;
    }
}

class CEvaluadorPosFijo
{
    public float Evaluar(string expresionPosFijo)
    {
        CPila<float> pila = new CPila<float>();
        string[] tokens = expresionPosFijo.Split(' ');

        foreach (string token in tokens)
        {
            if (float.TryParse(token, out float num))
            {
                pila.Apilar(num);
            }
            else if (EsOperador(token))
            {
                float operandoDer = pila.Desapilar();
                float operandoIzq = pila.Desapilar();
                float resultado = EjecutarOperacion(token, operandoIzq, operandoDer);
                pila.Apilar(resultado);
            }
        }

        return pila.Desapilar();
    }

    private bool EsOperador(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
    }

    private float EjecutarOperacion(string operador, float operandoIzq, float operandoDer)
    {
        switch (operador)
        {
            case "+": return operandoIzq + operandoDer;
            case "-": return operandoIzq - operandoDer;
            case "*": return operandoIzq * operandoDer;
            case "/": return operandoIzq / operandoDer;
            case "^": return (float)Math.Pow(operandoIzq, operandoDer);
            default: throw new ArgumentException("Operador desconocido: " + operador);
        }
    }
}

public class CCalculadora
{
    private string aExpresion;

    public CCalculadora() { aExpresion = ""; }

    public CCalculadora(string expresion) { aExpresion = expresion; }

    public void AsignarExpresion(string expresion)
    {
        aExpresion = expresion;
    }

    public string ObtenerExpresion()
    {
        return aExpresion;
    }

    public string ConvertirAPosFijo()
    {
        CConvertidorAPosFijo convertidor = new CConvertidorAPosFijo();
        return convertidor.Convertir(aExpresion);
    }

    public bool ContieneSoloNumeros()
    {
        // Verifica si la expresión contiene únicamente dígitos y operadores, sin letras
        return !Regex.IsMatch(aExpresion, @"[a-zA-Z]");
    }

    public float Evaluar()
    {
        if (!ContieneSoloNumeros())
        {
            throw new InvalidOperationException("La expresión contiene variables no numéricas.");
        }

        CConvertidorAPosFijo convertidor = new CConvertidorAPosFijo();
        string expresionPosFijo = convertidor.Convertir(aExpresion);

        CEvaluadorPosFijo evaluador = new CEvaluadorPosFijo();
        return evaluador.Evaluar(expresionPosFijo);
    }
}
