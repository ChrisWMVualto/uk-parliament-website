﻿namespace UKP.Website.Service.Model
{
    public class SearchHighlightFieldModel
    {
        public SearchHighlightFieldModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
    }
}