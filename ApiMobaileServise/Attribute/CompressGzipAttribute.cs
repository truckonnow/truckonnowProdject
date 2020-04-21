using iTextSharp.tool.xml.parser.io;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace ApiMobaileServise.Attribute
{
    public class CompressGzipAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
            //PropertyInfo propertyInfo = context.Result.GetType().GetProperty("Value");
            //string acceptEncoding = context.HttpContext.Response.Headers["Accept-Encoding"];
            //if (acceptEncoding == null)
            //{
            //    return;
            //}

            //acceptEncoding = acceptEncoding.ToLower();

            //if (acceptEncoding.Contains("gzip"))
            //{
            //    propertyInfo.SetValue(context.Result, Compress(Encoding.UTF8.GetBytes((string)propertyInfo.GetValue(context.Result, null)), CompressionLevel.Optimal));
            //    //context.HttpContext.Response.Headers.Add("Content-Encoding", "gzip");
            //}
            //else if (acceptEncoding.Contains("deflate"))
            //{
            //    propertyInfo.SetValue(context.Result, Compress(Encoding.UTF8.GetBytes((string)propertyInfo.GetValue(context.Result, null)), CompressionLevel.Optimal));
            //    //context.HttpContext.Response.Headers.Add("Content-Encoding", "deflate");
            //}
            PropertyInfo propertyInfo = context.Result.GetType().GetProperty("Value");
            propertyInfo.SetValue(context.Result, Compress(Encoding.UTF8.GetBytes((string)propertyInfo.GetValue(context.Result, null)), CompressionLevel.Optimal));
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        private string Compress(byte[] data, CompressionLevel level)
        {
            string res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gz = new GZipStream(ms, level, leaveOpen: true))
                {
                    gz.Write(data, 0, data.Length);
                }
                res = Convert.ToBase64String(ms.ToArray());
            }
            return res;
        }
    }
}