using System;
using System.Collections.Generic;
using tp1;

namespace tp2
{
    public class ArbolBinario<T>
    {
        private T dato;
        private ArbolBinario<T> hijoIzquierdo;
        private ArbolBinario<T> hijoDerecho;

        public ArbolBinario(T dato)
        {
            this.dato = dato;
        }

        public T getDatoRaiz()
        {
            return this.dato;
        }

        public ArbolBinario<T> getHijoIzquierdo()
        {
            return this.hijoIzquierdo;
        }

        public ArbolBinario<T> getHijoDerecho()
        {
            return this.hijoDerecho;
        }

        public void agregarHijoIzquierdo(ArbolBinario<T> hijo)
        {
            this.hijoIzquierdo = hijo;
        }

        public void agregarHijoDerecho(ArbolBinario<T> hijo)
        {
            this.hijoDerecho = hijo;
        }

        public void eliminarHijoIzquierdo()
        {
            this.hijoIzquierdo = null;
        }

        public void eliminarHijoDerecho()
        {
            this.hijoDerecho = null;
        }

        public bool esHoja()
        {
            return this.hijoIzquierdo == null && this.hijoDerecho == null;
        }

        public void inorden()
        {
            if (this.hijoIzquierdo != null)
            {
                this.hijoIzquierdo.inorden();
            }

            Console.WriteLine(this.dato);

            if (this.hijoDerecho != null)
            {
                this.hijoDerecho.inorden();
            }
        }

        // Nueva sobrecarga: pública, retorna la lista
        public List<T> preorden()
        {
            var lista = new List<T>();
            preorden(this, lista);
            return lista;
        }

        // Sobrecarga privada recursiva
        private void preorden(ArbolBinario<T> arbol, List<T> lista)
        {
            if (arbol == null) return;

            lista.Add(arbol.getDatoRaiz());

            if (arbol.getHijoIzquierdo() != null)
            {
                preorden(arbol.getHijoIzquierdo(), lista);
            }
            if (arbol.getHijoDerecho() != null)
            {
                preorden(arbol.getHijoDerecho(), lista);
            }
        }

        public void postorden()
        {
            if (this.hijoIzquierdo != null)
            {
                this.hijoIzquierdo.postorden();
            }
            if (this.hijoDerecho != null)
            {
                this.hijoDerecho.postorden();
            }
            Console.WriteLine(this.dato);
        }

        public List<List<T>> recorridoPorNiveles()
        {
            var resultado = new List<List<T>>();
            Cola<(ArbolBinario<T> nodo, int nivel)> cola = new Cola<(ArbolBinario<T>, int)>();
            cola.encolar((this, 0));

            while (!cola.esVacia())
            {
                var (nodo, nivel) = cola.desencolar();

                if (resultado.Count <= nivel)
                    resultado.Add(new List<T>());

                resultado[nivel].Add(nodo.getDatoRaiz());

                if (nodo.getHijoIzquierdo() != null)
                    cola.encolar((nodo.getHijoIzquierdo(), nivel + 1));
                if (nodo.getHijoDerecho() != null)
                    cola.encolar((nodo.getHijoDerecho(), nivel + 1));
            }

            return resultado;
        }

        public int contarHojas()
        {
            if (this.esHoja())
            {
                return 1;
            }

            int hojas = 0;
            if (this.hijoIzquierdo != null)
            {
                hojas += this.hijoIzquierdo.contarHojas();
            }
            if (this.hijoDerecho != null)
            {
                hojas += this.hijoDerecho.contarHojas();
            }
            return hojas;
        }

        public void recorridoEntreNiveles(int n, int m)
        {
            if (n < 0 || m < n)
            {
                throw new ArgumentException("Los niveles n y m deben cumplir 0 ≤ n ≤ m.");
            }

            int initialLevel = 0;
            Cola<(ArbolBinario<T> nodo, int level)> cola = new Cola<(ArbolBinario<T> nodo, int level)>();

            cola.encolar((this, initialLevel));

            while (!cola.esVacia())
            {
                var (nodo, level) = cola.desencolar();

                if (level > m)
                {
                    break;
                }

                if (level >= n && level <= m)
                {
                    Console.WriteLine(nodo.getDatoRaiz());
                }

                if (nodo.getHijoIzquierdo() != null)
                {
                    cola.encolar((nodo.getHijoIzquierdo(), level + 1));
                }

                if (nodo.getHijoDerecho() != null)
                {
                    cola.encolar((nodo.getHijoDerecho(), level + 1));
                }
            }
        }
    }
}