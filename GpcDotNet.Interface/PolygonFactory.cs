using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Gpc
{
    public abstract class PolygonFactory
    {
        private static readonly PolygonFactory _FACTORY;

        static PolygonFactory()
        {
            List<Exception> exceptions = new List<Exception>();
            String absoluteAssemblyPath = Assembly.GetExecutingAssembly().Location;

            if (_FACTORY == null)
            {
                try
                {
                    Assembly archAssembly = Assembly.LoadFile(Path.Combine(Path.GetDirectoryName(absoluteAssemblyPath), "GpcDotNet.IA64.dll"));

                    Type t = archAssembly.GetType("Gpc.PolygonFactoryImpl");
                    if (t != null && typeof(PolygonFactory).IsAssignableFrom(t))
                    {
                        _FACTORY = (PolygonFactory)Activator.CreateInstance(t);
                    }
                }
                catch (Exception e) { exceptions.Add(e); }
            }

            if (_FACTORY == null)
            {
                try
                {
                    Assembly archAssembly = Assembly.LoadFile(Path.Combine(Path.GetDirectoryName(absoluteAssemblyPath), "GpcDotNet.AMD64.dll"));

                    Type t = archAssembly.GetType("Gpc.PolygonFactoryImpl");
                    if (t != null && typeof(PolygonFactory).IsAssignableFrom(t))
                    {
                        _FACTORY = (PolygonFactory)Activator.CreateInstance(t);
                    }
                }
                catch (Exception e) { exceptions.Add(e); }
            }

            if (_FACTORY == null)
            {
                try
                {
                    Assembly archAssembly = Assembly.LoadFile(Path.Combine(Path.GetDirectoryName(absoluteAssemblyPath), "GpcDotNet.IA32.dll"));

                    Type t = archAssembly.GetType("Gpc.PolygonFactoryImpl");
                    if (t != null && typeof(PolygonFactory).IsAssignableFrom(t))
                    {
                        _FACTORY = (PolygonFactory)Activator.CreateInstance(t);
                    }
                }
                catch (Exception e) { exceptions.Add(e); }
            }

            if (_FACTORY == null)
            {
                String msg = "Unable to load a native library for GPC.";
                foreach (Exception e in exceptions)
                {
                    msg += " {" + e.GetType().Name + ": " + e.Message + "}";
                }
                throw new Exception(msg);
            }
        }

        public static IPolygon Create()
        {
            return _FACTORY._Create();
        }

        public static IPolygon Read(TextReader reader, Boolean readHoleFlags)
        {
            return _FACTORY._Read(reader, readHoleFlags);
        }

        protected PolygonFactory() {}

        protected abstract IPolygon _Create();
        protected abstract IPolygon _Read(TextReader reader, Boolean readHoleFlags);
    }
}
