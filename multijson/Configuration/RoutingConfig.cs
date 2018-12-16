using System.Configuration;
namespace multijson.Configuration
{
    //This class reads the defined config section (if available) and stores it locally in the static _Config variable.
    //This config data is available by calling RoutingGroups.GetRoutingGroups().
    public class RoutingGroups
    {
        public static RoutingGroupConfigsSection _Config = ConfigurationManager.GetSection("RoutingGroupConfigs") as RoutingGroupConfigsSection;
        public static RoutingGroupElementCollection GetRoutingGroups()
        {
            return _Config.RoutingGroups;
        }
    }
    //Extend the ConfigurationSection class.  Your class name should match your section name and be postfixed with "Section".
    public class RoutingGroupConfigsSection : ConfigurationSection
    {
        //Decorate the property with the tag for your collection.
        [ConfigurationProperty("RoutingGroups")]
        public RoutingGroupElementCollection RoutingGroups
        {
            get { return (RoutingGroupElementCollection)this["RoutingGroups"]; }
        }
    }
    //Extend the ConfigurationElementCollection class.
    //Decorate the class with the class that represents a single element in the collection.
    [ConfigurationCollection(typeof(RoutingGroupElement))]
    public class RoutingGroupElementCollection : ConfigurationElementCollection
    {
        public RoutingGroupElement this[int index]
        {
            get { return (RoutingGroupElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new RoutingGroupElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RoutingGroupElement)element).Name;
        }
    }
    //Extend the ConfigurationElement class.  This class represents a single element in the collection.
    //Create a property for each xml attribute in your element.
    //Decorate each property with the ConfigurationProperty decorator.  See MSDN for all available options.
    public class RoutingGroupElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("connectionStringName", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string)base["connectionStringName"]; }
            set { base["connectionStringName"] = value; }
        }

        [ConfigurationProperty("isActive", DefaultValue = "true", IsKey = false, IsRequired = false)]
        public bool IsActive
        {
            get { return (bool)base["isActive"]; }
            set { base["isActive"] = value; }
        }

        [ConfigurationProperty("isDebug", DefaultValue = "false", IsKey = false, IsRequired = false)]
        public bool IsDebug
        {
            get { return (bool)base["isDebug"]; }
            set { base["isDebug"] = value; }
        }

        [ConfigurationProperty("doUseTableValuedParameters", DefaultValue = "false", IsKey = false, IsRequired = false)]
        public bool DoUseTableValuedParameters
        {
            get { return (bool)base["doUseTableValuedParameters"]; }
            set { base["doUseTableValuedParameters"] = value; }
        }
        [ConfigurationProperty("routeTemplate", DefaultValue = "{*request}", IsKey = false, IsRequired = false)]
        public string RouteTemplate
        {
            get { return (string)base["routeTemplate"]; }
            set { base["routeTemplate"] = value; }
        }

        [ConfigurationProperty("storedProcedureName", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string StoredProcedureName
        {
            get { return (string)base["storedProcedureName"]; }
            set { base["storedProcedureName"] = value; }
        }
    }
}