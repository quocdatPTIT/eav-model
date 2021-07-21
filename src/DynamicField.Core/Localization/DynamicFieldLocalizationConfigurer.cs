using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace DynamicField.Localization
{
    public static class DynamicFieldLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(DynamicFieldConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(DynamicFieldLocalizationConfigurer).GetAssembly(),
                        "DynamicField.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
