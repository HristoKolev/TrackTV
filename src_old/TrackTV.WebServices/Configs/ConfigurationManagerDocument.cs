namespace TrackTV.WebServices
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;

    using NetInfrastructure.Configuration;

    public class ConfigurationManagerDocument : IConfigurationDocument
    {
        public string DefaultSection { get; set; }

        public string this[string section, string key]
        {
            get
            {
                return this[key];
            }

            set
            {
                this[key] = value;
            }
        }

        public string this[string key]
        {
            get
            {
                return ConfigurationManager.AppSettings[key];
            }

            set
            {
                ConfigurationManager.AppSettings[key] = value;
            }
        }

        public IEnumerator<KeyValuePair<string, KeyValuePair<string, string>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool KeyExists(string section, string key)
        {
            throw new NotImplementedException();
        }

        public bool KeyExists(string key)
        {
            throw new NotImplementedException();
        }

        public bool SectionExists(string section)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}