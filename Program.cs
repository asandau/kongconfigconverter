using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace kongconfigconverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2) {
                Console.WriteLine("You need to provide the input and output filename!");
                return;
            }

            var input = new StreamReader(args[0]);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            var serializer = new SerializerBuilder().Build();
            var converter = new ConfigConverter();

            var kongfigFormat = deserializer.Deserialize<KongfigFormat>(input);
            var kongInitFormatYaml = serializer.Serialize(converter.convert(kongfigFormat));

            using (StreamWriter outputFile = new StreamWriter(args[1]))
            {
                outputFile.Write(kongInitFormatYaml);
                outputFile.Flush();
                outputFile.Close();
            }
        }

    }
}
