using Ninject.Modules;
using Words.Api;
using Words.Common;
using Words.Input.Text;
using Words.Output.Csv;

namespace Words1
{
    /// <summary>
    /// Ninject configuration module
    /// </summary>
    class MyConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWordsInput>().To<TextInput>();
            Bind<IWordsOutput>().To<CsvOutput>();
            Bind<IOccurencies>().To<Occurencies>();
        }
    }
}
