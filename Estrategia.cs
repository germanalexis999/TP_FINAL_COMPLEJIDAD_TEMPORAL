
using System;
using System.Collections.Generic;
using tp1;
using tp2;

namespace tpfinal
{

	class Estrategia
	{

        public String Consulta1(ArbolBinario<DecisionData> arbol)
        {
            HashSet<string> predicciones = new HashSet<string>();

            void Preorden(ArbolBinario<DecisionData> nodo)
            {
                if (nodo == null) return;

                if (nodo.esHoja())
                {
                    var hojaPred = nodo.getDatoRaiz().Predictions;
                    if (hojaPred != null)
                    {
                        foreach (var clave in hojaPred.Keys)
                        {
                            predicciones.Add(clave);
                        }
                    }
                }

                if (nodo.getHijoIzquierdo() != null)
                    Preorden(nodo.getHijoIzquierdo());
                if (nodo.getHijoDerecho() != null)
                    Preorden(nodo.getHijoDerecho());
            }

            Preorden(arbol);

            return "Posibles predicciones: " + string.Join(", ", predicciones);
        }


        public String Consulta2(ArbolBinario<DecisionData> arbol)
        {
            List<string> caminos = new List<string>();

            void Preorden(ArbolBinario<DecisionData> nodo, List<string> caminoActual)
            {
                if (nodo == null) return;

                var dato = nodo.getDatoRaiz();

                if (!nodo.esHoja())
                {
                    if (dato.Question != null)
                        caminoActual.Add(dato.Question.TextoParaUsuario());
                }

                if (nodo.esHoja())
                {
                    if (dato.Predictions != null)
                    {
                        foreach (var pred in dato.Predictions.Keys)
                        {
                            string camino = string.Join(" -> ", caminoActual);
                            caminos.Add($"{camino} => Predicción: {pred}");
                        }
                    }
                }
                else
                {
                    if (nodo.getHijoIzquierdo() != null)
                        Preorden(nodo.getHijoIzquierdo(), new List<string>(caminoActual));
                    if (nodo.getHijoDerecho() != null)
                        Preorden(nodo.getHijoDerecho(), new List<string>(caminoActual));
                }
            }

            Preorden(arbol, new List<string>());

            return string.Join(Environment.NewLine, caminos);
        }


        public String Consulta3(ArbolBinario<DecisionData> arbol)
        {
            if (arbol == null) return "Árbol vacío.";

            var niveles = arbol.recorridoPorNiveles();
            List<string> resultado = new List<string>();

            for (int i = 0; i < niveles.Count; i++)
            {
                // Usamos ToString() para mostrar el dato de cada nodo
                var datosNivel = niveles[i].ConvertAll(d => d.ToString());
                resultado.Add($"Nivel {i}: {string.Join(" | ", datosNivel)}");
            }

            return string.Join(Environment.NewLine, resultado);
        }

        public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
        {
           
            if (clasificador.crearHoja())
            {
                var predicciones = clasificador.obtenerDatoHoja();
                //TODO Delete Depuración:
                Console.WriteLine("Creando hoja con predicciones: " + string.Join(", ", predicciones.Keys));
                var hoja = new DecisionData(predicciones);
                return new ArbolBinario<DecisionData>(hoja);
            }
            else
            {
                var pregunta = clasificador.obtenerPregunta();
                var nodo = new DecisionData(pregunta);

                var clasificadorIzq = clasificador.obtenerClasificadorIzquierdo();
                var clasificadorDer = clasificador.obtenerClasificadorDerecho();

                var arbol = new ArbolBinario<DecisionData>(nodo);

                arbol.agregarHijoIzquierdo(CrearArbol(clasificadorIzq));
                arbol.agregarHijoDerecho(CrearArbol(clasificadorDer));

                return arbol;
            }
        }
    }
}