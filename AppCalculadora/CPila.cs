namespace AppCalculadora
{
    // Clase que implementa una pila con una estructura recursiva
    internal class CPila
    {
        private object aCima;       // Elemento en la cima de la pila
        private CPila aSubPila;     // Subpila que contiene los demás elementos

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
    }
}
