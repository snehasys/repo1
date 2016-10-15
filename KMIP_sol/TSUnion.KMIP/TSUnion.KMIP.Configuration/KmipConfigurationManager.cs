using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;

using System.Xml;
using System.Xml.XPath;    


namespace TSUnion.KMIP.Configuration
{
    public class KmipConfigurationManager
    {

        public static Object ConfigureObject(Object obj) 
        {
            string path = @"\vitalApp.config";
            XmlDocument configDoc = new XmlDocument();
            System.IO.File.AppendAllText("../myfile.sne","this is a sample message");

            configDoc.Load(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + path );
            //string value = configDoc.DocumentElement.SelectSingleNode("/configuration/appSettings/add[@key='TSUnion.KMIP.Communication.ConnectionDetails.Port']").Attributes["value"].Value;

            
            //System.Configuration.Configuration myAppConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(@"d:\myApp.config");
            //ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = path };
            //System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            var type = obj.GetType();
            List<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance|BindingFlags.NonPublic | BindingFlags.Public).ToList();
            String settingValue = null;
            foreach (var property in properties)
            {
                if(Attribute.IsDefined(property, typeof(AppSettingAttribute)))
                {
                    String appSettingName=type.FullName + "." + property.Name;
                    //if (ConfigurationManager.AppSettings[appSettingName] != null) { };
                    settingValue = null;
                    try {
                        settingValue = configDoc.DocumentElement.SelectSingleNode("/configuration/appSettings/add[@key='" + appSettingName + "']").Attributes["value"].Value;
                    }
                    catch (Exception ) { 
                        // some property value not found in config file..
                    }
                    if ((settingValue) != null)
                    {
                        var propertyValue = settingValue;// System.Configuration.ConfigurationManager.AppSettings[appSettingName];
                        var propertyType = property.PropertyType;
                        if (property.PropertyType.IsEnum) 
                        {
                            property.SetValue(obj, Enum.Parse(propertyType, propertyValue),null);
                        }
                        else
                        {
                            property.SetValue(obj, Convert.ChangeType(propertyValue, propertyType), null);
                        }
                    }
                }
            }

            return obj;
        
        }
    }
}
