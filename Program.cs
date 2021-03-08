using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1.内存注入字典类型数据源
            //IConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.AddInMemoryCollection(new Dictionary<string, string>()  // 注入内存的数据源
            //{
            //    { "key1", "value1"},
            //    { "key2", "value2"},
            //    { "section1:key3","value3"},
            //    { "section1:key4","value4"},
            //    { "section1:section2:key5","value5"},
            //    { "section1:section2:key6","value6"}
            //});

            //// 把所有配置构建出来
            //// IConfigurationRoot：获取配置的接口
            //IConfigurationRoot config = builder.Build();

            //Console.WriteLine(config["key1"]);
            //Console.WriteLine(config["key2"]);
            //Console.WriteLine(config["key3"]);

            //var section1 = config.GetSection("section1");
            //Console.WriteLine(section1["key3"]);
            //Console.WriteLine(section1["key4"]);
            //Console.WriteLine(section1["key5"]);

            //var section2 = section1.GetSection("section2");
            //Console.WriteLine(section2["key5"]);
            //Console.WriteLine(section2["key6"]);

            #endregion

            #region 2.命令行数据源
            // 首先需要在项目属性-调式中添加应用程序参数或者在properties-launchSetting.json-CommandLineArgs
            //var builder = new ConfigurationBuilder();
            //builder.AddCommandLine(args);

            #region 命令替换
            //// 将CMDLineKey1替换成k1
            //var mapper = new Dictionary<string, string>() {
            //    { "-k1", "CMDLineKey1"}
            //};
            //builder.AddCommandLine(args, mapper);
            #endregion

            //// 获取ConfigurationRoot
            //var config = builder.Build();
            //// 输出
            //Console.WriteLine($"CMDLineKey1:{config["CMDLineKey1"]}");
            //Console.WriteLine($"CMDLineKey2:{config["CMDLineKey2"]}");
            //Console.WriteLine($"CMDLineKey3:{config["CMDLineKey3"]}");
            //Console.WriteLine($"k1:{config["k1"]}");

            #endregion

            #region 3.环境变量配置
            //// 首先需要在项目属性-调式中添加环境变量或者在properties-launchSetting.json-environmentVariables
            //var builder = new ConfigurationBuilder();
            ////builder.AddEnvironmentVariables();  // 添加环境变量
            ////var config = builder.Build();

            ////Console.WriteLine(config["key1"]);

            ////// 分层键（section__key3）可多级
            ////var section = config.GetSection("section1");
            ////Console.WriteLine(section["key3"]);

            //// 前缀过滤 只注入指定前缀的变量
            //builder.AddEnvironmentVariables("TT");
            //var config = builder.Build();

            //Console.WriteLine(config["key1"]);
            //Console.WriteLine(config["key4"]);
            #endregion

            #region 4.文件配置提供程序
            //const string CONFIG_FILE = "appsettings.json";
            //var builder = new ConfigurationBuilder();
            //// 添加配置文件 appsettings.json appsettings.ini
            //// 将配置文件添加进来 应用包：microsoft.extensions.configuration.json
            //// optional:当找不到配置文件时，程序是否继续执行下去 true：是 false：抛异常
            //// reloadOnChange:当配置文件变更时，是否重新读取 
            //builder.AddJsonFile(CONFIG_FILE, optional: true, reloadOnChange: true);
            //builder.AddIniFile("appsettings.ini", optional: true, reloadOnChange: true);

            //var configurationRoot = builder.Build();

            //Console.WriteLine($"json key1:{configurationRoot["Key1"]}");
            //Console.WriteLine($"json key2:{configurationRoot["Key2"]}");
            //Console.WriteLine($"json key3:{configurationRoot["Key3"]}");
            //Console.WriteLine($"json key4:{configurationRoot.GetSection("Section1")["Key4"]}");
            //Console.WriteLine($"json key8:{configurationRoot["Key8"]}");
            //Console.WriteLine($"json key9:{configurationRoot["Key9"]}");
            #endregion

            #region 6.监视文件配置变更：配置热更新能力的核心
            //var builder = new ConfigurationBuilder();

            //builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //var configurationRoot = builder.Build();

            //var token = configurationRoot.GetReloadToken();

            ////// 监听一次变更后不再执行
            ////token.RegisterChangeCallback(state =>
            ////{
            ////    Console.WriteLine($"json key1:{configurationRoot["Key1"]}");
            ////    Console.WriteLine($"json key2:{configurationRoot["Key2"]}");
            ////    Console.WriteLine($"json key3:{configurationRoot["Key3"]}");
            ////    Console.WriteLine($"json key4:{configurationRoot.GetSection("Section1")["Key4"]}");
            ////    Console.WriteLine($"json key8:{configurationRoot["Key8"]}");
            ////    Console.WriteLine($"json key9:{configurationRoot["Key9"]}");
            ////}, configurationRoot);

            //ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
            //{
            //    Console.WriteLine($"json key1:{configurationRoot["Key1"]}");
            //    Console.WriteLine($"json key2:{configurationRoot["Key2"]}");
            //    Console.WriteLine($"json key3:{configurationRoot["Key3"]}");
            //    Console.WriteLine($"json key4:{configurationRoot.GetSection("Section1")["Key4"]}");
            //    Console.WriteLine($"json key8:{configurationRoot["Key8"]}");
            //    Console.WriteLine($"json key9:{configurationRoot["Key9"]}");
            //});

            #endregion

            #region 7.配置绑定：使用强类型对象承载配置数据
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json");

            var configurationRoot = builder.Build();

            config = new Config();
            configurationRoot.Bind(config);
            configurationRoot.GetSection("Section1").Bind(config);

            Console.WriteLine($"json key1:{config.Key1}");
            Console.WriteLine($"json key2:{config.Key2}");
            Console.WriteLine($"json key3:{config.Key3}");
            Console.WriteLine($"json key4:{config.Key4}");
            Console.WriteLine($"json key5:{config.Key5}");

            #endregion
            Console.WriteLine("end!");
            Console.ReadLine();
        }
        static Config config { get; set; }
    }

    class Config
    {
        public string Key1 { get; set; }
        public bool Key2 { get; set; }
        public int Key3 { get; set; }
        public double Key4 { get; set; }
        public string Key5 { get; set; }
    }
}
