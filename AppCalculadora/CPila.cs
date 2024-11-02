using System;

namespace AppCalculadora
{
    internal class CPila
    {
        /*********************  ATRIBUTOS ***********************/
        protected object aCima;      // Elemento en la cima de la pila
        protected CPila aSubPila;    // Subpila que contiene el resto de los elementos

        /******************   MÉTODOS  *********************/

        /* =================== Constructores ===================*/
        public CPila()
        {
            aCima = null;
            aSubPila = null;
        }

        public CPila(object pCima, CPila pSubPila)
        {
            aCima = pCima;
            aSubPila = pSubPila;
        }

        /* ==================== Modificadores ================*/

        protected void AsignarCima(object pCima)
        {
            aCima = pCima;
        }

        protected void AsignarSubPila(CPila pSubPila)
        {
            aSubPila = pSubPila;
        }

        /* ==================  Selectores   ==================*/

        protected object ObtenerCima()
        {
            return aCima;
        }

        protected CPila ObtenerSubPila()
        {
            return aSubPila;
        }

        /* =============== Operaciones de Base ===============*/

        // Método para apilar un nuevo elemento en la pila
        public void Apilar(object pCima)
        {
            aSubPila = new CPila(aCima, aSubPila);
            aCima = pCima;
        }

        // Método para desapilar el elemento en la cima
        public void Desapilar()
        {
            if (!EsVacia())
            {
                aCima = aSubPila.aCima;
                aSubPila = aSubPila.aSubPila;
            }
        }

        // Método que devuelve el elemento en la cima de la pila sin desapilarlo
        public object Cima()
        {
            return EsVacia() ? null : aCima;
        }

        // Método que verifica si la pila está vacía
        public bool EsVacia()
        {
            return aCima == null;
        }

        // Método para listar todos los elementos de la pila
        public void Listar()
        {
            if (EsVacia())
                return;
            else
            {
                Console.WriteLine(Cima());
                if (aSubPila != null)  // Evita una llamada recursiva nula
                {
                    aSubPila.Listar();
                }
            }
        }
    }
}
