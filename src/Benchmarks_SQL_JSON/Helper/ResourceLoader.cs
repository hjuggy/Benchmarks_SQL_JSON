using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Benchmarks_SQL_JSON.Helper
{
    public static class ResourceLoader
    {
        private const string DefaultPath = "Resources";

        public static string Read<T>([CallerMemberName] string fileName = null)
        {
            return ReadByPath<T>(DefaultPath, fileName);
        }

        public static string ReadByPath<T>(
            string path,
            [CallerMemberName] string fileName = null)
        {
            Assembly assembly = typeof(T).Assembly;
            string assemblyName = assembly.GetName().Name;
            using (Stream stream = assembly.GetManifestResourceStream($"{assemblyName}.{path}.{fileName}.sql"))
            {
                if (stream == null)
                {
                    throw new ArgumentException("Resource not found", fileName);
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static Lazy<string> ReadLazy<T>([CallerMemberName] string fileName = null)
        {
            return new Lazy<string>(() => Read<T>(fileName), true);
        }

        public static Lazy<string> ReadLazyByPath<T>(
            string path,
            [CallerMemberName] string fileName = null)
        {
            return new Lazy<string>(() => ReadByPath<T>(path, fileName), true);
        }
    }
}
