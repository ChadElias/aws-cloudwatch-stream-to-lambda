using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ParseCloudWatch
{
    public class Function
    {
        
        /// <summary>
        /// A function that will read cloud watch streams
        /// </summary>
        /// <param name="cloudWatchStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(CloudWatchLog cloudWatchStream, ILambdaContext context)
        {
            var logEvent = JsonConvert.DeserializeObject<Log>(DecompressText(cloudWatchStream.awslogs.data));

            foreach(LogEvent logEventMessage in logEvent.logEvents)
            {
                // Do Something with Individual Logs
                // ex logEventMessage.message
            }
        }

        public string DecompressText(string compressedText)
        {
            return System.Text.Encoding.UTF8.GetString(DecompressBytes(compressedText));
        }

        public byte[] DecompressBytes(string compressedText)
        {
            byte[] bytes = Convert.FromBase64String(compressedText);
            byte[] outputBytes;

            using (MemoryStream inputStream = new MemoryStream(bytes))
            {
                using (GZipStream zip = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        zip.CopyTo(outputStream);

                        outputBytes = outputStream.ToArray();
                    }
                }
            }
            return outputBytes;
        }
    }
}
