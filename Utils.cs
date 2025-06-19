using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiW
{
    internal class Utils
    {
        public static string patron = GetDefaultDatasetPath();
        public static string patron_imgs = "\\imgs\\{0}.png";
        public static string file_preguntas = "\\preguntas.csv";

        public static int w = 100;
        public static int h = 145;  

        public static bool DatasetEncontrado { get; private set; } = false;
        private static bool patronInicializado = false;

    
        private static string GetDefaultDatasetPath()
        {
            string current = Directory.GetCurrentDirectory();
            string? root = Directory.GetParent(current)?.Parent?.Parent?.FullName;
            if (root == null) root = current;

            Console.WriteLine($"[DEBUG] Buscando carpeta 'datasets' recursivamente desde: {root}");

            string? datasetsPath = FindDirectoryRecursive(root, "datasets");
            if (datasetsPath != null)
            {
                string preguntasPath = Path.Combine(datasetsPath, "preguntas.csv");
                string imgsPath = Path.Combine(datasetsPath, "imgs");
                if (File.Exists(preguntasPath) && Directory.Exists(imgsPath))
                {
                    DatasetEncontrado = true;
                    Console.WriteLine($"[DEBUG] Carpeta 'datasets' encontrada en: {datasetsPath}");
                    Console.WriteLine($"[DEBUG] Contiene preguntas.csv e imgs");
                    return datasetsPath;
                }
                else
                {
                    Console.WriteLine($"[DEBUG] Carpeta 'datasets' encontrada en: {datasetsPath}, pero faltan preguntas.csv o imgs");
                }
            }

            DatasetEncontrado = false;
            Console.WriteLine("[DEBUG] Carpeta 'datasets' NO encontrada con preguntas.csv e imgs en el proyecto.");
            return current; // fallback
        }

        private static string? FindDirectoryRecursive(string root, string folderName)
        {
            try
            {
                var dirs = Directory.GetDirectories(root, folderName, SearchOption.AllDirectories);
                Console.WriteLine($"[DEBUG] Carpetas encontradas con nombre '{folderName}': {dirs.Length}");
                foreach (var dir in dirs)
                {
                    Console.WriteLine($"[DEBUG] Coincidencia: {dir}");
                    return dir;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Error buscando en {root}: {ex.Message}");
            }
            return null;
        }

        public static void init_patron(bool forzar = false)
        {
            if (patronInicializado && !forzar)
            {
                Console.WriteLine("[DEBUG] init_patron() ignorado porque ya fue inicializado.");
                return;
            }
            Console.WriteLine("[DEBUG] Llamada a init_patron()");
            patron = GetDefaultDatasetPath();
            patronInicializado = true;
        }

        public static string get_path_img(string nombre)
        {
            return Path.Combine(patron, "imgs", $"{nombre}.png");
        }

        public static string get_path_preguntas()
        {
            return Path.Combine(patron, "preguntas.csv");
        }

        public static void set_patron(string patron_parm)
        {
            patron = patron_parm;
        }
    }
}